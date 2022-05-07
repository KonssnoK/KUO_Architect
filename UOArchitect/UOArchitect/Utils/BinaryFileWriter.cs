namespace UOArchitect
{
    using System;
    using System.IO;

    public class BinaryFileWriter
    {
        private FileStream m_File;
        private BinaryWriter m_Writer;

        public BinaryFileWriter(FileStream stream)
        {
            this.m_File = stream;
            this.m_Writer = new BinaryWriter(stream);
        }

        public void Close()
        {
            this.m_File.Close();
        }

        public void WriteBool(bool value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteByte(byte value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteChar(char value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteDecimal(decimal value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteFloat(float value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteInt(int value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteLong(long value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteSByte(sbyte value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteShort(short value)
        {
            this.m_Writer.Write(value);
        }

        public void WriteString(string value)
        {
            byte num = (value != null) ? ((byte) 1) : ((byte) 0);
            this.m_Writer.Write(num);
            if (num == 1)
            {
                this.m_Writer.Write(value);
            }
        }

        public Stream BaseStream
        {
            get
            {
                return this.m_File;
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

