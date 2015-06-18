using NPushover.ResponseObjects;
using System;

namespace NPushover.Exceptions
{
    public class BadRequestException : ResponseException
    {
        public BadRequestException()
            : this(null) { }

        public BadRequestException(PushoverResponse response)
            : this(response, null) { }

        public BadRequestException(PushoverResponse response, Exception innerException)
            : base("Bad request", response, innerException) { }
    }
}
