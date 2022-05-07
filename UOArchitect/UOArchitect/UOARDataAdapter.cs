namespace UOArchitect
{
    using System;
    using System.IO;
    using UOArchitectInterface;

    public class UOARDataAdapter : BaseDesignAdapter
    {
        private const byte EXPORT_VERSION = 1;

        public UOARDataAdapter() : base("(*.uoa)|*.uoa", "UO Architect Design")
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
                BinaryFileWriter writer = new BinaryFileWriter(File.Open(exportFileName, FileMode.Create, FileAccess.Write, FileShare.None));
                writer.WriteShort(1);
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
                writer.Close();
            }
        }

        public override DesignData ImportDesign()
        {
            string importFileName = this.GetImportFileName();
            if (!File.Exists(importFileName))
            {
                return null;
            }
            BinaryFileReader reader = new BinaryFileReader(File.Open(importFileName, FileMode.Open, FileAccess.Read, FileShare.Read));
            short num = reader.ReadShort();
            DesignData data = new DesignData();
            if (num == 1)
            {
                data.Name = reader.ReadString();
                data.Category = reader.ReadString();
                data.Subsection = reader.ReadString();
                data.Width = reader.ReadInt();
                data.Height = reader.ReadInt();
                data.UserWidth = reader.ReadInt();
                data.UserHeight = reader.ReadInt();
                int num2 = reader.ReadInt();
                for (int i = 0; i < num2; i++)
                {
                    short itemID = reader.ReadShort();
                    short x = reader.ReadShort();
                    short y = reader.ReadShort();
                    short z = reader.ReadShort();
                    short level = reader.ReadShort();
                    short hue = reader.ReadShort();
                    data.Items.Add(new DesignItem(itemID, x, y, z, level, hue));
                }
            }
            reader.Close();
            return data;
        }
    }
}

