namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using UOArchitect.Toolbox.Controls;

    public class ExtractPanel : UserControl
    {
        private bool _busy = false;
        private DesignData _extractedDesign = null;
        private bool _foundation = false;
        private bool _frozen = true;
        private bool _hues = true;
        private bool _nonStatic = true;
        private bool _static = true;
        private Button btnExtract;
        private Button btnTele;
        private Button btnWipeArea;
        private CheckBox chkExtractHues;
        private CheckBox chkFoundation;
        private CheckBox chkFrozen;
        private CheckBox chkNonStatic;
        private CheckBox chkStatic;
        private Container components = null;
        private FilterByZControl ctlFilterZ;
        private DesignPropertyControl ctlProperties;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private Label lblResults;

        public ExtractPanel()
        {
            this.InitializeComponent();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ExtractDesign(false);
        }

        private void btnExtract_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ExtractDesign(true);
            }
        }

        private void btnTele_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("tele");
        }

        private void btnWipeArea_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("WipeItems");
        }

        private void chkExtractHues_CheckedChanged(object sender, EventArgs e)
        {
            this._hues = this.chkExtractHues.Checked;
        }

        private void chkFoundation_CheckedChanged(object sender, EventArgs e)
        {
            this._foundation = this.chkFoundation.Checked;
        }

        private void chkFrozen_CheckedChanged(object sender, EventArgs e)
        {
            this._frozen = this.chkFrozen.Checked;
        }

        private void chkNonStatic_CheckedChanged(object sender, EventArgs e)
        {
            this._nonStatic = this.chkNonStatic.Checked;
        }

        private void chkStatic_CheckedChanged(object sender, EventArgs e)
        {
            this._static = this.chkStatic.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExtractDesign(bool multipleRects)
        {
            ItemExtracter extracter = new ItemExtracter();
            extracter.UseMaxZ = this.ctlFilterZ.UseMaxZ;
            extracter.UseMinZ = this.ctlFilterZ.UseMinZ;
            extracter.MaxZ = this.ctlFilterZ.MaxZ;
            extracter.MinZ = this.ctlFilterZ.MinZ;
            extracter.Name = this.ctlProperties.DesignName;
            extracter.Category = this.ctlProperties.Category;
            extracter.Subsection = this.ctlProperties.Subsection;
            extracter.NonStatic = this._nonStatic;
            extracter.Static = this._static;
            extracter.Frozen = this._frozen;
            extracter.Foundation = this._foundation;
            extracter.Hues = this._hues;
            extracter.MultipleRects = multipleRects;
            if (this.ctlProperties.CustomLevels)
            {
                extracter.LevelZ = (int[]) this.ctlProperties.LevelZ.ToArray(typeof(int));
            }
            extracter.OnExtracted = (ItemExtracter.DesignExtractEvent) Delegate.Combine(extracter.OnExtracted, new ItemExtracter.DesignExtractEvent(this.OnExtracted));
            extracter.ExtractDesign();
            this.WaitForExtract();
            extracter.OnExtracted = (ItemExtracter.DesignExtractEvent) Delegate.Remove(extracter.OnExtracted, new ItemExtracter.DesignExtractEvent(this.OnExtracted));
            if (this._extractedDesign != null)
            {
                this._extractedDesign.Save();
            }
            this.UpdateControlStates();
        }

        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkNonStatic = new System.Windows.Forms.CheckBox();
			this.chkStatic = new System.Windows.Forms.CheckBox();
			this.chkFrozen = new System.Windows.Forms.CheckBox();
			this.chkExtractHues = new System.Windows.Forms.CheckBox();
			this.chkFoundation = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ctlFilterZ = new UOArchitect.Toolbox.Controls.FilterByZControl();
			this.lblResults = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnExtract = new System.Windows.Forms.Button();
			this.btnTele = new System.Windows.Forms.Button();
			this.btnWipeArea = new System.Windows.Forms.Button();
			this.ctlProperties = new UOArchitect.Toolbox.Controls.DesignPropertyControl();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkNonStatic);
			this.groupBox1.Controls.Add(this.chkStatic);
			this.groupBox1.Controls.Add(this.chkFrozen);
			this.groupBox1.Controls.Add(this.chkExtractHues);
			this.groupBox1.Controls.Add(this.chkFoundation);
			this.groupBox1.Location = new System.Drawing.Point(8, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 87);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// chkNonStatic
			// 
			this.chkNonStatic.Checked = true;
			this.chkNonStatic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkNonStatic.Location = new System.Drawing.Point(8, 38);
			this.chkNonStatic.Name = "chkNonStatic";
			this.chkNonStatic.Size = new System.Drawing.Size(80, 16);
			this.chkNonStatic.TabIndex = 3;
			this.chkNonStatic.Text = "Non-Static";
			this.chkNonStatic.CheckedChanged += new System.EventHandler(this.chkNonStatic_CheckedChanged);
			// 
			// chkStatic
			// 
			this.chkStatic.Checked = true;
			this.chkStatic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatic.Location = new System.Drawing.Point(8, 15);
			this.chkStatic.Name = "chkStatic";
			this.chkStatic.Size = new System.Drawing.Size(56, 21);
			this.chkStatic.TabIndex = 1;
			this.chkStatic.Text = "Static";
			this.chkStatic.CheckedChanged += new System.EventHandler(this.chkStatic_CheckedChanged);
			// 
			// chkFrozen
			// 
			this.chkFrozen.Checked = true;
			this.chkFrozen.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFrozen.Location = new System.Drawing.Point(77, 15);
			this.chkFrozen.Name = "chkFrozen";
			this.chkFrozen.Size = new System.Drawing.Size(96, 21);
			this.chkFrozen.TabIndex = 2;
			this.chkFrozen.Text = "Frozen (muls)";
			this.chkFrozen.CheckedChanged += new System.EventHandler(this.chkFrozen_CheckedChanged);
			// 
			// chkExtractHues
			// 
			this.chkExtractHues.Checked = true;
			this.chkExtractHues.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExtractHues.Location = new System.Drawing.Point(100, 37);
			this.chkExtractHues.Name = "chkExtractHues";
			this.chkExtractHues.Size = new System.Drawing.Size(51, 21);
			this.chkExtractHues.TabIndex = 4;
			this.chkExtractHues.Text = "Hues";
			this.chkExtractHues.CheckedChanged += new System.EventHandler(this.chkExtractHues_CheckedChanged);
			// 
			// chkFoundation
			// 
			this.chkFoundation.Location = new System.Drawing.Point(8, 59);
			this.chkFoundation.Name = "chkFoundation";
			this.chkFoundation.Size = new System.Drawing.Size(104, 21);
			this.chkFoundation.TabIndex = 5;
			this.chkFoundation.Text = "Has Foundation";
			this.chkFoundation.CheckedChanged += new System.EventHandler(this.chkFoundation_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ctlFilterZ);
			this.groupBox2.Location = new System.Drawing.Point(8, 103);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 72);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Item Z Filter";
			// 
			// ctlFilterZ
			// 
			this.ctlFilterZ.Location = new System.Drawing.Point(13, 17);
			this.ctlFilterZ.Name = "ctlFilterZ";
			this.ctlFilterZ.Size = new System.Drawing.Size(152, 45);
			this.ctlFilterZ.TabIndex = 0;
			// 
			// lblResults
			// 
			this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblResults.ForeColor = System.Drawing.Color.Blue;
			this.lblResults.Location = new System.Drawing.Point(56, 0);
			this.lblResults.Name = "lblResults";
			this.lblResults.Size = new System.Drawing.Size(128, 16);
			this.lblResults.TabIndex = 9;
			this.lblResults.Text = "0 Items Extracted";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Results:";
			// 
			// btnExtract
			// 
			this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnExtract.Location = new System.Drawing.Point(121, 326);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(56, 23);
			this.btnExtract.TabIndex = 10;
			this.btnExtract.Text = "Extract";
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			this.btnExtract.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnExtract_MouseUp);
			// 
			// btnTele
			// 
			this.btnTele.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnTele.Location = new System.Drawing.Point(66, 326);
			this.btnTele.Name = "btnTele";
			this.btnTele.Size = new System.Drawing.Size(56, 23);
			this.btnTele.TabIndex = 9;
			this.btnTele.Text = "Tele";
			this.btnTele.Click += new System.EventHandler(this.btnTele_Click);
			// 
			// btnWipeArea
			// 
			this.btnWipeArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnWipeArea.Location = new System.Drawing.Point(11, 326);
			this.btnWipeArea.Name = "btnWipeArea";
			this.btnWipeArea.Size = new System.Drawing.Size(56, 23);
			this.btnWipeArea.TabIndex = 8;
			this.btnWipeArea.Text = "Wipe";
			this.btnWipeArea.Click += new System.EventHandler(this.btnWipeArea_Click);
			// 
			// ctlProperties
			// 
			this.ctlProperties.Location = new System.Drawing.Point(8, 177);
			this.ctlProperties.Name = "ctlProperties";
			this.ctlProperties.Size = new System.Drawing.Size(176, 143);
			this.ctlProperties.TabIndex = 7;
			// 
			// ExtractPanel
			// 
			this.Controls.Add(this.ctlProperties);
			this.Controls.Add(this.btnWipeArea);
			this.Controls.Add(this.btnTele);
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.lblResults);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ExtractPanel";
			this.Size = new System.Drawing.Size(192, 360);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

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

        private void OnExtracted(DesignData design)
        {
            this._extractedDesign = design;
            this._busy = false;
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
            this.ctlFilterZ.UpdateControlStates();
            this.ctlProperties.UpdateControlStates();
            if ((!this._busy && Connection.IsConnected) && !Connection.IsBusy)
            {
                this.btnExtract.Enabled = true;
            }
            else
            {
                this.btnExtract.Enabled = false;
            }
            if (this._busy || Connection.IsBusy)
            {
                this.btnTele.Enabled = false;
                this.btnWipeArea.Enabled = false;
            }
            else
            {
                this.btnTele.Enabled = true;
                this.btnWipeArea.Enabled = true;
            }
            int num = (this._extractedDesign != null) ? this._extractedDesign.Items.Count : 0;
            this.lblResults.Text = string.Format("{0} items extracted", num);
        }

        private void WaitForExtract()
        {
            this.btnExtract.Enabled = false;
            this.btnTele.Enabled = false;
            this._busy = true;
            while (this._busy)
            {
                Application.DoEvents();
                Thread.Sleep(50);
            }
        }
    }
}

