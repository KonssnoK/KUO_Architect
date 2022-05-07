namespace UOArchitect.Toolbox.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using UOArchitect;
    using UOArchitectInterface;

    public class DesignPropertyControl : UserControl
    {
        private string _category = "Unassigned";
        private bool _custom = false;
        private int[] _defaultLevelZ = new int[] { 0, 7, 0x1b, 0x2f, 0x43, 0x57, 0x6b };
        private string _designName = "New Design";
        private ArrayList _levelZ = new ArrayList();
        private string _subsection = "Unassigned";
        private Button btnGetLevelZ;
        private ComboBox cboCategory;
        private ComboBox cboLevel;
        private ComboBox cboSubsection;
        private CheckBox checkBox1;
        private CheckBox chkCustomLevels;
        private Container components = null;
        private const int FLOOR_HEIGHT = 20;
        private const int FOUNDATION_HEIGHT = 6;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown numLevelZ;
        private TextBox txtName;

        public DesignPropertyControl()
        {
            this.InitializeComponent();
        }

        private void btnGetLevelZ_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetLocationResp resp = Connection.SendGetLocationRequest(null);
            if (resp != null)
            {
                this.numLevelZ.Value = resp.Z;
                this._levelZ[this.cboLevel.SelectedIndex] = resp.Z;
            }
            Cursor.Current = Cursors.Default;
        }

        private void cboCategory_Leave(object sender, EventArgs e)
        {
            this._category = this.cboCategory.Text.Trim();
            if (this._category.Length == 0)
            {
                this._category = "Unassigned";
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateSubCategoryList(this.cboCategory.Text);
        }

        private void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num = (int) this._levelZ[this.cboLevel.SelectedIndex];
            this.numLevelZ.Value = num;
        }

        private void cboSubsection_Leave(object sender, EventArgs e)
        {
            this._subsection = this.cboSubsection.Text.Trim();
            if (this._subsection.Length == 0)
            {
                this._subsection = "Unassigned";
            }
        }

        private void chkCustomLevels_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCustomLevels.Checked)
            {
                this._custom = true;
                this.PopulateLevelList();
                this.UpdateControlStates();
            }
            else
            {
                this._custom = false;
                this.SetDefaultValues();
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

        private int FindCategoryIndex(string category)
        {
            string str = category.ToLower();
            for (int i = 0; i < this.cboCategory.Items.Count; i++)
            {
                if (this.cboCategory.Items[i].ToString().ToLower() == str)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnGetLevelZ = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.numLevelZ = new System.Windows.Forms.NumericUpDown();
			this.cboLevel = new System.Windows.Forms.ComboBox();
			this.chkCustomLevels = new System.Windows.Forms.CheckBox();
			this.cboSubsection = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cboCategory = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numLevelZ)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnGetLevelZ);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.numLevelZ);
			this.groupBox3.Controls.Add(this.cboLevel);
			this.groupBox3.Controls.Add(this.chkCustomLevels);
			this.groupBox3.Controls.Add(this.cboSubsection);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.txtName);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.cboCategory);
			this.groupBox3.Controls.Add(this.checkBox1);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(176, 144);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Properties";
			// 
			// btnGetLevelZ
			// 
			this.btnGetLevelZ.Enabled = false;
			this.btnGetLevelZ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnGetLevelZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetLevelZ.Location = new System.Drawing.Point(135, 117);
			this.btnGetLevelZ.Name = "btnGetLevelZ";
			this.btnGetLevelZ.Size = new System.Drawing.Size(38, 20);
			this.btnGetLevelZ.TabIndex = 6;
			this.btnGetLevelZ.Text = "Get";
			this.btnGetLevelZ.Click += new System.EventHandler(this.btnGetLevelZ_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(74, 119);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 13);
			this.label5.TabIndex = 22;
			this.label5.Text = "Z";
			// 
			// numLevelZ
			// 
			this.numLevelZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numLevelZ.Enabled = false;
			this.numLevelZ.Location = new System.Drawing.Point(90, 117);
			this.numLevelZ.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.numLevelZ.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
			this.numLevelZ.Name = "numLevelZ";
			this.numLevelZ.Size = new System.Drawing.Size(40, 20);
			this.numLevelZ.TabIndex = 5;
			this.numLevelZ.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			// 
			// cboLevel
			// 
			this.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLevel.Location = new System.Drawing.Point(8, 115);
			this.cboLevel.Name = "cboLevel";
			this.cboLevel.Size = new System.Drawing.Size(64, 21);
			this.cboLevel.TabIndex = 4;
			this.cboLevel.SelectedIndexChanged += new System.EventHandler(this.cboLevel_SelectedIndexChanged);
			// 
			// chkCustomLevels
			// 
			this.chkCustomLevels.Location = new System.Drawing.Point(9, 94);
			this.chkCustomLevels.Name = "chkCustomLevels";
			this.chkCustomLevels.Size = new System.Drawing.Size(160, 16);
			this.chkCustomLevels.TabIndex = 3;
			this.chkCustomLevels.Text = "Set starting z for each level";
			this.chkCustomLevels.CheckedChanged += new System.EventHandler(this.chkCustomLevels_CheckedChanged);
			// 
			// cboSubsection
			// 
			this.cboSubsection.Location = new System.Drawing.Point(64, 64);
			this.cboSubsection.Name = "cboSubsection";
			this.cboSubsection.Size = new System.Drawing.Size(104, 21);
			this.cboSubsection.TabIndex = 2;
			this.cboSubsection.Text = "Unassigned";
			this.cboSubsection.Leave += new System.EventHandler(this.cboSubsection_Leave);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Subsect:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 3;
			this.label6.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtName.Location = new System.Drawing.Point(64, 16);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(104, 20);
			this.txtName.TabIndex = 0;
			this.txtName.Text = "New Design";
			this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 13;
			this.label4.Text = "Category:";
			// 
			// cboCategory
			// 
			this.cboCategory.Location = new System.Drawing.Point(64, 40);
			this.cboCategory.Name = "cboCategory";
			this.cboCategory.Size = new System.Drawing.Size(104, 21);
			this.cboCategory.TabIndex = 1;
			this.cboCategory.Text = "Unassigned";
			this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
			this.cboCategory.Leave += new System.EventHandler(this.cboCategory_Leave);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 96);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(160, 16);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "Set starting z for each level";
			this.checkBox1.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(72, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(14, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Z";
			this.label2.Visible = false;
			// 
			// DesignPropertyControl
			// 
			this.Controls.Add(this.groupBox3);
			this.Name = "DesignPropertyControl";
			this.Size = new System.Drawing.Size(176, 144);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numLevelZ)).EndInit();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Connection.OnBusy = (Connection.ClientBusyEvent) Delegate.Combine(Connection.OnBusy, new Connection.ClientBusyEvent(this.OnClientBusy));
            Connection.OnConnect = (Connection.ConnectEvent) Delegate.Combine(Connection.OnConnect, new Connection.ConnectEvent(this.OnConnection));
            Connection.OnDisconnect = (Connection.DisconnectEvent) Delegate.Combine(Connection.OnDisconnect, new Connection.DisconnectEvent(this.OnDisconnection));
            Connection.OnReady = (Connection.ClientReadyEvent) Delegate.Combine(Connection.OnReady, new Connection.ClientReadyEvent(this.OnClientReady));
            CategoryList.OnRefresh = (CategoryList.CategoryRefresh) Delegate.Combine(CategoryList.OnRefresh, new CategoryList.CategoryRefresh(this.RefreshCategories));
            this.PopulateCategories();
            this.SetDefaultValues();
            this.UpdateControlStates();
        }

        private void PopulateCategories()
        {
            this.cboCategory.Items.Clear();
            string[] categoryNames = CategoryList.GetCategoryNames();
            if (categoryNames != null)
            {
                foreach (string str in categoryNames)
                {
                    this.cboCategory.Items.Add(str);
                }
            }
        }

        private void PopulateLevelList()
        {
            this.cboLevel.BeginUpdate();
            this.cboLevel.Items.Clear();
            for (int i = 0; i < this._levelZ.Count; i++)
            {
                this.cboLevel.Items.Add(string.Format("Level {0}", i + 1));
            }
            this.cboLevel.SelectedIndex = 0;
            this.cboLevel.EndUpdate();
        }

        private void PopulateSubCategoryList(string category)
        {
            string[] subSectionNames = CategoryList.GetSubSectionNames(category);
            this.cboSubsection.Items.Clear();
            if (subSectionNames == null)
            {
                this.cboSubsection.Text = "Unassigned";
            }
            else
            {
                foreach (string str in subSectionNames)
                {
                    this.cboSubsection.Items.Add(str);
                    this.cboSubsection.SelectedIndex = 0;
                }
            }
        }

        public void RefreshCategories()
        {
            this.PopulateCategories();
        }

        private void SetDefaultValues()
        {
            this._levelZ.Clear();
            this._levelZ.AddRange(this._defaultLevelZ);
            this.PopulateLevelList();
            this.UpdateControlStates();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this._designName = this.txtName.Text.Trim();
            if (this._designName.Length == 0)
            {
                this._designName = "New Design";
            }
        }

        public void UpdateControlStates()
        {
            this.chkCustomLevels.Enabled = false;
            if (!this._custom)
            {
                this.cboLevel.Items.Clear();
                this.numLevelZ.Enabled = false;
                this.btnGetLevelZ.Enabled = false;
                this.cboLevel.Enabled = false;
            }
        }

        public string Category
        {
            get
            {
                return this._category;
            }
        }

        public bool CustomLevels
        {
            get
            {
                return this._custom;
            }
        }

        public string DesignName
        {
            get
            {
                return this._designName;
            }
        }

        public int Levels
        {
            get
            {
                return this._levelZ.Count;
            }
        }

        public ArrayList LevelZ
        {
            get
            {
                return this._levelZ;
            }
        }

        public string Subsection
        {
            get
            {
                return this._subsection;
            }
        }
    }
}

