using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class ListMessagesResponse : PushoverResponse
    {
        [JsonProperty("messages")]
        public StoredMessage[] Messages { get; set; }

        //TODO: This user "conflicts" with the base PushoverResponse object BUG? Report?
        [JsonProperty("user")]
        public ListMessagesUser User { get; set; }
    }
}
