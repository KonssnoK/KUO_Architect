namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class EditServerListing : Form
    {
        private Button btnCancel;
        private Button btnSave;
        private Container components = null;
        private Label label1 = new Label();
        private Label label2;
        private Label label3;
        private Label label4;
        private ServerListing m_listing = new ServerListing("", "", Config.Port, "");
        private bool m_newRecord = true;
        private ServerListing m_oldListingData;
        private TextBox txtIPAddress;
        private TextBox txtPort;
        private TextBox txtServerName;
        private TextBox txtUserName;

        public EditServerListing()
        {
            this.InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.m_newRecord)
            {
                Config.ServerListings.Add(this.m_listing);
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void EditListing(ServerListing listing, Form owner)
        {
            base.Owner = owner;
            this.Text = "Edit Listing";
            this.m_newRecord = false;
            this.m_listing = listing;
            this.m_oldListingData = new ServerListing(listing.ServerName, listing.ServerIP, listing.Port, listing.UserName);
            this.m_oldListingData.Password = listing.Password;
            this.UpdateControlStates();
            this.UpdateDisplay();
            base.ShowDialog(owner);
            if (base.DialogResult == DialogResult.Cancel)
            {
                this.m_listing.ServerName = this.m_oldListingData.ServerName;
                this.m_listing.ServerIP = this.m_oldListingData.ServerIP;
                this.m_listing.Port = this.m_oldListingData.Port;
                this.m_listing.UserName = this.m_oldListingData.UserName;
                this.m_listing.Password = this.m_oldListingData.Password;
            }
        }

        private void InitializeComponent()
        {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.txtIPAddress = new System.Windows.Forms.TextBox();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "IP Address";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Port";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "User Name";
			// 
			// txtServerName
			// 
			this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtServerName.Location = new System.Drawing.Point(72, 16);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(128, 20);
			this.txtServerName.TabIndex = 4;
			this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
			// 
			// txtIPAddress
			// 
			this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtIPAddress.Location = new System.Drawing.Point(72, 40);
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(128, 20);
			this.txtIPAddress.TabIndex = 5;
			this.txtIPAddress.TextChanged += new System.EventHandler(this.txtIPAddress_TextChanged);
			// 
			// txtPort
			// 
			this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPort.Location = new System.Drawing.Point(72, 64);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(72, 20);
			this.txtPort.TabIndex = 6;
			this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
			// 
			// txtUserName
			// 
			this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtUserName.Location = new System.Drawing.Point(72, 88);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(128, 20);
			this.txtUserName.TabIndex = 7;
			this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(136, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSave.Location = new System.Drawing.Point(64, 120);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(64, 24);
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// EditServerListing
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(208, 152);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.txtIPAddress);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "EditServerListing";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Listing";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.m_newRecord)
            {
                this.m_listing.GUID = Guid.NewGuid().ToString();
            }
        }

        private void txtIPAddress_TextChanged(object sender, EventArgs e)
        {
            this.m_listing.ServerIP = this.txtIPAddress.Text.Trim();
            this.UpdateControlStates();
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            this.m_listing.Port = Utility.ToInt(this.txtPort.Text.Trim());
            this.UpdateControlStates();
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            this.m_listing.ServerName = this.txtServerName.Text.Trim();
            this.UpdateControlStates();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            this.m_listing.UserName = this.txtUserName.Text.Trim();
            this.UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            bool flag = true;
            if ((this.m_listing.ServerName == null) || (this.m_listing.ServerName.Length == 0))
            {
                flag = false;
            }
            else if ((this.m_listing.ServerIP == null) || (this.m_listing.ServerIP.Length == 0))
            {
                flag = false;
            }
            else if ((this.m_listing.UserName == null) || (this.m_listing.UserName.Length == 0))
            {
                flag = false;
            }
            this.btnSave.Enabled = flag;
        }

        private void UpdateDisplay()
        {
            this.txtServerName.Text = (this.m_listing.ServerName != null) ? this.m_listing.ServerName : "";
            this.txtIPAddress.Text = (this.m_listing.ServerIP != null) ? this.m_listing.ServerIP : "";
            this.txtUserName.Text = (this.m_listing.UserName != null) ? this.m_listing.UserName : "";
            this.txtPort.Text = this.m_listing.Port.ToString();
        }
    }
}

