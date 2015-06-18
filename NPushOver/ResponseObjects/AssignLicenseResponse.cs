using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class AssignLicenseResponse : PushoverUserResponse
    {
        [JsonProperty("credits")]
        public int Credits { get; set; }
    }
}
