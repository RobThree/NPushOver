using Newtonsoft.Json;
using NPushover.Exceptions;
using NPushover.Ratelimiters;
using NPushover.RequestObjects;
using NPushover.ResponseObjects;
using NPushover.Validators;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NPushover
{
    //Based on documentation from https://pushover.net/api
    //TODO: Extend with a rate-limiter

    //TODO: Implement OpenClient API realtime notifications? https://pushover.net/api/client

    public class Pushover
    {
        private static readonly string HOMEURL = "https://github.com/RobThree/NPushOver";
        private static readonly AssemblyName ASSEMBLYNAME = typeof(Pushover).Assembly.GetName();

        public static readonly Uri DEFAULTBASEURI = new Uri("https://api.pushover.net/");
        public static readonly Encoding DEFAULTENCODING = Encoding.UTF8;

        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public Encoding Encoding { get; private set; }
        public Uri BaseUri { get; private set; }
        public string ApplicationToken { get; private set; }
        public IRateLimiter RateLimiter { get; private set; }

        public IWebProxy Proxy { get; set; }
        public IValidator<Message> MessageValidator { get; set; }
        public IValidator<string> AppKeyValidator { get; set; }
        public IValidator<string> UserOrGroupKeyValidator { get; set; }
        public IValidator<string> DeviceNameValidator { get; set; }
        public IValidator<string> ReceiptValidator { get; set; }
        public IValidator<string> EmailValidator { get; set; }

        public Pushover(string applicationToken)
            : this(applicationToken, DEFAULTBASEURI) { }

        public Pushover(string applicationToken, Uri baseUri)
            : this(applicationToken, baseUri, new NullRateLimiter()) { }

        public Pushover(string applicationToken, Uri baseUri, IRateLimiter rateLimiter)
            : this(applicationToken, baseUri, rateLimiter, DEFAULTENCODING) { }

        public Pushover(string applicationToken, Uri baseUri, IRateLimiter rateLimiter, Encoding encoding)
        {
            (this.AppKeyValidator ?? new ApplicationKeyValidator()).Validate("applicationToken", applicationToken);
            if (baseUri == null)
                throw new ArgumentNullException("baseUri");
            if (rateLimiter == null)
                throw new ArgumentNullException("rateLimiter");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            this.ApplicationToken = applicationToken;
            this.BaseUri = baseUri;
            this.RateLimiter = rateLimiter;
            this.Encoding = encoding;
        }

        public async Task<PushoverUserResponse> SendMessageAsync(Message message, string userOrGroup)
        {
            return await this.SendMessageAsync(message, userOrGroup, (string[])null);
        }

        public async Task<PushoverUserResponse> SendMessageAsync(Message message, string userOrGroup, string device)
        {
            return await this.SendMessageAsync(message, userOrGroup, new[] { device });
        }

        public async Task<PushoverUserResponse> SendMessageAsync(Message message, string userOrGroup, string[] devices)
        {
            (this.MessageValidator ?? new DefaultMessageValidator()).Validate("message", message);
            (this.UserOrGroupKeyValidator ?? new UserOrGroupKeyValidator()).Validate("userOrGroup", userOrGroup);
            if (devices != null && devices.Length > 0)
            {
                foreach (var device in devices)
                {
                    (this.DeviceNameValidator ?? new DeviceNameValidator()).Validate("device", device);
                }
            }

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverUserResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
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
                        parameters.Add("timestamp", (int)(TimeZoneInfo.ConvertTimeToUtc(message.Timestamp.Value).Subtract(EPOCH).TotalSeconds));

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("messages.json"), parameters));
                    return await ParseResponse<PushoverUserResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<SoundsResponse> ListSoundsAsync()
        {
            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<SoundsResponse>(async () =>
                {
                    var json = await wc.DownloadStringTaskAsync(GetV1APIUriFromBase("sounds.json?token={0}", this.ApplicationToken));
                    return await ParseResponse<SoundsResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<ReceiptResponse> GetReceiptAsync(string receipt)
        {
            (this.ReceiptValidator ?? new ReceiptValidator()).Validate("receipt", receipt);
            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<ReceiptResponse>(async () =>
                {
                    var json = await wc.DownloadStringTaskAsync(GetV1APIUriFromBase("receipts/{0}.json?token={1}", receipt, this.ApplicationToken));
                    return await ParseResponse<ReceiptResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<PushoverUserResponse> CancelReceiptAsync(string receipt)
        {
            (this.ReceiptValidator ?? new ReceiptValidator()).Validate("receipt", receipt);
            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverUserResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "token", this.ApplicationToken }
                    };

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("receipts/{0}/cancel.json", receipt), parameters));
                    return await ParseResponse<PushoverUserResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<ValidateUserOrGroupResponse> ValidateUserOrGroupAsync(string userOrGroup)
        {
            return await ValidateUserOrGroupAsync(userOrGroup, null);
        }

        //NOTE: THROWS!! When user is unknown/invalid AND/OR device is unknown/invalid, otherwise returns
        public async Task<ValidateUserOrGroupResponse> ValidateUserOrGroupAsync(string userOrGroup, string device)
        {
            (this.UserOrGroupKeyValidator ?? new UserOrGroupKeyValidator()).Validate("userOrGroup", userOrGroup);
            if (device != null)
                (this.DeviceNameValidator ?? new DeviceNameValidator()).Validate("device", device);

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<ValidateUserOrGroupResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "token", this.ApplicationToken }, 
                        { "user", userOrGroup }
                    };
                    parameters.AddConditional("device", device);

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("users/validate.json"), parameters));
                    return await ParseResponse<ValidateUserOrGroupResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<MigrateSubscriptionResponse> MigrateSubscriptionAsync(string subscription, string userOrGroup)
        {
            return await MigrateSubscriptionAsync(subscription, userOrGroup, null);
        }

        public async Task<MigrateSubscriptionResponse> MigrateSubscriptionAsync(string subscription, string userOrGroup, string device)
        {
            return await MigrateSubscriptionAsync(subscription, userOrGroup, null, null);
        }

        public async Task<MigrateSubscriptionResponse> MigrateSubscriptionAsync(string subscription, string userOrGroup, string device, string sound)
        {
            (this.UserOrGroupKeyValidator ?? new UserOrGroupKeyValidator()).Validate("userOrGroup", userOrGroup);
            if (device != null)
                (this.DeviceNameValidator ?? new DeviceNameValidator()).Validate("device", device);

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<MigrateSubscriptionResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "token", this.ApplicationToken }, 
                        { "subscription", subscription },
                        { "user", userOrGroup },
                    };
                    parameters.AddConditional("device_name", device);
                    parameters.AddConditional("sound", sound);

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("subscriptions/migrate.json"), parameters));
                    return await ParseResponse<MigrateSubscriptionResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<AssignLicenseResponse> AssignLicenseAsync(string user, string email)
        {
            return await AssignLicenseAsync(user, email, OS.Any);
        }

        public async Task<AssignLicenseResponse> AssignLicenseAsync(string user, string email, OS os)
        {
            if (user != null)
                (this.UserOrGroupKeyValidator ?? new UserOrGroupKeyValidator()).Validate("user", user);
            if (email != null)
                (this.EmailValidator ?? new EMailValidator()).Validate("email", email);

            if (user == null && email == null)
                throw new InvalidOperationException("User or Email required");

            if (!Enum.IsDefined(typeof(OS), os))
                throw new ArgumentOutOfRangeException("os");

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<AssignLicenseResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "token", this.ApplicationToken }, 
                    };
                    parameters.AddConditional("user", user);
                    parameters.AddConditional("email", email);
                    parameters.AddConditional("os", os);

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("licenses/assign.json"), parameters));
                    return await ParseResponse<AssignLicenseResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            (this.EmailValidator ?? new EMailValidator()).Validate("email", email);
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<LoginResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "email", email }, 
                        { "password", password }, 
                    };

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("users/login.json"), parameters));
                    return await ParseResponse<LoginResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(string secret, string deviceName)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("secret");
            (this.DeviceNameValidator ?? new DeviceNameValidator()).Validate("deviceName", deviceName);

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<RegisterDeviceResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "secret", secret }, 
                        { "name", deviceName }, 
                        { "os", "O" }, //This is, currently, the only supported value ("Open Client")
                    };

                    //TODO: BUG? Report? See https://pushover.net/api/client#register
                    //When an existing devicename is used, a 400 bad request is returned
                    //However, the JSON returned is {"errors":{"name":["has already been taken"]},"status":0,"request":"b3e85163d0bd8fc84565839ffc33bb42"}
                    //Where "errors" normally is an array, it is now an object... this is not (very) consistent with the rest of the responses
                    //The call, currently, throws a BadRequestException, as it should, however the errorresponse fails to parse because of this inconsistency...

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("devices.json"), parameters));
                    return await ParseResponse<RegisterDeviceResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<ListMessagesResponse> ListMessagesAsync(string secret, string deviceId)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("secret");
            if (string.IsNullOrEmpty(deviceId))
                throw new ArgumentNullException("deviceId");

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<ListMessagesResponse>(async () =>
                {
                    var json = await wc.DownloadStringTaskAsync(GetV1APIUriFromBase("messages.json?secret={0}&device_id={1}", secret, deviceId));
                    return await ParseResponse<ListMessagesResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<PushoverUserResponse> DeleteMessagesAsync(string secret, string deviceId, int upToAndIncludingMessageId)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("secret");
            if (string.IsNullOrEmpty(deviceId))
                throw new ArgumentNullException("deviceId");
            if (upToAndIncludingMessageId < 0)
                throw new ArgumentOutOfRangeException("upToAndIncludingMessageId");

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverUserResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "secret", secret }, 
                        { "message", upToAndIncludingMessageId },
                    };

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("devices/{0}/update_highest_message.json", deviceId), parameters));
                    return await ParseResponse<PushoverUserResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<PushoverUserResponse> AcknowledgeMessageAsync(string secret, string receipt)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("secret");
            (this.ReceiptValidator ?? new ReceiptValidator()).Validate("receipt", receipt);

            using (var wc = this.GetWebClient())
            {
                return await ExecuteWebRequest<PushoverUserResponse>(async () =>
                {
                    var parameters = new PushoverParams { 
                        { "secret", secret }
                    };

                    var json = this.Encoding.GetString(await wc.UploadValuesTaskAsync(GetV1APIUriFromBase("receipts/{0}/acknowledge.json", receipt), parameters));
                    return await ParseResponse<PushoverUserResponse>(json, wc.ResponseHeaders);
                });
            }
        }

        public async Task<Stream> DownloadIconAsync(string iconName)
        {
            if (string.IsNullOrEmpty(iconName))
                throw new ArgumentNullException("iconName");
            return await this.DownloadFileAsync(GetIconUriFromBase("{0}.png", iconName));
        }

        public async Task<Stream> DownloadSoundAsync(string soundname)
        {
            return await DownloadSoundAsync(soundname, SoundType.Mp3);
        }

        public async Task<Stream> DownloadSoundAsync(string soundName, SoundType soundType)
        {
            if (string.IsNullOrEmpty(soundName))
                throw new ArgumentNullException("soundName");
            if (!Enum.IsDefined(typeof(SoundType), soundType))
                throw new ArgumentOutOfRangeException("soundType");

            return await this.DownloadFileAsync(GetSoundsUriFromBase("{0}.{1}", soundName, soundType.ToString().ToLowerInvariant()));
        }

        private async Task<Stream> DownloadFileAsync(Uri uri)
        {
            using (var wc = this.GetWebClient())
                return await wc.OpenReadTaskAsync(uri);
        }

        private Uri GetUriFromBase(string relative, params object[] args)
        {
            return new Uri(this.BaseUri, string.Format(relative, args));
        }


        private Uri GetV1APIUriFromBase(string relative, params object[] args)
        {
            return new Uri(GetUriFromBase("1/"), string.Format(relative, args));
        }

        private Uri GetIconUriFromBase(string relative, params object[] args)
        {
            return new Uri(GetUriFromBase("icons/"), string.Format(relative, args));
        }

        private Uri GetSoundsUriFromBase(string relative, params object[] args)
        {
            return new Uri(GetUriFromBase("sounds/"), string.Format(relative, args));
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
                var response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    //Try parse any json response... IF any
                    var errorresponse = ParseErrorResponse(response);
                    switch ((int)response.StatusCode)
                    {
                        case 400:   //Bad request
                            throw new BadRequestException(errorresponse, wex);
                        case 429:   //Rate limited
                            throw new RateLimitExceededException(errorresponse);
                    }
                }
                throw;
            }
            catch (PushoverException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PushoverException("Error retrieving response", ex);
            }
        }

        private PushoverUserResponse ParseErrorResponse(HttpWebResponse response)
        {
            PushoverUserResponse errorresponse = null;
            try
            {
                using (var s = response.GetResponseStream())
                using (var r = new StreamReader(s, this.Encoding))
                {
                    var json = r.ReadToEnd();
                    errorresponse = JsonConvert.DeserializeObject<PushoverUserResponse>(json);
                }

                errorresponse.RateLimitInfo = ParseRateLimitInfo(response.Headers);
            }
            catch
            {
                //NOP
            }
            return errorresponse;
        }

        private static RateLimitInfo ParseRateLimitInfo(WebHeaderCollection headers)
        {
            int limit, remaining, reset;

            if (int.TryParse(headers["X-Limit-App-Limit"], out limit)
                && int.TryParse(headers["X-Limit-App-Remaining"], out remaining)
                && int.TryParse(headers["X-Limit-App-Reset"], out reset))
            {
                return new RateLimitInfo(limit, remaining, EPOCH.AddSeconds(reset));
            }
            return null;
        }

        private static async Task<T> ParseResponse<T>(string json, WebHeaderCollection headers)
            where T : PushoverResponse
        {
            T result;
            try
            {
                result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
            }
            catch (Exception ex)
            {
                throw new PushoverException("Error parsing response", ex);
            }

            result.RateLimitInfo = ParseRateLimitInfo(headers);
            if (!result.IsOk)
                throw new ResponseException("API returned one or more errors", result);

            return result;
        }

        private WebClient GetWebClient()
        {
            var wc = new WebClient();
            wc.Proxy = this.Proxy;
            wc.Headers.Add(HttpRequestHeader.UserAgent, string.Format("{0} v{1} ({2})", ASSEMBLYNAME.Name, ASSEMBLYNAME.Version.ToString(2), HOMEURL));
            wc.Encoding = this.Encoding;
            return wc;
        }

    }
}
