using Newtonsoft.Json;
using System.Collections.Generic;

namespace NPushover.ResponseObjects
{
    public class SoundsResponse : PushoverUserResponse
    {
        [JsonProperty("sounds")]
        public IDictionary<string, string> Sounds { get; set; }
    }
}
