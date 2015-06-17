using System;

namespace NPushOver
{
    public class PushOverException : Exception
    {
        public PushoverResponse Response { get; private set; }

        public PushOverException(string message)
            : this(message, null) { }

        public PushOverException(string message, PushoverResponse response)
            : this(message, response, null) { }

        public PushOverException(string message, PushoverResponse response, Exception innerException)
            : base(message, innerException)
        {
            this.Response = response;
        }
    }
}
