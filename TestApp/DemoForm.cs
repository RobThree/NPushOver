using NPushover;
using NPushover.RequestObjects;    
using System;
using System.Windows.Forms;

namespace TestApp
{
    public partial class DemoForm : Form
    {
        private string _receiptid = null;

        public DemoForm()
        {
            InitializeComponent();

        }

        private async void cmdSendMessage_Click(object sender, EventArgs e)
        {
            var pushover = new Pushover(txtAppKey.Text);
            var msg = NPushover.RequestObjects.Message.Create(txtMessage.Text); // We need to use the fully qualified 
                                                                                // namespace since Message conflicts 
                                                                                // with System.Windows.Forms.Message
            
            await pushover.SendMessageAsync(msg, txtUserKey.Text);
        }

        private async void cmdSendEmergency_Click(object sender, EventArgs e)
        {
            var pushover = new Pushover(txtAppKey.Text);

            var msg = NPushover.RequestObjects.Message.Create(
                Priority.Emergency, 
                txtTitle.Text, 
                txtEmergency.Text, 
                false, 
                Sounds.Siren
            );
            msg.RetryOptions = new RetryOptions {
                RetryEvery = TimeSpan.FromSeconds(30),
                RetryPeriod = TimeSpan.FromHours(3)
            };
            msg.SupplementaryUrl = new SupplementaryURL {
                Uri = new Uri("http://robiii.me"),
                Title = "Awesome dude!"
            };

            var result = await pushover.SendMessageAsync(msg, txtUserKey.Text);
            btnCancel.Enabled = true;
            _receiptid = result.Receipt;    //Store receipt ID
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var pushover = new Pushover(txtAppKey.Text);
            var result = await pushover.CancelReceiptAsync(_receiptid);
            btnCancel.Enabled = !result.IsOk;
        }
    }
}


//// Other examples:
//pushover.ValidateUserOrGroupAsync("[USER-ID-HERE]"),
//pushover.CancelReceiptAsync("[RECEIPT-ID-HERE]"),
//pushover.ListSoundsAsync(),
//pushover.LoginAsync("[EMAIL-HERE]", "[PASSWORD-HERE]"),
//pushover.ListMessagesAsync("[SECRET-HERE]", "[DEVICE-ID-HERE]")
