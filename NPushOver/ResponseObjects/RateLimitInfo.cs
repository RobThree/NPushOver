using System;

namespace NPushOver.ResponseObjects
{
    public class RateLimitInfo
    {
        public int AppLimit { get; private set; }
        public int AppRemaining { get; private set; }
        public DateTime AppReset { get; private set; }

        //X-Limit-App-Limit: 7500
        //X-Limit-App-Remaining: 7496
        //X-Limit-App-Reset: 1393653600
    }
}
