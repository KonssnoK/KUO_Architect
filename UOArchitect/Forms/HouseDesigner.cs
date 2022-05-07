namespace UOArchitect
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Resources;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;
	using Ultima;

	public class HouseDesigner : Form
	{
		private int m_buildZ;
		private int m_CameraX;
		private int m_CameraY;
		private TileSetEntry m_Cursor;
		private static ImageAttributes m_CursorAttributes;
		private static ColorMatrix m_CursorMatrix;
		private HouseDesign m_Design;
		private bool m_DrawVirtualFloor = true;
		private int m_H = 1;
		private int m_Level;
		private Brush m_LevelBrush = new SolidBrush(Color.FromArgb(0x60, 0x20, 0xc0, 0x20));//Green
		private bool m_MouseDown = false;
		private Point m_MousePos;
		private int m_ResizeX;
		private int m_ResizeY;
		private bool m_Resizing;
		private int m_ShiftCX;
		private int m_ShiftCY;
		private Bitmap m_ShiftImage = global::Properties.Resources.scroll;
		private bool m_Shifting;
		private bool m_ShiftKeyDown = false;
		private int m_ShiftX;
		private int m_ShiftY;
		private int m_W = 1;
		private int m_X;
		private int m_Y;
		private int m_Z;
		private ToolTip toolTip;
		private MenuItem Menu_currentZ;
		private ListToolBox listToolBox1;
		//private ToolBox toolBox1;

		static HouseDesigner()
		{
			/*float[][] newColorMatrix = new float[5][];
			float[] numArray2 = new float[5];
			newColorMatrix[0] = numArray2;
			numArray2 = new float[5];
			newColorMatrix[1] = numArray2;
			numArray2 = new float[5];
			newColorMatrix[2] = numArray2;
			numArray2 = new float[5];

			numArray2[3] = 0.5f;
			newColorMatrix[3] = numArray2;
			numArray2 = new float[5];

			numArray2[4] = 1f;
			newColorMatrix[4] = numArray2;*/

			#region K
			m_CursorMatrix = new ColorMatrix(new float[][] { 
							new float[] {0, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0.5f, 0}, 
							new float[] {0, 0, 0, 0, 1} 
							});

			m_CursorMatK = new ColorMatrix(new float[][] { 
							new float[] {1, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0, 0}, 
							new float[] {0, 0, 0, 0.5f, 0}, 
							new float[] {0, 0, 0, 0, 1} 
							});
			#endregion


			//m_CursorMatrix = new ColorMatrix(newColorMatrix);
			m_CursorAttributes = null;
		}
		private static ColorMatrix m_CursorMatK;

		public HouseDesigner(HouseDesign design)
		{
			this.m_Design = design;
			this.InitializeComponent();
			this.ChangeLevel(1);
			//this.toolBox1.Designer = this;
			//this.toolBox1.LoadItems();
			this.listToolBox1.Designer = this;
			this.listToolBox1.LoadItems();
			base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.Opaque | ControlStyles.UserPaint, true);
			this.UpdateText();
		}

		private void ChangeLevel(int level)
		{
			this.m_Level = level;
			this.m_buildZ = HouseDesign.LevelZ[level];
			base.Invalidate();
			this.Menu_currentZ.Text = "Z: " + m_buildZ;
		}

		public void FixPoints(ref int xStart, ref int yStart, ref int xEnd, ref int yEnd)
		{
			int num = xStart;
			int num2 = yStart;
			int num3 = xEnd;
			int num4 = yEnd;
			if (xEnd < num)
			{
				num = xEnd;
				num3 = xStart;
			}
			if (yEnd < num2)
			{
				num2 = yEnd;
				num4 = yStart;
			}
			xStart = num;
			xEnd = num3;
			yStart = num2;
			yEnd = num4;
		}

		private Bitmap GetBitmap()
		{
			int num = 0x7fffffff;
			int num2 = 0x7fffffff;
			int num3 = -2147483648;
			int num4 = -2147483648;
			for (int i = 0; i <= this.m_Level; i++)
			{
				int index = 0;
				for (int k = 0; index < this.m_Design.Width; k += 0x16)
				{
					int num8 = k;
					int num9 = k;
					int num10 = 0;
					while (num10 < this.m_Design.Height)
					{
						ArrayList list = this.m_Design.Components[index][num10][i];
						for (int m = 0; m < list.Count; m++)
						{
							HouseComponent component = (HouseComponent)list[m];
							Bitmap bitmap = component.Image;
							int num12 = num8 - (bitmap.Width / 2);
							int num13 = (num9 - (component.Z * 4)) - bitmap.Height;
							if (num12 < num)
							{
								num = num12;
							}
							if (num13 < num2)
							{
								num2 = num13;
							}
							num12 += bitmap.Width;
							num13 += bitmap.Height;
							if (num12 > num3)
							{
								num3 = num12;
							}
							if (num13 > num4)
							{
								num4 = num13;
							}
						}
						num10++;
						num8 -= 0x16;
						num9 += 0x16;
					}
					index++;
				}
			}
			if (((num == 0x7fffffff) || (num2 == 0x7fffffff)) || ((num3 == -2147483648) || (num4 == -2147483648)))
			{
				return new Bitmap(0, 0);
			}
			Bitmap image = new Bitmap(num3 - num, num4 - num2);
			Graphics graphics = Graphics.FromImage(image);
			int num14 = -num;
			int num15 = -num2;
			for (int j = 0; j <= this.m_Level; j++)
			{
				int num17 = 0;
				for (int n = 0; num17 < this.m_Design.Width; n += 0x16)
				{
					int num19 = n + num14;
					int num20 = n + num15;
					int num21 = 0;
					while (num21 < this.m_Design.Height)
					{
						ArrayList list2 = this.m_Design.Components[num17][num21][j];
						for (int num22 = 0; num22 < list2.Count; num22++)
						{
							HouseComponent component2 = (HouseComponent)list2[num22];
							graphics.DrawImage(component2.Image, (int)(num19 - (component2.Image.Width / 2)), (int)((num20 - (component2.Z * 4)) - component2.Image.Height));
						}
						num21++;
						num19 -= 0x16;
						num20 += 0x16;
					}
					num17++;
				}
			}
			graphics.Dispose();
			return image;
		}

		public Point GetPoint(int xOffset, int yOffset, int x, int y)
		{
			return new Point(xOffset + ((x - y) * 0x16), ((yOffset + ((x + y) * 0x16)) - (this.m_buildZ * 4)) - 0x2c);
		}

		#region Designer
		private IContainer components;
		private MenuItem menuItem1;
		private MenuItem menuItem2;
		private MenuItem menuItem3;
		private MenuItem menuItem4;
		private MenuItem menuItem5;
		private MenuItem menuItem6;
		private MenuItem miBuild;
		private MenuItem miBuildQuick;
		private MenuItem miClear;
		private MenuItem miDesign;
		private MenuItem miFifth;
		private MenuItem miFile;
		private MenuItem miFirst;
		private MenuItem miFoundation;
		private MenuItem miLevel;
		private MenuItem miRenderBitmap;
		private MenuItem miRenderGif;
		private MenuItem miRenderImage;
		private MenuItem miRenderJpeg;
		private MenuItem miRenderPng;
		private MenuItem miRoof;
		private MenuItem miSave;
		private MenuItem miSecond;
		private MenuItem miSeperator;
		private MenuItem miSixth;
		private MenuItem miThird;
		private MenuItem miVirtFloor;
		private MainMenu mmMain;
		private MenuItem mnuSaveAs;
		private SaveFileDialog saveDialog;

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
			this.mmMain = new System.Windows.Forms.MainMenu(this.components);
			this.miFile = new System.Windows.Forms.MenuItem();
			this.miSave = new System.Windows.Forms.MenuItem();
			this.mnuSaveAs = new System.Windows.Forms.MenuItem();
			this.miRenderImage = new System.Windows.Forms.MenuItem();
			this.miRenderPng = new System.Windows.Forms.MenuItem();
			this.miRenderJpeg = new System.Windows.Forms.MenuItem();
			this.miRenderGif = new System.Windows.Forms.MenuItem();
			this.miRenderBitmap = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.miDesign = new System.Windows.Forms.MenuItem();
			this.miLevel = new System.Windows.Forms.MenuItem();
			this.miFoundation = new System.Windows.Forms.MenuItem();
			this.miFirst = new System.Windows.Forms.MenuItem();
			this.miSecond = new System.Windows.Forms.MenuItem();
			this.miThird = new System.Windows.Forms.MenuItem();
			this.miRoof = new System.Windows.Forms.MenuItem();
			this.miFifth = new System.Windows.Forms.MenuItem();
			this.miSixth = new System.Windows.Forms.MenuItem();
			this.miVirtFloor = new System.Windows.Forms.MenuItem();
			this.miSeperator = new System.Windows.Forms.MenuItem();
			this.miClear = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miBuild = new System.Windows.Forms.MenuItem();
			this.miBuildQuick = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.listToolBox1 = new UOArchitect.ListToolBox();
			this.Menu_currentZ = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mmMain
			// 
			this.mmMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miDesign,
            this.menuItem2,
            this.Menu_currentZ});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miSave,
            this.mnuSaveAs,
            this.miRenderImage,
            this.menuItem3});
			this.miFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.miFile.Text = "&File";
			// 
			// miSave
			// 
			this.miSave.Index = 0;
			this.miSave.Text = "&Save";
			this.miSave.Click += new System.EventHandler(this.miSave_Click);
			// 
			// mnuSaveAs
			// 
			this.mnuSaveAs.Index = 1;
			this.mnuSaveAs.Text = "Save &As...";
			this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
			// 
			// miRenderImage
			// 
			this.miRenderImage.Index = 2;
			this.miRenderImage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miRenderPng,
            this.miRenderJpeg,
            this.miRenderGif,
            this.miRenderBitmap});
			this.miRenderImage.Text = "Render &Image";
			// 
			// miRenderPng
			// 
			this.miRenderPng.Index = 0;
			this.miRenderPng.Text = "Png";
			this.miRenderPng.Click += new System.EventHandler(this.miRenderPng_Click);
			// 
			// miRenderJpeg
			// 
			this.miRenderJpeg.Index = 1;
			this.miRenderJpeg.Text = "Jpg";
			this.miRenderJpeg.Click += new System.EventHandler(this.miRenderJpeg_Click);
			// 
			// miRenderGif
			// 
			this.miRenderGif.Index = 2;
			this.miRenderGif.Text = "Gif";
			this.miRenderGif.Click += new System.EventHandler(this.miRenderGif_Click);
			// 
			// miRenderBitmap
			// 
			this.miRenderBitmap.Index = 3;
			this.miRenderBitmap.Text = "Bitmap";
			this.miRenderBitmap.Click += new System.EventHandler(this.miRenderBitmap_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "-";
			// 
			// miDesign
			// 
			this.miDesign.Index = 1;
			this.miDesign.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miLevel,
            this.miVirtFloor,
            this.miSeperator,
            this.miClear,
            this.menuItem1,
            this.miBuild,
            this.miBuildQuick});
			this.miDesign.MergeOrder = 2;
			this.miDesign.Text = "&Design";
			this.miDesign.Popup += new System.EventHandler(this.miDesign_Popup);
			// 
			// miLevel
			// 
			this.miLevel.Index = 0;
			this.miLevel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFoundation,
            this.miFirst,
            this.miSecond,
            this.miThird,
            this.miRoof,
            this.miFifth,
            this.miSixth});
			this.miLevel.Text = "&Level";
			this.miLevel.Popup += new System.EventHandler(this.miLevel_Popup);
			// 
			// miFoundation
			// 
			this.miFoundation.Index = 0;
			this.miFoundation.Text = "F&oundation: z = 0";
			this.miFoundation.Click += new System.EventHandler(this.miFoundation_Click);
			// 
			// miFirst
			// 
			this.miFirst.Index = 1;
			this.miFirst.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.miFirst.Text = "F&irst: z = 20";
			this.miFirst.Click += new System.EventHandler(this.miFirst_Click);
			// 
			// miSecond
			// 
			this.miSecond.Index = 2;
			this.miSecond.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.miSecond.Text = "&Second";
			this.miSecond.Click += new System.EventHandler(this.miSecond_Click);
			// 
			// miThird
			// 
			this.miThird.Index = 3;
			this.miThird.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.miThird.Text = "&Third";
			this.miThird.Click += new System.EventHandler(this.miThird_Click);
			// 
			// miRoof
			// 
			this.miRoof.Index = 4;
			this.miRoof.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.miRoof.Text = "&Fourth";
			this.miRoof.Click += new System.EventHandler(this.miRoof_Click);
			// 
			// miFifth
			// 
			this.miFifth.Index = 5;
			this.miFifth.Shortcut = System.Windows.Forms.Shortcut.F6;
			this.miFifth.Text = "Fifth";
			this.miFifth.Click += new System.EventHandler(this.miFifth_Click);
			// 
			// miSixth
			// 
			this.miSixth.Index = 6;
			this.miSixth.Shortcut = System.Windows.Forms.Shortcut.F7;
			this.miSixth.Text = "Sixth";
			this.miSixth.Click += new System.EventHandler(this.miSixth_Click);
			// 
			// miVirtFloor
			// 
			this.miVirtFloor.Index = 1;
			this.miVirtFloor.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.miVirtFloor.Text = "&Virtual Floor";
			this.miVirtFloor.Click += new System.EventHandler(this.miVirtFloor_Click);
			// 
			// miSeperator
			// 
			this.miSeperator.Index = 2;
			this.miSeperator.Text = "-";
			// 
			// miClear
			// 
			this.miClear.Index = 3;
			this.miClear.Text = "&Clear";
			this.miClear.Click += new System.EventHandler(this.miClear_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 4;
			this.menuItem1.Text = "-";
			// 
			// miBuild
			// 
			this.miBuild.Index = 5;
			this.miBuild.Text = "&Build";
			this.miBuild.Click += new System.EventHandler(this.miBuild_Click);
			// 
			// miBuildQuick
			// 
			this.miBuildQuick.Enabled = false;
			this.miBuildQuick.Index = 6;
			this.miBuildQuick.Text = "Build &Quick";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
			this.menuItem2.MergeOrder = 2;
			this.menuItem2.Text = "&Build";
			this.menuItem2.Visible = false;
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = false;
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Build Design";
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = false;
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "Extract Design";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "-";
			// 
			// saveDialog
			// 
			this.saveDialog.DefaultExt = "uoh";
			this.saveDialog.Filter = "House Design Files (*.uoh)|*.uoh";
			this.saveDialog.Title = "Save";
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 300;
			// 
			// listToolBox1
			// 
			this.listToolBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listToolBox1.Designer = null;
			this.listToolBox1.Location = new System.Drawing.Point(12, 132);
			this.listToolBox1.Name = "listToolBox1";
			this.listToolBox1.Size = new System.Drawing.Size(195, 421);
			this.listToolBox1.TabIndex = 1;
			this.listToolBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listToolBox1_MouseMove);
			this.listToolBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listToolBox1_MouseDown);
			this.listToolBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listToolBox1_MouseUp);
			// 
			// Menu_currentZ
			// 
			this.Menu_currentZ.Enabled = false;
			this.Menu_currentZ.Index = 3;
			this.Menu_currentZ.Text = "Z";
			// 
			// HouseDesigner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(776, 397);
			this.Controls.Add(this.listToolBox1);
			this.KeyPreview = true;
			this.Menu = this.mmMain;
			this.Name = "HouseDesigner";
			this.Text = "HouseDesigner";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion

		#region Clicks Handling
		private void miBuild_Click(object sender, EventArgs e)
		{
			ArrayList list = this.m_Design.Compress();
			for (int i = 0; i < list.Count; i++)
			{
				((BuildEntry)list[i]).Send();
			}
		}

		private void miClear_Click(object sender, EventArgs e)
		{
			this.m_Design.Clear();
			this.m_Design.BuildFoundation();
			base.Invalidate();
		}

		private void miDesign_Popup(object sender, EventArgs e)
		{
			this.miVirtFloor.Checked = this.m_DrawVirtualFloor;
		}

		private void miFifth_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 5)
			{
				this.ChangeLevel(5);
			}
		}

		private void miFirst_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 1)
			{
				this.ChangeLevel(1);
			}
		}

		private void miFoundation_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 0)
			{
				this.ChangeLevel(0);
			}
		}

		private void miLevel_Popup(object sender, EventArgs e)
		{
			this.miFoundation.Checked = this.m_Level == 0;
			this.miFirst.Checked = this.m_Level == 1;
			this.miSecond.Checked = this.m_Level == 2;
			this.miThird.Checked = this.m_Level == 3;
			this.miRoof.Checked = this.m_Level == 4;
		}

		private void miRenderBitmap_Click(object sender, EventArgs e)
		{
			this.GetBitmap().Save(this.m_Design.Name + ".bmp", ImageFormat.Bmp);
		}

		private void miRenderGif_Click(object sender, EventArgs e)
		{
			this.GetBitmap().Save(this.m_Design.Name + ".gif", ImageFormat.Gif);
		}

		private void miRenderJpeg_Click(object sender, EventArgs e)
		{
			this.GetBitmap().Save(this.m_Design.Name + ".jpg", ImageFormat.Jpeg);
		}

		private void miRenderPng_Click(object sender, EventArgs e)
		{
			this.GetBitmap().Save(this.m_Design.Name + ".png", ImageFormat.Png);
		}

		private void miRoof_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 4)
			{
				this.ChangeLevel(4);
			}
		}

		private void miSave_Click(object sender, EventArgs e)
		{
			if (this.m_Design.IsNewRecord)
			{
				DesignPropertyEditor editor = new DesignPropertyEditor();
				editor.LoadForm(ref this.m_Design, this);
				editor.Dispose();
			}
			Cursor.Current = Cursors.WaitCursor;
			this.m_Design.Save();
			this.m_Design.FileHeader.Unload();
			Cursor.Current = Cursors.Default;
		}

		private void miSecond_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 2)
			{
				this.ChangeLevel(2);
			}
		}

		private void miSixth_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 6)
			{
				this.ChangeLevel(6);
			}
		}

		private void miThird_Click(object sender, EventArgs e)
		{
			if (this.m_Level != 3)
			{
				this.ChangeLevel(3);
			}
		}

		private void miVirtFloor_Click(object sender, EventArgs e)
		{
			this.m_DrawVirtualFloor = !this.m_DrawVirtualFloor;
			base.Invalidate();
		}

		private void mnuSaveAs_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			DesignPropertyEditor editor = new DesignPropertyEditor();
			editor.LoadForm(ref this.m_Design, this);
			editor.Dispose();
			this.m_Design.Save(true);
			Cursor.Current = Cursors.Default;
		}
		#endregion

		#region HouseDesigner Handlers
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ShiftKey)
			{
				this.m_ShiftKeyDown = true;
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			this.m_ShiftKeyDown = false;
			base.OnKeyUp(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
			{
				this.m_Shifting = true;
				this.m_ShiftX = e.X;
				this.m_ShiftY = e.Y;
				this.m_ShiftCX = this.m_CameraX;
				this.m_ShiftCY = this.m_CameraY;
				base.Invalidate(new Rectangle(this.m_ShiftX - (this.m_ShiftImage.Width / 2), this.m_ShiftY - (this.m_ShiftImage.Height / 2), this.m_ShiftImage.Width, this.m_ShiftImage.Height));
				if (this.m_Cursor != null)
				{
					int num = base.ClientSize.Width / 2;
					int num2 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
					num += this.m_CameraX;
					num2 += this.m_CameraY;
					for (int i = this.m_X; i < (this.m_X + this.m_W); i++)
					{
						for (int j = this.m_Y; j < (this.m_Y + this.m_H); j++)
						{
							base.Invalidate(new Rectangle((num + ((i - j) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num2 + ((i + j) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
				}
			}
			else
			{
				int tz;
				if (this.m_Shifting)
				{
					this.m_Shifting = false;
					base.Invalidate(new Rectangle(this.m_ShiftX - (this.m_ShiftImage.Width / 2), this.m_ShiftY - (this.m_ShiftImage.Height / 2), this.m_ShiftImage.Width, this.m_ShiftImage.Height));
				}
				int x = e.X;
				int y = e.Y;
				int tx = x;
				int ty = y;
				this.Translate(ref tx, ref ty, out tz);
				this.m_Resizing = true;
				this.m_ResizeX = tx;
				this.m_ResizeY = ty;
				int resizeX = this.m_ResizeX;
				int resizeY = this.m_ResizeY;
				int xEnd = tx;
				int yEnd = ty;
				this.FixPoints(ref resizeX, ref resizeY, ref xEnd, ref yEnd);
				int num14 = base.ClientSize.Width / 2;
				int num15 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				num14 += this.m_CameraX;
				num15 += this.m_CameraY;
				int num16 = this.m_X;
				int num17 = this.m_Y;
				int w = this.m_W;
				int h = this.m_H;
				if (((resizeX != num16) || (resizeY != num17)) || ((((xEnd - resizeX) + 1) != this.m_W) || (((yEnd - resizeY) + 1) != this.m_H)))
				{
					this.m_X = resizeX;
					this.m_Y = resizeY;
					this.m_W = (xEnd - resizeX) + 1;
					this.m_H = (yEnd - resizeY) + 1;
					int num20 = resizeX;
					int num21 = resizeY;
					int num22 = xEnd;
					int num23 = yEnd;
					if (num16 < num20)
					{
						num20 = num16;
					}
					if (num17 < num21)
					{
						num21 = num17;
					}
					if (((num16 + w) - 1) > num22)
					{
						num22 = (num16 + w) - 1;
					}
					if (((num17 + h) - 1) > num23)
					{
						num23 = (num17 + h) - 1;
					}
					if (this.m_Cursor != null)
					{
						for (int k = num20; k <= num22; k++)
						{
							for (int m = num21; m <= num23; m++)
							{
								bool flag = (((k >= num16) && (k < (num16 + w))) && (m >= num17)) && (m < (num17 + h));
								bool flag2 = (((k >= this.m_X) && (k < (this.m_X + this.m_W))) && (m >= this.m_Y)) && (m < (this.m_Y + this.m_H));
								if (flag != flag2)
								{
									base.Invalidate(new Rectangle((num14 + ((k - m) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num15 + ((k + m) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
								}
							}
						}
					}
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.m_Shifting)
			{
				this.m_CameraX = (e.X - this.m_ShiftX) + this.m_ShiftCX;
				this.m_CameraY = (e.Y - this.m_ShiftY) + this.m_ShiftCY;
				base.Invalidate();
			}
			else if (this.m_Resizing)
			{
				int z;
				int x = e.X;
				int y = e.Y;
				int dx = x;
				int dy = y;
				this.Translate(ref dx, ref dy, out z);
				int resizeX = this.m_ResizeX;
				int resizeY = this.m_ResizeY;
				int xEnd = dx;
				int yEnd = dy;
				this.FixPoints(ref resizeX, ref resizeY, ref xEnd, ref yEnd);
				int centeredX = base.ClientSize.Width / 2;
				int num11 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				centeredX += this.m_CameraX;
				num11 += this.m_CameraY;
				int num12 = this.m_X;
				int num13 = this.m_Y;
				int w = this.m_W;
				int h = this.m_H;
				if (((resizeX != num12) || (resizeY != num13)) || ((((xEnd - resizeX) + 1) != this.m_W) || (((yEnd - resizeY) + 1) != this.m_H)))
				{
					this.m_X = resizeX;
					this.m_Y = resizeY;
					this.m_W = (xEnd - resizeX) + 1;
					this.m_H = (yEnd - resizeY) + 1;
					int currX = resizeX;
					int currY = resizeY;
					int num18 = xEnd;
					int num19 = yEnd;
					if (num12 < currX)
					{
						currX = num12;
					}
					if (num13 < currY)
					{
						currY = num13;
					}
					if (((num12 + w) - 1) > num18)
					{
						num18 = (num12 + w) - 1;
					}
					if (((num13 + h) - 1) > num19)
					{
						num19 = (num13 + h) - 1;
					}
					if (this.m_Cursor != null)
					{
						for (int i = currX; i <= num18; i++)
						{
							for (int j = currY; j <= num19; j++)
							{
								bool flag = (((i >= num12) && (i < (num12 + w))) && (j >= num13)) && (j < (num13 + h));
								bool flag2 = (((i >= this.m_X) && (i < (this.m_X + this.m_W))) && (j >= this.m_Y)) && (j < (this.m_Y + this.m_H));
								if (flag != flag2)
								{
									base.Invalidate(new Rectangle((centeredX + ((i - j) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num11 + ((i + j) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
								}
							}
						}
					}
				}
			}
			else
			{
				this.m_Resizing = false;
				this.m_W = 1;
				this.m_H = 1;
				int num22 = base.ClientSize.Width / 2;
				int num23 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				num22 += this.m_CameraX;
				num23 += this.m_CameraY;
				if (this.m_Cursor != null)
				{
					for (int k = this.m_X; k < (this.m_X + this.m_W); k++)
					{
						for (int m = this.m_Y; m < (this.m_Y + this.m_H); m++)
						{
							base.Invalidate(new Rectangle((num22 + ((k - m) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num23 + ((k + m) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
				}
				int num26 = this.m_X;
				int num27 = this.m_Y;
				this.SetCursorMouseXY(e.X, e.Y, false);
				if (((e.Button != MouseButtons.None) && (this.m_Cursor != null)) && ((this.m_X != num26) || (this.m_Y != num27)))
				{
					if (this.m_Cursor.Destroy || (e.Button == MouseButtons.Right))
					{
						HouseComponent component = null;
						for (int n = this.m_X; n < (this.m_X + this.m_W); n++)
						{
							for (int num29 = this.m_Y; num29 < (this.m_Y + this.m_H); num29++)
							{
								if (((n >= 0) && (n < this.m_Design.Width)) && ((num29 >= 0) && (num29 < this.m_Design.Height)))
								{
									ArrayList list = this.m_Design.Components[n][num29][this.m_Level];
									int buildZ = this.m_buildZ;
									for (int dx1 = 0; dx1 < list.Count; dx1++)
									{
										HouseComponent component2 = (HouseComponent)list[dx1];
										int dx2 = component2.Z + component2.Height;
										if (dx2 >= buildZ)
										{
											buildZ = dx2;
											component = component2;
										}
									}
									if (component != null)
									{
										base.Invalidate(new Rectangle((num22 + ((n - num29) * 0x16)) - (component.Image.Width / 2), ((num23 + ((n + num29) * 0x16)) - component.Image.Height) - (component.Z * 4), component.Image.Width, component.Image.Height));
										list.Remove(component);
									}
								}
							}
						}
						this.SetCursorMouseXY(e.X, e.Y, true);
					}
					else
					{
						for (int dx3 = this.m_X; dx3 < (this.m_X + this.m_W); dx3++)
						{
							for (int dx4 = this.m_Y; dx4 < (this.m_Y + this.m_H); dx4++)
							{
								this.m_Design.AddComponent(dx3, dx4, this.m_Z, this.m_Level, this.m_Cursor.GetRandomIndex(), this.m_Cursor.BaseIndex, this.m_Cursor.Count);
								base.Invalidate(new Rectangle((num22 + ((dx3 - dx4) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num23 + ((dx3 + dx4) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
							}
						}
					}
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.m_Shifting)
			{
				this.m_Shifting = false;
				this.m_CameraX = (e.X - this.m_ShiftX) + this.m_ShiftCX;
				this.m_CameraY = (e.Y - this.m_ShiftY) + this.m_ShiftCY;
				base.Invalidate();
			}
			else if (this.m_Resizing)
			{
				int num5;
				int x = e.X;
				int y = e.Y;
				int num3 = x;
				int num4 = y;
				this.Translate(ref num3, ref num4, out num5);
				int resizeX = this.m_ResizeX;
				int resizeY = this.m_ResizeY;
				int xEnd = num3;
				int yEnd = num4;
				this.FixPoints(ref resizeX, ref resizeY, ref xEnd, ref yEnd);
				int num10 = base.ClientSize.Width / 2;
				int num11 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				num10 += this.m_CameraX;
				num11 += this.m_CameraY;
				if (this.m_Cursor != null)
				{
					for (int i = this.m_X; i < (this.m_X + this.m_W); i++)
					{
						for (int j = this.m_Y; j < (this.m_Y + this.m_H); j++)
						{
							base.Invalidate(new Rectangle((num10 + ((i - j) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num11 + ((i + j) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
				}
				this.m_X = resizeX;
				this.m_Y = resizeY;
				this.m_W = (xEnd - resizeX) + 1;
				this.m_H = (yEnd - resizeY) + 1;
				if (this.m_Cursor != null)
				{
					for (int k = this.m_X; k < (this.m_X + this.m_W); k++)
					{
						for (int m = this.m_Y; m < (this.m_Y + this.m_H); m++)
						{
							base.Invalidate(new Rectangle((num10 + ((k - m) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num11 + ((k + m) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
					#region Destroy
					if (this.m_Cursor.Destroy || (e.Button == MouseButtons.Right))
					{
						Clipboard.SetDataObject(string.Format(", new Rectangle2D( {0}, {1}, {2}, {3} )", new object[] { this.m_X - this.m_Design.xc, this.m_Y - this.m_Design.yc, this.m_W, this.m_H }));
						HouseComponent component = null;
						for (int n = this.m_X; n < (this.m_X + this.m_W); n++)
						{
							for (int num17 = this.m_Y; num17 < (this.m_Y + this.m_H); num17++)
							{
								if (((n >= 0) && (n < this.m_Design.Width)) && ((num17 >= 0) && (num17 < this.m_Design.Height)))
								{
									ArrayList list = this.m_Design.Components[n][num17][this.m_Level];
									int buildZ = this.m_buildZ;
									for (int num19 = 0; num19 < list.Count; num19++)
									{
										HouseComponent component2 = (HouseComponent)list[num19];
										int totalZ = component2.Z + component2.Height;
										if (totalZ >= buildZ)
										{
											buildZ = totalZ;
											component = component2;
										}
									}
									if (component != null)
									{
										base.Invalidate(new Rectangle((num10 + ((n - num17) * 0x16)) - (component.Image.Width / 2), ((num11 + ((n + num17) * 0x16)) - component.Image.Height) - (component.Z * 4), component.Image.Width, component.Image.Height));
										list.Remove(component);
									}
								}
							}
						}
					}
					#endregion
					#region Add Component
					else
					{
						Clipboard.SetDataObject(string.Format(", new Rectangle2D( {0}, {1}, {2}, {3} )", new object[] { this.m_X - this.m_Design.xc, this.m_Y - this.m_Design.yc, this.m_W, this.m_H }));
						for (int cx = this.m_X; cx < (this.m_X + this.m_W); cx++)
						{
							for (int cy = this.m_Y; cy < (this.m_Y + this.m_H); cy++)
							{
								#region K
								bool levelc = false;
								if (this.m_Z >= ( HouseDesign.LevelZ[this.m_Level] + 20))
								{
									ChangeLevel(this.m_Level + 1);
									levelc = true;
								}
								#endregion

								this.m_Design.AddComponent(cx, cy, this.m_Z, this.m_Level, this.m_Cursor.GetRandomIndex(), this.m_Cursor.BaseIndex, this.m_Cursor.Count);
								base.Invalidate(new Rectangle((num10 + ((cx - cy) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num11 + ((cx + cy) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
								if (levelc)
									ChangeLevel(this.m_Level - 1);
							}
						}
					}
					#endregion
				}
				this.m_Resizing = false;
				this.m_W = 1;
				this.m_H = 1;
				if (this.m_Cursor != null)
				{
					for (int num23 = this.m_X; num23 < (this.m_X + this.m_W); num23++)
					{
						for (int num24 = this.m_Y; num24 < (this.m_Y + this.m_H); num24++)
						{
							base.Invalidate(new Rectangle((num10 + ((num23 - num24) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num11 + ((num23 + num24) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
				}
			}
			else
			{
				int num25 = base.ClientSize.Width / 2;
				int num26 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				num25 += this.m_CameraX;
				num26 += this.m_CameraY;
				this.m_Resizing = false;
				this.m_W = 1;
				this.m_H = 1;
				if (this.m_Cursor != null)
				{
					for (int num27 = this.m_X; num27 < (this.m_X + this.m_W); num27++)
					{
						for (int num28 = this.m_Y; num28 < (this.m_Y + this.m_H); num28++)
						{
							base.Invalidate(new Rectangle((num25 + ((num27 - num28) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num26 + ((num27 + num28) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
						}
					}
				}
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.m_ShiftKeyDown)
			{
				if (e.Delta > 0)
				{
					if ((this.m_Level + 1) != 7)
					{
						this.ChangeLevel(this.m_Level + 1);
					}
				}
				else if ((e.Delta < 0) && (this.m_Level != 0))
				{
					this.ChangeLevel(this.m_Level - 1);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (m_CursorAttributes == null)
			{
				m_CursorAttributes = new ImageAttributes();
				m_CursorAttributes.SetColorMatrix(m_CursorMatrix);
			}
			Graphics graphics = e.Graphics;
			graphics.Clear(SystemColors.ControlDark);
			int xOffset = base.ClientSize.Width / 2;
			int yOffset = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
			xOffset += this.m_CameraX;
			yOffset += this.m_CameraY;
			for (int i = 0; i <= this.m_Level; i++)
			{
				if ((i == this.m_Level) && this.m_DrawVirtualFloor)
				{
					Point[] points = new Point[] { this.GetPoint(xOffset, yOffset, 1, 1), this.GetPoint(xOffset, yOffset, this.m_Design.UserWidth, 1), this.GetPoint(xOffset, yOffset, this.m_Design.UserWidth, this.m_Design.UserHeight), this.GetPoint(xOffset, yOffset, 1, this.m_Design.UserHeight) };
					graphics.FillPolygon(this.m_LevelBrush, points);
				}
				int index = 0;
				for (int j = 0; index < this.m_Design.Width; j += 0x16)
				{
					int num6 = j + xOffset;
					int num7 = j + yOffset;
					int num8 = 0;
					while (num8 < this.m_Design.Height)
					{
						ArrayList list = this.m_Design.Components[index][num8][i];
						for (int k = 0; k < list.Count; k++)
						{
							HouseComponent component = (HouseComponent)list[k];
							graphics.DrawImage(component.Image, (int)(num6 - (component.Image.Width / 2)), (int)((num7 - (component.Z * 4)) - component.Image.Height));
						}
						if (((!this.m_Shifting && (this.m_Cursor != null)) && ((index >= this.m_X) && (num8 >= this.m_Y))) && (((index < (this.m_X + this.m_W)) && (num8 < (this.m_Y + this.m_H))) && (this.m_Level == i)))
						{
							graphics.DrawImage(this.m_Cursor.Image, new Rectangle((xOffset + ((index - num8) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((yOffset + ((index + num8) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height), 0, 0, this.m_Cursor.Image.Width, this.m_Cursor.Image.Height, GraphicsUnit.Pixel, m_CursorAttributes);
						}
						num8++;
						num6 -= 0x16;
						num7 += 0x16;
					}
					index++;
				}
			}
			if (this.m_Shifting)
			{
				graphics.DrawImage(this.m_ShiftImage, this.m_ShiftX - (this.m_ShiftImage.Width / 2), this.m_ShiftY - (this.m_ShiftImage.Height / 2), this.m_ShiftImage.Width, this.m_ShiftImage.Height);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}
		#endregion


		#region Handlers ListToolBox
		void listToolBox1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.m_MouseDown = false;
				Cursor.Current = Cursors.Default;
			}
		}

		void listToolBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Cursor.Current = Cursors.Hand;
				this.m_MouseDown = true;
				this.m_MousePos = new Point(e.X, e.Y);
			}
		}

		void listToolBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.m_MouseDown)
			{
				int x;
				int y;
				Rectangle clientRectangle;
				Point location = this.listToolBox1.Location;
				Point point = new Point(location.X + (e.X - this.m_MousePos.X), location.Y + (e.Y - this.m_MousePos.Y));
				if (point.X < 0)
				{
					x = 0;
				}
				else
				{
					clientRectangle = base.ClientRectangle;
					if ((point.X + this.listToolBox1.Width) > clientRectangle.Width)
					{
						x = base.ClientRectangle.Width - this.listToolBox1.Width;
					}
					x = point.X;
				}
				if (point.Y < 0)
				{
					y = 0;
				}
				else
				{
					clientRectangle = base.ClientRectangle;
					if ((point.Y + this.listToolBox1.Height) > clientRectangle.Height)
					{
						y = base.ClientRectangle.Height - this.listToolBox1.Height;
					}
					y = point.Y;
				}
				this.listToolBox1.Location = new Point(x, y);
			}
		}
		#endregion

		#region Handlers DispCase
		public void display_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.m_MouseDown = false;
				Cursor.Current = Cursors.Default;
			}
		}

		public void display_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Cursor.Current = Cursors.Hand;
				this.m_MouseDown = true;
				this.m_MousePos = new Point(e.X, e.Y);
			}
		}

		private DispCase m_dispcase;
		public DispCase DisplayC
		{
			get { return m_dispcase; }
			set { m_dispcase = value; }
		}
		public void display_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.m_MouseDown)
			{
				int x;
				int y;
				Rectangle clientRectangle;
				Point location = this.DisplayC.Location;
				Point point = new Point(location.X + (e.X - this.m_MousePos.X), location.Y + (e.Y - this.m_MousePos.Y));
				if (point.X < 0)
				{
					x = 0;
				}
				else
				{
					clientRectangle = base.ClientRectangle;
					if ((point.X + this.DisplayC.Width) > clientRectangle.Width)
					{
						x = base.ClientRectangle.Width - this.DisplayC.Width;
					}
					x = point.X;
				}
				if (point.Y < 0)
				{
					y = 0;
				}
				else
				{
					clientRectangle = base.ClientRectangle;
					if ((point.Y + this.DisplayC.Height) > clientRectangle.Height)
					{
						y = base.ClientRectangle.Height - this.DisplayC.Height;
					}
					y = point.Y;
				}
				this.DisplayC.Location = new Point(x, y);
			}
		}
		#endregion

		#region Misc
		private void LowerBuildZ()
		{
			if (this.m_buildZ > HouseDesign.LevelZ[this.m_Level])
			{
				this.m_buildZ--;
				base.Invalidate();
			}
		}

		private void RaiseBuildZ()
		{
			if (this.m_buildZ < (HouseDesign.LevelZ[this.m_Level] + 0x13))
			{
				this.m_buildZ++;
				base.Invalidate();
			}
		}

		public void SetCursorMouseXY(int x, int y, bool ignoreSame)
		{
			int z;
			this.Translate(ref x, ref y, out z);
			this.SetCursorXY(x, y, z, ignoreSame);
		}

		public void SetCursorXY(int x, int y, int z, bool ignoreSame)
		{
			if (((ignoreSame || (this.m_X != x)) || ((this.m_Y != y) || (this.m_Z != z))) && (this.m_Cursor != null))
			{
				int num = base.ClientSize.Width / 2;
				int num2 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
				num += this.m_CameraX;
				num2 += this.m_CameraY;
				for (int i = this.m_X; i < (this.m_X + this.m_W); i++)
				{
					for (int k = this.m_Y; k < (this.m_Y + this.m_H); k++)
					{
						base.Invalidate(new Rectangle((num + ((i - k) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num2 + ((i + k) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
					}
				}
				this.m_X = x;
				this.m_Y = y;
				this.m_Z = z;
				for (int j = this.m_X; j < (this.m_X + this.m_W); j++)
				{
					for (int m = this.m_Y; m < (this.m_Y + this.m_H); m++)
					{
						base.Invalidate(new Rectangle((num + ((j - m) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num2 + ((j + m) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
					}
				}
			}
		}

		public void Translate(ref int x, ref int y, out int z)
		{
			int num = base.ClientSize.Width / 2;
			int num2 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
			num += this.m_CameraX;
			num2 += this.m_CameraY;
			x -= num;
			y -= num2;
			y += HouseDesign.LevelZ[this.m_Level] * 4;
			y += 0x2c;
			int num3 = x + y;
			int num4 = y - x;
			if (num3 < 0)
			{
				num3 -= 0x2c;
			}
			if (num4 < 0)
			{
				num4 -= 0x2c;
			}
			num3 /= 0x2c;
			num4 /= 0x2c;
			x = num3;
			y = num4;
			z = HouseDesign.LevelZ[this.m_Level];
			if (((x >= 0) && (x < this.m_Design.Width)) && ((y >= 0) && (y < this.m_Design.Height)))
			{
				ArrayList list = this.m_Design.Components[x][y][this.m_Level];
				for (int i = 0; i < list.Count; i++)
				{
					HouseComponent component = (HouseComponent)list[i];
					int totalZ = component.Z + component.Height;
					if (totalZ > z)
					{
						z = totalZ;
					}
				}
				#region K
				if (((this.m_Cursor != null) && !this.m_Cursor.Destroy) && (z >= (HouseDesign.LevelZ[this.m_Level] + 20)))
				{
					m_CursorAttributes.SetColorMatrix(m_CursorMatK);
					//z -= 20;// z for preview
					toolTip.SetToolTip(this, "Next Level");
				}
				else
				{
					m_CursorAttributes.SetColorMatrix(m_CursorMatrix);
					toolTip.RemoveAll();
				}
				#endregion
			}
		}

		public void UpdateText()
		{
			this.Text = string.Format("{0} ({1}x{2})", this.m_Design.Name, this.m_Design.UserWidth, this.m_Design.UserHeight);
		}

		public TileSetEntry TileCursor
		{
			get
			{
				return this.m_Cursor;
			}
			set
			{
				if (this.m_Cursor != value)
				{
					int num = base.ClientSize.Width / 2;
					int num2 = (base.ClientSize.Height - ((this.m_Design.Height + this.m_Design.Width) * 0x16)) + 0x16;
					num += this.m_CameraX;
					num2 += this.m_CameraY;
					if ((((this.m_Cursor == null) || (value == null)) || ((this.m_Cursor.Image.Width != value.Image.Width) || (this.m_Cursor.Image.Height != value.Image.Height))) && (this.m_Cursor != null))
					{
						for (int i = this.m_X; i < (this.m_X + this.m_W); i++)
						{
							for (int j = this.m_Y; j < (this.m_Y + this.m_H); j++)
							{
								base.Invalidate(new Rectangle((num + ((i - j) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num2 + ((i + j) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
							}
						}
					}
					this.m_Cursor = value;
					if (this.m_Cursor != null)
					{
						for (int k = this.m_X; k < (this.m_X + this.m_W); k++)
						{
							for (int m = this.m_Y; m < (this.m_Y + this.m_H); m++)
							{
								base.Invalidate(new Rectangle((num + ((k - m) * 0x16)) - (this.m_Cursor.Image.Width / 2), ((num2 + ((k + m) * 0x16)) - this.m_Cursor.Image.Height) - (this.m_Z * 4), this.m_Cursor.Image.Width, this.m_Cursor.Image.Height));
							}
						}
					}
				}
			}
		}
		#endregion
	}
}