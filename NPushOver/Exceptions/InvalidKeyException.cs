namespace NPushover.Exceptions
{
    public class InvalidKeyException : PushoverException
    {
        public string Key { get; private set; }

        public InvalidKeyException(string paramName, string key)
            : base(paramName)
        {
            this.Key = key;
        }
    }
}
