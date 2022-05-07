namespace UOArchitect
{
    using System;
    using System.Windows.Forms;

    public class CategoryNode : TreeNode
    {
        public CategoryNode(string name) : base(name)
        {
        }

        public void AddDesign(DesignData design)
        {
            int num = this.FindSubsection(design.Subsection.ToLower());
            SubsectionNode node = null;
            if (num != -1)
            {
                node = (SubsectionNode) base.Nodes[num];
            }
            else
            {
                node = new SubsectionNode(design.Subsection);
                base.Nodes.Add(node);
            }
            node.Nodes.Add(new DesignNode(design));
        }

        public void Delete()
        {
            base.Remove();
        }

        private int FindSubsection(string subsection)
        {
            for (int i = 0; i < base.Nodes.Count; i++)
            {
                SubsectionNode node = (SubsectionNode) base.Nodes[i];
                if (node.Text.ToLower() == subsection)
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveNode(int index)
        {
            base.Nodes.RemoveAt(index);
            if (base.Nodes.Count == 0)
            {
                this.Delete();
            }
        }

        public void Update()
        {
            foreach (SubsectionNode node in base.Nodes)
            {
                node.Update();
            }
        }
    }
}

