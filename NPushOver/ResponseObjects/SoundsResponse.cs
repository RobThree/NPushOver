using System.Collections.Generic;

namespace NPushOver.ResponseObjects
{
    public class SoundsResponse : PushoverResponse
    {
        public IDictionary<string, string> Sounds { get; set; }
    }
}
