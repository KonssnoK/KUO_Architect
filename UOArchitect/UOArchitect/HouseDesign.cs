namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using UOArchitectInterface;

    public class HouseDesign
    {
        private const short DESIGN_VERSION = 1;
        public const int Levels = 7;
        private string m_Category;
        private ArrayList[][][] m_Components;
        private DesignData m_FileHeader;
        private int m_Height;
        private static int[] m_LevelZ = new int[] { 0, 20, 40, 60, 80, 100, 120 };
        private string m_Name;
        private string m_SubSection;
        private int m_UserHeight;
        private int m_UserWidth;
        private int m_Width;
        public int xc;
        public int yc;

        public HouseDesign()
        {
            this.m_Name = "New Design";
            this.m_Category = "Unassigned";
            this.m_SubSection = "Unassigned";
            this.Clear();
        }

        public HouseDesign(DesignData fileHeader)
        {
            this.m_FileHeader = fileHeader;
            this.m_Name = fileHeader.Name;
            this.m_Category = fileHeader.Category;
            this.m_SubSection = fileHeader.Subsection;
            this.m_Width = fileHeader.Width;
            this.m_Height = fileHeader.Height;
            this.m_UserWidth = fileHeader.UserWidth;
            this.m_UserHeight = fileHeader.UserHeight;
            this.m_Components = null;
            this.Clear();
            if (fileHeader != null)
            {
                if (!this.m_FileHeader.IsLoaded)
                {
                    this.m_FileHeader.Load();
                }
                for (int i = 0; i < fileHeader.Items.Count; i++)
                {
                    DesignItem item = fileHeader.Items[i];
                    HouseComponent component = new HouseComponent(item.ItemID, item.Z);
                    component.Hue = item.Hue;
                    this.m_Components[item.X][item.Y][this.GetZLevel(item.Z)].Add(component);
                }
                this.Sort();
                this.m_FileHeader.Unload();
            }
        }

        public HouseDesign(string name, int width, int height)
        {
            this.m_Name = name;
            this.m_Category = "Unassigned";
            this.m_SubSection = "Unassigned";
            this.m_UserWidth = width;
            this.m_UserHeight = height;
            this.m_Width = width;
            this.m_Height = height + 1;
            this.Clear();
            this.BuildFoundation();
        }

        public void AddComponent(int x, int y, int level, int index)
        {
            this.AddComponent(x, y, LevelZ[level], level, index);
        }

        public void AddComponent(int x, int y, int z, int level, int index)
        {
            this.AddComponent(x, y, z, level, index, index, 1, 0);
        }

        public void AddComponent(int x, int y, int level, int index, HouseComponent hc)
        {
            this.AddComponent(x, y, LevelZ[level], level, index, hc);
        }

        public void AddComponent(int x, int y, int z, int level, int index, HouseComponent hc)
        {
            this.AddComponent(x, y, z, level, index, index, 1, hc.Hue);
        }

        public void AddComponent(int x, int y, int z, int level, int index, int baseIndex, int count)
        {
            this.AddComponent(x, y, z, level, index, baseIndex, count, 0);
        }

        public void AddComponent(int x, int y, int z, int level, int index, int baseIndex, int count, int hue)
        {
            if ((((x >= 0) && (x < this.m_Width)) && ((y >= 0) && (y < this.m_Height))) && ((level >= 0) && (level < 7)))
            {
                ArrayList list = this.m_Components[x][y][level];
                HouseComponent component = new HouseComponent(index, z, baseIndex, count);
                component.Hue = hue;
                for (int i = 0; i < list.Count; i++)
                {
                    HouseComponent component2 = (HouseComponent) list[i];
                    if (((component2.Z == z) && (component2.Height == 0)) && (component.Height == 0))
                    {
                        list[i] = component;
                        list.Sort();
                        return;
                    }
                    if (((component2.Z == z) && (component2.Height != 0)) && (component.Height != 0))
                    {
                        list[i] = component;
                        list.Sort();
                        return;
                    }
                }
                list.Add(component);
                list.Sort();
            }
        }

        public void BuildFoundation()
        {
            this.AddComponent(0, 0, 0, 0x66);
            this.AddComponent(this.m_UserWidth - 1, this.m_UserHeight - 1, 0, 0x65);
            for (int i = 1; i < this.m_UserWidth; i++)
            {
                this.AddComponent(i, this.m_UserHeight, 0, 0x751);
                this.AddComponent(i, 0, 0, 0x63);
                if (i < (this.m_UserWidth - 1))
                {
                    this.AddComponent(i, this.m_UserHeight - 1, 0, 0x63);
                }
            }
            for (int j = 1; j < this.m_UserHeight; j++)
            {
                this.AddComponent(0, j, 0, 100);
                if (j < (this.m_UserHeight - 1))
                {
                    this.AddComponent(this.m_UserWidth - 1, j, 0, 100);
                }
            }
            TileSetEntry entry = new TileSetEntry(0x31f4, 4);
            for (int k = 1; k < this.m_UserWidth; k++)
            {
                for (int m = 1; m < this.m_UserHeight; m++)
				{
					#region K
					this.AddComponent(k, m, 7, 0, entry.GetRandomIndex(), entry.BaseIndex, entry.Count, 0);
					#endregion
				}
            }
        }

        public void Clear()
        {
            bool flag = this.m_Components == null;
            if (flag)
            {
                this.m_Components = new ArrayList[this.m_Width][][];
            }
            for (int i = 0; i < this.m_Width; i++)
            {
                if (flag)
                {
                    this.m_Components[i] = new ArrayList[this.m_Height][];
                }
                for (int j = 0; j < this.m_Height; j++)
                {
                    if (flag)
                    {
                        this.m_Components[i][j] = new ArrayList[7];
                    }
                    for (int k = 0; k < 7; k++)
                    {
                        if (flag)
                        {
                            this.m_Components[i][j][k] = new ArrayList();
                        }
                        else
                        {
                            this.m_Components[i][j][k].Clear();
                        }
                    }
                }
            }
        }

        public ArrayList Compress()
        {
            bool flag;
            ArrayList list = new ArrayList();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < this.m_Width; j++)
                {
                    for (int k = 0; k < this.m_Height; k++)
                    {
                        ArrayList list2 = this.m_Components[j][k][i];
                        for (int m = 0; m < list2.Count; m++)
                        {
                            HouseComponent component = (HouseComponent) list2[m];
                            list.Add(new BuildEntry(j, k, component.Z, component.BaseIndex, component.Count, i));
                        }
                    }
                }
            }
            do
            {
                flag = false;
                for (int n = 0; n < list.Count; n++)
                {
                    BuildEntry entry = (BuildEntry) list[n];
                    bool flag2 = false;
                    for (int num6 = 0; !flag2 && (num6 < list.Count); num6++)
                    {
                        BuildEntry e = (BuildEntry) list[num6];
                        if ((n != num6) && entry.CombineWith(e))
                        {
                            list.RemoveAt(n);
                            n--;
                            flag = flag2 = true;
                        }
                    }
                }
            }
            while (flag);
            return list;
        }

        private void ExportDesignItems(ref DesignData design)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < design.Width; j++)
                {
                    for (int k = 0; k < design.Height; k++)
                    {
                        ArrayList list = this.m_Components[j][k][i];
                        foreach (HouseComponent component in list)
                        {
                            design.Items.Add(new DesignItem(component.Index, j, k, component.Z, i, component.Hue));
                        }
                    }
                }
            }
        }

        public void ExportRunUOScript()
        {
            StringBuilder builder;
            using (StreamReader reader = new StreamReader("Internal/scriptbase.txt"))
            {
                builder = new StringBuilder(reader.ReadToEnd());
            }
            bool flag = true;
            bool flag2 = false;
            StringBuilder builder2 = new StringBuilder();
            ArrayList list = this.Compress();
            for (int i = 0; i < list.Count; i++)
            {
                BuildEntry entry = (BuildEntry) list[i];
                if ((entry.m_Width == 1) && (entry.m_Height == 1))
                {
                    flag2 = false;
                    if (entry.m_Count == 1)
                    {
                        builder2.AppendFormat("\r\n\t\t\tAddComponent( new AddonComponent( 0x{0:X4} ), {1}, {2}, {3} );", new object[] { entry.m_Index, entry.m_X, entry.m_Y, entry.m_Z });
                    }
                    else
                    {
                        builder2.AppendFormat("\r\n\t\t\tAddComponent( new AddonComponent( Utility.Random( 0x{0:X4}, {4} ) ), {1}, {2}, {3} );", new object[] { entry.m_Index, entry.m_X, entry.m_Y, entry.m_Z, entry.m_Count });
                    }
                }
                else
                {
                    string str;
                    string str2;
                    if (!flag && !flag2)
                    {
                        builder2.AppendFormat("\r\n", new object[0]);
                    }
                    bool flag3 = entry.m_Width != 1;
                    bool flag4 = entry.m_Height != 1;
                    bool flag5 = entry.m_Count != 1;
                    bool flag6 = entry.m_X != 0;
                    bool flag7 = entry.m_Y != 0;
                    if (flag3)
                    {
                        builder2.AppendFormat("\r\n\t\t\tfor ( int x = 0; x < {0}; ++x )", entry.m_Width);
                    }
                    if (flag4)
                    {
                        if (flag3)
                        {
                            builder2.AppendFormat("\r\n\t\t\t\tfor ( int y = 0; y < {0}; ++y )", entry.m_Height);
                        }
                        else
                        {
                            builder2.AppendFormat("\r\n\t\t\tfor ( int y = 0; y < {0}; ++y )", entry.m_Height);
                        }
                    }
                    if (!flag3)
                    {
                        str = entry.m_X.ToString();
                    }
                    else if (!flag6)
                    {
                        str = "x";
                    }
                    else
                    {
                        str = string.Format("{0} + x", entry.m_X);
                    }
                    if (!flag4)
                    {
                        str2 = entry.m_Y.ToString();
                    }
                    else if (!flag7)
                    {
                        str2 = "y";
                    }
                    else
                    {
                        str2 = string.Format("{0} + y", entry.m_Y);
                    }
                    if (flag5)
                    {
                        if (flag3 && flag4)
                        {
                            builder2.AppendFormat("\r\n\t\t\t\t\tAddComponent( new AddonComponent( Utility.Random( 0x{0:X4}, {4} ) ), {1}, {2}, {3} );", new object[] { entry.m_Index, str, str2, entry.m_Z, entry.m_Count });
                        }
                        else
                        {
                            builder2.AppendFormat("\r\n\t\t\t\tAddComponent( new AddonComponent( Utility.Random( 0x{0:X4}, {4} ) ), {1}, {2}, {3} );", new object[] { entry.m_Index, str, str2, entry.m_Z, entry.m_Count });
                        }
                    }
                    else if (flag3 && flag4)
                    {
                        builder2.AppendFormat("\r\n\t\t\t\t\tAddComponent( new AddonComponent( 0x{0:X4} ), {1}, {2}, {3} );", new object[] { entry.m_Index, str, str2, entry.m_Z });
                    }
                    else
                    {
                        builder2.AppendFormat("\r\n\t\t\t\tAddComponent( new AddonComponent( 0x{0:X4} ), {1}, {2}, {3} );", new object[] { entry.m_Index, str, str2, entry.m_Z });
                    }
                    builder2.AppendFormat("\r\n", new object[0]);
                    flag2 = true;
                }
                flag = false;
            }
            builder.Replace("~NAME~", this.Name);
            builder.Replace("~CLASSNAME~", this.Safe(this.Name));
            builder.Replace("~COMPONENTS~", builder2.ToString());
            using (StreamWriter writer = new StreamWriter(string.Format("{0}.cs", this.Safe(this.Name))))
            {
                writer.Write(builder.ToString());
            }
        }

        public bool GetBaseCount(TileSet root, int index, out int baseIndex, out int count)
        {
            for (int i = 0; i < root.Entries.Count; i++)
            {
                object obj2 = root.Entries[i];
                if (obj2 is TileSet)
                {
                    if (this.GetBaseCount((TileSet) obj2, index, out baseIndex, out count))
                    {
                        return true;
                    }
                }
                else if (obj2 is TileSetEntry)
                {
                    TileSetEntry entry = (TileSetEntry) obj2;
                    if (((index >= entry.BaseIndex) && (index < (entry.BaseIndex + entry.Count))) && (Array.IndexOf(entry.Tiles, index) >= 0))
                    {
                        baseIndex = entry.BaseIndex;
                        count = entry.Count;
                        return true;
                    }
                }
            }
            baseIndex = index;
            count = 1;
            return false;
        }

        public Bitmap GetPreviewImage(int level)
        {
            int num = 0x7fffffff;
            int num2 = 0x7fffffff;
            int num3 = -2147483648;
            int num4 = -2147483648;
            for (int i = 0; i <= level; i++)
            {
                int index = 0;
                for (int k = 0; index < this.m_Width; k += 0x16)
                {
                    int num8 = k;
                    int num9 = k;
                    int num10 = 0;
                    while (num10 < this.m_Height)
                    {
                        ArrayList list = this.m_Components[index][num10][i];
                        for (int m = 0; m < list.Count; m++)
                        {
                            HouseComponent component = (HouseComponent) list[m];
                            Bitmap bitmap = component.Image;
                            int num12 = num8 - (bitmap.Width / 2);
                            int num13 = (num9 - (component.Z * 4)) - bitmap.Height;
                            if (num12 < num)
                            {
                                num = num12;
                            }
                            if (num13 < num2)
                            {
                                num2 = num13;
                            }
                            num12 += bitmap.Width;
                            num13 += bitmap.Height;
                            if (num12 > num3)
                            {
                                num3 = num12;
                            }
                            if (num13 > num4)
                            {
                                num4 = num13;
                            }
                        }
                        num10++;
                        num8 -= 0x16;
                        num9 += 0x16;
                    }
                    index++;
                }
            }
            if (((num == 0x7fffffff) || (num2 == 0x7fffffff)) || ((num3 == -2147483648) || (num4 == -2147483648)))
            {
                return new Bitmap(0, 0);
            }
            Bitmap image = new Bitmap(num3 - num, num4 - num2);
            Graphics graphics = Graphics.FromImage(image);
            int num14 = -num;
            int num15 = -num2;
            for (int j = 0; j <= level; j++)
            {
                int num17 = 0;
                for (int n = 0; num17 < this.m_Width; n += 0x16)
                {
                    int num19 = n + num14;
                    int num20 = n + num15;
                    int num21 = 0;
                    while (num21 < this.m_Height)
                    {
                        ArrayList list2 = this.m_Components[num17][num21][j];
                        for (int num22 = 0; num22 < list2.Count; num22++)
                        {
                            HouseComponent component2 = (HouseComponent) list2[num22];
                            graphics.DrawImage(component2.Image, (int) (num19 - (component2.Image.Width / 2)), (int) ((num20 - (component2.Z * 4)) - component2.Image.Height));
                        }
                        num21++;
                        num19 -= 0x16;
                        num20 += 0x16;
                    }
                    num17++;
                }
            }
            graphics.Dispose();
            return image;
        }

        private int GetZLevel(int z)
        {
            if (z < LevelZ[1])
            {
                return 0;
            }
            if (z < LevelZ[2])
            {
                return 1;
            }
            if (z < LevelZ[3])
            {
                return 2;
            }
            if (z < LevelZ[4])
            {
                return 3;
            }
            if (z < LevelZ[5])
            {
                return 4;
            }
            if (z < LevelZ[6])
            {
                return 5;
            }
            return 6;
        }

        private static int ReadEnc7(BinaryReader bin)
        {
            byte num3;
            int num = 0;
            int num2 = 0;
            do
            {
                num3 = bin.ReadByte();
                num |= (num3 & 0x7f) << num2;
                num2 += 7;
            }
            while (num3 >= 0x80);
            return num;
        }

        public string Safe(string ip)
        {
            ip = ip.Trim();
            if (ip.Length == 0)
            {
                return "NoName";
            }
            for (int i = 0; i < ip.Length; i++)
            {
                if (!char.IsLetterOrDigit(ip, i))
                {
                    ip = ip.Replace(ip[i].ToString(), "");
                }
            }
            if (ip.Length == 0)
            {
                return "NoName";
            }
            if (!char.IsLetter(ip, 0))
            {
                ip = "_" + ip;
            }
            return ip;
        }

        public void Save()
        {
            this.Save(this.m_FileHeader == null);
        }

        public void Save(bool New)
        {
            if ((this.m_FileHeader == null) || New)
            {
                this.m_FileHeader = new DesignData(this.m_Name, this.m_Category, this.m_SubSection);
                this.m_FileHeader.Width = this.m_Width;
                this.m_FileHeader.Height = this.m_Height;
                this.m_FileHeader.UserWidth = this.m_UserWidth;
                this.m_FileHeader.UserHeight = this.m_UserHeight;
                this.ExportDesignItems(ref this.m_FileHeader);
                HouseDesignData.SaveNewDesign(this.m_FileHeader);
            }
            else
            {
                this.m_FileHeader.Items.Clear();
                this.m_FileHeader.Width = this.m_Width;
                this.m_FileHeader.Height = this.m_Height;
                this.m_FileHeader.UserWidth = this.m_UserWidth;
                this.m_FileHeader.UserHeight = this.m_UserHeight;
                this.ExportDesignItems(ref this.m_FileHeader);
                HouseDesignData.UpdateDesign(this.m_FileHeader);
            }
        }

        public void Sort()
        {
            for (int i = 0; i < this.m_Width; i++)
            {
                for (int j = 0; j < this.m_Height; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        if (this.m_Components[i][j][k].Count > 1)
                        {
                            this.m_Components[i][j][k].Sort();
                        }
                    }
                }
            }
        }

        private static void WriteEnc7(BinaryWriter bin, int num)
        {
            uint num2 = (uint) num;
            while (num2 >= 0x80)
            {
                bin.Write((byte) (num2 | 0x80));
                num2 = num2 >> 7;
            }
            bin.Write((byte) num2);
        }

        public string Category
        {
            get
            {
                return this.m_Category;
            }
            set
            {
                this.m_Category = value;
            }
        }

        public ArrayList[][][] Components
        {
            get
            {
                return this.m_Components;
            }
        }

        public DesignData FileHeader
        {
            get
            {
                return this.m_FileHeader;
            }
        }

        public int Height
        {
            get
            {
                return this.m_Height;
            }
        }

        public bool IsNewRecord
        {
            get
            {
                if (this.m_FileHeader != null)
                {
                    return false;
                }
                return true;
            }
        }

        public static int[] LevelZ
        {
            get
            {
                return m_LevelZ;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public string SubSection
        {
            get
            {
                return this.m_SubSection;
            }
            set
            {
                this.m_SubSection = value;
            }
        }

        public bool UnsavedProperties
        {
            get
            {
                return ((this.m_FileHeader == null) || ((this.m_Name != this.m_FileHeader.Name) || ((this.m_Category != this.m_FileHeader.Category) || (this.m_SubSection != this.m_FileHeader.Subsection))));
            }
        }

        public int UserHeight
        {
            get
            {
                return this.m_UserHeight;
            }
        }

        public int UserWidth
        {
            get
            {
                return this.m_UserWidth;
            }
        }

        public int Width
        {
            get
            {
                return this.m_Width;
            }
        }

        private class Plane
        {
            public int m_Level;
            public ArrayList m_List;
            public int m_Z;

            public Plane(int z, int level)
            {
                this.m_Z = z;
                this.m_Level = level;
                this.m_List = new ArrayList();
            }
        }
    }
}

