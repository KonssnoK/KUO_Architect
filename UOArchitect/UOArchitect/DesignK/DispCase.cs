using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PictureViewer;

namespace UOArchitect
{
	public partial class DispCase: UserControl
	{
		public DispCase()
		{
			InitializeComponent();
		}
		private HouseDesigner m_Designer;
		public HouseDesigner Designer
		{
			get
			{
				return this.m_Designer;
			}
			set
			{
				this.m_Designer = value;
			}
		}
		public Viewer PicTileSet
		{
			get { return picTileSet; }
		}

		#region Right Box
		/// <summary>
		/// Final tile choosen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tileEntry_Click(object sender, EventArgs e)
		{
			PictureBox box = (PictureBox)sender;
			TileSetEntry tag = (TileSetEntry)box.Tag;
			this.m_Designer.TileCursor = tag;
		}
		/// <summary>
		/// Called when image on right box is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tileSet_Click(object sender, EventArgs e)
		{
			PictureBox box = (PictureBox)sender;
			TileSet tag = (TileSet)box.Tag;
			if (((tag.Entries.Count > 0) && (tag.Entries[0] is TileSetEntry)) && ((TileSetEntry)tag.Entries[0]).Destroy)
			{
				this.m_Designer.TileCursor = (TileSetEntry)tag.Entries[0];
			}
			else
			{
				this.SetTileSet(tag, false);
			}
		}

		public void SetTileSet(TileSet tileSet, bool palette)
		{
			this.picTileSet.Controls.Clear();
			if (tileSet != null)
			{
				int x = 10;
				for (int i = 0; i < tileSet.Entries.Count; i++)
				{
					PictureBox control = new PictureBox();
					Bitmap bitmap = (tileSet.Entries[i] is TileSet) ? ((TileSet)tileSet.Entries[i]).Image : ((TileSetEntry)tileSet.Entries[i]).Image;
					control.BackColor = Color.Transparent;
					control.Image = bitmap;
					if (palette)
					{
						control.SetBounds(10 + ((i % 8) * 0x36), 10 + ((i / 8) * 0x4a), 0x2c, (tileSet.Entries.Count > 8) ? 0x40 : bitmap.Height);
					}
					else
					{
						control.SetBounds(x, 10, bitmap.Width, bitmap.Height);
					}

					control.Tag = tileSet.Entries[i];
					if (tileSet.Entries[i] is TileSet)
					{
						control.Click += new EventHandler(this.tileSet_Click);
					}
					else
					{
						control.Click += new EventHandler(this.tileEntry_Click);
					}

					if (tileSet.Entries[i] is TileSet)
					{
						this.toolTip.SetToolTip(control, ((TileSet)tileSet.Entries[i]).Name);
					}
					this.picTileSet.Controls.Add(control);
					x += bitmap.Width;
					x += 10;
				}
			}
		}
		#endregion
	}
}
