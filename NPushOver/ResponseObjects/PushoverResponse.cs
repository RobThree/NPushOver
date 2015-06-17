namespace NPushOver.ResponseObjects
{
    public class PushoverResponse
    {
        public int Status { get; set; }
        public string Request { get; set; }
        public string Receipt { get; set; }
        public string[] Errors { get; set; }
        public RateLimitInfo RateLimitInfo { get; set; }
        public bool HasErrors
        {
            get { return this.Errors != null && this.Errors.Length > 0; }
        }

        public bool IsOkStatus
        {
            get { return this.Status == 1; }
        }

        public bool IsOk
        {
            get { return this.IsOkStatus && !this.HasErrors; }
        }
    }
}
