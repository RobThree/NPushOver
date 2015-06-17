using NPushOver.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NPushOver.Validators
{
    public abstract class RegexValidator : IValidator<string>
    {
        public Regex Regex { get; private set; }

        public RegexValidator(Regex regex)
        {
            this.Regex = regex;
        }

        public void Validate(string paramName, string value)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (!this.Regex.IsMatch(value))
                throw new InvalidKeyException(paramName, value);
        }
    }

    public class ApplicationKeyValidator : RegexValidator
    {
        public ApplicationKeyValidator()
            : base(new Regex("[A-Za-z0-9]{30}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline)) { }
    }

    public class UserOrGroupKeyValidator : RegexValidator
    {
        public UserOrGroupKeyValidator()
            : base(new Regex("[A-Za-z0-9]{30}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline)) { }
    }

    public class DeviceNameValidator : RegexValidator
    {
        public DeviceNameValidator()
            : base(new Regex("[A-Za-z0-9_-]{1,25}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline)) { }
    }
}
