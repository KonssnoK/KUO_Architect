namespace UOArchitect.Toolbox
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;

    public class AboutTab : UserControl
    {
        private Container components = null;
		private TextBox textBox1;
        private PictureBox pictureBox1;

        public AboutTab()
        {
            this.InitializeComponent();
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(3, 15);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(186, 166);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "UO Architect v2.6.7 mod Kons\r\n\r\nThis tool supports SA client, new SA items and so o" +
				"n.\r\n\r\n"
                +"Made exclusively for \r\n\"KR Italia/Requiem\" Shards\r\n\r\n"
                +"Project site:\r\n"
                + "http://code.google.com/p/kprojects/wiki/UOArchitect";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Properties.Resources.Clipboard01;
			this.pictureBox1.Location = new System.Drawing.Point(3, 187);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(186, 170);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// AboutTab
			// 
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "AboutTab";
			this.Size = new System.Drawing.Size(192, 360);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }
    }
}

