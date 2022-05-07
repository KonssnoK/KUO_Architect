namespace PictureViewer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Viewer : UserControl
    {
        private IContainer components;
        private PictureBox pictureBox1;
        private SizeMode sizeMode;

        public Viewer()
        {
            this.InitializeComponent();
            this.ImageSizeMode = SizeMode.RatioStretch;
        }

        private void CenterImage()
        {
            int num = (int) (((double) (base.Height - this.pictureBox1.Height)) / 2.0);
            int num2 = (int) (((double) (base.Width - this.pictureBox1.Width)) / 2.0);
            if (num < 0)
            {
                num = 0;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            this.pictureBox1.Top = num;
            this.pictureBox1.Left = num2;
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox1.Location = new System.Drawing.Point(24, 32);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(296, 208);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// Viewer
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.pictureBox1);
			this.Name = "Viewer";
			this.Size = new System.Drawing.Size(352, 272);
			this.Load += new System.EventHandler(this.Viewer_Load);
			this.Resize += new System.EventHandler(this.Viewer_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

        }

        private void RatioStretch()
        {
            float num = ((float) base.Width) / ((float) base.Height);
            float num2 = ((float) this.pictureBox1.Image.Width) / ((float) this.pictureBox1.Image.Height);
            if ((base.Width >= this.pictureBox1.Image.Width) && (base.Height >= this.pictureBox1.Image.Height))
            {
                this.pictureBox1.Width = this.pictureBox1.Image.Width;
                this.pictureBox1.Height = this.pictureBox1.Image.Height;
            }
            else if ((base.Width > this.pictureBox1.Image.Width) && (base.Height < this.pictureBox1.Image.Height))
            {
                this.pictureBox1.Height = base.Height;
                this.pictureBox1.Width = (int) (base.Height * num2);
            }
            else if ((base.Width < this.pictureBox1.Image.Width) && (base.Height > this.pictureBox1.Image.Height))
            {
                this.pictureBox1.Width = base.Width;
                this.pictureBox1.Height = (int) (((float) base.Width) / num2);
            }
            else if ((base.Width < this.pictureBox1.Image.Width) && (base.Height < this.pictureBox1.Image.Height))
            {
                if (base.Width >= base.Height)
                {
                    if ((this.pictureBox1.Image.Width >= this.pictureBox1.Image.Height) && (num2 >= num))
                    {
                        this.pictureBox1.Width = base.Width;
                        this.pictureBox1.Height = (int) (((float) base.Width) / num2);
                    }
                    else
                    {
                        this.pictureBox1.Height = base.Height;
                        this.pictureBox1.Width = (int) (base.Height * num2);
                    }
                }
                else if ((this.pictureBox1.Image.Width < this.pictureBox1.Image.Height) && (num2 < num))
                {
                    this.pictureBox1.Height = base.Height;
                    this.pictureBox1.Width = (int) (base.Height * num2);
                }
                else
                {
                    this.pictureBox1.Width = base.Width;
                    this.pictureBox1.Height = (int) (((float) base.Width) / num2);
                }
            }
            this.CenterImage();
        }

        private void Scrollable()
        {
            this.pictureBox1.Width = this.pictureBox1.Image.Width;
            this.pictureBox1.Height = this.pictureBox1.Image.Height;
            this.CenterImage();
        }

        private void SetLayout()
        {
            if (this.pictureBox1.Image != null)
            {
                if (this.sizeMode == SizeMode.RatioStretch)
                {
                    this.RatioStretch();
                }
                else
                {
                    this.AutoScroll = false;
                    this.Scrollable();
                    this.AutoScroll = true;
                }
            }
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Width = 0;
            this.pictureBox1.Height = 0;
            this.SetLayout();
        }

        private void Viewer_Resize(object sender, EventArgs e)
        {
            this.SetLayout();
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this.pictureBox1.Image;
            }
            set
            {
                this.pictureBox1.Image = value;
                this.SetLayout();
            }
        }

        public SizeMode ImageSizeMode
        {
            get
            {
                return this.sizeMode;
            }
            set
            {
                this.sizeMode = value;
                this.AutoScroll = this.sizeMode == SizeMode.Scrollable;
                this.SetLayout();
            }
        }
    }
}

