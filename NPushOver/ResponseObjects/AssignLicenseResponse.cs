using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class AssignLicenseResponse : PushoverResponse
    {
        [JsonProperty("credits")]
        public int Credits { get; set; }
    }
}
