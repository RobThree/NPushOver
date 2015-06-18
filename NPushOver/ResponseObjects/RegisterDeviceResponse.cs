using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class RegisterDeviceResponse : PushoverResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
