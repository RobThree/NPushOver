using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPushover.Converters;
using NPushover.RequestObjects;
using System;

namespace NPushover.ResponseObjects
{
    public class StoredMessage
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("umid")]
        public int UMId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("app")]
        public string Application { get; set; }

        [JsonProperty("aid")]
        public int ApplicationId { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }


        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("url_title")]
        public string UrlTitle { get; set; }

        [JsonProperty("acked")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Acknowledged { get; set; }

        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        [JsonProperty("html")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsHtmlBody { get; set; }
    }
}
