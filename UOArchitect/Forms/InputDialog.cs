namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InputDialog : Form
    {
        private Button cmdCancel;
        private Button cmdOK;
        private Container components = null;
        private TextBox txtInput = new TextBox();

        public InputDialog()
        {
            this.InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
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

        private void InitializeComponent()
        {
			this.txtInput = new System.Windows.Forms.TextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(16, 8);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(248, 20);
			this.txtInput.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(128, 40);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(64, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(200, 40);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(64, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// InputDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 72);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.txtInput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        public string LoadForm(string Caption, string DefaultText, Form owner)
        {
            this.Text = Caption;
            if (DefaultText != null)
            {
                this.txtInput.Text = DefaultText;
            }
            base.ShowDialog(owner);
            if (base.DialogResult == DialogResult.OK)
            {
                return this.txtInput.Text.Trim();
            }
            return "";
        }
    }
}

