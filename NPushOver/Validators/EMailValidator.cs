using System;
using System.Linq;

namespace NPushover.Validators
{
    // Provides VERY simple e-mail address validation (all that's required to validate is the string to contain an '@', 
    // the rest is up to Pushover's servers).
    public class EMailValidator : IValidator<string>
    {
        public void Validate(string paramName, string value)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            if (!value.Contains('@'))
                throw new ArgumentException("Invalid email address", paramName);
        }
    }
}
