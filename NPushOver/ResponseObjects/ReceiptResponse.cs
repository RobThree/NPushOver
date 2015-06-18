using Newtonsoft.Json;
using NPushOver.Converters;
using System;

namespace NPushOver.ResponseObjects
{
    public class ReceiptResponse : PushoverResponse
    {
        [JsonProperty("acknowledged")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Acknowledged { get; set; }

        [JsonProperty("acknowledged_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime AcknowledgedAt { get; set; }

        [JsonProperty("acknowledged_by")]
        public string AcknowledgedBy { get; set; }

        [JsonProperty("last_delivered_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastDeliveredAt { get; set; }

        [JsonProperty("expired")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Expired { get; set; }
        
        [JsonProperty("expires_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ExpiresAt { get; set; }
        
        [JsonProperty("called_back")]
        [JsonConverter(typeof(BoolConverter))]
        public bool CalledBack { get; set; }

        [JsonProperty("called_back_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CalledBackAt { get; set; }
    }
}
