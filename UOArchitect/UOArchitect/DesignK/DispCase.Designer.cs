namespace UOArchitect
{
	partial class DispCase
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.picTileSet = new PictureViewer.Viewer();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// picTileSet
			// 
			this.picTileSet.AutoScroll = true;
			this.picTileSet.BackColor = System.Drawing.Color.Black;
			this.picTileSet.Image = null;
			this.picTileSet.ImageSizeMode = PictureViewer.SizeMode.Scrollable;
			this.picTileSet.Location = new System.Drawing.Point(36, 3);
			this.picTileSet.Name = "picTileSet";
			this.picTileSet.Size = new System.Drawing.Size(499, 144);
			this.picTileSet.TabIndex = 2;
			// 
			// DispCase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.picTileSet);
			this.Name = "DispCase";
			this.Size = new System.Drawing.Size(538, 151);
			this.ResumeLayout(false);

		}

		#endregion

		private PictureViewer.Viewer picTileSet;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
