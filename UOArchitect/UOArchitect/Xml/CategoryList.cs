namespace UOArchitect {
	using System;
	using System.Collections;
	using System.Runtime.CompilerServices;

	public class CategoryList {
		private static Hashtable m_Categories = new Hashtable(0, new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
		//private static Hashtable m_Categorie = new Hashtable(0, new CaseInsensitiveComparer());
		public static CategoryRefresh OnRefresh;

		static CategoryList() {
			LoadCategories(HouseDesignData.DesignHeaders);
		}

		public static string[] GetCategoryNames() {
			string[] array = null;
			if (m_Categories.Count > 0) {
				array = new string[m_Categories.Count];
				m_Categories.Keys.CopyTo(array, 0);
			}
			return array;
		}

		public static ArrayList GetHeaderList(string category, string subSection) {
			ArrayList list = new ArrayList();
			ArrayList designHeaders = HouseDesignData.DesignHeaders;
			for (int i = 0; i < designHeaders.Count; i++) {
				DesignData data = (DesignData)designHeaders[i];
				if ((string.Compare(data.Category, category, true) == 0) && (string.Compare(data.Subsection, subSection, true) == 0)) {
					list.Add(data);
				}
			}
			return list;
		}

		public static string[] GetSubSectionNames(string category) {
			string[] array = null;
			Hashtable hashtable = (Hashtable)m_Categories[category];
			if (hashtable != null) {
				array = new string[hashtable.Count];
				hashtable.Keys.CopyTo(array, 0);
			}
			return array;
		}

		private static Hashtable GetSubSections(string category) {
			Hashtable hashtable = null;
			if (!m_Categories.ContainsKey(category)) {
				hashtable = new Hashtable(0, new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
				m_Categories.Add(category, hashtable);
				return hashtable;
			}
			return (Hashtable)m_Categories[category];
		}

		private static void LoadCategories(ArrayList headers) {
			for (int i = 0; i < headers.Count; i++) {
				DesignData data = (DesignData)headers[i];
				string category = ((data.Category == null) || (data.Category.Length == 0)) ? "Misc" : data.Category;
				string key = ((data.Subsection == null) || (data.Subsection.Length == 0)) ? "Misc" : data.Subsection;
				Hashtable subSections = GetSubSections(category);
				if (!subSections.ContainsKey(key)) {
					subSections.Add(key, null);
				}
			}
		}

		public static void Refresh() {
			m_Categories.Clear();
			LoadCategories(HouseDesignData.DesignHeaders);
			if (OnRefresh != null) {
				OnRefresh();
			}
		}

		public delegate void CategoryRefresh();
	}
}