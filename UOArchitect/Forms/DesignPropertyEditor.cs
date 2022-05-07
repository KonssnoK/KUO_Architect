namespace UOArchitect
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DesignPropertyEditor : Form
    {
        private Button btnCancel;
        private Button btnSave;
        private ComboBox cboCategory;
        private ComboBox cboSubSection;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private bool m_Cancelled = true;
        private string m_Category;
        private string m_Name;
        private string m_Subsection;
        private TextBox txtName = new TextBox();

        public DesignPropertyEditor()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.m_Cancelled = false;
            base.Close();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateSubCategoryList(this.cboCategory.Text);
        }

        private void cboCategory_TextChanged(object sender, EventArgs e)
        {
            this.SelectCategory(this.cboCategory.Text);
            this.m_Category = this.cboCategory.Text.Trim();
        }

        private void cboSubSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Subsection = this.cboSubSection.Text.Trim();
        }

        private void cboSubSection_TextChanged(object sender, EventArgs e)
        {
            this.m_Subsection = this.cboSubSection.Text.Trim();
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboSubSection = new System.Windows.Forms.ComboBox();
			this.cboCategory = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(88, 9);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(184, 20);
			this.txtName.TabIndex = 23;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(32, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Name:";
			// 
			// cboSubSection
			// 
			this.cboSubSection.Location = new System.Drawing.Point(88, 67);
			this.cboSubSection.Name = "cboSubSection";
			this.cboSubSection.Size = new System.Drawing.Size(184, 21);
			this.cboSubSection.Sorted = true;
			this.cboSubSection.TabIndex = 21;
			this.cboSubSection.SelectedIndexChanged += new System.EventHandler(this.cboSubSection_SelectedIndexChanged);
			this.cboSubSection.TextChanged += new System.EventHandler(this.cboSubSection_TextChanged);
			// 
			// cboCategory
			// 
			this.cboCategory.Location = new System.Drawing.Point(88, 37);
			this.cboCategory.Name = "cboCategory";
			this.cboCategory.Size = new System.Drawing.Size(184, 21);
			this.cboCategory.Sorted = true;
			this.cboCategory.TabIndex = 20;
			this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
			this.cboCategory.TextChanged += new System.EventHandler(this.cboCategory_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "SubSection:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "Category:";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(200, 96);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 24;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(120, 96);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 24);
			this.btnSave.TabIndex = 25;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// DesignPropertyEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(282, 128);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboSubSection);
			this.Controls.Add(this.cboCategory);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DesignPropertyEditor";
			this.ShowInTaskbar = false;
			this.Text = "Design Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        public void LoadForm(ref DesignData design)
        {
            this.PopulateCategories();
            this.SelectCategory(design.Category);
            this.m_Name = design.Name;
            this.m_Category = design.Category;
            this.m_Subsection = design.Subsection;
            this.txtName.Text = design.Name;
            this.cboCategory.Text = design.Category;
            this.cboSubSection.Text = design.Subsection;
            base.ShowDialog();
            if (!this.m_Cancelled)
            {
                design.Name = this.m_Name;
                design.Category = this.m_Category;
                design.Subsection = this.m_Subsection;
            }
        }

        public void LoadForm(ref HouseDesign design, Form parent)
        {
            this.PopulateCategories();
            this.SelectCategory(design.Category);
            this.m_Name = design.Name;
            this.m_Category = design.Category;
            this.m_Subsection = design.SubSection;
            this.txtName.Text = design.Name;
            this.cboCategory.Text = design.Category;
            this.cboSubSection.Text = design.SubSection;
            base.ShowDialog(parent);
            if (!this.m_Cancelled)
            {
                design.Name = this.m_Name;
                design.Category = this.m_Category;
                design.SubSection = this.m_Subsection;
            }
        }

        private void PopulateCategories()
        {
            string[] categoryNames = CategoryList.GetCategoryNames();
            if (categoryNames != null)
            {
                foreach (string str in categoryNames)
                {
                    this.cboCategory.Items.Add(str);
                }
            }
        }

        private void PopulateSubCategoryList(string category)
        {
            string[] subSectionNames = CategoryList.GetSubSectionNames(category);
            this.cboSubSection.Items.Clear();
            if (subSectionNames != null)
            {
                foreach (string str in subSectionNames)
                {
                    this.cboSubSection.Items.Add(str);
                    this.cboSubSection.SelectedIndex = 0;
                }
            }
        }

        private void SelectCategory(string category)
        {
            this.cboSubSection.Items.Clear();
            if (this.FindCategoryIndex(category) != -1)
            {
                this.PopulateSubCategoryList(category);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.m_Name = this.txtName.Text.Trim();
        }

        public bool Cancelled
        {
            get
            {
                return this.m_Cancelled;
            }
        }
    }
}

