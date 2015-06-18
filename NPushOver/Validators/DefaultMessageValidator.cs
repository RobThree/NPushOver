using NPushOver.RequestObjects;
using System;

namespace NPushOver.Validators
{
    public class DefaultMessageValidator : IValidator<Message>
    {
        private const int MAXBODYLENGTH = 1024;
        private const int MAXTITLELENGTH = 250;
        private const int MAXSUPURLTITLELENGTH = 100;
        private static readonly TimeSpan MINRETRYEVERY = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan MAXRETRYPERIOD = TimeSpan.FromHours(24);


        public void Validate(string paramName, Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (message.Body == null)
                throw new ArgumentNullException("body");
            if (message.Body.Length > MAXBODYLENGTH)
                throw new ArgumentOutOfRangeException("body");

            if ((message.Title ?? string.Empty).Length > MAXTITLELENGTH)
                throw new ArgumentOutOfRangeException("title");

            if (!Enum.IsDefined(typeof(Priority), message.Priority))
                throw new ArgumentOutOfRangeException("priority");

            if (message.Priority == Priority.Emergency)
            {
                if (message.RetryOptions == null)
                    throw new ArgumentNullException("retryOptions");

                if (message.RetryOptions.RetryEvery < MINRETRYEVERY)
                    throw new ArgumentOutOfRangeException("retryOptions.retryEvery");
                if (message.RetryOptions.RetryPeriod > MAXRETRYPERIOD)
                    throw new ArgumentOutOfRangeException("retryOptions.retryPeriod");
            }
            else
            {
                if (message.RetryOptions != null)
                    throw new ArgumentException("retryOptions");
            }

            if (message.SupplementaryUrl != null)
            {
                if (message.SupplementaryUrl.Uri == null)
                    throw new ArgumentNullException("supplementaryUrl.uri");

                if ((message.SupplementaryUrl.Title ?? string.Empty).Length > MAXSUPURLTITLELENGTH)
                    throw new ArgumentOutOfRangeException("supplementaryUrl.title");
            }
        }
    }
}
