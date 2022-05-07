using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UOArchitect
{

    public class Utility
    {
		public const string ToolBoxPATH = @"Internal\toolbox.xml";
		public const string NewItemsPATH = @"Internal\NewItems.xml";

		public const string UOSA_CLIENT = @"\uosa.exe";
		public const string UOSA_REGKEY = @"Electronic Arts\EA Games\Ultima Online Stygian Abyss";
		public const string UOHS_REGKEY = @"Electronic Arts\EA Games\Ultima Online High Seas Enhanced BETA";
		public const string UOEC_REGKEY = @"Electronic Arts\EA Games\Ultima Online Enhanced";

		public const string UOML_CLIENT = @"\client.exe";
		public const string UOML_REGKEY = @"Origin Worlds Online\Ultima Online\1.0";

		public static string GetExePath(string subName)
		{
			bool Is64Bit = (IntPtr.Size == 8);
			try
			{
				if (Is64Bit)
					subName = @"Wow6432Node\" + subName;
				RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));
				if (key == null)
				{
					key = Registry.CurrentUser.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));
					if (key == null)
					{
						return null;
					}
				}
				string path = key.GetValue("ExePath") as string;
				if (((path == null) || (path.Length <= 0)) || (!Directory.Exists(path) && !File.Exists(path)))
				{
					path = key.GetValue("InstallDir") as string;
					if (((path == null) || (path.Length <= 0)) || (!Directory.Exists(path) && !File.Exists(path)))
					{
						return null;
					}
				}
				else
				{
					path = Path.GetDirectoryName(path);
				}
				if ((path == null) || !Directory.Exists(path))
				{
					return null;
				}
				return path;
			}
			catch
			{
				return null;
			}
		}

        public static string BrowseForFile(string filter, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Title = title;
            dialog.Filter = filter;
            dialog.RestoreDirectory = true;
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public static string[] BrowseForFiles(string filter, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Title = title;
            dialog.Filter = filter;
            dialog.RestoreDirectory = true;
            dialog.Multiselect = true;
            dialog.ShowDialog();
            return dialog.FileNames;
        }

        public static string GetSaveFileName(string filter, string title)
        {
            return GetSaveFileName(filter, title, "");
        }

        public static string GetSaveFileName(string filter, string title, string fileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = title;
            dialog.Filter = filter;
            dialog.FileName = StripInvalidFilenameChars(fileName);
            dialog.CheckPathExists = true;
            dialog.AddExtension = true;
            dialog.OverwritePrompt = true;
            dialog.ValidateNames = true;
            dialog.RestoreDirectory = true;
            string str = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                str = dialog.FileName;
                dialog.Dispose();
            }
            return str;
        }

        public static void OpenWebLink(string url)
        {
            Process.Start(url);
        }

        public static void SaveImageToDisk(Image image, ImageFormat format, Form owner)
        {
            string str = null;
            string str2 = null;
            SaveFileDialog dialog;
            switch (format.ToString())
            {
                case "Bmp":
                    str = "(Bitmap *.bmp)|*.bmp";
                    str2 = "bmp";
                    goto Label_008A;

                case "Jpeg":
                    str = "(Jpg *.jpg)|*.jpg";
                    str2 = "jpg";
                    goto Label_008A;

                case "Png":
                    str = "(Png *.png)|*.png";
                    str2 = "png";
                    break;

                case "Gif":
                    str = "(Gif *.gif)|*.gif";
                    str2 = "gif";
                    break;
            }
        Label_008A:
            dialog = new SaveFileDialog();
            dialog.Title = "Save Picture";
            dialog.DefaultExt = str2;
            dialog.Filter = str;
            dialog.CheckPathExists = true;
            dialog.AddExtension = true;
            dialog.OverwritePrompt = true;
            dialog.ValidateNames = true;
            dialog.RestoreDirectory = true;
            dialog.ShowDialog(owner);
            string fileName = dialog.FileName;
            dialog.Dispose();
            if (fileName != "")
            {
                image.Save(fileName, format);
            }
        }

        public static void SendClientCommand(string command)
        {
            try
            {
                /*Client.BringToTop();
				Client.SendText(Config.CommandPrefix + command);*/
				ClientUtility.BringClientToFront();
				ClientUtility.SendToClient(command);
            }
            catch
            {
            }
        }

        private static string StripInvalidFilenameChars(string fileName)
        {
            StringBuilder builder = new StringBuilder(fileName);
            builder.Replace(@"\", "");
            builder.Replace("/", "");
            builder.Replace("*", "");
            builder.Replace("?", "");
            builder.Replace("<", "");
            builder.Replace(">", "");
            builder.Replace("|", "");
            builder.Replace(":", "");
            return builder.ToString();
        }

        public static string ToHex(int Value)
        {
            return string.Format("0x{0:X}", Value);
        }

        public static int ToInt(string Value)
        {
            try
            {
                if (Value.StartsWith("0x"))
                {
                    return Convert.ToInt32(Value.Substring(2), 0x10);
                }
                return Convert.ToInt32(Value);
            }
            catch
            {
                return 0;
            }
        }
    }
}

