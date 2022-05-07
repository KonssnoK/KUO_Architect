namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DesignTreeView : UserControl
    {
        private int _mouseX;
        private int _mouseY;
        private DesignNode _selectedNode = null;
        private Container components = null;
        public DoubleClickDesign OnDoubleClickDesign;
        public RightClickNode OnRightClickNode;
        public DesignNodeSelected OnSelected;
        private TreeView treeView1;

        public DesignTreeView()
        {
            this.InitializeComponent();
            HouseDesignData.OnNewDesignSaved = (HouseDesignData.SaveNewDesignEvent) Delegate.Combine(HouseDesignData.OnNewDesignSaved, new HouseDesignData.SaveNewDesignEvent(this.OnNewDesign));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public TreeNode GetNodeAt(Point point)
        {
            return this.treeView1.GetNodeAt(point.X, point.Y);
        }

        public TreeNode GetNodeAt(int x, int y)
        {
            return this.treeView1.GetNodeAt(x, y);
        }

        private void InitializeComponent()
        {
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.LabelEdit = true;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(160, 160);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect_1);
			this.treeView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseMove);
			// 
			// DesignTreeView
			// 
			this.Controls.Add(this.treeView1);
			this.Name = "DesignTreeView";
			this.Size = new System.Drawing.Size(160, 160);
			this.ResumeLayout(false);

        }

        private void OnNewDesign(DesignData design)
        {
            bool flag = false;
            this.treeView1.BeginUpdate();
            foreach (CategoryNode node in this.treeView1.Nodes)
            {
                if (node.Text.ToLower() == design.Category.ToLower())
                {
                    node.AddDesign(design);
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                CategoryNode node2 = new CategoryNode(design.Category);
                node2.AddDesign(design);
                this.treeView1.Nodes.Add(node2);
            }
            this.treeView1.EndUpdate();
        }

        public void Populate(ArrayList designs)
        {
            Hashtable hashtable = new Hashtable(0, new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            for (int i = 0; i < designs.Count; i++)
            {
                DesignData design = (DesignData) designs[i];
                CategoryNode node = null;
                if (hashtable.ContainsKey(design.Category))
                {
                    node = (CategoryNode) hashtable[design.Category];
                }
                else
                {
                    node = new CategoryNode(design.Category);
                    hashtable.Add(node.Text, node);
                    this.treeView1.Nodes.Add(node);
                }
                node.AddDesign(design);
            }
            this.treeView1.EndUpdate();
            hashtable.Clear();
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                e.Node.Text = e.Label;
                if (e.Node is CategoryNode)
                {
                    (e.Node as CategoryNode).Update();
                }
                else if (e.Node is SubsectionNode)
                {
                    (e.Node as SubsectionNode).Update();
                }
                else if (e.Node is DesignNode)
                {
                    (e.Node as DesignNode).Update();
                }
            }
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DesignNode)
            {
                if (this._selectedNode != null)
                {
                    this._selectedNode.ForeColor = Color.Black;
                }
                this._selectedNode = (DesignNode) e.Node;
                e.Node.ForeColor = Color.Blue;
            }
            else
            {
                this._selectedNode = null;
            }
            if (this.OnSelected != null)
            {
                this.OnSelected(this._selectedNode);
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode nodeAt = this.treeView1.GetNodeAt(this._mouseX, this._mouseY);
            if (((nodeAt != null) && (nodeAt is DesignNode)) && (this.OnDoubleClickDesign != null))
            {
                this.OnDoubleClickDesign((DesignNode) nodeAt);
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            this._mouseX = e.X;
            this._mouseY = e.Y;
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode nodeAt = this.treeView1.GetNodeAt(this._mouseX, this._mouseY);
                if ((nodeAt != null) && (this.OnRightClickNode != null))
                {
                    this.OnRightClickNode(nodeAt);
                }
            }
        }

        public Point MouseCoords
        {
            get
            {
                Point p = this.treeView1.PointToScreen(new Point(this._mouseX, this._mouseY));
                return base.PointToClient(p);
            }
        }

        public DesignNode SelectedNode
        {
            get
            {
                return this._selectedNode;
            }
        }

        public delegate void DesignNodeSelected(DesignNode node);

        public delegate void DoubleClickDesign(DesignNode node);

        public delegate void RightClickNode(TreeNode node);
    }
}

