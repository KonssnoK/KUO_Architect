namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.IO;
    using UOArchitectInterface;

    public class MultiCacheDataImporter
    {
        private const string _filter = "(*.dat)|*.dat";
        private long _itemCount = 0L;
        private const string _title = "UO MultiCache.dat";
        private const byte EXPORT_VERSION = 2;

        protected string GetImportFileName()
        {
            return Utility.BrowseForFile("(*.dat)|*.dat", "UO MultiCache.dat");
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
            string importFileName = this.GetImportFileName();
            if ((importFileName != null) && File.Exists(importFileName))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(File.Open(importFileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        string str2;
                        char[] separator = new char[] { '\t' };
                        DesignData data = null;
                        DesignItemCol items = new DesignItemCol();
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Length != 0)
                            {
                                string[] strArray = str2.Split(separator);
                                if (strArray.Length > 5)
                                {
                                    if ((data != null) && (items.Count > 0))
                                    {
                                        data.ImportItems(items, true, true);
                                        list.Add(data);
                                        data = null;
                                        items.Clear();
                                    }
                                    data = new DesignData("Multi " + list.Count + 1, "multicache", "misc");
                                }
                                else if ((strArray.Length == 5) && (data != null))
                                {
                                    DesignItem item = new DesignItem();
                                    item.ItemID = Convert.ToInt16(strArray[0]);
                                    item.X = Convert.ToInt32(strArray[2]);
                                    item.Y = Convert.ToInt32(strArray[3]);
                                    item.Z = Convert.ToInt32(strArray[4]);
                                    if (item.ItemID != 1)
                                    {
                                        items.Add(item);
                                    }
                                }
                            }
                        }
                        reader.Close();
                        if ((data != null) && (items.Count > 0))
                        {
                            data.ImportItems(items, true, true);
                            list.Add(data);
                        }
                        return list;
                    }
                }
                catch
                {
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

