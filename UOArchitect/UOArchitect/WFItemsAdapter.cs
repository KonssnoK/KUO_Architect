namespace UOArchitect
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using UOArchitectInterface;

    public class WFItemsAdapter : BaseDesignAdapter
    {
        private static int[] m_LevelZ = new int[] { 0, 20, 40, 60, 80, 100 };

        public WFItemsAdapter() : base("(*.xml)|*.xml", "World Forge Export")
        {
        }

        private int CalculateMinZ(DesignItemCol items)
        {
            int z = 0x1869f;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Z < z)
                {
                    z = items[i].Z;
                }
            }
            return z;
        }

        public override void Export(DesignData design)
        {
            string path = Utility.GetSaveFileName("(*.mlt)|*.mlt", "Multi Data File", "");
            if (path.Length > 0)
            {
                if (!design.IsLoaded)
                {
                    design.Load();
                }
                int num = this.CalculateMinZ(design.Items);
                BinaryFileWriter writer = new BinaryFileWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None));
                for (int i = 0; i < design.Items.Count; i++)
                {
                    DesignItem item = design.Items[i];
                    writer.WriteShort(item.ItemID);
                    writer.WriteShort((short) item.X);
                    writer.WriteShort((short) item.Y);
                    writer.WriteShort((short) (item.Z - num));
                    writer.WriteShort(1);
                    writer.WriteShort(item.Hue);
                }
                writer.Close();
            }
        }

        private int GetZLevel(int z)
        {
            if (z < m_LevelZ[1])
            {
                return 0;
            }
            if (z < m_LevelZ[2])
            {
                return 1;
            }
            if (z < m_LevelZ[3])
            {
                return 2;
            }
            if (z < m_LevelZ[4])
            {
                return 3;
            }
            if (z < m_LevelZ[5])
            {
                return 4;
            }
            return 5;
        }

        public override DesignData ImportDesign()
        {
            string importFileName = this.GetImportFileName();
            if (!File.Exists(importFileName))
            {
                return null;
            }
            DesignData data = new DesignData();
            DesignItemCol items = new DesignItemCol();
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(importFileName);
                XmlNodeList list = document.SelectNodes("//export/tile");
                for (int i = 0; i < list.Count; i++)
                {
                    XmlNode node = list[i];
                    string s = null;
                    int itemID = 0;
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    int hue = 0;
                    s = node.Attributes.GetNamedItem("id").Value;
                    if ((s != null) && (s.Length > 0))
                    {
                        itemID = int.Parse(s);
                    }
                    s = node.Attributes.GetNamedItem("x").Value;
                    if ((s != null) && (s.Length > 0))
                    {
                        x = int.Parse(s);
                    }
                    s = node.Attributes.GetNamedItem("y").Value;
                    if ((s != null) && (s.Length > 0))
                    {
                        y = int.Parse(s);
                    }
                    s = node.Attributes.GetNamedItem("z").Value;
                    if ((s != null) && (s.Length > 0))
                    {
                        z = int.Parse(s);
                    }
                    s = node.Attributes.GetNamedItem("hue").Value;
                    if ((s != null) && (s.Length > 0))
                    {
                        hue = int.Parse(s);
                    }
                    items.Add(new DesignItem(itemID, x, y, z, this.GetZLevel(z), hue));
                }
                data.ImportItems(items, true, false);
            }
            catch (Exception exception)
            {
                data = null;
                MessageBox.Show("The import failed due to the following error.\n" + exception.Message);
            }
            return data;
        }
    }
}

