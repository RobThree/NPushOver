using System;

namespace NPushOver.Exceptions
{
    public class PushOverException : Exception
    {
        public PushOverException(string message)
            : this(message, null) { }

        public PushOverException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
