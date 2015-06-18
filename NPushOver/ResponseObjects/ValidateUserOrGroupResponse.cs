using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class ValidateUserOrGroupResponse : PushoverResponse
    {
        [JsonProperty("devices")]
        public string[] Devices { get; set; }
    }
}
