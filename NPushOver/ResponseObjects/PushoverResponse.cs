using Newtonsoft.Json;
namespace NPushOver.ResponseObjects
{
    public class PushoverResponse
    {
        //TODO: This conflicts with ListMessages response (BUG? REPORT? See https://pushover.net/api#response)
        //[JsonProperty("user")]
        //public string User { get; set; }
        
        [JsonProperty("status")]
        public int Status { get; set; }
        
        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        [JsonProperty("errors")]
        public string[] Errors { get; set; }

        [JsonIgnore]
        public RateLimitInfo RateLimitInfo { get; set; }

        public bool HasErrors
        {
            get { return this.Errors != null && this.Errors.Length > 0; }
        }

        public bool IsOkStatus
        {
            get { return this.Status == 1; }
        }

        public bool IsOk
        {
            get { return this.IsOkStatus && !this.HasErrors; }
        }
    }
}
