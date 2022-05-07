namespace UOArchitect
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using Ultima;
    using UOArchitectInterface;

    public class ItemExtracter
    {
        private string _category = "Unassigned";
        private bool _foundation = false;
        private bool _frozen = true;
        private bool _hues = true;
        private DesignItemCol _items;
        private int[] _itemSerials = null;
        private int[] _levelZ = new int[] { 0, 7, 0x1b, 0x2f, 0x43, 0x57, 0x6b };
        private int _maxZ = 0;
        private int _minZ = 0;
        private ExtractMode _mode;
        private bool _multipleRects = false;
        private string _name = "New Design";
        private bool _nonStatic = true;
        private bool _static = true;
        private string _subsection = "Unassigned";
        private bool _useMaxZ = false;
        private bool _useMinZ = false;
        public DesignExtractEvent OnExtracted;

        private ExtractRequestArgs CreateExtractRequestArgs()
        {
            ExtractRequestArgs args = new ExtractRequestArgs();
            args.ItemSerials = this._itemSerials;
            args.NonStatic = this._nonStatic;
            args.Static = this._static;
            args.Frozen = this._frozen;
            args.MaxZSet = this._useMaxZ;
            args.MaxZ = (short) this._maxZ;
            args.MinZSet = this._useMinZ;
            args.MinZ = (short) this._minZ;
            args.Foundation = this._foundation;
            args.ExtractHues = this._hues;
            args.MultipleRects = this._multipleRects;
            return args;
        }

        public void ExtractDesign()
        {
            this._mode = ExtractMode.Area;
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartExtraction));
        }

        public void ExtractDesign(int[] itemSerials)
        {
            this._mode = ExtractMode.ItemSerials;
            this._itemSerials = itemSerials;
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartExtraction));
        }

        private void ExtractFrozenItems(Rect2D rect, string mapName, bool hued)
        {
            Map mapByName = this.GetMapByName(mapName);
            if (mapByName == null)
            {
                MessageBox.Show("Failed to extract the frozen items from the map " + mapName);
            }
            else
            {
                int num = rect.TopX + rect.Width;
                int num2 = rect.TopY + rect.Height;
                for (int i = rect.TopX; i < num; i++)
                {
                    for (int j = rect.TopY; j < num2; j++)
                    {
                        HuedTile[] staticTiles = mapByName.Tiles.GetStaticTiles(i, j);
                        if ((staticTiles != null) && (staticTiles.Length != 0))
                        {
                            for (int k = 0; k < staticTiles.Length; k++)
                            {
                                HuedTile tile = staticTiles[k];
                                if ((!this._useMinZ || (tile.Z >= this._minZ)) && (!this._useMaxZ || (tile.Z <= this._maxZ)))
                                {
                                    DesignItem item = new DesignItem();
                                    item.ItemID = (short) (tile.ID ^ 0x4000);
                                    item.X = i;
                                    item.Y = j;
                                    item.Z = tile.Z;
                                    if (hued)
                                    {
                                        item.Hue = (short) tile.Hue;
                                    }
                                    if (this._items.IndexOf(item) == -1)
                                    {
                                        this._items.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private Map GetMapByName(string name)
        {
            Map map = null;
            string str = name.ToLower();
            if (str == null)
            {
                return map;
            }
            str = string.IsInterned(str);
            if (str == "trammel")
            {
                return Map.Trammel;
            }
            if (str == "felucca")
            {
                return Map.Felucca;
            }
            if (str == "ilshenar")
            {
                return Map.Ilshenar;
            }
            if (str == "malas")
            {
                return Map.Malas;
            }
            if (str != "tokuno")
            {
                return map;
            }
            return Map.Tokuno;
        }

        private void RaiseExtractedEvent(DesignData design)
        {
            if (this.OnExtracted != null)
            {
                this.OnExtracted(design);
            }
        }

        private void StartExtraction(object state)
        {
            ExtractResponse response = Connection.ExtractDesign(this.CreateExtractRequestArgs());
            if (response == null)
            {
                this.RaiseExtractedEvent(null);
            }
            else
            {
                this._items = (response.Items != null) ? response.Items : new DesignItemCol();
                if (this._frozen && (this._mode == ExtractMode.Area))
                {
                    for (int i = 0; i < response.Rects.Count; i++)
                    {
                        this.ExtractFrozenItems(response.Rects[i], response.Map, this._hues);
                    }
                }
                if ((response == null) || (response.Items.Count == 0))
                {
                    this.RaiseExtractedEvent(null);
                }
                DesignData design = null;
                if (response.Items.Count > 0)
                {
                    design = new DesignData(this._name, this._category, this._subsection);
                    design.ImportItems(response.Items, true, this._foundation);
                }
                this.RaiseExtractedEvent(design);
            }
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

        public bool Foundation
        {
            get
            {
                return this._foundation;
            }
            set
            {
                this._foundation = value;
            }
        }

        public bool Frozen
        {
            get
            {
                return this._frozen;
            }
            set
            {
                this._frozen = value;
            }
        }

        public bool Hues
        {
            get
            {
                return this._hues;
            }
            set
            {
                this._hues = value;
            }
        }

        public int Levels
        {
            get
            {
                return this._levelZ.Length;
            }
        }

        public int[] LevelZ
        {
            get
            {
                return this._levelZ;
            }
            set
            {
                this._levelZ = value;
            }
        }

        public int MaxZ
        {
            get
            {
                return this._maxZ;
            }
            set
            {
                this._maxZ = value;
            }
        }

        public int MinZ
        {
            get
            {
                return this._minZ;
            }
            set
            {
                this._minZ = value;
            }
        }

        public bool MultipleRects
        {
            get
            {
                return this._multipleRects;
            }
            set
            {
                this._multipleRects = value;
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

        public bool NonStatic
        {
            get
            {
                return this._nonStatic;
            }
            set
            {
                this._nonStatic = value;
            }
        }

        public bool Static
        {
            get
            {
                return this._static;
            }
            set
            {
                this._static = value;
            }
        }

        public string Subsection
        {
            get
            {
                return this._subsection;
            }
            set
            {
                this._subsection = value;
            }
        }

        public bool UseMaxZ
        {
            get
            {
                return this._useMaxZ;
            }
            set
            {
                this._useMaxZ = value;
            }
        }

        public bool UseMinZ
        {
            get
            {
                return this._useMinZ;
            }
            set
            {
                this._useMinZ = value;
            }
        }

        public delegate void DesignExtractEvent(DesignData design);
    }
}

