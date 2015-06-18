using NPushover.ResponseObjects;
using System;

namespace NPushover.Exceptions
{
    public class ResponseException : PushoverException
    {
        public PushoverResponse Response { get; private set; }

        public ResponseException(string message)
            : base(message) { }

        public ResponseException(string message, PushoverResponse response)
            : this(message, response, null) { }

        public ResponseException(string message, PushoverResponse response, Exception innerException)
            : base(message, innerException)
        {
            this.Response = response;
        }
    }
}
