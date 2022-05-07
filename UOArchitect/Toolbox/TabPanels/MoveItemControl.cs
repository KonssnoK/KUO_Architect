namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class MoveItemControl : UserControl
    {
        private Button btnE;
        private Button btnLower;
        private Button btnN;
        private Button btnNE;
        private Button btnNW;
        private Button btnRaise;
        private Button btnS;
        private Button btnSE;
        private Button btnSW;
        private Button btnW;
        private Button btnWipe;
        private Container components = null;
        private Label label1;
        private NumericUpDown numMoveSteps;
        private NumericUpDown numZAmount;
        public MoveItemsEvent OnMoveItems;
        public NudgeItemsEvent OnNudgeItems;
        public WipeItemsEvent OnWipeItems;

        public MoveItemControl()
        {
            this.InitializeComponent();
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.E, (int) this.numMoveSteps.Value);
        }

        private void btnLower_Click(object sender, EventArgs e)
        {
            if (this.OnNudgeItems != null)
            {
                this.OnNudgeItems((short) (this.numZAmount.Value * -1M));
            }
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.N, (int) this.numMoveSteps.Value);
        }

        private void btnNE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.NE, (int) this.numMoveSteps.Value);
        }

        private void btnNW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.NW, (int) this.numMoveSteps.Value);
        }

        private void btnRaise_Click(object sender, EventArgs e)
        {
            if (this.OnNudgeItems != null)
            {
                this.OnNudgeItems((short) this.numZAmount.Value);
            }
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.S, (int) this.numMoveSteps.Value);
        }

        private void btnSE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.SE, (int) this.numMoveSteps.Value);
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.SW, (int) this.numMoveSteps.Value);
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.W, (int) this.numMoveSteps.Value);
        }

        private void btnWipe_Click(object sender, EventArgs e)
        {
            if (this.OnWipeItems != null)
            {
                this.OnWipeItems();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveItemControl));
			this.btnN = new System.Windows.Forms.Button();
			this.btnNE = new System.Windows.Forms.Button();
			this.btnE = new System.Windows.Forms.Button();
			this.btnSE = new System.Windows.Forms.Button();
			this.btnS = new System.Windows.Forms.Button();
			this.btnSW = new System.Windows.Forms.Button();
			this.btnW = new System.Windows.Forms.Button();
			this.btnNW = new System.Windows.Forms.Button();
			this.btnWipe = new System.Windows.Forms.Button();
			this.numMoveSteps = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRaise = new System.Windows.Forms.Button();
			this.btnLower = new System.Windows.Forms.Button();
			this.numZAmount = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numMoveSteps)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numZAmount)).BeginInit();
			this.SuspendLayout();
			// 
			// btnN
			// 
			this.btnN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnN.BackgroundImage")));
			this.btnN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnN.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnN.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnN.Location = new System.Drawing.Point(32, 0);
			this.btnN.Name = "btnN";
			this.btnN.Size = new System.Drawing.Size(32, 24);
			this.btnN.TabIndex = 0;
			this.btnN.Click += new System.EventHandler(this.btnN_Click);
			// 
			// btnNE
			// 
			this.btnNE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNE.BackgroundImage")));
			this.btnNE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnNE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNE.Location = new System.Drawing.Point(64, 0);
			this.btnNE.Name = "btnNE";
			this.btnNE.Size = new System.Drawing.Size(32, 24);
			this.btnNE.TabIndex = 1;
			this.btnNE.Click += new System.EventHandler(this.btnNE_Click);
			// 
			// btnE
			// 
			this.btnE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnE.BackgroundImage")));
			this.btnE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnE.Location = new System.Drawing.Point(64, 24);
			this.btnE.Name = "btnE";
			this.btnE.Size = new System.Drawing.Size(32, 24);
			this.btnE.TabIndex = 2;
			this.btnE.Click += new System.EventHandler(this.btnE_Click);
			// 
			// btnSE
			// 
			this.btnSE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSE.BackgroundImage")));
			this.btnSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSE.Location = new System.Drawing.Point(64, 48);
			this.btnSE.Name = "btnSE";
			this.btnSE.Size = new System.Drawing.Size(32, 24);
			this.btnSE.TabIndex = 3;
			this.btnSE.Click += new System.EventHandler(this.btnSE_Click);
			// 
			// btnS
			// 
			this.btnS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnS.BackgroundImage")));
			this.btnS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnS.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnS.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnS.Location = new System.Drawing.Point(32, 48);
			this.btnS.Name = "btnS";
			this.btnS.Size = new System.Drawing.Size(32, 24);
			this.btnS.TabIndex = 4;
			this.btnS.Click += new System.EventHandler(this.btnS_Click);
			// 
			// btnSW
			// 
			this.btnSW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSW.BackgroundImage")));
			this.btnSW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSW.Location = new System.Drawing.Point(0, 48);
			this.btnSW.Name = "btnSW";
			this.btnSW.Size = new System.Drawing.Size(32, 24);
			this.btnSW.TabIndex = 5;
			this.btnSW.Click += new System.EventHandler(this.btnSW_Click);
			// 
			// btnW
			// 
			this.btnW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnW.BackgroundImage")));
			this.btnW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnW.Location = new System.Drawing.Point(0, 24);
			this.btnW.Name = "btnW";
			this.btnW.Size = new System.Drawing.Size(32, 24);
			this.btnW.TabIndex = 6;
			this.btnW.Click += new System.EventHandler(this.btnW_Click);
			// 
			// btnNW
			// 
			this.btnNW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNW.BackgroundImage")));
			this.btnNW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnNW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNW.Location = new System.Drawing.Point(0, 0);
			this.btnNW.Name = "btnNW";
			this.btnNW.Size = new System.Drawing.Size(32, 24);
			this.btnNW.TabIndex = 7;
			this.btnNW.Click += new System.EventHandler(this.btnNW_Click);
			// 
			// btnWipe
			// 
			this.btnWipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWipe.Location = new System.Drawing.Point(32, 24);
			this.btnWipe.Name = "btnWipe";
			this.btnWipe.Size = new System.Drawing.Size(32, 24);
			this.btnWipe.TabIndex = 8;
			this.btnWipe.Text = "W";
			this.btnWipe.Click += new System.EventHandler(this.btnWipe_Click);
			// 
			// numMoveSteps
			// 
			this.numMoveSteps.Location = new System.Drawing.Point(0, 80);
			this.numMoveSteps.Name = "numMoveSteps";
			this.numMoveSteps.Size = new System.Drawing.Size(40, 20);
			this.numMoveSteps.TabIndex = 9;
			this.numMoveSteps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numMoveSteps.ValueChanged += new System.EventHandler(this.numMoveSteps_ValueChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(46, 82);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Move";
			// 
			// btnRaise
			// 
			this.btnRaise.BackgroundImage = global::Properties.Resources.Gump_4500;
			this.btnRaise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnRaise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaise.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnRaise.Location = new System.Drawing.Point(104, 0);
			this.btnRaise.Name = "btnRaise";
			this.btnRaise.Size = new System.Drawing.Size(40, 28);
			this.btnRaise.TabIndex = 11;
			this.btnRaise.Click += new System.EventHandler(this.btnRaise_Click);
			// 
			// btnLower
			// 
			this.btnLower.BackgroundImage = global::Properties.Resources.Gump_4504;
			this.btnLower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnLower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnLower.Location = new System.Drawing.Point(104, 29);
			this.btnLower.Name = "btnLower";
			this.btnLower.Size = new System.Drawing.Size(40, 28);
			this.btnLower.TabIndex = 12;
			this.btnLower.Click += new System.EventHandler(this.btnLower_Click);
			// 
			// numZAmount
			// 
			this.numZAmount.Location = new System.Drawing.Point(104, 65);
			this.numZAmount.Name = "numZAmount";
			this.numZAmount.Size = new System.Drawing.Size(40, 20);
			this.numZAmount.TabIndex = 15;
			this.numZAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numZAmount.ValueChanged += new System.EventHandler(this.numZAmount_ValueChanged);
			// 
			// MoveItemControl
			// 
			this.Controls.Add(this.numZAmount);
			this.Controls.Add(this.btnLower);
			this.Controls.Add(this.btnRaise);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numMoveSteps);
			this.Controls.Add(this.btnWipe);
			this.Controls.Add(this.btnNW);
			this.Controls.Add(this.btnW);
			this.Controls.Add(this.btnSW);
			this.Controls.Add(this.btnS);
			this.Controls.Add(this.btnSE);
			this.Controls.Add(this.btnE);
			this.Controls.Add(this.btnNE);
			this.Controls.Add(this.btnN);
			this.Name = "MoveItemControl";
			this.Size = new System.Drawing.Size(152, 104);
			((System.ComponentModel.ISupportInitialize)(this.numMoveSteps)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numZAmount)).EndInit();
			this.ResumeLayout(false);

        }

        private void MoveItems(MoveDirections direction, int amount)
        {
            int num = 0;
            int num2 = 0;
            switch (direction)
            {
                case MoveDirections.N:
                    num = -1 * amount;
                    num2 = -1 * amount;
                    break;

                case MoveDirections.NE:
                    num = 0;
                    num2 = -1 * amount;
                    break;

                case MoveDirections.E:
                    num = amount;
                    num2 = -1 * amount;
                    break;

                case MoveDirections.SE:
                    num = amount;
                    num2 = 0;
                    break;

                case MoveDirections.S:
                    num = amount;
                    num2 = amount;
                    break;

                case MoveDirections.SW:
                    num = 0;
                    num2 = amount;
                    break;

                case MoveDirections.W:
                    num = -1 * amount;
                    num2 = amount;
                    break;

                case MoveDirections.NW:
                    num = -1 * amount;
                    num2 = 0;
                    break;
            }
            if (this.OnMoveItems != null)
            {
                this.OnMoveItems((short) num, (short) num2);
            }
        }

        private void numMoveSteps_ValueChanged(object sender, EventArgs e)
        {
            int num = (int) this.numMoveSteps.Value;
            if (num <= 0)
            {
                this.numMoveSteps.Value = 1M;
            }
        }

        private void numZAmount_ValueChanged(object sender, EventArgs e)
        {
            int num = (int) this.numZAmount.Value;
            if (num <= 0)
            {
                this.numZAmount.Value = 1M;
            }
        }

        public enum MoveDirections
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }

        public delegate void MoveItemsEvent(short xoffset, short yoffset);

        public delegate void NudgeItemsEvent(short zoffset);

        public delegate void WipeItemsEvent();
    }
}

