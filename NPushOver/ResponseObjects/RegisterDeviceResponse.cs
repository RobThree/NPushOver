using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class RegisterDeviceResponse : PushoverUserResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
