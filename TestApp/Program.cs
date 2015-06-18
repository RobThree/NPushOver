using NPushover;
using NPushover.RequestObjects;
using System;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var po = new Pushover("[APPLICATION-KEY-HERE]");

            // Quick message:
            var msg1 = Message.Create(Priority.Normal, "Hello world!");
            var sendtask1 = po.SendMessageAsync(msg1, "[RECIPIENT-ID-HERE]");

            // More comprehensive:
            var msg2 = Message.Create(Priority.High, "The roof!", "The roof is on fire!", false, Sounds.Siren);
            msg2.RetryOptions = new RetryOptions { RetryEvery = TimeSpan.FromSeconds(30), RetryPeriod = TimeSpan.FromHours(3) };
            msg2.SupplementaryUrl = new SupplementaryURL { Uri = new Uri("http://robiii.me"), Title = "Awesome dude!" };
            var sendtask2 = po.SendMessageAsync(msg2, "[RECIPIENT-ID-HERE]", "[DEVICE-ID-HERE]");


            // Send our messages            
            Task.WaitAll(sendtask1, sendtask2);

            // Other examples:
            Task.WaitAll(
                po.ValidateUserOrGroupAsync("[USER-ID-HERE]"),
                po.CancelReceiptAsync("[RECEIPT-ID-HERE]"),
                po.ListSoundsAsync(),
                po.LoginAsync("[EMAIL-HERE]", "[PASSWORD-HERE]"),
                po.ListMessagesAsync("[SECRET-HERE]", "[DEVICE-ID-HERE]")
            );
            // ...etc.
        }
    }
}
