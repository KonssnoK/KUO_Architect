namespace UOArchitect
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FileIndex
    {
        private int mLookup;
        private int mLength;
        private int mExtra;
        public int Lookup
        {
            get
            {
                return this.mLookup;
            }
            set
            {
                this.mLookup = value;
            }
        }
        public int Length
        {
            get
            {
                return this.mLength;
            }
            set
            {
                this.mLength = value;
            }
        }
        public int Extra
        {
            get
            {
                return this.mExtra;
            }
            set
            {
                this.mExtra = value;
            }
        }
    }
}

