namespace UOArchitect
{
	using System;
	using System.Collections;
	using System.Drawing;
	using System.IO;
	using System.Xml;
	using Ultima;
	using System.Windows.Forms;

	public class TileSet
	{
		private ArrayList m_Entries;
		private object m_ID;
		private Bitmap m_Image;
		private string m_Name;
		private Bitmap m_SelectedImage;
		public static readonly TileSet Toolbox = LoadFromXml(Utility.ToolBoxPATH);
		public static readonly TileSet NewToolbox = LoadFromXml(Utility.NewItemsPATH);

		private TileSet m_parent = null;
		public TileSet Parent
		{
			get { return m_parent; }
			set { m_parent = value; }
		}

		public TileSet(string name)
			: this(name, null, null)
		{
		}

		public TileSet(XmlElement e)
		{
			try
			{
				this.m_Entries = new ArrayList();
				string attribute = e.GetAttribute("id");
				string str2 = e.GetAttribute("Name");
				try
				{
					int index = Convert.ToInt32(attribute);
					this.m_ID = index;
					this.m_Name = str2;
					this.m_Image = Art.GetStatic(index);
				}
				catch
				{
					this.m_ID = attribute;
					this.m_Name = str2;
					try
					{
						this.m_Image = new Bitmap("Internal/Graphics/" + attribute + "_reg.png");
						this.m_SelectedImage = new Bitmap("Internal/Graphics/" + attribute + "_sel.png");
					}
					catch
					{
						if (e.ChildNodes != null && e.ChildNodes.Count > 0)
						{
							XmlNode n = e.ChildNodes[0];
							try
							{
								this.m_Image = Art.GetStatic(Convert.ToInt32(e.GetAttribute("index")));
							}
							catch
							{
								this.m_Image = new Bitmap("Internal/Graphics/tb_wall_reg.png");
								this.m_SelectedImage = new Bitmap("Internal/Graphics/tb_wall_sel.png");
							}
						}
						else
						{
							this.m_Image = new Bitmap("Internal/Graphics/tb_wall_reg.png");
							this.m_SelectedImage = new Bitmap("Internal/Graphics/tb_wall_sel.png");
						}
					}
				}
				foreach (XmlNode node in e)
				{
					if (node is XmlElement)
					{
						if (node.Name == "group")
						{
							this.AddSet(new TileSet((XmlElement)node));
						}
						else if (node.Name == "entry")
						{
							this.m_Entries.Add(new TileSetEntry((XmlElement)node));
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public TileSet(string name, int index)
			: this(name, Art.GetStatic(index), null)
		{
			this.m_ID = index;
		}

		public TileSet(string name, string fname)
			: this(name, new Bitmap("Internal/Graphics/" + fname + "_reg.png"), new Bitmap("Internal/Graphics/" + fname + "_sel.png"))
		{
			this.m_ID = fname;
		}

		public TileSet(string name, Bitmap image, Bitmap selectedImage)
		{
			this.m_Name = name;
			this.m_Image = image;
			this.m_SelectedImage = selectedImage;
			this.m_Entries = new ArrayList();
		}

		public void AddEntry(int[] tiles)
		{
			this.m_Entries.Add(new TileSetEntry(tiles));
		}

		public void AddEntry(int index)
		{
			this.m_Entries.Add(new TileSetEntry(index));
		}

		public void AddEntry(int index, int count)
		{
			this.m_Entries.Add(new TileSetEntry(index, count));
		}

		public void AddSet(TileSet tileSet)
		{
			this.m_Entries.Add(tileSet);
		}

		public static TileSet LoadFromXml(string filePath)
		{
			if (!File.Exists(filePath))
			{
				MessageBox.Show("INTERNAL FOLDER MISSING!");
				return null;
			}
			using (new StreamReader(filePath))
			{
				XmlDocument document = new XmlDocument();
				document.Load(filePath);
				XmlElement element = document["root"];
				TileSet set = new TileSet((string)null);
				foreach (XmlElement element2 in element)
				{
					set.AddSet(new TileSet(element2));
				}
				return set;
			}
		}

		public void Save(XmlTextWriter xml)
		{
			if (this.m_ID != null)
			{
				xml.WriteStartElement("group");
				xml.WriteAttributeString("id", (this.m_ID == null) ? "" : this.m_ID.ToString());
				xml.WriteAttributeString("name", this.m_Name);
			}
			foreach (object obj2 in this.m_Entries)
			{
				if (obj2 is TileSet)
				{
					((TileSet)obj2).Save(xml);
				}
				else if (obj2 is TileSetEntry)
				{
					((TileSetEntry)obj2).Save(xml);
				}
			}
			if (this.m_ID != null)
			{
				xml.WriteEndElement();
			}
		}

		public ArrayList Entries
		{
			get
			{
				return this.m_Entries;
			}
		}

		public Bitmap Image
		{
			get
			{
				return this.m_Image;
			}
		}

		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		public Bitmap SelectedImage
		{
			get
			{
				return this.m_SelectedImage;
			}
		}
	}
}