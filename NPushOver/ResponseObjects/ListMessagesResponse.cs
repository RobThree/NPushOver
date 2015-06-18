using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class ListMessagesResponse : PushoverResponse
    {
        [JsonProperty("messages")]
        public StoredMessage[] Messages { get; set; }

        [JsonProperty("user")]
        public ListMessagesUser User { get; set; }
    }
}
