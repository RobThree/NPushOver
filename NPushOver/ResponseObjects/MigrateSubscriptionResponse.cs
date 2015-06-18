using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    public class MigrateSubscriptionResponse : PushoverUserResponse
    {
        [JsonProperty("subscribed_user_key")]
        public string SubscribedUserKey { get; set; }
    }
}
