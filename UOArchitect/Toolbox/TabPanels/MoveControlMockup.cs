namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class MoveControlMockup : UserControl
    {
        private int _moveAmount = 1;
        private short _nudgeAmount = 1;
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
        private Container components = null;
        public MoveItemsEvent OnMoveItems;
        public NudgeItemsEvent OnNudgeItems;
        private TextBox txtMoveAmount;
        private TextBox txtNudgeAmount;

        public MoveControlMockup()
        {
            this.InitializeComponent();
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.E, this._moveAmount);
        }

        private void btnLower_Click_1(object sender, EventArgs e)
        {
            if (this.OnNudgeItems != null)
            {
                this.OnNudgeItems((short) (this._nudgeAmount * -1));
            }
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.N, this._moveAmount);
        }

        private void btnNE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.NE, this._moveAmount);
        }

        private void btnNW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.NW, this._moveAmount);
        }

        private void btnRaise_Click(object sender, EventArgs e)
        {
            if (this.OnNudgeItems != null)
            {
                this.OnNudgeItems(this._nudgeAmount);
            }
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.S, this._moveAmount);
        }

        private void btnSE_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.SE, this._moveAmount);
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.SW, this._moveAmount);
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            this.MoveItems(MoveDirections.W, this._moveAmount);
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
			this.txtNudgeAmount = new System.Windows.Forms.TextBox();
			this.txtMoveAmount = new System.Windows.Forms.TextBox();
			this.btnLower = new System.Windows.Forms.Button();
			this.btnRaise = new System.Windows.Forms.Button();
			this.btnNW = new System.Windows.Forms.Button();
			this.btnW = new System.Windows.Forms.Button();
			this.btnSW = new System.Windows.Forms.Button();
			this.btnS = new System.Windows.Forms.Button();
			this.btnSE = new System.Windows.Forms.Button();
			this.btnE = new System.Windows.Forms.Button();
			this.btnNE = new System.Windows.Forms.Button();
			this.btnN = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtNudgeAmount
			// 
			this.txtNudgeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNudgeAmount.Location = new System.Drawing.Point(104, 26);
			this.txtNudgeAmount.Name = "txtNudgeAmount";
			this.txtNudgeAmount.Size = new System.Drawing.Size(32, 20);
			this.txtNudgeAmount.TabIndex = 29;
			this.txtNudgeAmount.Text = "1";
			this.txtNudgeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtNudgeAmount.Leave += new System.EventHandler(this.txtNudgeAmount_Leave);
			// 
			// txtMoveAmount
			// 
			this.txtMoveAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMoveAmount.Location = new System.Drawing.Point(32, 26);
			this.txtMoveAmount.Name = "txtMoveAmount";
			this.txtMoveAmount.Size = new System.Drawing.Size(32, 20);
			this.txtMoveAmount.TabIndex = 28;
			this.txtMoveAmount.Text = "1";
			this.txtMoveAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtMoveAmount.Leave += new System.EventHandler(this.txtMoveAmount_Leave);
			// 
			// btnLower
			// 
			this.btnLower.BackgroundImage = global::Properties.Resources.Gump_4504;
			this.btnLower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnLower.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnLower.Location = new System.Drawing.Point(104, 48);
			this.btnLower.Name = "btnLower";
			this.btnLower.Size = new System.Drawing.Size(32, 24);
			this.btnLower.TabIndex = 27;
			this.btnLower.Click += new System.EventHandler(this.btnLower_Click_1);
			// 
			// btnRaise
			// 
			this.btnRaise.BackgroundImage = global::Properties.Resources.Gump_4500;
			this.btnRaise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnRaise.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaise.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnRaise.Location = new System.Drawing.Point(104, 0);
			this.btnRaise.Name = "btnRaise";
			this.btnRaise.Size = new System.Drawing.Size(32, 24);
			this.btnRaise.TabIndex = 26;
			this.btnRaise.Click += new System.EventHandler(this.btnRaise_Click);
			// 
			// btnNW
			// 
			this.btnNW.BackgroundImage = global::Properties.Resources.Gump_4507;
			this.btnNW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnNW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnNW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNW.Location = new System.Drawing.Point(0, 0);
			this.btnNW.Name = "btnNW";
			this.btnNW.Size = new System.Drawing.Size(32, 24);
			this.btnNW.TabIndex = 25;
			this.btnNW.Click += new System.EventHandler(this.btnNW_Click);
			// 
			// btnW
			// 
			this.btnW.BackgroundImage = global::Properties.Resources.Gump_4506;
			this.btnW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnW.Location = new System.Drawing.Point(0, 24);
			this.btnW.Name = "btnW";
			this.btnW.Size = new System.Drawing.Size(32, 24);
			this.btnW.TabIndex = 24;
			this.btnW.Click += new System.EventHandler(this.btnW_Click);
			// 
			// btnSW
			// 
			this.btnSW.BackgroundImage = global::Properties.Resources.Gump_4505;
			this.btnSW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnSW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSW.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSW.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSW.Location = new System.Drawing.Point(0, 48);
			this.btnSW.Name = "btnSW";
			this.btnSW.Size = new System.Drawing.Size(32, 24);
			this.btnSW.TabIndex = 23;
			this.btnSW.Click += new System.EventHandler(this.btnSW_Click);
			// 
			// btnS
			// 
			this.btnS.BackgroundImage = global::Properties.Resources.Gump_4504;
			this.btnS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnS.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnS.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnS.Location = new System.Drawing.Point(32, 48);
			this.btnS.Name = "btnS";
			this.btnS.Size = new System.Drawing.Size(32, 24);
			this.btnS.TabIndex = 22;
			this.btnS.Click += new System.EventHandler(this.btnS_Click);
			// 
			// btnSE
			// 
			this.btnSE.BackgroundImage = global::Properties.Resources.Gump_4503;
			this.btnSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnSE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSE.Location = new System.Drawing.Point(64, 48);
			this.btnSE.Name = "btnSE";
			this.btnSE.Size = new System.Drawing.Size(32, 24);
			this.btnSE.TabIndex = 21;
			this.btnSE.Click += new System.EventHandler(this.btnSE_Click);
			// 
			// btnE
			// 
			this.btnE.BackgroundImage = global::Properties.Resources.Gump_4502;
			this.btnE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnE.Location = new System.Drawing.Point(64, 24);
			this.btnE.Name = "btnE";
			this.btnE.Size = new System.Drawing.Size(32, 24);
			this.btnE.TabIndex = 20;
			this.btnE.Click += new System.EventHandler(this.btnE_Click);
			// 
			// btnNE
			// 
			this.btnNE.BackgroundImage = global::Properties.Resources.Gump_4501;
			this.btnNE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnNE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnNE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNE.Location = new System.Drawing.Point(64, 0);
			this.btnNE.Name = "btnNE";
			this.btnNE.Size = new System.Drawing.Size(32, 24);
			this.btnNE.TabIndex = 19;
			this.btnNE.Click += new System.EventHandler(this.btnNE_Click);
			// 
			// btnN
			// 
			this.btnN.BackgroundImage = global::Properties.Resources.Gump_4500;
			this.btnN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnN.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnN.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnN.Location = new System.Drawing.Point(32, 0);
			this.btnN.Name = "btnN";
			this.btnN.Size = new System.Drawing.Size(32, 24);
			this.btnN.TabIndex = 18;
			this.btnN.Click += new System.EventHandler(this.btnN_Click);
			// 
			// MoveControlMockup
			// 
			this.Controls.Add(this.txtNudgeAmount);
			this.Controls.Add(this.txtMoveAmount);
			this.Controls.Add(this.btnLower);
			this.Controls.Add(this.btnRaise);
			this.Controls.Add(this.btnNW);
			this.Controls.Add(this.btnW);
			this.Controls.Add(this.btnSW);
			this.Controls.Add(this.btnS);
			this.Controls.Add(this.btnSE);
			this.Controls.Add(this.btnE);
			this.Controls.Add(this.btnNE);
			this.Controls.Add(this.btnN);
			this.Name = "MoveControlMockup";
			this.Size = new System.Drawing.Size(136, 72);
			this.ResumeLayout(false);
			this.PerformLayout();

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

        private void txtMoveAmount_Leave(object sender, EventArgs e)
        {
            int num = 1;
            try
            {
                num = int.Parse(this.txtMoveAmount.Text.Trim());
                if (num < 1)
                {
                    num = 1;
                }
            }
            catch
            {
                num = 1;
            }
            this._moveAmount = num;
            this.txtMoveAmount.Text = this._moveAmount.ToString();
        }

        private void txtNudgeAmount_Leave(object sender, EventArgs e)
        {
            int num = 1;
            try
            {
                num = int.Parse(this.txtNudgeAmount.Text.Trim());
                if (num < 1)
                {
                    num = 1;
                }
            }
            catch
            {
                num = 1;
            }
            this._nudgeAmount = (short) num;
            this.txtNudgeAmount.Text = this._nudgeAmount.ToString();
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
    }
}

