using System;
using System.Text;

namespace UOArchitect
{
    public class BuildEntry : IComparable
    {
        public int m_Count;
        public int m_Height;
        public int m_Hue = 0;
        public int m_Index;
        public int m_Level;
        public int m_PairIndex;
        public int m_Width;
        public int m_X;
        public int m_Y;
        public int m_Z;

        public BuildEntry(int x, int y, int z, int index, int count, int level)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Width = 1;
            this.m_Height = 1;
            this.m_Z = z;
            this.m_Index = index;
            this.m_Count = count;
            this.m_Level = level;
        }

        public void Append(StringBuilder sb)
        {
            if (this.m_Count == 1)
            {
                sb.AppendFormat("[TileRXYZ {0} {1} {2} {3} {4} Static {5}\r\n", new object[] { this.m_X, this.m_Y, this.m_Width, this.m_Height, this.m_Z, this.m_Index });
            }
            else
            {
                sb.AppendFormat("[TileRXYZ {0} {1} {2} {3} {4} Static {5} {6}\r\n", new object[] { this.m_X, this.m_Y, this.m_Width, this.m_Height, this.m_Z, this.m_Index, this.m_Count });
            }
        }

        public bool CombineWith(BuildEntry e)
        {
            if ((((this.m_Level == e.m_Level) && (this.m_Z == e.m_Z)) && ((this.m_X == (e.m_X + e.m_Width)) && (this.m_Y == e.m_Y))) && (((this.m_Height == e.m_Height) && (this.m_Index == e.m_Index)) && (e.m_Count == this.m_Count)))
            {
                e.m_Width += this.m_Width;
                return true;
            }
            if ((((this.m_Level == e.m_Level) && (this.m_Z == e.m_Z)) && ((this.m_Y == (e.m_Y + e.m_Height)) && (this.m_X == e.m_X))) && (((this.m_Width == e.m_Width) && (this.m_Index == e.m_Index)) && (e.m_Count == this.m_Count)))
            {
                e.m_Height += this.m_Height;
                return true;
            }
            return false;
        }

        public int CompareTo(object obj)
        {
            BuildEntry entry = (BuildEntry) obj;
            if (this.m_Y < entry.m_Y)
            {
                return -1;
            }
            if (entry.m_Y < this.m_Y)
            {
                return 1;
            }
            if (this.m_X < entry.m_X)
            {
                return -1;
            }
            if (entry.m_X < this.m_X)
            {
                return 1;
            }
            return 0;
        }

        public void Send()
        {
			string Arg = null;
            if (this.m_Count == 1)
            {
                Arg = string.Format("TileRXYZ {0} {1} {2} {3} {4} Static {5}", new object[] { this.m_X, this.m_Y, this.m_Width, this.m_Height, this.m_Z, this.m_Index });
            }
            else
            {
                 Arg = string.Format("TileRXYZ {0} {1} {2} {3} {4} Static {5} {6}", new object[] { this.m_X, this.m_Y, this.m_Width, this.m_Height, this.m_Z, this.m_Index, this.m_Count });
            }
			ClientUtility.SendToClient(Arg);

        }
    }
}

