using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NPushOver
{
    //Based on documentation from https://pushover.net/api
    //TODO: Extend with a rate-limiter

    public class PushOver
    {
        public static readonly Uri DEFAULTURI = new Uri("https://api.pushover.net/1/");
        public static readonly Encoding DEFAULTENCODING = Encoding.UTF8;

        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static Regex appkeyregex = new Regex("[A-Za-z0-9]{30}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private static Regex usergroupregex = new Regex("[A-Za-z0-9]{30}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private static Regex deviceregex = new Regex("[A-Za-z0-9_-]{1,25}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public Encoding Encoding { get; private set; }
        public Uri BaseUri { get; private set; }
        public string ApplicationToken { get; private set; }
        public IRateLimiter RateLimiter { get; private set; }
        public IWebProxy Proxy { get; set; }

        //public ICredentials Credentials { get; set; }
        //public RequestCachePolicy RequestCachePolicy { get; set; }

        public PushOver(string applicationToken)
            : this(applicationToken, DEFAULTURI) { }

        public PushOver(string applicationToken, Uri baseUri)
            : this(applicationToken, baseUri, new NullRateLimiter()) { }

        public PushOver(string applicationToken, Uri baseUri, IRateLimiter rateLimiter)
            : this(applicationToken, baseUri, rateLimiter, DEFAULTENCODING) { }

        public PushOver(string applicationToken, Uri baseUri, IRateLimiter rateLimiter, Encoding encoding)
        {
            if (applicationToken == null)
                throw new ArgumentNullException("applicationToken");
            if (baseUri == null)
                throw new ArgumentNullException("baseUri");
            if (rateLimiter == null)
                throw new ArgumentNullException("rateLimiter");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (!appkeyregex.IsMatch(applicationToken))
                throw new InvalidKeyException("applicationToken", applicationToken);

            this.ApplicationToken = applicationToken;
            this.BaseUri = baseUri;
            this.RateLimiter = rateLimiter;
            this.Encoding = encoding;
        }

        public async Task<PushoverResponse> SendMessageAsync(Message message, string userOrGroup)
        {
            return await this.SendMessageAsync(message, userOrGroup, (string[])null);
        }

        public async Task<PushoverResponse> SendMessageAsync(Message message, string userOrGroup, string device)
        {
            return await this.SendMessageAsync(message, userOrGroup, new[] { device });
        }

        public async Task<PushoverResponse> SendMessageAsync(Message message, string userOrGroup, string[] devices)
        {
            ValidateMessage(message);

            if (userOrGroup == null)
                throw new ArgumentNullException("user");
            if (!usergroupregex.IsMatch(userOrGroup))
                throw new InvalidKeyException("user", userOrGroup);
            if (devices != null && devices.Length > 0)
            {
                foreach (var device in devices)
                {
                    if (!deviceregex.IsMatch(device))
                        throw new InvalidKeyException("device", device);
                }
            }

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverResponse>(async () =>
                {
                    var parameters = new PushOverParams { 
                        { "token", this.ApplicationToken }, 
                        { "user", userOrGroup },
                        { "priority", (int)message.Priority },
                        { "message", message.Body }
                    };

                    parameters.AddConditional("device", devices);
                    parameters.AddConditional("title", message.Title);
                    parameters.AddConditional("sound", message.Sound);
                    parameters.AddConditional("html", message.IsHtmlBody);
                    if (message.SupplementaryUrl != null)
                    {
                        parameters.Add("url", message.SupplementaryUrl.Uri);
                        parameters.AddConditional("url_title", message.SupplementaryUrl.Title);
                    }
                    if (message.Priority == Priority.Emergency)
                    {
                        parameters.Add("retry", message.RetryOptions.RetryEvery);
                        parameters.Add("expire", message.RetryOptions.RetryPeriod);
                        parameters.Add("callback", message.RetryOptions.CallBackUrl);
                    }
                    if (message.Timestamp != null)
                        parameters.Add("timestamp", ((int)(message.Timestamp.Value.ToUniversalTime() - EPOCH).TotalSeconds).ToString());

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetUriFromBase("messages.json"), parameters));
                    return await ParseResponse<PushoverResponse>(json);
                });
            }
        }

        private void ValidateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (message.Body == null)
                throw new ArgumentNullException("body");
            if (message.Body.Length > 1024)                     //TODO: Move to some "consts"
                throw new ArgumentOutOfRangeException("body");  //TODO: Maybe add explanation?

            if ((message.Title ?? string.Empty).Length > 250)   //TODO: Move to some "consts"?
                throw new ArgumentOutOfRangeException("title"); //TODO: Maybe add explanation?

            if (!Enum.IsDefined(typeof(Priority), message.Priority))
                throw new ArgumentOutOfRangeException("priority");

            if (message.Priority == Priority.Emergency)
            {
                if (message.RetryOptions == null)
                    throw new ArgumentNullException("retryOptions");

                if (message.RetryOptions.RetryEvery < TimeSpan.FromSeconds(30))
                    throw new ArgumentOutOfRangeException("retryOptions.retryEvery");
                if (message.RetryOptions.RetryPeriod > TimeSpan.FromHours(24))
                    throw new ArgumentOutOfRangeException("retryOptions.retryPeriod");
            }
            else
            {
                if (message.RetryOptions != null)
                    throw new ArgumentException("retryOptions");
            }

            if (message.SupplementaryUrl != null)
            {
                if (message.SupplementaryUrl.Uri == null)
                    throw new ArgumentNullException("supplementaryUrl.uri");

                if ((message.SupplementaryUrl.Title ?? string.Empty).Length > 100)      //TODO: Move to some "consts"?
                    throw new ArgumentOutOfRangeException("supplementaryUrl.title");    //TODO: Maybe add explanation?
            }
        }

        public async Task<SoundsResponse> ListSoundsAsync()
        {
            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<SoundsResponse>(async () =>
                {
                    var json = await wc.DownloadStringTaskAsync(GetUriFromBase("sounds.json?token={0}", this.ApplicationToken));
                    return await ParseResponse<SoundsResponse>(json);
                });
            }
        }

        private Uri GetUriFromBase(string relative, params object[] args)
        {
            return new Uri(this.BaseUri, string.Format(relative, args));
        }


        private async Task<T> ExecuteWebRequest<T>(Func<Task<T>> func)
            where T : PushoverResponse
        {
            try
            {
                //TODO: Use this.RateLimiter here somewhere?
                return await func.Invoke();
            }
            catch (WebException wex)
            {
                var response = (HttpWebResponse)wex.Response;
                switch ((int)response.StatusCode)
                {
                    case 400:   //Bad request
                        var r = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                        throw;
                    case 429:
                        //Rate limited: https://pushover.net/api#limits
                        throw new NotImplementedException();    //TODO...
                    default:
                        throw;
                }
            }
            catch (PushOverException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PushOverException("Error retrieving response", ex);
            }
        }

        private static async Task<T> ParseResponse<T>(string json)
            where T : PushoverResponse
        {
            T result;
            try
            {
                result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
            }
            catch (Exception ex)
            {
                throw new PushOverException("Error parsing response", ex);
            }

            if (!result.IsOk)
                throw new PushOverResponseException("API returned one or more errors", result);
            return result;
        }

        private WebClient GetWebClient()
        {
            var wc = new WebClient();
            wc.Proxy = this.Proxy;
            wc.Headers.Add(HttpRequestHeader.UserAgent, string.Format("NPushover {0}", typeof(PushOver).Assembly.GetName().Version));
            //wc.Credentials = Credentials;
            //wc.CachePolicy = 
            wc.Encoding = this.Encoding;
            return wc;
        }

    }
}
