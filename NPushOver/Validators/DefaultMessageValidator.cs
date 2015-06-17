using NPushOver.RequestObjects;
using System;

namespace NPushOver.Validators
{
    public class DefaultMessageValidator : IValidator<Message>
    {
        public void Validate(string paramName, Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (message.Body == null)
                throw new ArgumentNullException("body");
            if (message.Body.Length > 1024)                     //TODO: Move to some "consts"
                throw new ArgumentOutOfRangeException("body");  //TODO: Maybe add explanation?

            if ((message.Title ?? string.Empty).Length > 250)   //TODO: Move to some "consts"?
                throw new ArgumentOutOfRangeException("title"); //TODO: Maybe add explanation?

            if (!Enum.IsDefined(typeof(Priority), message.Priority))
                throw new ArgumentOutOfRangeException("priority");

            if (message.Priority == Priority.Emergency)
            {
                if (message.RetryOptions == null)
                    throw new ArgumentNullException("retryOptions");

                if (message.RetryOptions.RetryEvery < TimeSpan.FromSeconds(30))
                    throw new ArgumentOutOfRangeException("retryOptions.retryEvery");
                if (message.RetryOptions.RetryPeriod > TimeSpan.FromHours(24))
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

                if ((message.SupplementaryUrl.Title ?? string.Empty).Length > 100)      //TODO: Move to some "consts"?
                    throw new ArgumentOutOfRangeException("supplementaryUrl.title");    //TODO: Maybe add explanation?
            }
        }
    }
}
