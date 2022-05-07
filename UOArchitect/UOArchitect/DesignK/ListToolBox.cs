using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UOArchitect
{
	public partial class ListToolBox: UserControl
	{
		private TileSet m_Root2 = TileSet.NewToolbox;
		private TileSetEntry destroyer;

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
		private DispCase m_displaycase;
		public DispCase DisplayCase
		{
			get { return m_displaycase; }
		}

		public ListToolBox()
		{
			InitializeComponent();
		}

		public void LoadItems()
		{
			if (m_Root2 == null)
				return;

			for (int i = 0; i < this.m_Root2.Entries.Count; ++i)
			{
				TreeNode c = new TreeNode();
				c.Tag = this.m_Root2.Entries[i];
				c.Text = (((TileSet)this.m_Root2.Entries[i]).Name);
				tree.Nodes.Add(c);
			}
			m_displaycase = new DispCase();
			Designer.Controls.Add(m_displaycase);
			Designer.DisplayC = m_displaycase;
			m_displaycase.Designer = Designer;
			m_displaycase.MouseMove += new MouseEventHandler(Designer.display_MouseMove);
			m_displaycase.MouseDown += new MouseEventHandler(Designer.display_MouseDown);
			m_displaycase.MouseUp += new MouseEventHandler(Designer.display_MouseUp);
			m_displaycase.Show();

			destroyer = new TileSetEntry(true);
		}

		#region Treeview
		void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node == tree.SelectedNode || e.Node.Nodes.Contains(tree.SelectedNode))
				return;

			TileSet tag = (TileSet)e.Node.Tag;
			if (((tag.Entries.Count > 0) && (tag.Entries[0] is TileSetEntry)) && ((TileSetEntry)tag.Entries[0]).Destroy)
			{
				this.m_Designer.TileCursor = (TileSetEntry)tag.Entries[0];
			}
			else
			{
				if (e.Node.Parent == null)
				{
					DisplayCase.SetTileSet(tag, true);
					this.SetSubNode(tag, e.Node);
				}
				else
					DisplayCase.SetTileSet(tag, false);
			}
		}

		public void SetSubNode(TileSet tileSet, TreeNode currentnode)
		{
			DisplayCase.PicTileSet.Controls.Clear();
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
					foreach (TreeNode t in tree.Nodes)
					{
						if (t == currentnode)
							currentnode.Expand();
						else if (t.Nodes.Count > 0)
							t.Nodes.Clear();
					}
				}
			}

		}
		#endregion

		private void btn_destroy_Click(object sender, EventArgs e)
		{
			this.m_Designer.TileCursor = destroyer;
		}
	}
}
