using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPushOver
{
    internal class PushOverParams : NameValueCollection
    {
        public void AddConditional(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                this.Add(key, value);
        }

        public void Add(string key, Uri value)
        {
            if (value != null)
                this.Add(key, value.AbsoluteUri);
        }

        public void Add(string key, TimeSpan value)
        {
            if (value != null)
                this.Add(key, (int)value.TotalSeconds);
        }

        public void Add(string key, int value)
        {
            this.Add(key, value.ToString());
        }
    }
}
