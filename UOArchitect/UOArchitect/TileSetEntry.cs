using System;
using System.Drawing;
using System.Xml;
using Ultima;

namespace UOArchitect
{
    public class TileSetEntry
	{
		#region Props
		private int m_BaseIndex;
        private int m_Count;
        private bool m_Destroy;
        private Bitmap m_Image;
        private static System.Random m_Random = new System.Random();
        private int[] m_Tiles;

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

		public bool Destroy
		{
			get
			{
				return this.m_Destroy;
			}
		}

		public Bitmap Image
		{
			get
			{
				return this.m_Image;
			}
		}

		public static System.Random Random
		{
			get
			{
				return m_Random;
			}
		}

		public int[] Tiles
		{
			get
			{
				return this.m_Tiles;
			}
		}
		#endregion
		public TileSetEntry(int index) : this(index, 1)
        {
        }
		public TileSetEntry(bool Destroyer)
		{
			this.m_Image = global::Properties.Resources.cursor_del;//new Bitmap("Internal/Graphics/cursor_del.png");
			this.m_Destroy = true;
		}

        public TileSetEntry(XmlElement e)
        {
            string attribute = e.GetAttribute("index");
            string str2 = e.GetAttribute("count");
            this.m_BaseIndex = Convert.ToInt32(attribute);
            this.m_Count = (str2 == "") ? 1 : Convert.ToInt32(str2);
            if (this.m_Count == 0)
            {
				this.m_Image = global::Properties.Resources.cursor_del;//new Bitmap("Internal/Graphics/cursor_del.png");
                this.m_Destroy = true;
            }
            else
            {
                this.m_Image = Art.GetStatic(this.m_BaseIndex);
            }
            this.m_Tiles = new int[this.m_Count];
            for (int i = 0; i < this.m_Count; ++i)
            {
                this.m_Tiles[i] = this.m_BaseIndex + i;
            }
        }

        public TileSetEntry(int[] tiles)
        {
            this.m_Tiles = tiles;
            if (tiles.Length == 0)
            {
                this.m_Image = new Bitmap("Internal/Graphics/cursor_del.png");
            }
            else
            {
                this.m_Image = Art.GetStatic(this.m_Tiles[0]);
            }
        }

        public TileSetEntry(int index, int count)
        {
            this.m_BaseIndex = index;
            this.m_Count = count;
            this.m_Tiles = new int[count];
            for (int i = 0; i < count; i++)
            {
                this.m_Tiles[i] = index + i;
            }
            this.m_Image = Art.GetStatic(this.m_Tiles[0]);
        }

        public int GetRandomIndex()
        {
            if (this.m_Tiles.Length == 0)
            {
                return -1;
            }
            return this.m_Tiles[m_Random.Next(this.m_Tiles.Length)];
        }

        public void Save(XmlTextWriter xml)
        {
            xml.WriteStartElement("entry");
            xml.WriteAttributeString("index", this.m_BaseIndex.ToString());
            if (this.m_Count != 1)
            {
                xml.WriteAttributeString("count", this.m_Count.ToString());
            }
            xml.WriteEndElement();
        }
    }
}

