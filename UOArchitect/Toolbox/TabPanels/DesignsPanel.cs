namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DesignsPanel : UserControl
    {
        private MenuActions _currentAction;
        private MenuItem _mnuBuildDesign;
        private MenuItem _mnuDeleteDesigns;
        private MenuItem _mnuEditDesign;
        private MenuItem _mnuExportDesigns;
        private MenuItem _mnuPreviewDesign;
        private Button btnBuild;
        private Button btnDelete;
        private Button btnDesigner;
        private Button btnPatch;
        private Button btnPreview;
        private Button btnProperties;
        private Container components = null;
        private ContextMenu contextMenu1;
        private GroupBox groupBox1;
        private Label lblDesignDesc;
        public DesignSelected OnDesignSelected;
        public DesignUnselected OnDesignUnselected;
        private DesignTreeView tvwDesigns;

        public DesignsPanel()
        {
            this.InitializeComponent();
        }

        private void _mnuBuildDesign_Click(object sender, EventArgs e)
        {
            TreeNode nodeAt = this.tvwDesigns.GetNodeAt(this.tvwDesigns.MouseCoords);
            if ((nodeAt != null) && (nodeAt is DesignNode))
            {
                this.OpenBuildDialog(((DesignNode) nodeAt).Design);
            }
        }

        private void _mnuDeleteDesigns_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            TreeNode nodeAt = this.tvwDesigns.GetNodeAt(this.tvwDesigns.MouseCoords);
            if (nodeAt != null)
            {
                this.DeleteDesignsFromNode(nodeAt);
            }
            Cursor.Current = Cursors.Default;
        }

        private void _mnuEditDesign_Click(object sender, EventArgs e)
        {
            this.OpenSelectedDesignInEditor();
        }

        private void _mnuExportDesigns_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            TreeNode nodeAt = this.tvwDesigns.GetNodeAt(this.tvwDesigns.MouseCoords);
            if (nodeAt != null)
            {
                this.ExtractDesignsFromNode(nodeAt);
            }
            Cursor.Current = Cursors.Default;
        }

        private void _mnuPreviewDesign_Click(object sender, EventArgs e)
        {
            if (this.IsSelected)
            {
                this.OpenDesignPreview(this.SelectedNode.Design);
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (this.SelectedNode != null)
            {
                this.OpenBuildDialog(this.SelectedNode.Design);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SelectedNode != null)
            {
                this.SelectedNode.Delete();
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDesigner_Click(object sender, EventArgs e)
        {
            this.OpenSelectedDesignInEditor();
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            if (Config.DoTargetMultiMulsExist)
            {
                if (MessageBox.Show("Are you sure you want to patch this multi to the multi muls?", "Multi Patcher", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int num = this.SelectedDesign.PatchToMultiMuls(Config.CreateMultiPatchInfo());
                    string text = "";
                    if (num != 1)
                    {
                        text = string.Format("This design was succesfully patched to multi index {0}.", num);
                    }
                    else
                    {
                        text = "The multi patch failed. The target multi muls may be in use by another program.";
                    }
                    MessageBox.Show(text, "Patch Results");
                }
            }
            else
            {
                MessageBox.Show("The target multi muls specified on the Options tab do not exist", "Invalid Multi Mul Settings");
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (this.IsSelected)
            {
                this.OpenDesignPreview(this.SelectedNode.Design);
            }
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (this.SelectedNode != null)
            {
                if (!this.SelectedNode.Design.IsLoaded)
                {
                    this.SelectedNode.Design.Load();
                }
                DesignPropertyEditor editor = new DesignPropertyEditor();
                DesignData design = this.SelectedNode.Design;
                editor.LoadForm(ref design);
                if (editor.DialogResult == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    design.Save();
                    Cursor.Current = Cursors.Default;
                }
                design.Unload();
                editor.Dispose();
            }
        }

        private void DeleteDesignsFromNode(TreeNode node)
        {
            if (MessageBox.Show("Are you sure you want to delete this?", "UO Architect", MessageBoxButtons.YesNo) != DialogResult.No)
            {
                try
                {
                    ArrayList designs = new ArrayList();
                    if (node is CategoryNode)
                    {
                        this.GetDesignsFromCategoryNode((CategoryNode) node, ref designs);
                    }
                    else if (node is SubsectionNode)
                    {
                        this.GetDesignsFromSubsectionNode((SubsectionNode) node, ref designs);
                    }
                    else if (node is DesignNode)
                    {
                        ((DesignNode) node).Delete();
                    }
                    if (designs.Count > 0)
                    {
                        foreach (DesignData data in designs)
                        {
                            HouseDesignData.DeleteDesign(data);
                        }
                        this.tvwDesigns.Populate(HouseDesignData.DesignHeaders);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Export failed: \n" + exception.Message);
                }
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

        private void ExtractDesignsFromNode(TreeNode node)
        {
            try
            {
                ArrayList designs = new ArrayList();
                if (node is CategoryNode)
                {
                    this.GetDesignsFromCategoryNode((CategoryNode) node, ref designs);
                }
                else if (node is SubsectionNode)
                {
                    this.GetDesignsFromSubsectionNode((SubsectionNode) node, ref designs);
                }
                else if (node is DesignNode)
                {
                    designs.Add(((DesignNode) node).Design);
                }
                if (designs.Count > 0)
                {
                    new UOARBatchDataAdapter().ExportDesigns(designs, node.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Export failed: \n" + exception.Message);
            }
        }

        private void GetDesignsFromCategoryNode(CategoryNode node, ref ArrayList designs)
        {
            foreach (SubsectionNode node2 in node.Nodes)
            {
                this.GetDesignsFromSubsectionNode(node2, ref designs);
            }
        }

        private void GetDesignsFromSubsectionNode(SubsectionNode node, ref ArrayList designs)
        {
            foreach (DesignNode node2 in node.Nodes)
            {
                designs.Add(node2.Design);
            }
        }

        private void InitializeComponent()
        {
			this.lblDesignDesc = new System.Windows.Forms.Label();
			this.btnPreview = new System.Windows.Forms.Button();
			this.btnDesigner = new System.Windows.Forms.Button();
			this.btnProperties = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnBuild = new System.Windows.Forms.Button();
			this.btnPatch = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tvwDesigns = new UOArchitect.DesignTreeView();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblDesignDesc
			// 
			this.lblDesignDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDesignDesc.ForeColor = System.Drawing.Color.Navy;
			this.lblDesignDesc.Location = new System.Drawing.Point(8, 1);
			this.lblDesignDesc.Name = "lblDesignDesc";
			this.lblDesignDesc.Size = new System.Drawing.Size(176, 16);
			this.lblDesignDesc.TabIndex = 1;
			// 
			// btnPreview
			// 
			this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPreview.Location = new System.Drawing.Point(96, 80);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(72, 23);
			this.btnPreview.TabIndex = 1;
			this.btnPreview.Text = "Preview";
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			// 
			// btnDesigner
			// 
			this.btnDesigner.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDesigner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDesigner.Location = new System.Drawing.Point(96, 48);
			this.btnDesigner.Name = "btnDesigner";
			this.btnDesigner.Size = new System.Drawing.Size(72, 23);
			this.btnDesigner.TabIndex = 2;
			this.btnDesigner.Text = "Editor";
			this.btnDesigner.Click += new System.EventHandler(this.btnDesigner_Click);
			// 
			// btnProperties
			// 
			this.btnProperties.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProperties.Location = new System.Drawing.Point(8, 16);
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(72, 23);
			this.btnProperties.TabIndex = 3;
			this.btnProperties.Text = "Properties";
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(8, 80);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(72, 23);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnBuild
			// 
			this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBuild.Location = new System.Drawing.Point(96, 16);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Size = new System.Drawing.Size(72, 23);
			this.btnBuild.TabIndex = 4;
			this.btnBuild.Text = "Build";
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			// 
			// btnPatch
			// 
			this.btnPatch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnPatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPatch.Location = new System.Drawing.Point(8, 48);
			this.btnPatch.Name = "btnPatch";
			this.btnPatch.Size = new System.Drawing.Size(72, 23);
			this.btnPatch.TabIndex = 6;
			this.btnPatch.Text = "Patch Muls";
			this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDesigner);
			this.groupBox1.Controls.Add(this.btnProperties);
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.btnPatch);
			this.groupBox1.Controls.Add(this.btnPreview);
			this.groupBox1.Controls.Add(this.btnBuild);
			this.groupBox1.Location = new System.Drawing.Point(8, 248);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 112);
			this.groupBox1.TabIndex = 50;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions";
			// 
			// tvwDesigns
			// 
			this.tvwDesigns.Location = new System.Drawing.Point(8, 20);
			this.tvwDesigns.Name = "tvwDesigns";
			this.tvwDesigns.Size = new System.Drawing.Size(176, 220);
			this.tvwDesigns.TabIndex = 0;
			// 
			// DesignsPanel
			// 
			this.Controls.Add(this.tvwDesigns);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblDesignDesc);
			this.Name = "DesignsPanel";
			this.Size = new System.Drawing.Size(192, 360);
			this.groupBox1.ResumeLayout(false);
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

        private void OnDoubleClickDesign(DesignNode node)
        {
            this.OpenDesignPreview(node.Design);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.tvwDesigns.OnSelected = (DesignTreeView.DesignNodeSelected) Delegate.Combine(this.tvwDesigns.OnSelected, new DesignTreeView.DesignNodeSelected(this.OnSelection));
            this.tvwDesigns.OnDoubleClickDesign = (DesignTreeView.DoubleClickDesign) Delegate.Combine(this.tvwDesigns.OnDoubleClickDesign, new DesignTreeView.DoubleClickDesign(this.OnDoubleClickDesign));
            this.tvwDesigns.OnRightClickNode = (DesignTreeView.RightClickNode) Delegate.Combine(this.tvwDesigns.OnRightClickNode, new DesignTreeView.RightClickNode(this.OnRightClickNode));
            this._mnuExportDesigns = new MenuItem("Export");
            this._mnuDeleteDesigns = new MenuItem("Delete");
            this._mnuBuildDesign = new MenuItem("Build");
            this._mnuEditDesign = new MenuItem("Edit");
            this._mnuPreviewDesign = new MenuItem("Preview");
            this._mnuExportDesigns.Click += new EventHandler(this._mnuExportDesigns_Click);
            this._mnuDeleteDesigns.Click += new EventHandler(this._mnuDeleteDesigns_Click);
            this._mnuBuildDesign.Click += new EventHandler(this._mnuBuildDesign_Click);
            this._mnuEditDesign.Click += new EventHandler(this._mnuEditDesign_Click);
            this._mnuPreviewDesign.Click += new EventHandler(this._mnuPreviewDesign_Click);
            this.tvwDesigns.Populate(HouseDesignData.DesignHeaders);
            Connection.OnBusy = (Connection.ClientBusyEvent) Delegate.Combine(Connection.OnBusy, new Connection.ClientBusyEvent(this.OnClientBusy));
            Connection.OnConnect = (Connection.ConnectEvent) Delegate.Combine(Connection.OnConnect, new Connection.ConnectEvent(this.OnConnection));
            Connection.OnDisconnect = (Connection.DisconnectEvent) Delegate.Combine(Connection.OnDisconnect, new Connection.DisconnectEvent(this.OnDisconnection));
            Connection.OnReady = (Connection.ClientReadyEvent) Delegate.Combine(Connection.OnReady, new Connection.ClientReadyEvent(this.OnClientReady));
            HouseDesignData.OnRefreshDesignsList = (HouseDesignData.RefreshDesignsList) Delegate.Combine(HouseDesignData.OnRefreshDesignsList, new HouseDesignData.RefreshDesignsList(this.OnRefreshDesignList));
            this.UpdateControlStates();
        }

        private void OnRefreshDesignList()
        {
            this.tvwDesigns.Populate(HouseDesignData.DesignHeaders);
        }

        private void OnRightClickNode(TreeNode node)
        {
            if ((node is CategoryNode) || (node is SubsectionNode))
            {
                this._mnuExportDesigns.Text = "Export Designs";
                this._mnuDeleteDesigns.Text = "Delete Designs";
            }
            else
            {
                this._mnuExportDesigns.Text = "Export";
                this._mnuDeleteDesigns.Text = "Delete";
            }
            this.contextMenu1.MenuItems.Clear();
            if (node is DesignNode)
            {
                this.contextMenu1.MenuItems.Add(this._mnuBuildDesign);
                this.contextMenu1.MenuItems.Add(this._mnuPreviewDesign);
                this.contextMenu1.MenuItems.Add(new MenuItem("-"));
                this.contextMenu1.MenuItems.Add(this._mnuEditDesign);
            }
            this.contextMenu1.MenuItems.Add(this._mnuDeleteDesigns);
            if (node is DesignNode)
            {
                this.contextMenu1.MenuItems.Add(new MenuItem("-"));
            }
            this.contextMenu1.MenuItems.Add(this._mnuExportDesigns);
            this.contextMenu1.Show(this.tvwDesigns, this.tvwDesigns.MouseCoords);
        }

        private void OnSelection(DesignNode node)
        {
            if (node != null)
            {
                this.lblDesignDesc.Text = string.Format("{0}: {1} items", node.Design.Name, node.Design.RecordCount);
                if (this.OnDesignSelected != null)
                {
                    this.OnDesignSelected();
                }
            }
            else
            {
                this.lblDesignDesc.Text = "";
                if (this.OnDesignUnselected != null)
                {
                    this.OnDesignUnselected();
                }
            }
            this.UpdateControlStates();
        }

        private void OpenBuildDialog(DesignData design)
        {
            new BuildDialog(base.ParentForm).LoadForm(design);
        }

        private void OpenDesignPreview(DesignData design)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!design.IsLoaded)
            {
                design.Load();
            }
            Cursor.Current = Cursors.Default;
            new DesignPreview().LoadForm(new HouseDesign(design));
        }

        private void OpenSelectedDesignInEditor()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SelectedNode != null)
            {
                if (!this.SelectedNode.Design.IsLoaded)
                {
                    this.SelectedNode.Design.Load();
                }
                HouseDesign design = new HouseDesign(this.SelectedNode.Design);
                HouseDesigner designer = new HouseDesigner(design);
                designer.Show();
                designer.WindowState = FormWindowState.Maximized;
            }
            Cursor.Current = Cursors.Default;
        }

        public void UpdateControlStates()
        {
            if ((Connection.IsConnected && !Connection.IsBusy) && (this.SelectedNode != null))
            {
                this.btnBuild.Enabled = true;
            }
            else
            {
                this.btnBuild.Enabled = false;
            }
            this.btnPreview.Enabled = this.IsSelected;
            this.btnDelete.Enabled = this.IsSelected;
            this.btnDesigner.Enabled = this.IsSelected;
            this.btnProperties.Enabled = this.IsSelected;
            this.btnPatch.Enabled = this.IsSelected;
        }

        private bool IsSelected
        {
            get
            {
                return (this.tvwDesigns.SelectedNode != null);
            }
        }

        public DesignData SelectedDesign
        {
            get
            {
                DesignData design = null;
                if (this.IsSelected)
                {
                    design = this.SelectedNode.Design;
                }
                return design;
            }
        }

        private DesignNode SelectedNode
        {
            get
            {
                return this.tvwDesigns.SelectedNode;
            }
        }

        public delegate void DesignSelected();

        public delegate void DesignUnselected();

        private enum MenuActions
        {
            None,
            Extract
        }
    }
}

