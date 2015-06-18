using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class ValidateUserOrGroupResponse : PushoverUserResponse
    {
        [JsonProperty("devices")]
        public string[] Devices { get; set; }
    }
}
