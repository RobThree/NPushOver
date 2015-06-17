using System;
using NPushOver;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var po = new PushOver("[APPLICATION-KEY-HERE]");

            var m = new Message();
            //.SetSound(Sound.Alien)
            //.SetTitle("test")
            //.SetBody("foo");

            var t1 = po.ListSoundsAsync();
            var t2 = po.SendMessageAsync(new Message
            {
                Title = "WOAH!",
                Body = "Awesome\r\nThis is cool!\r\nTesting, tësting Iñtërnâtiônàlizætiøn ☃",
                Priority = Priority.Emergency,
                Timestamp = DateTime.UtcNow.AddHours(-3),
                Sound = "bike",
                SupplementaryUrl = new SupplementaryURL { 
                    Title = "RobIII", 
                    Uri = new Uri("http://robiii.me") 
                },
                RetryOptions = new RetryOptions
                {
                    RetryEvery = TimeSpan.FromSeconds(30),
                    RetryPeriod = TimeSpan.FromSeconds(90)
                }
            }, "[RECIPIENT-ID-HERE]");

            Task.WaitAll(t1, t2);

            var sounds = t1.Result;
            var sndmsg = t2.Result;
        }
    }
}
