namespace UOArchitect
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MultiDataFile
    {
        public short ItemID;
        public short X;
        public short Y;
        public short Z;
        public int IsVisible;
    }
}

