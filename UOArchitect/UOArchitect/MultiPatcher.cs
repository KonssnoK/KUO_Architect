namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Windows.Forms;
    using UOArchitectInterface;

    public class MultiPatcher
    {
        private ArrayList mComponents;
        private DesignData mDesign;
        private int mFreeSlot = -1;
        private ArrayList mIndexes;
        private ArrayList mItemBlocks;
        private PatchInfo mPatchInfo;

        public MultiPatcher(PatchInfo patchInfo)
        {
            this.mPatchInfo = patchInfo;
            this.mIndexes = new ArrayList(0x2710);
            this.mItemBlocks = new ArrayList(0x4e20);
        }

        private void AddBlocksToMultiMul()
        {
            MultiDataFile file;
            if (this.mDesign.Items.Count > 0)
            {
                file = new MultiDataFile();
                file.ItemID = 1;
                file.X = 0;
                file.Y = 0;
                file.Z = 0;
                file.IsVisible = 0;
                this.mItemBlocks.Add(file);
            }
            foreach (DesignItem item in this.mDesign.Items)
            {
                file = new MultiDataFile();
                file.ItemID = item.ItemID;
                file.X = (short) item.X;
                file.Y = (short) item.Y;
                file.Z = (short) item.Z;
                if ((this.mComponents != null) && this.mComponents.Contains((int) file.ItemID))
                {
                    file.IsVisible = 0;
                }
                else
                {
                    file.IsVisible = 1;
                }
                this.mItemBlocks.Add(file);
            }
        }

        private void GenerateNewMultiIdxFile()
        {
            FileStream output = new FileStream(this.mPatchInfo.MultiIdx, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter writer = new BinaryWriter(output);
            foreach (FileIndex index in this.mIndexes)
            {
                writer.Write(index.Lookup);
                writer.Write(index.Length);
                writer.Write(index.Extra);
            }
            writer.Close();
            output.Close();
        }

        private void GenerateNewMultiMulFile()
        {
            FileStream output = new FileStream(this.mPatchInfo.MultiMul, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter writer = new BinaryWriter(output);
            foreach (MultiDataFile file in this.mItemBlocks)
            {
                writer.Write(file.ItemID);
                writer.Write(file.X);
                writer.Write(file.Y);
                writer.Write(file.Z);
                writer.Write(file.IsVisible);
            }
            writer.Close();
            output.Close();
        }

        private bool LoadSourceMultiIndex()
        {
            FileStream input = new FileStream(this.mPatchInfo.MultiIdx, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(input);
            int num = 0;
            //bool flag = false;
            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    num = ((int) reader.BaseStream.Position) / 12;
                    FileIndex index = new FileIndex();
                    index.Lookup = reader.ReadInt32();
                    index.Length = reader.ReadInt32();
                    index.Extra = reader.ReadInt32();
                    this.mIndexes.Add(index);
                    if ((this.mFreeSlot == -1) && (index.Lookup == -1))
                    {
                        this.mFreeSlot = num;
                    }
                }
                //flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                reader.Close();
                input.Close();
                //flag = false;
            }
            return (this.mIndexes.Count > 0);
        }

        private bool LoadSourceMultiMul()
        {
            FileStream input = new FileStream(this.mPatchInfo.MultiMul, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(input);
           // bool flag = false;
            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    MultiDataFile file = new MultiDataFile();
                    file.ItemID = reader.ReadInt16();
                    file.X = reader.ReadInt16();
                    file.Y = reader.ReadInt16();
                    file.Z = reader.ReadInt16();
                    file.IsVisible = reader.ReadInt32();
                    this.mItemBlocks.Add(file);
                }
                //flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                reader.Close();
                input.Close();
                //flag = false;
            }
            return (this.mIndexes.Count > 0);
        }

        public int PatchMulti(DesignData design, ArrayList Components)
        {
            this.mDesign = design;
            this.mComponents = Components;
            bool flag = this.LoadSourceMultiIndex();
            flag = this.LoadSourceMultiMul();
            if (flag)
            {
                this.UpdateIndex();
                this.AddBlocksToMultiMul();
                this.GenerateNewMultiIdxFile();
                this.GenerateNewMultiMulFile();
            }
            if (flag)
            {
                return this.mFreeSlot;
            }
            return -1;
        }

        private void UpdateIndex()
        {
            FileIndex index = (FileIndex) this.mIndexes[this.mFreeSlot];
            int num = this.mItemBlocks.Count * 12;
            index.Lookup = num;
            index.Length = (this.mDesign.Items.Count + 1) * 12;
            this.mIndexes[this.mFreeSlot] = index;
        }

        public PatchInfo Patch
        {
            get
            {
                return this.mPatchInfo;
            }
        }
    }
}

