namespace UOArchitect
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PatchInfo
    {
        private string mMultiMul;
        private string mMultiIdx;
        public string MultiMul
        {
            get
            {
                return this.mMultiMul;
            }
            set
            {
                this.mMultiMul = value;
            }
        }
        public string MultiIdx
        {
            get
            {
                return this.mMultiIdx;
            }
            set
            {
                this.mMultiIdx = value;
            }
        }
    }
}

