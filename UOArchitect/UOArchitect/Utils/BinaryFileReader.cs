namespace UOArchitect
{
    using System;
    using System.IO;

    public class BinaryFileReader
    {
        private FileStream m_File;
        private BinaryReader m_Reader;

        public BinaryFileReader(FileStream stream)
        {
            this.m_File = stream;
            this.m_Reader = new BinaryReader(this.m_File);
        }

        public void Close()
        {
            this.m_File.Close();
        }

        public bool ReadBool()
        {
            return this.m_Reader.ReadBoolean();
        }

        public byte ReadByte()
        {
            return this.m_Reader.ReadByte();
        }

        public char ReadChar()
        {
            return this.m_Reader.ReadChar();
        }

        public decimal ReadDecimal()
        {
            return this.m_Reader.ReadDecimal();
        }

        public float ReadFloat()
        {
            return this.m_Reader.ReadSingle();
        }

        public int ReadInt()
        {
            return this.m_Reader.ReadInt32();
        }

        public long ReadLong()
        {
            return this.m_Reader.ReadInt64();
        }

        public sbyte ReadSByte()
        {
            return this.m_Reader.ReadSByte();
        }

        public short ReadShort()
        {
            return this.m_Reader.ReadInt16();
        }

        public string ReadString()
        {
            if (this.m_Reader.ReadByte() == 0)
            {
                return null;
            }
            return this.m_Reader.ReadString();
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            this.m_File.Seek(offset, origin);
        }

        public bool EOF
        {
            get
            {
                return (this.m_Reader.PeekChar() == -1);
            }
        }

        public long Position
        {
            get
            {
                return this.m_File.Position;
            }
        }
    }
}

