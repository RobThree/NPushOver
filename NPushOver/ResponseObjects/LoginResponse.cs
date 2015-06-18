using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class LoginResponse : PushoverUserResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
