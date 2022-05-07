namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Windows.Forms;
    using UOArchitectInterface;

    public class UOARBatchDataAdapter
    {
        private const string _filter = "(*.uoa)|*.uoa";
        private long _itemCount = 0L;
        private const string _title = "UO Architect Design";
        private const byte EXPORT_VERSION = 2;

        public void ExportDesign(DesignData design, BinaryFileWriter writer)
        {
            writer.WriteString(design.Name);
            writer.WriteString(design.Category);
            writer.WriteString(design.Subsection);
            writer.WriteInt(design.Width);
            writer.WriteInt(design.Height);
            writer.WriteInt(design.UserWidth);
            writer.WriteInt(design.UserHeight);
            writer.WriteInt(design.Items.Count);
            for (int i = 0; i < design.Items.Count; i++)
            {
                DesignItem item = design.Items[i];
                writer.WriteShort(item.ItemID);
                writer.WriteShort((short) item.X);
                writer.WriteShort((short) item.Y);
                writer.WriteShort((short) item.Z);
                writer.WriteShort(item.Level);
                writer.WriteShort(item.Hue);
            }
        }

        public void ExportDesigns(ArrayList designs)
        {
            this.ExportDesigns(designs, "");
        }

        public void ExportDesigns(ArrayList designs, string defaultFile)
        {
            if (designs.Count != 0)
            {
                string exportFileName = this.GetExportFileName(defaultFile);
                if (exportFileName.Length > 0)
                {
                    BinaryFileWriter writer = new BinaryFileWriter(File.Open(exportFileName, FileMode.Create, FileAccess.Write, FileShare.None));
                    writer.WriteShort(2);
                    writer.WriteShort((short) designs.Count);
                    for (int i = 0; i < designs.Count; i++)
                    {
                        DesignData design = (DesignData) designs[i];
                        if (!design.IsLoaded)
                        {
                            design.Load();
                        }
                        this.ExportDesign(design, writer);
                        design.Unload();
                    }
                    writer.Close();
                    MessageBox.Show(string.Format("{0} designs were exported.", designs.Count));
                }
            }
        }

        protected string GetExportFileName()
        {
            return this.GetExportFileName("");
        }

        protected string GetExportFileName(string defaultName)
        {
            return Utility.GetSaveFileName("(*.uoa)|*.uoa", "UO Architect Design", defaultName);
        }

        protected string[] GetImportFileNames()
        {
            return Utility.BrowseForFiles("(*.uoa)|*.uoa", "UO Architect Design");
        }

        private DesignData ImportDesign(BinaryFileReader reader, short version)
        {
            DesignData data = new DesignData();
            switch (version)
            {
                case 1:
                case 2:
                {
                    data.Name = reader.ReadString();
                    data.Category = reader.ReadString();
                    data.Subsection = reader.ReadString();
                    data.Width = reader.ReadInt();
                    data.Height = reader.ReadInt();
                    data.UserWidth = reader.ReadInt();
                    data.UserHeight = reader.ReadInt();
                    int num = reader.ReadInt();
                    for (int i = 0; i < num; i++)
                    {
                        short itemID = reader.ReadShort();
                        short x = reader.ReadShort();
                        short y = reader.ReadShort();
                        short z = reader.ReadShort();
                        short level = reader.ReadShort();
                        short hue = reader.ReadShort();
                        data.Items.Add(new DesignItem(itemID, x, y, z, level, hue));
                        this._itemCount += 1L;
                    }
                    return data;
                }
            }
            return data;
        }

        public ArrayList ImportDesigns()
        {
            ArrayList list = new ArrayList();
            string[] importFileNames = this.GetImportFileNames();
            if (importFileNames != null)
            {
                foreach (string str in importFileNames)
                {
                    int num2;
                    int num3;
                    if (!File.Exists(str))
                    {
                        goto Label_009A;
                    }
                    BinaryFileReader reader = new BinaryFileReader(File.Open(str, FileMode.Open, FileAccess.Read, FileShare.Read));
                    short version = reader.ReadShort();
                    switch (version)
                    {
                        case 1:
                            list.Add(this.ImportDesign(reader, version));
                            goto Label_0094;

                        case 2:
                            num2 = reader.ReadShort();
                            num3 = 0;
                            goto Label_008E;

                        default:
                            goto Label_0094;
                    }
                Label_0078:
                    list.Add(this.ImportDesign(reader, version));
                    num3++;
                Label_008E:
                    if (num3 < num2)
                    {
                        goto Label_0078;
                    }
                Label_0094:
                    reader.Close();
                Label_009A:;
                }
            }
            return list;
        }

        public long Count
        {
            get
            {
                return this._itemCount;
            }
        }
    }
}

