using PictureViewer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UOArchitect
{
	public class ToolBox : UserControl
	{
		private IContainer components;
		private HouseDesigner m_Designer;
		private TileSet m_Root = TileSet.Toolbox;
		private TileSet m_Root2 = TileSet.NewToolbox;
		private PictureBox picRoot;
		private Viewer picTileSet;
		private TreeView tree;
		private ToolTip toolTip;

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

		public ToolBox()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
		}

		public void LoadItems()
		{
			this.picRoot.Image = global::Properties.Resources.tb_back;
			this.picTileSet.Image = global::Properties.Resources.tb_back_tileset;

			if (m_Root == null || m_Root2 == null)
				return;
			for (int i = 0; i < this.m_Root.Entries.Count; i++)
			{
				PictureBox control = new PictureBox();
				control.BackColor = Color.Transparent;
				control.Tag = this.m_Root.Entries[i];
				control.Image = ((TileSet)this.m_Root.Entries[i]).Image;
				control.SetBounds(2 + ((i % 3) * 0x1f), 2 + ((i / 3) * 0x1f), 30, 30);
				control.MouseEnter += new EventHandler(this.box_MouseEnter);
				control.MouseLeave += new EventHandler(this.box_MouseLeave);
				control.Click += new EventHandler(this.box_Click);
				this.toolTip.SetToolTip(control, ((TileSet)this.m_Root.Entries[i]).Name);
				this.picRoot.Controls.Add(control);
			}
			for (int i = 0; i < this.m_Root2.Entries.Count; ++i)
			{
				TreeNode c = new TreeNode();
				c.Tag = this.m_Root2.Entries[i];
				c.Text = (((TileSet)this.m_Root2.Entries[i]).Name);
				tree.Nodes.Add(c);
			}
		}

		#region Treeview
		void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TileSet tag = (TileSet)e.Node.Tag;
			if (((tag.Entries.Count > 0) && (tag.Entries[0] is TileSetEntry)) && ((TileSetEntry)tag.Entries[0]).Destroy)
			{
				this.m_Designer.TileCursor = (TileSetEntry)tag.Entries[0];
			}
			else
			{
				if (e.Node.Parent == null)
				{
					this.SetTileSet(tag, true);
					this.SetSubNode(tag, e.Node);
				}
				else
					this.SetTileSet(tag, false);
			}
		}

		public void SetSubNode(TileSet tileSet, TreeNode currentnode)
		{
			this.picTileSet.Controls.Clear();
			if (tileSet != null)
			{
				if (tileSet.Entries[0] is TileSet)
				{
					for (int i = 0; i < tileSet.Entries.Count; i++)
					{
						TreeNode control = new TreeNode();
						control.Text = ((TileSet)tileSet.Entries[i]).Name;
						control.Tag = tileSet.Entries[i];
						currentnode.Nodes.Add(control);
					}
					foreach( TreeNode t in tree.Nodes )
					{
						if( t == currentnode )
							currentnode.Expand();
						else if (t.Nodes.Count > 0)
							t.Nodes.Clear();
					}
				}
			}

		}
		#endregion

		#region Left Box
		private void box_Click(object sender, EventArgs e)
		{
			PictureBox box = (PictureBox)sender;
			TileSet tag = (TileSet)box.Tag;
			if (((tag.Entries.Count > 0) && (tag.Entries[0] is TileSetEntry)) && ((TileSetEntry)tag.Entries[0]).Destroy)
			{
				this.m_Designer.TileCursor = (TileSetEntry)tag.Entries[0];
			}
			else
			{
				this.SetTileSet(tag, true);
			}
		}
		/// <summary>
		/// Mouse enters main commands box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void box_MouseEnter(object sender, EventArgs e)
		{
			PictureBox box = (PictureBox)sender;
			TileSet tag = (TileSet)box.Tag;
			box.Image = tag.SelectedImage;
		}
		/// <summary>
		/// Mouse leaves main commands box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void box_MouseLeave(object sender, EventArgs e)
		{
			PictureBox box = (PictureBox)sender;
			TileSet tag = (TileSet)box.Tag;
			box.Image = tag.Image;
		}
		#endregion

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


		#region Designer
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
			this.picRoot = new System.Windows.Forms.PictureBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tree = new System.Windows.Forms.TreeView();
			this.picTileSet = new PictureViewer.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.picRoot)).BeginInit();
			this.SuspendLayout();
			// 
			// picRoot
			// 
			this.picRoot.Location = new System.Drawing.Point(8, 8);
			this.picRoot.Name = "picRoot";
			this.picRoot.Size = new System.Drawing.Size(96, 96);
			this.picRoot.TabIndex = 0;
			this.picRoot.TabStop = false;
			// 
			// tree
			// 
			this.tree.Location = new System.Drawing.Point(110, 8);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(202, 144);
			this.tree.TabIndex = 2;
			this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
			// 
			// picTileSet
			// 
			this.picTileSet.AutoScroll = true;
			this.picTileSet.BackColor = System.Drawing.Color.Black;
			this.picTileSet.Image = null;
			this.picTileSet.ImageSizeMode = PictureViewer.SizeMode.Scrollable;
			this.picTileSet.Location = new System.Drawing.Point(318, 8);
			this.picTileSet.Name = "picTileSet";
			this.picTileSet.Size = new System.Drawing.Size(480, 144);
			this.picTileSet.TabIndex = 1;
			this.picTileSet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picTileSet_MouseClick);
			// 
			// ToolBox
			// 
			this.Controls.Add(this.tree);
			this.Controls.Add(this.picTileSet);
			this.Controls.Add(this.picRoot);
			this.Name = "ToolBox";
			this.Size = new System.Drawing.Size(810, 160);
			((System.ComponentModel.ISupportInitialize)(this.picRoot)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		void picTileSet_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				//TODO right click = go back
				Viewer vi = (Viewer)sender;
				TileSet tag = (TileSet)vi.Tag;
				if (tag != null)
				{
					this.SetTileSet(tag, false);
				}
			}
		}

	}
}