namespace UOArchitect
{
    using System;

    public class ComponentRecord
    {
        public int Hue;
        public int Index;
        public int Level;
        public int Version;
        public int X;
        public int Y;
        public int Z;

        public ComponentRecord(int version, int index, int x, int y, int z, int hue, int level)
        {
            this.Version = version;
            this.Index = index;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Level = level;
            this.Hue = hue;
        }

        public void Deserialize(BinaryFileReader reader)
        {
            this.Version = reader.ReadInt();
            if (this.Version == 0)
            {
                this.Index = reader.ReadInt();
                this.X = reader.ReadInt();
                this.Y = reader.ReadInt();
                this.Z = reader.ReadInt();
                this.Level = reader.ReadInt();
            }
        }

        public void Serialize(BinaryFileWriter writer)
        {
            writer.WriteInt(this.Version);
            if (this.Version == 0)
            {
                writer.WriteInt(this.Index);
                writer.WriteInt(this.X);
                writer.WriteInt(this.Y);
                writer.WriteInt(this.Z);
                writer.WriteInt(this.Level);
            }
        }
    }
}

