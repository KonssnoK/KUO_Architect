namespace UOArchitect
{
    using System;
    using System.IO;
    using UOArchitectInterface;

    public class MultiTextDataAdapter : BaseDesignAdapter
    {
        public MultiTextDataAdapter() : base("(*.txt)|*.txt", "Multi Text")
        {
        }

        public override void Export(DesignData design)
        {
            string exportFileName = this.GetExportFileName(design.Name);
            if (exportFileName.Length > 0)
            {
                if (!design.IsLoaded)
                {
                    design.Load();
                }
                using (StreamWriter writer = new StreamWriter(exportFileName, false))
                {
                    writer.WriteLine("6 version");
                    writer.WriteLine("1 template id");
                    writer.WriteLine("-1 item version");
                    writer.WriteLine("{0} num components", design.Items.Count);
                    for (int i = 0; i < design.Items.Count; i++)
                    {
                        DesignItem item = design.Items[i];
                        writer.WriteLine("{0} {1} {2} {3} 1", new object[] { item.ItemID, item.X, item.Y, item.Z });
                    }
                }
            }
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
            StreamReader reader = new StreamReader(File.OpenRead(importFileName));
            char[] separator = new char[] { ' ' };
            try
            {
                while (reader.Peek() > -1)
                {
                    string text = reader.ReadLine();
                    if (this.IsMultiBlock(text))
                    {
                        string[] strArray = text.Split(separator);
                        DesignItem item = new DesignItem();
                        item.ItemID = short.Parse(strArray[0]);
                        item.X = int.Parse(strArray[1]);
                        item.Y = int.Parse(strArray[2]);
                        item.Z = int.Parse(strArray[3]);
                        items.Add(item);
                    }
                }
                data.ImportItems(items, true, false);
            }
            catch
            {
            }
            finally
            {
                reader.Close();
            }
            return data;
        }

        private bool IsMultiBlock(string Text)
        {
            bool flag = true;
            if (Text.IndexOf("version") > 0)
            {
                return false;
            }
            if (Text.IndexOf("template") > 0)
            {
                return false;
            }
            if (Text.IndexOf("components") > 0)
            {
                flag = false;
            }
            return flag;
        }
    }
}

