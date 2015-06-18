using NPushover.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NPushover.Validators
{
    public abstract class RegexValidator : IValidator<string>
    {
        public Regex Regex { get; private set; }

        protected const RegexOptions DefaultRegexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline;

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
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }

    public class UserOrGroupKeyValidator : RegexValidator
    {
        public UserOrGroupKeyValidator()
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }

    public class DeviceNameValidator : RegexValidator
    {
        public DeviceNameValidator()
            : base(new Regex("^[A-Za-z0-9_-]{1,25}$", DefaultRegexOptions)) { }
    }

    public class ReceiptValidator : RegexValidator
    {
        public ReceiptValidator()
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }
}
