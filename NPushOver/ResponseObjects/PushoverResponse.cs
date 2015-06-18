using Newtonsoft.Json;
namespace NPushover.ResponseObjects
{
    public class PushoverResponse
    {
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

    public class PushoverUserResponse : PushoverResponse
    {
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
