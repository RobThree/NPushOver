using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class MigrateSubscriptionResponse : PushoverResponse
    {
        [JsonProperty("subscribed_user_key")]
        public string SubscribedUserKey { get; set; }
    }
}
