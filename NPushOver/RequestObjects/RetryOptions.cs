using System;

namespace NPushOver
{
    public class RetryOptions
    {
        public TimeSpan RetryEvery { get; set; }
        public TimeSpan RetryPeriod { get; set; }
        public Uri CallBackUrl { get; set; }
    }
}
