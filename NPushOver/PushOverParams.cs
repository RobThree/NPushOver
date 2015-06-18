using NPushover.RequestObjects;
using System;
using System.Collections.Specialized;

namespace NPushover
{
    internal class PushoverParams : NameValueCollection
    {
        public void AddConditional(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                this.Add(key, value);
        }

        public void AddConditional(string key, string[] value, string separator = ",")
        {
            if (value != null && value.Length > 0)
                this.Add(key, string.Join(separator, value));
        }

        public void AddConditional(string key, bool value)
        {
            if (value)
                this.Add(key, 1);
        }

        public void AddConditional(string key, OS value)
        {
            if (value != OS.Any)
                this.Add(key, value.ToString().ToLowerInvariant());
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
