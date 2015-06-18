using System;

namespace NPushover.RequestObjects
{
    public class Message
    {
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public SupplementaryURL SupplementaryUrl { get; set; }

        //Make sure Timestamp is specified in UTC; if not it will be assumed local and converted to UTC.
        public DateTime? Timestamp { get; set; }
        public string Sound { get; set; }
        public bool IsHtmlBody { get; set; }
        public RetryOptions RetryOptions { get; set; }

        public static Message Create(Priority priority, string body)
        {
            return Create(priority, body, Sounds.Pushover);
        }

        public static Message Create(Priority priority, string body, Sounds sound)
        {
            return Create(priority, body, false, sound);
        }

        public static Message Create(Priority priority, string body, bool isHtmlBody, Sounds sound)
        {
            return Create(priority, null, body, isHtmlBody, sound);
        }

        public static Message Create(Priority priority, string title, string body, bool isHtmlBody, Sounds sound)
        {
            return new Message
            {
                Priority = priority,
                Title = title,
                Body = body,
                IsHtmlBody = isHtmlBody,
                Sound = sound.ToString().ToLowerInvariant()
            };
        }
    }
}
