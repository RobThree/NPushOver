using System;

namespace NPushover.Exceptions
{
    public class PushoverException : Exception
    {
        public PushoverException(string message)
            : this(message, null) { }

        public PushoverException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
