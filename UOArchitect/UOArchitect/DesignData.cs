namespace UOArchitect
{
    using System;
    using System.Runtime.CompilerServices;
    using UOArchitectInterface;

    [Serializable]
    public class DesignData
    {
        private string _category;
        private long _filePosition;
        private int _height;
        private DesignItemCol _items;
        private string _name;
        private int _recordCount;
        private string _subcategory;
        private int _userHeight;
        private int _userWidth;
        private int _width;
        private const short DESIGN_VERSION = 1;
        public const int Levels = 5;
        private static int[] m_LevelZ = new int[] { 0, 7, 0x1b, 0x2f, 0x43 };
        public SavedEvent OnSaved;

        public DesignData()
        {
            this._name = "";
            this._category = "";
            this._subcategory = "";
            this._width = 0;
            this._height = 0;
            this._userWidth = 0;
            this._userHeight = 0;
            this._filePosition = 0L;
            this._recordCount = 0;
            this._items = new DesignItemCol();
            this._name = "New Design";
            this._category = "Unassigned";
            this._subcategory = "Unassigned";
        }

        public DesignData(string name, string category, string subsection)
        {
            this._name = "";
            this._category = "";
            this._subcategory = "";
            this._width = 0;
            this._height = 0;
            this._userWidth = 0;
            this._userHeight = 0;
            this._filePosition = 0L;
            this._recordCount = 0;
            this._items = new DesignItemCol();
            this._name = name;
            this._category = category;
            this._subcategory = subsection;
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
            return 4;
        }

        public void ImportItems(DesignItemCol items, bool calculateOffsets, bool foundation)
        {
            if (items.Count != 0)
            {
                this._items.Clear();
                for (int i = 0; i < items.Count; i++)
                {
                    DesignItem item = items[i];
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    if (!foundation)
                    {
                        item.Z += LevelZ[1];
                    }
                    if (calculateOffsets)
                    {
                        x = item.X - items.OriginX;
                        y = item.Y - items.OriginY;
                        z = item.Z - items.OriginZ;
                    }
                    else
                    {
                        x = item.X;
                        y = item.Y;
                        z = item.Z;
                    }
                    this._items.Add(new DesignItem(item.ItemID, x, y, z, this.GetZLevel(z), item.Hue));
                }
                this.UpdateSize();
            }
        }

        public void Load()
        {
            this.Unload();
            HouseDesignData.LoadDesign(this);
        }

        public int PatchToMultiMuls(PatchInfo patchInfo)
        {
            if (!this.IsLoaded)
            {
                this.Load();
            }
            int num = new MultiPatcher(patchInfo).PatchMulti(this, null);
            this.Unload();
            return num;
        }

        public void Save()
        {
            this.Save(this.IsNewRecord);
        }

        public void Save(bool newRecord)
        {
            if (!this.IsLoaded)
            {
                this.Load();
            }
            if (newRecord)
            {
                HouseDesignData.SaveNewDesign(this);
            }
            else
            {
                HouseDesignData.UpdateDesign(this);
            }
            if (this.OnSaved != null)
            {
                this.OnSaved();
            }
        }

        public void SetItems(DesignItemCol items, bool UseCalculations)
        {
            this._items = items;
            if (UseCalculations)
            {
                this.UpdateSize();
            }
        }

        public void Unload()
        {
            this._items.Clear();
        }

        private void UpdateSize()
        {
            this._width = this._items.Width;
            this._height = this._items.Height;
            this._userWidth = this._items.Width;
            this._userHeight = this._items.Height;
        }

        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
            }
        }

        public long FilePosition
        {
            get
            {
                return this._filePosition;
            }
            set
            {
                this._filePosition = value;
            }
        }

        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public bool IsLoaded
        {
            get
            {
                return (this._items.Count > 0);
            }
        }

        public bool IsNewRecord
        {
            get
            {
                return ((this._filePosition == 0L) && (this._recordCount == 0));
            }
        }

        public DesignItemCol Items
        {
            get
            {
                return this._items;
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
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int RecordCount
        {
            get
            {
                return this._recordCount;
            }
            set
            {
                this._recordCount = value;
            }
        }

        public string Subsection
        {
            get
            {
                return this._subcategory;
            }
            set
            {
                this._subcategory = value;
            }
        }

        public int UserHeight
        {
            get
            {
                return this._userHeight;
            }
            set
            {
                this._userHeight = value;
            }
        }

        public int UserWidth
        {
            get
            {
                return this._userWidth;
            }
            set
            {
                this._userWidth = value;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public delegate void SavedEvent();
    }
}

