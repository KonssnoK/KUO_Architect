namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoginDialog : Form
    {
        private Button btnAdd;
        private Button btnCancel;
        private Button btnConnect;
        private Button btnDelete;
        private Button btnEdit;
        private CheckBox chkSavePassword;
        private Container components = null;
        private GroupBox groupBox1 = new GroupBox();
        private Label label1;
        private Label label2;
        private ListView lstServers;
        private string m_Password = "";
        private int m_Port = 0;
        private ServerListing m_selectedListing;
        private string m_ServerIP = "";
        private string m_UserName = "";
        private TextBox txtAccount;
        private TextBox txtPassword;

        public LoginDialog()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditServerListing listing = new EditServerListing();
            listing.ShowDialog(this);
            listing.Dispose();
            this.PopulateServerList();
            this.UpdateButtonState();
            this.UpdateDisplay();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.chkSavePassword.Checked)
            {
                this.m_selectedListing.Password = this.m_Password;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Config.ServerListings.Remove(this.m_selectedListing);
            this.PopulateServerList();
            this.UpdateButtonState();
            this.UpdateDisplay();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditServerListing listing = new EditServerListing();
            listing.EditListing(this.m_selectedListing, this);
            listing.Dispose();
            this.PopulateServerList();
            this.UpdateButtonState();
            this.UpdateDisplay();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.lstServers = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.txtAccount = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.chkSavePassword = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.btnEdit);
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.lstServers);
			this.groupBox1.Location = new System.Drawing.Point(9, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 176);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Server Listing";
			// 
			// btnAdd
			// 
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAdd.Location = new System.Drawing.Point(10, 144);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(62, 23);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Enabled = false;
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnEdit.Location = new System.Drawing.Point(71, 144);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(56, 23);
			this.btnEdit.TabIndex = 6;
			this.btnEdit.Text = "Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDelete.Location = new System.Drawing.Point(126, 144);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(56, 23);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// lstServers
			// 
			this.lstServers.FullRowSelect = true;
			this.lstServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstServers.Location = new System.Drawing.Point(8, 16);
			this.lstServers.Name = "lstServers";
			this.lstServers.Size = new System.Drawing.Size(176, 120);
			this.lstServers.TabIndex = 0;
			this.lstServers.UseCompatibleStateImageBehavior = false;
			this.lstServers.View = System.Windows.Forms.View.List;
			this.lstServers.SelectedIndexChanged += new System.EventHandler(this.lstServers_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 195);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Account:";
			// 
			// txtAccount
			// 
			this.txtAccount.BackColor = System.Drawing.SystemColors.Control;
			this.txtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtAccount.Enabled = false;
			this.txtAccount.Location = new System.Drawing.Point(66, 193);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(128, 20);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.TextChanged += new System.EventHandler(this.txtAccount_TextChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPassword.Enabled = false;
			this.txtPassword.Location = new System.Drawing.Point(66, 217);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(128, 20);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 217);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password:";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCancel.Location = new System.Drawing.Point(144, 246);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			// 
			// btnConnect
			// 
			this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnConnect.Enabled = false;
			this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnConnect.Location = new System.Drawing.Point(80, 246);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(56, 23);
			this.btnConnect.TabIndex = 3;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// chkSavePassword
			// 
			this.chkSavePassword.Location = new System.Drawing.Point(16, 240);
			this.chkSavePassword.Name = "chkSavePassword";
			this.chkSavePassword.Size = new System.Drawing.Size(56, 32);
			this.chkSavePassword.TabIndex = 5;
			this.chkSavePassword.Text = "Save Pwd";
			// 
			// LoginDialog
			// 
			this.AcceptButton = this.btnConnect;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(210, 280);
			this.Controls.Add(this.chkSavePassword);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtAccount);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "LoginDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login Manager";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstServers.SelectedItems.Count > 0)
            {
                this.m_selectedListing = (ServerListing) this.lstServers.SelectedItems[0].Tag;
                this.m_ServerIP = this.m_selectedListing.ServerIP;
                this.m_Port = this.m_selectedListing.Port;
                this.m_UserName = (this.m_selectedListing.UserName != null) ? this.m_selectedListing.UserName : "";
                this.m_Password = (this.m_selectedListing.Password != null) ? this.m_selectedListing.Password : "";
                this.chkSavePassword.Checked = this.m_Password.Length > 0;
            }
            else
            {
                this.m_selectedListing = null;
                this.m_ServerIP = "";
                this.m_Port = 0;
                this.m_UserName = "";
                this.m_Password = "";
            }
            this.UpdateButtonState();
            this.UpdateDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.PopulateServerList();
            this.UpdateButtonState();
            this.UpdateDisplay();
        }

        private void PopulateServerList()
        {
            this.lstServers.Items.Clear();
            this.m_selectedListing = null;
            this.m_ServerIP = "";
            this.m_Port = 0;
            this.m_UserName = "";
            this.m_Password = "";
            foreach (ServerListing listing in Config.ServerListings)
            {
                ListViewItem item = new ListViewItem(listing.ServerName);
                item.Tag = listing;
                this.lstServers.Items.Add(item);
            }
        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            this.m_UserName = this.txtAccount.Text.Trim();
            this.UpdateButtonState();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.m_Password = this.txtPassword.Text.Trim();
            this.UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            bool flag = this.m_selectedListing != null;
            this.btnDelete.Enabled = flag;
            this.btnEdit.Enabled = flag;
            this.txtPassword.Enabled = flag;
            if ((this.m_UserName.Length > 0) && (this.m_Password.Length > 0))
            {
                this.btnConnect.Enabled = true;
            }
            else
            {
                this.btnConnect.Enabled = false;
            }
        }

        private void UpdateDisplay()
        {
            this.txtAccount.Text = this.m_UserName;
            this.txtPassword.Text = this.m_Password;
        }

        public string Password
        {
            get
            {
                return this.m_Password;
            }
        }

        public int Port
        {
            get
            {
                return this.m_Port;
            }
        }

        public string ServerIP
        {
            get
            {
                return this.m_ServerIP;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
        }
    }
}

