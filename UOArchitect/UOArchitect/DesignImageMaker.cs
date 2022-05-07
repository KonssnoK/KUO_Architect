namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using Ultima;

    public sealed class DesignImageMaker
    {
        public static readonly DesignImageMaker Empty = new DesignImageMaker();
        private Point m_Center;
        private int m_Height;
        private Point m_Max;
        private Point m_Min;
        private Tile[][][] m_Tiles;
        private int m_Width;

        private DesignImageMaker()
        {
            this.m_Tiles = new Tile[0][][];
        }

        public DesignImageMaker(BinaryReader reader, int count)
        {
            this.m_Min = this.m_Max = Point.Empty;
            MultiTileEntry2[] entryArray = new MultiTileEntry2[count];
            for (int i = 0; i < count; i++)
            {
                entryArray[i].m_ItemID = reader.ReadInt16();
                entryArray[i].m_OffsetX = reader.ReadInt16();
                entryArray[i].m_OffsetY = reader.ReadInt16();
                entryArray[i].m_OffsetZ = reader.ReadInt16();
                entryArray[i].m_Flags = reader.ReadInt32();
                MultiTileEntry2 entry = entryArray[i];
                if (entry.m_OffsetX < this.m_Min.X)
                {
                    this.m_Min.X = entry.m_OffsetX;
                }
                if (entry.m_OffsetY < this.m_Min.Y)
                {
                    this.m_Min.Y = entry.m_OffsetY;
                }
                if (entry.m_OffsetX > this.m_Max.X)
                {
                    this.m_Max.X = entry.m_OffsetX;
                }
                if (entry.m_OffsetY > this.m_Max.Y)
                {
                    this.m_Max.Y = entry.m_OffsetY;
                }
            }
            this.m_Center = new Point(-this.m_Min.X, -this.m_Min.Y);
            this.m_Width = (this.m_Max.X - this.m_Min.X) + 1;
            this.m_Height = (this.m_Max.Y - this.m_Min.Y) + 1;
            TileList[][] listArray = new TileList[this.m_Width][];
            this.m_Tiles = new Tile[this.m_Width][][];
            for (int j = 0; j < this.m_Width; j++)
            {
                listArray[j] = new TileList[this.m_Height];
                this.m_Tiles[j] = new Tile[this.m_Height][];
                for (int n = 0; n < this.m_Height; n++)
                {
                    listArray[j][n] = new TileList();
                }
            }
            for (int k = 0; k < entryArray.Length; k++)
            {
                int index = entryArray[k].m_OffsetX + this.m_Center.X;
                int num6 = entryArray[k].m_OffsetY + this.m_Center.Y;
                listArray[index][num6].Add((short) ((entryArray[k].m_ItemID & 0x3fff) + 0x4000), (sbyte) entryArray[k].m_OffsetZ);
            }
            for (int m = 0; m < this.m_Width; m++)
            {
                for (int num8 = 0; num8 < this.m_Height; num8++)
                {
                    this.m_Tiles[m][num8] = listArray[m][num8].ToArray();
                    if (this.m_Tiles[m][num8].Length > 1)
                    {
                        Array.Sort(this.m_Tiles[m][num8]);
                    }
                }
            }
        }

        public DesignImageMaker(HouseDesign design, int MaxZ)
        {
            int recordCount = design.FileHeader.RecordCount;
            this.m_Min = this.m_Max = Point.Empty;
            int index = 0;
            MultiTileEntry2[] entryArray = new MultiTileEntry2[recordCount];
            for (int i = 0; i < 7; i++)
            {
                for (int n = 0; n < design.Width; n++)
                {
                    for (int num5 = 0; num5 < design.Height; num5++)
                    {
                        ArrayList list = design.Components[n][num5][i];
                        foreach (HouseComponent component in list)
                        {
                            if ((MaxZ <= 0) || (component.Z < MaxZ))
                            {
                                entryArray[index].m_ItemID = (short) component.Index;
                                entryArray[index].m_OffsetX = (short) n;
                                entryArray[index].m_OffsetY = (short) num5;
                                entryArray[index].m_OffsetZ = (short) component.Z;
                                entryArray[index].m_Flags = 0;
                                MultiTileEntry2 entry = entryArray[index];
                                if (entry.m_OffsetX < this.m_Min.X)
                                {
                                    this.m_Min.X = entry.m_OffsetX;
                                }
                                if (entry.m_OffsetY < this.m_Min.Y)
                                {
                                    this.m_Min.Y = entry.m_OffsetY;
                                }
                                if (entry.m_OffsetX > this.m_Max.X)
                                {
                                    this.m_Max.X = entry.m_OffsetX;
                                }
                                if (entry.m_OffsetY > this.m_Max.Y)
                                {
                                    this.m_Max.Y = entry.m_OffsetY;
                                }
                                index++;
                            }
                        }
                    }
                }
            }
            this.m_Center = new Point(-this.m_Min.X, -this.m_Min.Y);
            this.m_Width = (this.m_Max.X - this.m_Min.X) + 1;
            this.m_Height = (this.m_Max.Y - this.m_Min.Y) + 1;
            TileList[][] listArray = new TileList[this.m_Width][];
            this.m_Tiles = new Tile[this.m_Width][][];
            for (int j = 0; j < this.m_Width; j++)
            {
                listArray[j] = new TileList[this.m_Height];
                this.m_Tiles[j] = new Tile[this.m_Height][];
                for (int num7 = 0; num7 < this.m_Height; num7++)
                {
                    listArray[j][num7] = new TileList();
                }
            }
            for (int k = 0; k < entryArray.Length; k++)
            {
                int num9 = entryArray[k].m_OffsetX + this.m_Center.X;
                int num10 = entryArray[k].m_OffsetY + this.m_Center.Y;
                listArray[num9][num10].Add((short) ((entryArray[k].m_ItemID & 0x3fff) + 0x4000), (sbyte) entryArray[k].m_OffsetZ);
            }
            for (int m = 0; m < this.m_Width; m++)
            {
                for (int num12 = 0; num12 < this.m_Height; num12++)
                {
                    this.m_Tiles[m][num12] = listArray[m][num12].ToArray();
                    if (this.m_Tiles[m][num12].Length > 1)
                    {
                        Array.Sort(this.m_Tiles[m][num12]);
                    }
                }
            }
        }

        public Bitmap GetImage()
        {
            if ((this.m_Width == 0) || (this.m_Height == 0))
            {
                return null;
            }
            int num = 0x3e8;
            int num2 = 0x3e8;
            int num3 = -1000;
            int num4 = -1000;
            for (int i = 0; i < this.m_Width; i++)
            {
                for (int k = 0; k < this.m_Height; k++)
                {
                    Tile[] tileArray = this.m_Tiles[i][k];
                    for (int m = 0; m < tileArray.Length; m++)
                    {
                        Bitmap @static = Art.GetStatic(tileArray[m].ID - 0x4000);
                        if (@static != null)
                        {
                            int num8 = (i - k) * 0x16;
                            int num9 = (i + k) * 0x16;
                            num8 -= @static.Width / 2;
                            num9 -= tileArray[m].Z * 4;
                            num9 -= @static.Height;
                            if (num8 < num)
                            {
                                num = num8;
                            }
                            if (num9 < num2)
                            {
                                num2 = num9;
                            }
                            num8 += @static.Width;
                            num9 += @static.Height;
                            if (num8 > num3)
                            {
                                num3 = num8;
                            }
                            if (num9 > num4)
                            {
                                num4 = num9;
                            }
                        }
                    }
                }
            }
            Bitmap image = new Bitmap(num3 - num, num4 - num2);
            Graphics graphics = Graphics.FromImage(image);
            for (int j = 0; j < this.m_Width; j++)
            {
                for (int n = 0; n < this.m_Height; n++)
                {
                    Tile[] tileArray2 = this.m_Tiles[j][n];
                    for (int num13 = 0; num13 < tileArray2.Length; num13++)
                    {
                        Bitmap bitmap4 = Art.GetStatic(tileArray2[num13].ID - 0x4000);
                        if (bitmap4 != null)
                        {
                            int x = (j - n) * 0x16;
                            int y = (j + n) * 0x16;
                            x -= bitmap4.Width / 2;
                            y -= tileArray2[num13].Z * 4;
                            y -= bitmap4.Height;
                            x -= num;
                            y -= num2;
                            graphics.DrawImageUnscaled(bitmap4, x, y, bitmap4.Width, bitmap4.Height);
                        }
                    }
                    int num16 = (j - n) * 0x16;
                    int num11 = (j + n) * 0x16;
                    num16 -= num;
                    num11 -= num2;
                }
            }
            graphics.Dispose();
            return image;
        }

        public Point Center
        {
            get
            {
                return this.m_Center;
            }
        }

        public int Height
        {
            get
            {
                return this.m_Height;
            }
        }

        public Point Max
        {
            get
            {
                return this.m_Max;
            }
        }

        public Point Min
        {
            get
            {
                return this.m_Min;
            }
        }

        public Tile[][][] Tiles
        {
            get
            {
                return this.m_Tiles;
            }
        }

        public int Width
        {
            get
            {
                return this.m_Width;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MultiTileEntry2
        {
            public short m_ItemID;
            public short m_OffsetX;
            public short m_OffsetY;
            public short m_OffsetZ;
            public int m_Flags;
        }
    }
}

