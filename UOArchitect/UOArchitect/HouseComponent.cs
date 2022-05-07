namespace UOArchitect
{
    using System;
    using System.Drawing;
    using Ultima;

    public class HouseComponent : IComparable
    {
        private int m_BaseIndex;
        private int m_Count;
        private int m_Height;
        private int m_Hue;
        private Bitmap m_Image;
        private int m_Index;
        private int m_Z;

        public HouseComponent(int index, int z)
        {
            this.m_Hue = 0;
            this.m_Index = index;
            this.m_Z = z;
            this.m_Height = TileData.ItemTable[index].CalcHeight;
            this.m_Image = Art.GetStatic(index);
            this.ApplyHue(0);
            this.m_BaseIndex = index;
            this.m_Count = 1;
        }

        public HouseComponent(int index, int z, int baseIndex, int count)
        {
            this.m_Hue = 0;
            this.m_Index = index;
            this.m_Z = z;
            this.m_Height = TileData.ItemTable[index].CalcHeight;
            this.m_Image = Art.GetStatic(index);
            this.m_BaseIndex = baseIndex;
            this.m_Count = count;
        }

        private void ApplyHue(int hue)
        {
            if ((this.m_Image == null) || (hue != this.m_Hue))
            {
                this.m_Image = (Bitmap) Art.GetStatic(this.m_Index).Clone();
            }
            if (hue != this.m_Hue)
            {
                this.m_Hue = hue;
                if (this.m_Hue > 0)
                {
                    Hues.GetHue(this.m_Hue).ApplyTo(this.m_Image, false);
                }
            }
        }

        public int CompareTo(object obj)
        {
            HouseComponent component = (HouseComponent) obj;
            if (this.m_Z < component.m_Z)
            {
                return -1;
            }
            if (component.m_Z < this.m_Z)
            {
                return 1;
            }
            if (this.m_Height < component.m_Height)
            {
                return -1;
            }
            if (component.m_Height < this.m_Height)
            {
                return 1;
            }
            return 0;
        }

        public int BaseIndex
        {
            get
            {
                return this.m_BaseIndex;
            }
        }

        public int Count
        {
            get
            {
                return this.m_Count;
            }
        }

        public int Height
        {
            get
            {
                return this.m_Height;
            }
        }

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
            set
            {
                this.ApplyHue(value);
            }
        }

        public Bitmap Image
        {
            get
            {
                return this.m_Image;
            }
        }

        public int Index
        {
            get
            {
                return this.m_Index;
            }
        }

        public int Z
        {
            get
            {
                return this.m_Z;
            }
        }
    }
}

