namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Resources;
    using System.Windows.Forms;

    public class DesignPreview : Form
	{
		private IContainer components;
        private HouseDesign m_Design = null;
        private FloorView m_Level = FloorView.Roof;
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem mnuExit;
        private MenuItem mnuFile;
        private MenuItem mnuFirst;
        private MenuItem mnuFloorView;
        private MenuItem mnuFoundation;
        private MenuItem mnuRoof;
        private MenuItem mnuSaveBmp;
        private MenuItem mnuSaveGif;
        private MenuItem mnuSaveJpeg;
        private MenuItem mnuSavePicture;
        private MenuItem mnuSavePng;
        private MenuItem mnuSecond;
        private MenuItem mnuThird;
        private PictureBox picPreview;

        public DesignPreview()
        {
            this.InitializeComponent();
        }

        private void DisplayDesign()
        {
            this.Cursor = Cursors.WaitCursor;
            this.picPreview.Image = this.m_Design.GetPreviewImage((int) this.m_Level);
            this.Cursor = Cursors.Default;
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
			this.components = new System.ComponentModel.Container();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuSavePicture = new System.Windows.Forms.MenuItem();
			this.mnuSaveBmp = new System.Windows.Forms.MenuItem();
			this.mnuSaveGif = new System.Windows.Forms.MenuItem();
			this.mnuSaveJpeg = new System.Windows.Forms.MenuItem();
			this.mnuSavePng = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.mnuFloorView = new System.Windows.Forms.MenuItem();
			this.mnuFoundation = new System.Windows.Forms.MenuItem();
			this.mnuFirst = new System.Windows.Forms.MenuItem();
			this.mnuSecond = new System.Windows.Forms.MenuItem();
			this.mnuThird = new System.Windows.Forms.MenuItem();
			this.mnuRoof = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(0, 6);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(512, 344);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picPreview.TabIndex = 1;
			this.picPreview.TabStop = false;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuFloorView});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.mnuSavePicture,
            this.menuItem2,
            this.mnuExit});
			this.mnuFile.Text = "File";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "-";
			// 
			// mnuSavePicture
			// 
			this.mnuSavePicture.Index = 1;
			this.mnuSavePicture.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSaveBmp,
            this.mnuSaveGif,
            this.mnuSaveJpeg,
            this.mnuSavePng});
			this.mnuSavePicture.Text = "Save Picture";
			// 
			// mnuSaveBmp
			// 
			this.mnuSaveBmp.Index = 0;
			this.mnuSaveBmp.Text = "Bitmap (.bmp)";
			this.mnuSaveBmp.Click += new System.EventHandler(this.mnuSaveBmp_Click);
			// 
			// mnuSaveGif
			// 
			this.mnuSaveGif.Index = 1;
			this.mnuSaveGif.Text = "Gif (.gif)";
			this.mnuSaveGif.Click += new System.EventHandler(this.mnuSaveGif_Click);
			// 
			// mnuSaveJpeg
			// 
			this.mnuSaveJpeg.Index = 2;
			this.mnuSaveJpeg.Text = "Jpeg (.jpeg)";
			this.mnuSaveJpeg.Click += new System.EventHandler(this.mnuSaveJpeg_Click);
			// 
			// mnuSavePng
			// 
			this.mnuSavePng.Index = 3;
			this.mnuSavePng.Text = "Png (.png)";
			this.mnuSavePng.Click += new System.EventHandler(this.mnuSavePng_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 3;
			this.mnuExit.Text = "Exit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuFloorView
			// 
			this.mnuFloorView.Index = 1;
			this.mnuFloorView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFoundation,
            this.mnuFirst,
            this.mnuSecond,
            this.mnuThird,
            this.mnuRoof});
			this.mnuFloorView.Text = "Floor View";
			// 
			// mnuFoundation
			// 
			this.mnuFoundation.Index = 0;
			this.mnuFoundation.Text = "Foundation";
			this.mnuFoundation.Click += new System.EventHandler(this.mnuFoundationl_Click);
			// 
			// mnuFirst
			// 
			this.mnuFirst.Index = 1;
			this.mnuFirst.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.mnuFirst.Text = "First";
			this.mnuFirst.Click += new System.EventHandler(this.mnuFirst_Click);
			// 
			// mnuSecond
			// 
			this.mnuSecond.Index = 2;
			this.mnuSecond.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.mnuSecond.Text = "Second";
			this.mnuSecond.Click += new System.EventHandler(this.mnuSecond_Click);
			// 
			// mnuThird
			// 
			this.mnuThird.Index = 3;
			this.mnuThird.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.mnuThird.Text = "Third";
			this.mnuThird.Click += new System.EventHandler(this.mnuThird_Click);
			// 
			// mnuRoof
			// 
			this.mnuRoof.Index = 4;
			this.mnuRoof.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.mnuRoof.Text = "Roof";
			this.mnuRoof.Click += new System.EventHandler(this.mnuRoof_Click);
			// 
			// DesignPreview
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(592, 425);
			this.Controls.Add(this.picPreview);
			this.Menu = this.mainMenu1;
			this.Name = "DesignPreview";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Preview";
			this.Closed += new System.EventHandler(this.TemplatePreview_Closed);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        public void LoadForm(HouseDesign design)
        {
            this.m_Design = design;
            this.Text = string.Format("Design Preview - {0} ({1} items)", design.Name, design.FileHeader.RecordCount);
            this.m_Level = FloorView.Roof;
            base.WindowState = FormWindowState.Maximized;
            this.DisplayDesign();
            base.Show();
            Cursor.Current = Cursors.Default;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnuFirst_Click(object sender, EventArgs e)
        {
            this.m_Level = FloorView.First;
            this.DisplayDesign();
        }

        private void mnuFoundationl_Click(object sender, EventArgs e)
        {
            this.m_Level = FloorView.Foundation;
            this.DisplayDesign();
        }

        private void mnuRoof_Click(object sender, EventArgs e)
        {
            this.m_Level = FloorView.Roof;
            this.DisplayDesign();
        }

        private void mnuSaveBmp_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Utility.SaveImageToDisk(this.picPreview.Image, ImageFormat.Bmp, this);
            this.Cursor = Cursors.Default;
        }

        private void mnuSaveGif_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Utility.SaveImageToDisk(this.picPreview.Image, ImageFormat.Gif, this);
            this.Cursor = Cursors.Default;
        }

        private void mnuSaveJpeg_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Utility.SaveImageToDisk(this.picPreview.Image, ImageFormat.Jpeg, this);
            this.Cursor = Cursors.Default;
        }

        private void mnuSavePng_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Utility.SaveImageToDisk(this.picPreview.Image, ImageFormat.Png, this);
            this.Cursor = Cursors.Default;
        }

        private void mnuSecond_Click(object sender, EventArgs e)
        {
            this.m_Level = FloorView.Second;
            this.DisplayDesign();
        }

        private void mnuThird_Click(object sender, EventArgs e)
        {
            this.m_Level = FloorView.Third;
            this.DisplayDesign();
        }

        private void TemplatePreview_Closed(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private enum FloorView
        {
            Foundation,
            First,
            Second,
            Third,
            Roof
        }
    }
}

