using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class LoginResponse : PushoverResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
