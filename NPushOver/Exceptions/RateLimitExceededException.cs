using NPushOver.ResponseObjects;
using System;

namespace NPushOver.Exceptions
{
    public class RateLimitExceededException : ResponseException
    {
        public RateLimitInfo RateLimitInfo { get; set; }

        public RateLimitExceededException(PushoverResponse response)
            : this(response, null) { }

        public RateLimitExceededException(PushoverResponse response, Exception innerException)
            : base("Rate limit exceeded", response, innerException)
        {
            this.RateLimitInfo = response.RateLimitInfo;
        }
    }
}
