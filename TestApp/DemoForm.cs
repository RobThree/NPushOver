using NPushover;
using NPushover.RequestObjects;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace TestApp
{
    public partial class DemoForm : Form
    {
        private string _receiptid = null;

        public DemoForm()
        {
            InitializeComponent();

            cmbPriorities.DataSource = ((Priority[])Enum.GetValues(typeof(Priority))).OrderBy(v => v).ToArray();
            cmbSounds.DataSource = ((Sounds[])Enum.GetValues(typeof(Sounds))).OrderBy(v => v).ToArray();

            // See App.Config for values
            txtAppKey.Text = ConfigurationManager.AppSettings["applicationkey"];
            txtUserKey.Text = ConfigurationManager.AppSettings["userkey"];
        }

        private async void cmdSendMessage_Click(object sender, EventArgs e)
        {
            var pushover = new Pushover(txtAppKey.Text);
            var msg = NPushover.RequestObjects.Message.Create(txtMessage.Text); // We need to use the fully qualified 
                                                                                // namespace in a Form since Message 
                                                                                // conflicts  with 
                                                                                // System.Windows.Forms.Message

            await pushover.SendMessageAsync(msg, txtUserKey.Text);
        }

        private async void cmdSendEmergency_Click(object sender, EventArgs e)
        {
            // Create message
            var msg = NPushover.RequestObjects.Message.Create(
                (Priority)cmbPriorities.SelectedItem,
                txtTitle.Text,
                txtMessage.Text,
                chkIsHtml.Checked,
                (Sounds)cmbSounds.SelectedItem
            );

            // Emergency messages have retryoptions
            if (this.IsEmergencyMessage())
            {
                msg.RetryOptions = new RetryOptions
                {
                    RetryEvery = TimeSpan.FromSeconds((int)txtRetryEvery.Value),
                    RetryPeriod = TimeSpan.FromMinutes((int)txtRetryPeriod.Value),
                    //CallBackUrl = new Uri("http://example.org/foo/bar")
                };
            }

            // Also, supplementary URL's can be specified
            if (!string.IsNullOrEmpty(txtSupplementaryURL.Text))
            {
                msg.SupplementaryUrl = new SupplementaryURL
                {
                    Uri = new Uri(txtSupplementaryURL.Text),
                    Title = txtSupplementaryURLTitle.Text
                };
            }

            // Send the message
            var pushover = new Pushover(txtAppKey.Text);
            var result = await pushover.SendMessageAsync(msg, txtUserKey.Text);

            //Store receipt ID (if any)
            _receiptid = result.Receipt;
            btnCancel.Enabled = !string.IsNullOrEmpty(_receiptid);
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var pushover = new Pushover(txtAppKey.Text);
            var result = await pushover.CancelReceiptAsync(_receiptid);
            btnCancel.Enabled = !result.IsOk;
        }

        private void cmbPriorities_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pnlRetry.Enabled = this.IsEmergencyMessage();
        }

        private bool IsEmergencyMessage()
        {
            return (Priority)cmbPriorities.SelectedItem == Priority.Emergency;
        }
    }
}