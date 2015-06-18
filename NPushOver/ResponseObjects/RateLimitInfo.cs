using System;

namespace NPushOver.ResponseObjects
{
    public class RateLimitInfo
    {
        public int Limit { get; private set; }
        public int Remaining { get; private set; }
        public DateTime Reset { get; private set; }

        public RateLimitInfo(int limit, int remaining, DateTime reset)
        {
            this.Limit = limit;
            this.Remaining = remaining;
            this.Reset = reset;
        }
    }
}
