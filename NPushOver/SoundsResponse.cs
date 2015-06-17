using System.Collections.Generic;

namespace NPushOver
{
    public class SoundsResponse : PushoverResponse
    {
        public IDictionary<string, string> Sounds { get; set; }
    }
}
