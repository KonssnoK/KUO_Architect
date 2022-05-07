namespace UOArchitect.Toolbox.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using UOArchitect;
    using UOArchitectInterface;

    public class FilterByZControl : UserControl
    {
        private int _maxZ = 0;
        private int _minZ = 0;
        private bool _useMaxZ = false;
        private bool _useMinZ = false;
        private Button btnGetMaxZ;
        private Button btnGetMinZ;
        private CheckBox chkMaxZ;
        private CheckBox chkMinZ;
        private Container components = null;
        private NumericUpDown numMaxZ;
        private NumericUpDown numMinZ;

        public FilterByZControl()
        {
            this.InitializeComponent();
        }

        private void btnGetMaxZ_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetLocationResp resp = Connection.SendGetLocationRequest(null);
            if (resp != null)
            {
                this._maxZ = resp.Z;
                this.numMaxZ.Value = this._maxZ;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnGetMinZ_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetLocationResp resp = Connection.SendGetLocationRequest(null);
            if (resp != null)
            {
                this._minZ = resp.Z;
                this.numMinZ.Value = this._minZ;
            }
            Cursor.Current = Cursors.Default;
        }

        private void chkMaxZ_CheckedChanged(object sender, EventArgs e)
        {
            this._useMaxZ = this.chkMaxZ.Checked;
            this.numMaxZ.Enabled = this.chkMaxZ.Checked;
            this.UpdateControlStates();
        }

        private void chkMinZ_CheckedChanged(object sender, EventArgs e)
        {
            this._useMinZ = this.chkMinZ.Checked;
            this.numMinZ.Enabled = this.chkMinZ.Checked;
            this.UpdateControlStates();
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
			this.btnGetMaxZ = new System.Windows.Forms.Button();
			this.chkMinZ = new System.Windows.Forms.CheckBox();
			this.numMinZ = new System.Windows.Forms.NumericUpDown();
			this.chkMaxZ = new System.Windows.Forms.CheckBox();
			this.numMaxZ = new System.Windows.Forms.NumericUpDown();
			this.btnGetMinZ = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numMinZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).BeginInit();
			this.SuspendLayout();
			// 
			// btnGetMaxZ
			// 
			this.btnGetMaxZ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnGetMaxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetMaxZ.Location = new System.Drawing.Point(110, 24);
			this.btnGetMaxZ.Name = "btnGetMaxZ";
			this.btnGetMaxZ.Size = new System.Drawing.Size(39, 20);
			this.btnGetMaxZ.TabIndex = 5;
			this.btnGetMaxZ.Text = "Max";
			this.btnGetMaxZ.Click += new System.EventHandler(this.btnGetMaxZ_Click);
			// 
			// chkMinZ
			// 
			this.chkMinZ.Location = new System.Drawing.Point(0, 0);
			this.chkMinZ.Name = "chkMinZ";
			this.chkMinZ.Size = new System.Drawing.Size(51, 16);
			this.chkMinZ.TabIndex = 0;
			this.chkMinZ.Text = "Min Z";
			this.chkMinZ.CheckedChanged += new System.EventHandler(this.chkMinZ_CheckedChanged);
			// 
			// numMinZ
			// 
			this.numMinZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numMinZ.Location = new System.Drawing.Point(56, 0);
			this.numMinZ.Maximum = new decimal(new int[] {
            126,
            0,
            0,
            0});
			this.numMinZ.Name = "numMinZ";
			this.numMinZ.Size = new System.Drawing.Size(48, 20);
			this.numMinZ.TabIndex = 1;
			this.numMinZ.Value = new decimal(new int[] {
            126,
            0,
            0,
            0});
			this.numMinZ.ValueChanged += new System.EventHandler(this.numMinZ_ValueChanged);
			this.numMinZ.Leave += new System.EventHandler(this.numMinZ_Leave);
			// 
			// chkMaxZ
			// 
			this.chkMaxZ.Location = new System.Drawing.Point(0, 24);
			this.chkMaxZ.Name = "chkMaxZ";
			this.chkMaxZ.Size = new System.Drawing.Size(56, 16);
			this.chkMaxZ.TabIndex = 3;
			this.chkMaxZ.Text = "Max Z";
			this.chkMaxZ.CheckedChanged += new System.EventHandler(this.chkMaxZ_CheckedChanged);
			// 
			// numMaxZ
			// 
			this.numMaxZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numMaxZ.Location = new System.Drawing.Point(56, 24);
			this.numMaxZ.Maximum = new decimal(new int[] {
            126,
            0,
            0,
            0});
			this.numMaxZ.Name = "numMaxZ";
			this.numMaxZ.Size = new System.Drawing.Size(48, 20);
			this.numMaxZ.TabIndex = 4;
			this.numMaxZ.Value = new decimal(new int[] {
            126,
            0,
            0,
            0});
			this.numMaxZ.ValueChanged += new System.EventHandler(this.numMaxZ_ValueChanged);
			this.numMaxZ.Leave += new System.EventHandler(this.numMaxZ_Leave);
			// 
			// btnGetMinZ
			// 
			this.btnGetMinZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnGetMinZ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnGetMinZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetMinZ.Location = new System.Drawing.Point(110, 0);
			this.btnGetMinZ.Name = "btnGetMinZ";
			this.btnGetMinZ.Size = new System.Drawing.Size(39, 20);
			this.btnGetMinZ.TabIndex = 2;
			this.btnGetMinZ.Text = "Min";
			this.btnGetMinZ.Click += new System.EventHandler(this.btnGetMinZ_Click);
			// 
			// FilterByZControl
			// 
			this.Controls.Add(this.btnGetMaxZ);
			this.Controls.Add(this.btnGetMinZ);
			this.Controls.Add(this.chkMinZ);
			this.Controls.Add(this.numMinZ);
			this.Controls.Add(this.chkMaxZ);
			this.Controls.Add(this.numMaxZ);
			this.Name = "FilterByZControl";
			this.Size = new System.Drawing.Size(152, 45);
			((System.ComponentModel.ISupportInitialize)(this.numMinZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMaxZ)).EndInit();
			this.ResumeLayout(false);

        }

        private void numMaxZ_Leave(object sender, EventArgs e)
        {
            int num = Utility.ToInt(this.numMaxZ.Text);
            if (num != ((int) this.numMaxZ.Value))
            {
                this.numMaxZ.Value = num;
            }
        }

        private void numMaxZ_ValueChanged(object sender, EventArgs e)
        {
            this._maxZ = (int) this.numMaxZ.Value;
        }

        private void numMinZ_Leave(object sender, EventArgs e)
        {
            int num = Utility.ToInt(this.numMinZ.Text);
            if (num != ((int) this.numMinZ.Value))
            {
                this.numMinZ.Value = num;
            }
        }

        private void numMinZ_ValueChanged(object sender, EventArgs e)
        {
            this._minZ = (int) this.numMinZ.Value;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Connection.OnBusy = (Connection.ClientBusyEvent) Delegate.Combine(Connection.OnBusy, new Connection.ClientBusyEvent(this.OnClientBusy));
            Connection.OnConnect = (Connection.ConnectEvent) Delegate.Combine(Connection.OnConnect, new Connection.ConnectEvent(this.OnConnection));
            Connection.OnDisconnect = (Connection.DisconnectEvent) Delegate.Combine(Connection.OnDisconnect, new Connection.DisconnectEvent(this.OnDisconnection));
            Connection.OnReady = (Connection.ClientReadyEvent) Delegate.Combine(Connection.OnReady, new Connection.ClientReadyEvent(this.OnClientReady));
            this.UpdateControlStates();
        }

        public void UpdateControlStates()
        {
            if (this._useMinZ)
            {
                this.numMinZ.Enabled = true;
                this.btnGetMinZ.Enabled = Connection.IsConnected && !Connection.IsBusy;
            }
            else
            {
                this.numMinZ.Enabled = false;
                this.btnGetMinZ.Enabled = false;
            }
            if (this._useMaxZ)
            {
                this.numMaxZ.Enabled = true;
                this.btnGetMaxZ.Enabled = Connection.IsConnected && !Connection.IsBusy;
            }
            else
            {
                this.numMaxZ.Enabled = false;
                this.btnGetMaxZ.Enabled = false;
            }
        }

        public int MaxZ
        {
            get
            {
                int num = this._maxZ;
                if (this._useMinZ && (this._maxZ < this._minZ))
                {
                    num = this._minZ;
                }
                return num;
            }
        }

        public int MinZ
        {
            get
            {
                int num = this._minZ;
                if (this._useMaxZ && (this._minZ > this._maxZ))
                {
                    num = this._maxZ;
                }
                return num;
            }
        }

        public bool UseMaxZ
        {
            get
            {
                return this._useMaxZ;
            }
        }

        public bool UseMinZ
        {
            get
            {
                return this._useMinZ;
            }
        }
    }
}

