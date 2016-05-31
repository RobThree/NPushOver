namespace TestApp
{
    partial class DemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtAppKey = new System.Windows.Forms.TextBox();
            this.txtUserKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSounds = new System.Windows.Forms.ComboBox();
            this.cmbPriorities = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkIsHtml = new System.Windows.Forms.CheckBox();
            this.txtRetryEvery = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRetryPeriod = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSupplementaryURL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSupplementaryURLTitle = new System.Windows.Forms.TextBox();
            this.pnlRetry = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetryEvery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetryPeriod)).BeginInit();
            this.pnlRetry.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAppKey
            // 
            this.txtAppKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppKey.Location = new System.Drawing.Point(126, 12);
            this.txtAppKey.Name = "txtAppKey";
            this.txtAppKey.Size = new System.Drawing.Size(296, 20);
            this.txtAppKey.TabIndex = 1;
            // 
            // txtUserKey
            // 
            this.txtUserKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserKey.Location = new System.Drawing.Point(126, 38);
            this.txtUserKey.Name = "txtUserKey";
            this.txtUserKey.Size = new System.Drawing.Size(296, 20);
            this.txtUserKey.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Key";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnlRetry);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtSupplementaryURLTitle);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtSupplementaryURL);
            this.groupBox2.Controls.Add(this.chkIsHtml);
            this.groupBox2.Controls.Add(this.cmbPriorities);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbSounds);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmdSend);
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Location = new System.Drawing.Point(12, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 335);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send message";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(6, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel last emergency";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Title";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(114, 19);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(290, 20);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "The roof! The roof!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Message:";
            // 
            // cmdSend
            // 
            this.cmdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSend.Location = new System.Drawing.Point(329, 306);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(75, 23);
            this.cmdSend.TabIndex = 14;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSendEmergency_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(114, 45);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(290, 72);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.Text = "The roof is on fire!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sound";
            // 
            // cmbSounds
            // 
            this.cmbSounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSounds.FormattingEnabled = true;
            this.cmbSounds.Location = new System.Drawing.Point(114, 146);
            this.cmbSounds.Name = "cmbSounds";
            this.cmbSounds.Size = new System.Drawing.Size(290, 21);
            this.cmbSounds.TabIndex = 6;
            // 
            // cmbPriorities
            // 
            this.cmbPriorities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPriorities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriorities.FormattingEnabled = true;
            this.cmbPriorities.Location = new System.Drawing.Point(114, 173);
            this.cmbPriorities.Name = "cmbPriorities";
            this.cmbPriorities.Size = new System.Drawing.Size(290, 21);
            this.cmbPriorities.TabIndex = 8;
            this.cmbPriorities.SelectionChangeCommitted += new System.EventHandler(this.cmbPriorities_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Priority";
            // 
            // chkIsHtml
            // 
            this.chkIsHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsHtml.AutoSize = true;
            this.chkIsHtml.Location = new System.Drawing.Point(337, 123);
            this.chkIsHtml.Name = "chkIsHtml";
            this.chkIsHtml.Size = new System.Drawing.Size(67, 17);
            this.chkIsHtml.TabIndex = 4;
            this.chkIsHtml.Text = "Is HTML";
            this.chkIsHtml.UseVisualStyleBackColor = true;
            // 
            // txtRetryEvery
            // 
            this.txtRetryEvery.Location = new System.Drawing.Point(105, 3);
            this.txtRetryEvery.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.txtRetryEvery.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtRetryEvery.Name = "txtRetryEvery";
            this.txtRetryEvery.Size = new System.Drawing.Size(68, 20);
            this.txtRetryEvery.TabIndex = 1;
            this.txtRetryEvery.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Retry every";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "seconds for";
            // 
            // txtRetryPeriod
            // 
            this.txtRetryPeriod.Location = new System.Drawing.Point(247, 3);
            this.txtRetryPeriod.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.txtRetryPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRetryPeriod.Name = "txtRetryPeriod";
            this.txtRetryPeriod.Size = new System.Drawing.Size(68, 20);
            this.txtRetryPeriod.TabIndex = 3;
            this.txtRetryPeriod.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(321, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "minutes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Supplementary URL";
            // 
            // txtSupplementaryURL
            // 
            this.txtSupplementaryURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupplementaryURL.Location = new System.Drawing.Point(114, 226);
            this.txtSupplementaryURL.Name = "txtSupplementaryURL";
            this.txtSupplementaryURL.Size = new System.Drawing.Size(290, 20);
            this.txtSupplementaryURL.TabIndex = 11;
            this.txtSupplementaryURL.Text = "http://robiii.me";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Supp. URL title";
            // 
            // txtSupplementaryURLTitle
            // 
            this.txtSupplementaryURLTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupplementaryURLTitle.Location = new System.Drawing.Point(114, 252);
            this.txtSupplementaryURLTitle.Name = "txtSupplementaryURLTitle";
            this.txtSupplementaryURLTitle.Size = new System.Drawing.Size(290, 20);
            this.txtSupplementaryURLTitle.TabIndex = 13;
            this.txtSupplementaryURLTitle.Text = "Awesome dude!";
            // 
            // pnlRetry
            // 
            this.pnlRetry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRetry.Controls.Add(this.label7);
            this.pnlRetry.Controls.Add(this.txtRetryEvery);
            this.pnlRetry.Controls.Add(this.label8);
            this.pnlRetry.Controls.Add(this.txtRetryPeriod);
            this.pnlRetry.Controls.Add(this.label9);
            this.pnlRetry.Enabled = false;
            this.pnlRetry.Location = new System.Drawing.Point(9, 197);
            this.pnlRetry.Name = "pnlRetry";
            this.pnlRetry.Size = new System.Drawing.Size(395, 23);
            this.pnlRetry.TabIndex = 9;
            // 
            // DemoForm
            // 
            this.AcceptButton = this.cmdSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserKey);
            this.Controls.Add(this.txtAppKey);
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "DemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DemoApp";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetryEvery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetryPeriod)).EndInit();
            this.pnlRetry.ResumeLayout(false);
            this.pnlRetry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAppKey;
        private System.Windows.Forms.TextBox txtUserKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbSounds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPriorities;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIsHtml;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown txtRetryPeriod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtRetryEvery;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSupplementaryURLTitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSupplementaryURL;
        private System.Windows.Forms.Panel pnlRetry;
    }
}

