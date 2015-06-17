using System;

namespace NPushOver
{
    public class PushOverResponseException : PushOverException
    {
        public PushoverResponse Response { get; private set; }

        public PushOverResponseException(string message)
            : base(message) { }

        public PushOverResponseException(string message, PushoverResponse response)
            : this(message, response, null) { }

        public PushOverResponseException(string message, PushoverResponse response, Exception innerException)
            : base(message, innerException)
        {
            this.Response = response;
        }
    }
}
