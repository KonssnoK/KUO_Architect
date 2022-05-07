namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using UOArchitectInterface;

    public class BuildDialog : Form
    {
        private bool _defaultHues = false;
        private DesignData _design = null;
        private bool _foundation = false;
        private static int _LastLeft = -1;
        private static int _LastTop = -1;
        private int[] _serials = null;
        private Form _toolbox;
        private Button btnBuild;
        private Button btnGetZ;
        private Button btnToolbox;
        private CheckBox chkFoundation;
        private CheckBox chkHued;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label5;
        private Label lblName;
        private MoveItemControl moveItemControl1;

        public BuildDialog(Form toolbox)
        {
            this.InitializeComponent();
            this._toolbox = toolbox;
            this.moveItemControl1.OnWipeItems = (MoveItemControl.WipeItemsEvent) Delegate.Combine(this.moveItemControl1.OnWipeItems, new MoveItemControl.WipeItemsEvent(this.OnWipeItems));
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (!this._design.IsLoaded)
            {
                this._design.Load();
            }
            BuildResponse response = Connection.BuildDesign(this.GetFilteredItems(this._design.Items, this._foundation, !this._defaultHues));
            this._design.Unload();
            if ((response == null) || (response.Count == 0))
            {
                this.moveItemControl1.Enabled = false;
            }
            else
            {
                this._serials = response.ItemSerials;
                this.moveItemControl1.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnGetZ_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("get z");
        }

        private void btnToolbox_Click(object sender, EventArgs e)
        {
            if (this._toolbox.WindowState != FormWindowState.Normal)
            {
                this._toolbox.WindowState = FormWindowState.Normal;
            }
            this._toolbox.Activate();
            this._toolbox.BringToFront();
        }

        private void BuildDialog_Closing(object sender, CancelEventArgs e)
        {
            _LastLeft = base.Left;
            _LastTop = base.Top;
        }

        private void BuildDialog_Load(object sender, EventArgs e)
        {
            this.moveItemControl1.OnMoveItems = (MoveItemControl.MoveItemsEvent) Delegate.Combine(this.moveItemControl1.OnMoveItems, new MoveItemControl.MoveItemsEvent(this.OnMoveItems));
            this.moveItemControl1.OnNudgeItems = (MoveItemControl.NudgeItemsEvent) Delegate.Combine(this.moveItemControl1.OnNudgeItems, new MoveItemControl.NudgeItemsEvent(this.OnNudgeItems));
            Connection.OnBusy = (Connection.ClientBusyEvent) Delegate.Combine(Connection.OnBusy, new Connection.ClientBusyEvent(this.OnClientBusy));
            Connection.OnConnect = (Connection.ConnectEvent) Delegate.Combine(Connection.OnConnect, new Connection.ConnectEvent(this.OnConnection));
            Connection.OnDisconnect = (Connection.DisconnectEvent) Delegate.Combine(Connection.OnDisconnect, new Connection.DisconnectEvent(this.OnDisconnection));
            Connection.OnReady = (Connection.ClientReadyEvent) Delegate.Combine(Connection.OnReady, new Connection.ClientReadyEvent(this.OnClientReady));
            this.SetPosition();
            this.UpdateControlStates();
            this.btnGetZ.BringToFront();
        }

        private void chkFoundation_CheckedChanged(object sender, EventArgs e)
        {
            this._foundation = this.chkFoundation.Checked;
        }

        private void chkHued_CheckedChanged(object sender, EventArgs e)
        {
            this._defaultHues = !this.chkHued.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private DesignItemCol GetFilteredItems(DesignItemCol items, bool foundation, bool hues)
        {
            DesignItemCol col = new DesignItemCol();
            for (int i = 0; i < items.Count; i++)
            {
                if (foundation || (items[i].Level != 0))
                {
                    int z = foundation ? items[i].Z : (items[i].Z - 7);
                    int itemID = items[i].ItemID;
                    int x = items[i].X;
                    int y = items[i].Y;
                    int hue = 0;
                    if (hues)
                    {
                        hue = items[i].Hue;
                    }
                    col.Add(new DesignItem(itemID, x, y, z, 0, hue));
                }
            }
            return col;
        }

        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.moveItemControl1 = new UOArchitect.MoveItemControl();
			this.btnGetZ = new System.Windows.Forms.Button();
			this.btnBuild = new System.Windows.Forms.Button();
			this.chkFoundation = new System.Windows.Forms.CheckBox();
			this.chkHued = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.btnToolbox = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.moveItemControl1);
			this.groupBox1.Controls.Add(this.btnGetZ);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(4, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(160, 128);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Move Design";
			// 
			// moveItemControl1
			// 
			this.moveItemControl1.Enabled = false;
			this.moveItemControl1.Location = new System.Drawing.Point(8, 16);
			this.moveItemControl1.Name = "moveItemControl1";
			this.moveItemControl1.Size = new System.Drawing.Size(144, 104);
			this.moveItemControl1.TabIndex = 1;
			// 
			// btnGetZ
			// 
			this.btnGetZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGetZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetZ.Location = new System.Drawing.Point(112, 104);
			this.btnGetZ.Name = "btnGetZ";
			this.btnGetZ.Size = new System.Drawing.Size(40, 21);
			this.btnGetZ.TabIndex = 3;
			this.btnGetZ.Text = "Get Z";
			this.btnGetZ.Click += new System.EventHandler(this.btnGetZ_Click);
			// 
			// btnBuild
			// 
			this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBuild.Location = new System.Drawing.Point(88, 176);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Size = new System.Drawing.Size(72, 24);
			this.btnBuild.TabIndex = 2;
			this.btnBuild.Text = "Build";
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			// 
			// chkFoundation
			// 
			this.chkFoundation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkFoundation.Location = new System.Drawing.Point(80, 157);
			this.chkFoundation.Name = "chkFoundation";
			this.chkFoundation.Size = new System.Drawing.Size(88, 16);
			this.chkFoundation.TabIndex = 1;
			this.chkFoundation.Text = "Foundation";
			this.chkFoundation.CheckedChanged += new System.EventHandler(this.chkFoundation_CheckedChanged);
			// 
			// chkHued
			// 
			this.chkHued.Checked = true;
			this.chkHued.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHued.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkHued.Location = new System.Drawing.Point(16, 157);
			this.chkHued.Name = "chkHued";
			this.chkHued.Size = new System.Drawing.Size(56, 16);
			this.chkHued.TabIndex = 0;
			this.chkHued.Text = "Hued";
			this.chkHued.CheckedChanged += new System.EventHandler(this.chkHued_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Name:";
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(56, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(112, 16);
			this.lblName.TabIndex = 5;
			// 
			// btnToolbox
			// 
			this.btnToolbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnToolbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnToolbox.Location = new System.Drawing.Point(8, 176);
			this.btnToolbox.Name = "btnToolbox";
			this.btnToolbox.Size = new System.Drawing.Size(72, 24);
			this.btnToolbox.TabIndex = 7;
			this.btnToolbox.Text = "Toolbox";
			this.btnToolbox.Click += new System.EventHandler(this.btnToolbox_Click);
			// 
			// BuildDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(170, 208);
			this.Controls.Add(this.btnToolbox);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.chkHued);
			this.Controls.Add(this.chkFoundation);
			this.Controls.Add(this.btnBuild);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "BuildDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Build";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.BuildDialog_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.BuildDialog_Closing);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        public void LoadForm(DesignData design)
        {
            if (design != null)
            {
                this._design = design;
                this.lblName.Text = this._design.Name;
                this.moveItemControl1.Enabled = false;
                base.Show();
            }
            else
            {
                base.Close();
            }
        }

        private void OnClientBusy()
        {
            this.UpdateControlStates();
        }

        private void OnClientReady()
        {
            this.UpdateControlStates();
        }

        private void OnConnection()
        {
            this.UpdateControlStates();
        }

        private void OnDisconnection()
        {
            this.UpdateControlStates();
        }

        private void OnMoveItems(short xoffset, short yoffset)
        {
            MoveItemsArgs args = new MoveItemsArgs(this._serials);
            args.Xoffset = xoffset;
            args.Yoffset = yoffset;
            Connection.SendMoveItemsCommand(args);
        }

        private void OnNudgeItems(short zoffset)
        {
            MoveItemsArgs args = new MoveItemsArgs(this._serials);
            args.Zoffset = zoffset;
            Connection.SendMoveItemsCommand(args);
        }

        private void OnWipeItems()
        {
            DeleteCommandArgs args = new DeleteCommandArgs(this._serials);
            Connection.SendDeleteItemsCommand(args);
        }

        private void SetPosition()
        {
            if ((_LastLeft != -1) && (_LastTop != -1))
            {
                base.Left = _LastLeft;
                base.Top = _LastTop;
            }
            else
            {
                base.Top = base.ClientSize.Height / 2;
                base.Left = base.ClientSize.Width / 2;
            }
        }

        public void UpdateControlStates()
        {
            if (Connection.IsConnected && !Connection.IsBusy)
            {
                this.btnBuild.Enabled = true;
                if (this._serials != null)
                {
                    this.moveItemControl1.Enabled = true;
                }
                else
                {
                    this.moveItemControl1.Enabled = false;
                }
            }
            else
            {
                this.moveItemControl1.Enabled = false;
                this.btnBuild.Enabled = false;
            }
        }
    }
}

