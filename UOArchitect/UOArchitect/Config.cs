using System;
using System.IO;
using System.Xml;
using Ultima;
using System.Windows.Forms;

namespace UOArchitect {
	internal class Config {
		private static bool _autoDetectClientLocation = true;
		private static string _MLclientDirectory = "";
		private static string _SAclientDirectory = "";
		private static string _commandPrefix = "[";
		private static string _ipAddress = "127.0.0.1";
		private static string _multiIdxTarget;
		private static string _multiMulTarget;
		private static string _password = "";
		private static int _port = 0xa22;
		private static string _userName = "";
		public static ServerListingCol ServerListings = new ServerListingCol();
		public static string VERSION = "3.0";

		static Config() {
			Directory.GetCurrentDirectory();
			LoadSettings();
		}

		public static PatchInfo CreateMultiPatchInfo() {
			PatchInfo info = new PatchInfo();
			info.MultiIdx = _multiIdxTarget;
			info.MultiMul = _multiMulTarget;
			return info;
		}

		public static void IntializeSettings() {
			MLClientDirectory = Utility.GetExePath(Utility.UOML_REGKEY);
			SAClientDirectory = Utility.GetExePath(Utility.UOSA_REGKEY);
			//Latest versions
			if (SAClientDirectory == null) SAClientDirectory = Utility.GetExePath(Utility.UOHS_REGKEY);
			if (SAClientDirectory == null) SAClientDirectory = Utility.GetExePath(Utility.UOEC_REGKEY);

			if (SAClientDirectory == null) {
				MessageBox.Show("Warning: Cannot find UO:SA installed, using UO:ML settings..");
				ClientUtility.CustomClientPath = MLClientDirectory + Utility.UOML_CLIENT;
			} else {
				ClientUtility.CustomClientPath = SAClientDirectory + Utility.UOSA_CLIENT;
			}
			LoadSettings();
		}

		public static void LoadSettings() {
			if (File.Exists(ConfigFile)) {
				XmlDocument document = new XmlDocument();
				document.Load(ConfigFile);
				XmlNode node = document.SelectSingleNode("settings");
				XmlNode node2 = node.SelectSingleNode("mlclientDirectory");
				if (node2 != null) {
					string path = node2.InnerText.Trim();
					if (Directory.Exists(path)) {
						MLClientDirectory = path;
					}
					/*else{
						AutoDetectClientDirectory = true;
					}*/
				}
				XmlNode node3 = node.SelectSingleNode("prefix");
				if ((node3 == null) || (node3.InnerText == string.Empty)) {
					_commandPrefix = "[";
				} else {
					_commandPrefix = node3.InnerText.Trim();
				}
				XmlNode node4 = node.SelectSingleNode("multiPatch");
				if ((node4 != null) && (node4.Attributes.Count > 0)) {
					_multiIdxTarget = node4.Attributes.GetNamedItem("multiIdxTarget").Value;
					_multiMulTarget = node4.Attributes.GetNamedItem("multiMulTarget").Value;
				}
				if (node != null) {
					XmlNodeList serverNodeList = node.SelectNodes("server");
					if (serverNodeList.Count > 0) {
						ServerListings.LoadListings(serverNodeList);
					}
				}
			}
		}

		private static void SaveMiscSettings(XmlDocument doc, XmlNode rootNode) {
			if (!AutoDetectClientDirectory) {
				XmlNode node = doc.CreateElement("mlclientDirectory");
				node.InnerText = MLClientDirectory;
				rootNode.AppendChild(node);
			}
			XmlNode newChild = doc.CreateElement("prefix");
			newChild.InnerText = CommandPrefix;
			rootNode.AppendChild(newChild);
			XmlNode node3 = doc.CreateElement("multiPatch");
			XmlAttribute attribute = doc.CreateAttribute("multiIdxTarget");
			attribute.Value = _multiIdxTarget;
			node3.Attributes.Append(attribute);
			attribute = doc.CreateAttribute("multiMulTarget");
			attribute.Value = _multiMulTarget;
			node3.Attributes.Append(attribute);
			rootNode.AppendChild(node3);
		}

		public static void SaveSettings() {
			XmlDocument doc = new XmlDocument();
			XmlNode newChild = doc.CreateElement("settings");
			XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", "");
			doc.AppendChild(declaration);
			doc.AppendChild(newChild);
			SaveMiscSettings(doc, newChild);
			ServerListings.SaveListings(doc, newChild);
			doc.Save(ConfigFile);
		}

		public static bool AutoDetectClientDirectory {
			get {
				return _autoDetectClientLocation;
			}
			set {
				_autoDetectClientLocation = value;
				if (value) {
					MLClientDirectory = "";
				}
			}
		}

		public static string MLClientDirectory {
			get {
				return _MLclientDirectory;
			}
			set {
				if (Directory.Exists(value)) {
					_autoDetectClientLocation = false;
					_MLclientDirectory = value;
					Client.Directories.Clear();
					Client.Directories.Add(value);
				} else {
					_autoDetectClientLocation = true;
					Client.Directories.Clear();
				}
			}
		}
		public static string SAClientDirectory {
			get {
				return _SAclientDirectory;
			}
			set {
				if (Directory.Exists(value)) {
					//_autoDetectClientLocation = false;
					_SAclientDirectory = value;
				} else {
					_SAclientDirectory = null;
				}
				/*else
				{
					//_autoDetectClientLocation = true;
					//Client.Directories.Clear();
				}*/
			}
		}
		public static string CommandPrefix {
			get {
				return _commandPrefix;
			}
			set {
				_commandPrefix = value;
			}
		}

		public static string ConfigFile {
			get {
				return "UO Architect.xml";
			}
		}

		public static bool DoTargetMultiMulsExist {
			get {
				bool flag = true;
				if (!File.Exists(_multiIdxTarget)) {
					flag = false;
				}
				if (!File.Exists(_multiMulTarget)) {
					flag = false;
				}
				return flag;
			}
		}

		public static string MultiIdxTarget {
			get {
				return _multiIdxTarget;
			}
			set {
				_multiIdxTarget = value;
			}
		}

		public static string MultiMulTarget {
			get {
				return _multiMulTarget;
			}
			set {
				_multiMulTarget = value;
			}
		}

		public static string Password {
			get {
				return _password;
			}
			set {
				_password = value;
			}
		}

		public static int Port {
			get {
				return _port;
			}
			set {
				_port = value;
			}
		}

		public static string ServerIP {
			get {
				return _ipAddress;
			}
			set {
				_ipAddress = value;
			}
		}

		public static string UserName {
			get {
				return _userName;
			}
			set {
				_userName = value;
			}
		}
	}
}