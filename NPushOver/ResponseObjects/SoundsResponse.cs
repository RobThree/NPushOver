using Newtonsoft.Json;
using System.Collections.Generic;

namespace NPushOver.ResponseObjects
{
    public class SoundsResponse : PushoverResponse
    {
        [JsonProperty("sounds")]
        public IDictionary<string, string> Sounds { get; set; }
    }
}
