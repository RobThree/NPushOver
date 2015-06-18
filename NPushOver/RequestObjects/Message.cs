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

        //public Message SetPriority(Priority priority)
        //{
        //    this.Priority = priority;
        //    return this;
        //}

        //public Message SetTitle(string title)
        //{
        //    this.Title = title;
        //    return this;
        //}

        //public Message SetBody(string body)
        //{
        //    this.Body = body;
        //    return this;
        //}

        //public Message SetSupplementaryURL(SupplementaryURL url)
        //{
        //    this.SupplementaryURL = url;
        //    return this;
        //}

        //public Message SetSupplementaryURL(Uri uri)
        //{
        //    return this.SetSupplementaryURL(uri, null);
        //}

        //public Message SetSupplementaryURL(Uri uri, string title)
        //{
        //    this.SupplementaryURL = new SupplementaryURL { Uri = uri, Title = title };
        //    return this;
        //}

        //public Message SetTimestamp(DateTime timestamp)
        //{
        //    this.Timestamp = timestamp;
        //    return this;
        //}

        //public Message SetSound(Sound sound)
        //{
        //    if (sound == NPushover.Sound.None)
        //        this.Sound = null;
        //    else
        //        this.Sound = Enum.GetName(typeof(Sound), sound).ToLowerInvariant();
        //    return this;
        //}
    }
}
