using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NPushOver
{
    //TODO: Extend with a rate-limiter

    public class PushOver
    {
        public static readonly Uri DEFAULTURI = new Uri("https://api.pushover.net/1/");
        public static readonly Encoding DEFAULTENCODING = Encoding.UTF8;

        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private Encoding Encoding { get; set; }
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
        {
            this.ApplicationToken = applicationToken;
            this.BaseUri = baseUri;
            this.RateLimiter = rateLimiter;
            this.Encoding = DEFAULTENCODING;
        }

        public async Task<PushoverResponse> SendMessageAsync(Message message, string user)
        {
            return await this.SendMessageAsync(message, user, null);
        }
        public async Task<PushoverResponse> SendMessageAsync(Message message, string user, string device)
        {
            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverResponse>(async () =>
                {
                    var parameters = new PushOverParams { 
                        { "token", this.ApplicationToken }, 
                        { "user", user },
                        { "priority", (int)message.Priority },
                        { "message", message.Body }
                    };

                    parameters.AddConditional("device", device);
                    parameters.AddConditional("title", message.Title);
                    parameters.AddConditional("sound", message.Sound);
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
                throw new PushOverException("Error retrieving response", null, ex);
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
                throw new PushOverException("Error parsing response", null, ex);
            }

            if (!result.IsOk)
                throw new PushOverException("API returned one or more errors", result);
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
