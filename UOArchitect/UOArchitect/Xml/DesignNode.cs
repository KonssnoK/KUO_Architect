namespace UOArchitect
{
    using System;
    using System.Windows.Forms;

    public class DesignNode : TreeNode
    {
        private DesignData _design;

        public DesignNode(DesignData design)
        {
            base.Text = design.Name;
            this._design = design;
            this._design.OnSaved = (DesignData.SavedEvent) Delegate.Combine(this._design.OnSaved, new DesignData.SavedEvent(this.OnSaved));
        }

        public void Delete()
        {
            HouseDesignData.DeleteDesign(this._design);
            (base.Parent as SubsectionNode).RemoveNode(base.Index);
        }

        private int FindCategoryNode(string category)
        {
            for (int i = 0; i < base.TreeView.Nodes.Count; i++)
            {
                if (base.TreeView.Nodes[i].Text.ToLower() == category)
                {
                    return i;
                }
            }
            return -1;
        }

        private void OnSaved()
        {
            string str = base.Parent.Parent.Text.ToLower();
            string str2 = base.Parent.Text.ToLower();
            string str3 = this._design.Category.ToLower();
            string str4 = this._design.Subsection.ToLower();
            if (str == str3)
            {
                if (str2 == str4)
                {
                    base.Text = this._design.Name;
                }
                else
                {
                    CategoryNode parent = (CategoryNode) base.Parent.Parent;
                    (base.Parent as SubsectionNode).RemoveNode(base.Index);
                    parent.AddDesign(this._design);
                }
            }
            else
            {
                int num = this.FindCategoryNode(this._design.Category.ToLower());
                CategoryNode node = null;
                if (num != -1)
                {
                    node = (CategoryNode) base.TreeView.Nodes[num];
                }
                else
                {
                    node = new CategoryNode(this._design.Category);
                    base.TreeView.Nodes.Add(node);
                }
                node.AddDesign(this._design);
                (base.Parent as SubsectionNode).RemoveNode(base.Index);
            }
        }

        public void Update()
        {
            this._design.Category = base.Parent.Parent.Text;
            this._design.Subsection = base.Parent.Text;
            this._design.Name = base.Text;
            this._design.Save();
        }

        public DesignData Design
        {
            get
            {
                return this._design;
            }
        }
    }
}

