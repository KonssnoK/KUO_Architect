namespace UOArchitect
{
    using System;
    using System.Windows.Forms;

    public class SubsectionNode : TreeNode
    {
        public SubsectionNode(string name) : base(name)
        {
        }

        public void Delete()
        {
            (base.Parent as CategoryNode).RemoveNode(base.Index);
        }

        public void RemoveNode(int index)
        {
            base.Nodes.RemoveAt(index);
            if (base.Nodes.Count == 0)
            {
                this.Delete();
            }
            else
            {
                base.TreeView.SelectedNode = base.Nodes[0];
            }
        }

        public void Update()
        {
            foreach (DesignNode node in base.Nodes)
            {
                node.Update();
            }
        }
    }
}

