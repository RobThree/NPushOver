using NPushover;
using NPushover.RequestObjects;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var po = new Pushover("[APPLICATION-KEY-HERE]");

            var msg = Message.Create(Priority.Normal, "Hello world!");

            var sendtask = po.SendMessageAsync(msg, "[RECIPIENT-ID-HERE]");

            Task.WaitAll(sendtask);
        }
    }
}
