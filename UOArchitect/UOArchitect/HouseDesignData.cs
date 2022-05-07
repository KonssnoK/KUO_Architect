namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using UOArchitectInterface;

    public class HouseDesignData
    {
        private static readonly string BACKUP_BIN_FILE;
        private static readonly string BACKUP_DIR;
        private static readonly string BACKUP_INDEX_FILE;
        private static readonly string BIN_FILE;
        private static readonly int COMPONENT_VERSION = 1;
        private static readonly string INDEX_FILE;
        private static readonly int INDEX_FILE_VERSION = 0;
        private static ArrayList m_DesignHeaders;
        private static ArrayList m_UnsavedDesigns = new ArrayList();
        public static SaveNewDesignEvent OnNewDesignSaved;
        public static RefreshDesignsList OnRefreshDesignsList;
        private static readonly string SAVE_DIR;
        private static readonly string TMP_BIN_FILE;
        private static readonly string TMP_INDEX_FILE;

        static HouseDesignData()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (!currentDirectory.EndsWith(@"\"))
            {
                currentDirectory = currentDirectory + @"\";
            }
            SAVE_DIR = currentDirectory + @"Save\";
            BACKUP_DIR = SAVE_DIR + @"Backup\";
            INDEX_FILE = SAVE_DIR + "Designs.idx";
            BIN_FILE = SAVE_DIR + "Designs.bin";
            TMP_INDEX_FILE = SAVE_DIR + "DesignsIdx.tmp";
            TMP_BIN_FILE = SAVE_DIR + "DesignsBin.tmp";
            BACKUP_INDEX_FILE = BACKUP_DIR + "Designs.idx";
            BACKUP_BIN_FILE = BACKUP_DIR + "Designs.bin";
            LoadDesignHeaders();
        }

        public static void BatchSaveNewDesigns(ArrayList newDesigns)
        {
            m_UnsavedDesigns.AddRange(newDesigns);
            SaveData();
            if (OnRefreshDesignsList != null)
            {
                OnRefreshDesignsList();
            }
        }

        public static void DeleteDesign(DesignData header)
        {
            int index = m_DesignHeaders.IndexOf(header);
            if (index != -1)
            {
                m_DesignHeaders.RemoveAt(index);
                SaveData();
            }
        }

        public static void LoadDesign(DesignData designHeader)
        {
            if (File.Exists(BIN_FILE))
            {
                BinaryFileReader reader = new BinaryFileReader(File.Open(BIN_FILE, FileMode.Open, FileAccess.Read, FileShare.Read));
                try
                {
                    reader.Seek(designHeader.FilePosition, SeekOrigin.Begin);
                    int recordCount = designHeader.RecordCount;
                    for (int i = 0; i < recordCount; i++)
                    {
                        int itemID = 0;
                        int x = 0;
                        int y = 0;
                        int z = 0;
                        int level = 0;
                        int hue = 0;
                        switch (reader.ReadInt())
                        {
                            case 0:
                                itemID = reader.ReadInt();
                                x = reader.ReadInt();
                                y = reader.ReadInt();
                                z = reader.ReadInt();
                                level = reader.ReadInt();
                                break;

                            case 1:
                                itemID = reader.ReadInt();
                                x = reader.ReadInt();
                                y = reader.ReadInt();
                                z = reader.ReadInt();
                                level = reader.ReadInt();
                                hue = reader.ReadInt();
                                break;
                        }
                        designHeader.Items.Add(new DesignItem(itemID, x, y, z, level, hue));
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Unable to load design\n" + exception.Message);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        public static void LoadDesignHeaders()
        {
            if (File.Exists(INDEX_FILE))
            {
                BinaryFileReader reader = new BinaryFileReader(File.Open(INDEX_FILE, FileMode.Open, FileAccess.Read, FileShare.Read));
                try
                {
                    int capacity = reader.ReadInt();
                    int num2 = reader.ReadInt();
                    m_DesignHeaders = new ArrayList(capacity);
                    for (int i = 0; i < capacity; i++)
                    {
                        DesignData data = new DesignData();
                        switch (num2)
                        {
                            case 0:
                            {
                                data.Name = reader.ReadString();
                                data.Category = reader.ReadString();
                                data.Subsection = reader.ReadString();
                                data.Width = reader.ReadInt();
                                data.Height = reader.ReadInt();
                                data.UserWidth = reader.ReadInt();
                                data.UserHeight = reader.ReadInt();
                                data.FilePosition = reader.ReadLong();
                                data.RecordCount = reader.ReadInt();
                            }
							break;
                        }
                        m_DesignHeaders.Add(data);
                    }
                    return;
                }
                catch (Exception exception)
                {
                    m_DesignHeaders.Clear();
                    MessageBox.Show("Unable to load the designs\n" + exception.Message);
                    return;
                }
                finally
                {
                    reader.Close();
                }
            }
            m_DesignHeaders = new ArrayList();
        }

        private static void ReplaceSaveFiles()
        {
            if (File.Exists(INDEX_FILE))
            {
                File.Copy(INDEX_FILE, BACKUP_INDEX_FILE, true);
            }
            if (File.Exists(TMP_INDEX_FILE))
            {
                File.Copy(TMP_INDEX_FILE, INDEX_FILE, true);
                File.Delete(TMP_INDEX_FILE);
            }
            if (File.Exists(BIN_FILE))
            {
                File.Copy(BIN_FILE, BACKUP_BIN_FILE, true);
            }
            if (File.Exists(TMP_BIN_FILE))
            {
                File.Copy(TMP_BIN_FILE, BIN_FILE, true);
                File.Delete(TMP_BIN_FILE);
            }
        }

        private static void SaveData()
        {
            if (!Directory.Exists(SAVE_DIR))
            {
                Directory.CreateDirectory(SAVE_DIR);
            }
            if (!Directory.Exists(BACKUP_DIR))
            {
                Directory.CreateDirectory(BACKUP_DIR);
            }
            try
            {
                WriteBinFile();
                WriteHeaderFile();
                ReplaceSaveFiles();
            }
            catch (Exception exception)
            {
                if (File.Exists(TMP_INDEX_FILE))
                {
                    File.Delete(TMP_INDEX_FILE);
                }
                if (File.Exists(TMP_BIN_FILE))
                {
                    File.Delete(TMP_BIN_FILE);
                }
                MessageBox.Show("An error occurred while saving your changes. Recovered last save files");
                MessageBox.Show(string.Format("Exception Message: {0}\nStack Trace: {1}", exception.Message, exception.StackTrace));
            }
            CategoryList.Refresh();
        }

        public static void SaveNewDesign(DesignData design)
        {
            m_UnsavedDesigns.Add(design);
            SaveData();
            if (OnNewDesignSaved != null)
            {
                OnNewDesignSaved(design);
            }
        }

        public static void UpdateDesign(DesignData design)
        {
            if (!design.IsNewRecord)
            {
                int index = m_DesignHeaders.IndexOf(design);
                if (index != -1)
                {
                    m_DesignHeaders.RemoveAt(index);
                }
            }
            m_UnsavedDesigns.Add(design);
            SaveData();
        }

        private static void WriteBinFile()
        {
            bool flag = File.Exists(BIN_FILE);
            BinaryFileWriter binWriter = new BinaryFileWriter(File.Open(TMP_BIN_FILE, FileMode.Create, FileAccess.Write, FileShare.None));
            if (flag)
            {
                BinaryFileReader oldBinReader = new BinaryFileReader(File.Open(BIN_FILE, FileMode.Open, FileAccess.Read, FileShare.None));
                WriteSavedComponentData(oldBinReader, binWriter);
                oldBinReader.Close();
            }
            foreach (DesignData data in m_UnsavedDesigns)
            {
                data.FilePosition = binWriter.Position;
                data.RecordCount = data.Items.Count;
                WriteUpdatedDesignComponentData(data.Items, binWriter);
                m_DesignHeaders.Add(data);
            }
            m_UnsavedDesigns.Clear();
            binWriter.Close();
        }

        private static void WriteHeaderFile()
        {
            BinaryFileWriter writer = new BinaryFileWriter(File.Open(TMP_INDEX_FILE, FileMode.Create, FileAccess.Write, FileShare.None));
            writer.WriteInt(m_DesignHeaders.Count);
            writer.WriteInt(INDEX_FILE_VERSION);
            for (int i = 0; i < m_DesignHeaders.Count; i++)
            {
                DesignData data = (DesignData) m_DesignHeaders[i];
                writer.WriteString(data.Name);
                writer.WriteString(data.Category);
                writer.WriteString(data.Subsection);
                writer.WriteInt(data.Width);
                writer.WriteInt(data.Height);
                writer.WriteInt(data.UserWidth);
                writer.WriteInt(data.UserHeight);
                writer.WriteLong(data.FilePosition);
                writer.WriteInt(data.RecordCount);
            }
            writer.Close();
        }

        private static void WriteSavedComponentData(BinaryFileReader oldBinReader, BinaryFileWriter binWriter)
        {
            for (int i = 0; i < m_DesignHeaders.Count; i++)
            {
                DesignData header = (DesignData) m_DesignHeaders[i];
                oldBinReader.Seek(header.FilePosition, SeekOrigin.Begin);
                header.FilePosition = binWriter.Position;
                WriteSavedComponentData(header, oldBinReader, binWriter);
            }
        }

        private static void WriteSavedComponentData(DesignData header, BinaryFileReader oldFileReader, BinaryFileWriter writer)
        {
            int recordCount = header.RecordCount;
            for (int i = 0; i < recordCount; i++)
            {
                int num3 = oldFileReader.ReadInt();
                switch (num3)
                {
                    case 0:
                        writer.WriteInt(num3);
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        break;

                    case 1:
                        writer.WriteInt(num3);
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        writer.WriteInt(oldFileReader.ReadInt());
                        break;
                }
            }
        }

        private static void WriteUpdatedDesignComponentData(DesignItemCol designItems, BinaryFileWriter binWriter)
        {
            for (int i = 0; i < designItems.Count; i++)
            {
                binWriter.WriteInt(COMPONENT_VERSION);
                binWriter.WriteInt(designItems[i].ItemID);
                binWriter.WriteInt(designItems[i].X);
                binWriter.WriteInt(designItems[i].Y);
                binWriter.WriteInt(designItems[i].Z);
                binWriter.WriteInt(designItems[i].Level);
                binWriter.WriteInt(designItems[i].Hue);
            }
        }

        public static ArrayList DesignHeaders
        {
            get
            {
                return m_DesignHeaders;
            }
        }

        public delegate void RefreshDesignsList();

        public delegate void SaveNewDesignEvent(DesignData design);
    }
}

