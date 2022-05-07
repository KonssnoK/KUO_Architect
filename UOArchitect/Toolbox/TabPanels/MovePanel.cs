namespace UOArchitect.Toolbox.TabPanels
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using UOArchitect;
    using UOArchitect.Toolbox.Controls;
    using UOArchitectInterface;

    public class MovePanel : UserControl
    {
        private bool _busy = false;
        private DesignData _extractedDesign = null;
        private SelectItemsResponse _lastResponse = null;
        private int[] _serials = null;
        private Button btnAddArea;
        private Button btnAddItemToSel;
        private Button btnClearSel;
        private Button btnExtract;
        private Button btnRemove;
        private Button btnRemoveAreaFromSel;
        private Button btnRemoveItemFromSel;
        private Button btnSelectArea;
        private Button btnSelectItem;
        private Button btnTeleport;
        private Button btnWipeArea;
        private Button btnWipeSel;
        private Container components = null;
        private FilterByZControl ctlItemZFilter;
        private MoveControlMockup ctlMoveItems;
        private GroupBox fraSelection;
        private GroupBox groupBox4;
        private GroupBox groupBox5;

        public MovePanel()
        {
            this.InitializeComponent();
        }

        private void AddArea_Click(object sender, EventArgs e)
        {
            this.AddAreaToSelection(false);
        }

        private void AddAreaToSelection(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Area, multiple);
            if (response != null)
            {
                this.AddItemsToSel(response.ItemSerials);
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void AddItemsToSel(int[] serials)
        {
            ArrayList list = new ArrayList(this._serials);
            for (int i = 0; i < serials.Length; i++)
            {
                if (list.IndexOf(serials[i]) == -1)
                {
                    list.Add(serials[i]);
                }
            }
            this._serials = (int[]) list.ToArray(typeof(int));
        }

        private void AddItemToSelection(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Item, multiple);
            if (response != null)
            {
                this.AddItemsToSel(response.ItemSerials);
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void btnAddItemToSel_Click(object sender, EventArgs e)
        {
            this.AddItemToSelection(false);
        }

        private void btnAddItemToSel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.AddItemToSelection(true);
            }
        }

        private void btnClearSel_Click(object sender, EventArgs e)
        {
            this._serials = null;
            this.UpdateControlStates();
            this.UpdateSelectionCount();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this._busy = true;
            this.ExtractItems();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("MRemove");
        }

        private void btnRemoveAreaFromSel_Click(object sender, EventArgs e)
        {
            this.RemoveAreaFromSelection(false);
        }

        private void btnRemoveItemFromSel_Click(object sender, EventArgs e)
        {
            this.RemoveItemFromSelection(false);
        }

        private void btnRemoveItemFromSel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.RemoveItemFromSelection(true);
            }
        }

        private void btnSelectArea_Click(object sender, EventArgs e)
        {
            this.SelectArea(false);
        }

        private void btnSelectItem_Click(object sender, EventArgs e)
        {
            this.SelectSingleItem(false);
        }

        private void btnSelectItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.SelectSingleItem(true);
            }
        }

        private void btnTeleport_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("tele");
        }

        private void btnWipeArea_Click(object sender, EventArgs e)
        {
            Utility.SendClientCommand("wipeitems");
        }

        private void btnWipeSel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Connection.SendDeleteItemsCommand(new DeleteCommandArgs(this._serials));
            this._serials = null;
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExtractItems()
        {
            ItemExtracter extracter = new ItemExtracter();
            extracter.OnExtracted = (ItemExtracter.DesignExtractEvent) Delegate.Combine(extracter.OnExtracted, new ItemExtracter.DesignExtractEvent(this.OnExtracted));
            extracter.ExtractDesign(this._serials);
            this.WaitForSelection();
            extracter.OnExtracted = (ItemExtracter.DesignExtractEvent) Delegate.Remove(extracter.OnExtracted, new ItemExtracter.DesignExtractEvent(this.OnExtracted));
            if (this._extractedDesign != null)
            {
                this._extractedDesign.Save();
            }
            this._extractedDesign = null;
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void InitializeComponent()
        {
			this.btnExtract = new System.Windows.Forms.Button();
			this.btnTeleport = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnWipeArea = new System.Windows.Forms.Button();
			this.fraSelection = new System.Windows.Forms.GroupBox();
			this.btnWipeSel = new System.Windows.Forms.Button();
			this.btnClearSel = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.btnRemoveItemFromSel = new System.Windows.Forms.Button();
			this.btnAddItemToSel = new System.Windows.Forms.Button();
			this.btnSelectItem = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.btnRemoveAreaFromSel = new System.Windows.Forms.Button();
			this.btnSelectArea = new System.Windows.Forms.Button();
			this.btnAddArea = new System.Windows.Forms.Button();
			this.ctlMoveItems = new UOArchitect.MoveControlMockup();
			this.ctlItemZFilter = new UOArchitect.Toolbox.Controls.FilterByZControl();
			this.fraSelection.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExtract
			// 
			this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExtract.Location = new System.Drawing.Point(104, 328);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(72, 23);
			this.btnExtract.TabIndex = 16;
			this.btnExtract.Text = "Extract";
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// btnTeleport
			// 
			this.btnTeleport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnTeleport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTeleport.Location = new System.Drawing.Point(104, 299);
			this.btnTeleport.Name = "btnTeleport";
			this.btnTeleport.Size = new System.Drawing.Size(72, 23);
			this.btnTeleport.TabIndex = 14;
			this.btnTeleport.Text = "Teleport";
			this.btnTeleport.Click += new System.EventHandler(this.btnTeleport_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRemove.Location = new System.Drawing.Point(16, 328);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(72, 23);
			this.btnRemove.TabIndex = 15;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnWipeArea
			// 
			this.btnWipeArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnWipeArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWipeArea.Location = new System.Drawing.Point(16, 299);
			this.btnWipeArea.Name = "btnWipeArea";
			this.btnWipeArea.Size = new System.Drawing.Size(72, 23);
			this.btnWipeArea.TabIndex = 13;
			this.btnWipeArea.Text = "Wipe Area";
			this.btnWipeArea.Click += new System.EventHandler(this.btnWipeArea_Click);
			// 
			// fraSelection
			// 
			this.fraSelection.Controls.Add(this.ctlMoveItems);
			this.fraSelection.Controls.Add(this.btnWipeSel);
			this.fraSelection.Controls.Add(this.btnClearSel);
			this.fraSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fraSelection.Location = new System.Drawing.Point(11, 5);
			this.fraSelection.Name = "fraSelection";
			this.fraSelection.Size = new System.Drawing.Size(168, 128);
			this.fraSelection.TabIndex = 0;
			this.fraSelection.TabStop = false;
			this.fraSelection.Text = "Selected: 0 items";
			// 
			// btnWipeSel
			// 
			this.btnWipeSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnWipeSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWipeSel.Location = new System.Drawing.Point(88, 96);
			this.btnWipeSel.Name = "btnWipeSel";
			this.btnWipeSel.Size = new System.Drawing.Size(64, 23);
			this.btnWipeSel.TabIndex = 3;
			this.btnWipeSel.Text = "Wipe";
			this.btnWipeSel.Click += new System.EventHandler(this.btnWipeSel_Click);
			// 
			// btnClearSel
			// 
			this.btnClearSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnClearSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearSel.Location = new System.Drawing.Point(16, 96);
			this.btnClearSel.Name = "btnClearSel";
			this.btnClearSel.Size = new System.Drawing.Size(64, 23);
			this.btnClearSel.TabIndex = 2;
			this.btnClearSel.Text = "Unselect";
			this.btnClearSel.Click += new System.EventHandler(this.btnClearSel_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.btnRemoveItemFromSel);
			this.groupBox5.Controls.Add(this.btnAddItemToSel);
			this.groupBox5.Controls.Add(this.btnSelectItem);
			this.groupBox5.Location = new System.Drawing.Point(8, 141);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(176, 48);
			this.groupBox5.TabIndex = 4;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Select Item";
			// 
			// btnRemoveItemFromSel
			// 
			this.btnRemoveItemFromSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRemoveItemFromSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRemoveItemFromSel.Location = new System.Drawing.Point(113, 16);
			this.btnRemoveItemFromSel.Name = "btnRemoveItemFromSel";
			this.btnRemoveItemFromSel.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveItemFromSel.TabIndex = 7;
			this.btnRemoveItemFromSel.Text = "Exclude";
			this.btnRemoveItemFromSel.Click += new System.EventHandler(this.btnRemoveItemFromSel_Click);
			this.btnRemoveItemFromSel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRemoveItemFromSel_MouseUp);
			// 
			// btnAddItemToSel
			// 
			this.btnAddItemToSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddItemToSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddItemToSel.Location = new System.Drawing.Point(60, 16);
			this.btnAddItemToSel.Name = "btnAddItemToSel";
			this.btnAddItemToSel.Size = new System.Drawing.Size(56, 23);
			this.btnAddItemToSel.TabIndex = 6;
			this.btnAddItemToSel.Text = "Include";
			this.btnAddItemToSel.Click += new System.EventHandler(this.btnAddItemToSel_Click);
			this.btnAddItemToSel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddItemToSel_MouseUp);
			// 
			// btnSelectItem
			// 
			this.btnSelectItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSelectItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSelectItem.Location = new System.Drawing.Point(8, 16);
			this.btnSelectItem.Name = "btnSelectItem";
			this.btnSelectItem.Size = new System.Drawing.Size(53, 23);
			this.btnSelectItem.TabIndex = 5;
			this.btnSelectItem.Text = "Select";
			this.btnSelectItem.Click += new System.EventHandler(this.btnSelectItem_Click);
			this.btnSelectItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSelectItem_MouseUp);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.ctlItemZFilter);
			this.groupBox4.Controls.Add(this.btnRemoveAreaFromSel);
			this.groupBox4.Controls.Add(this.btnSelectArea);
			this.groupBox4.Controls.Add(this.btnAddArea);
			this.groupBox4.Location = new System.Drawing.Point(8, 197);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(176, 96);
			this.groupBox4.TabIndex = 8;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Select Area";
			// 
			// btnRemoveAreaFromSel
			// 
			this.btnRemoveAreaFromSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRemoveAreaFromSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRemoveAreaFromSel.Location = new System.Drawing.Point(112, 16);
			this.btnRemoveAreaFromSel.Name = "btnRemoveAreaFromSel";
			this.btnRemoveAreaFromSel.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveAreaFromSel.TabIndex = 11;
			this.btnRemoveAreaFromSel.Text = "Exclude";
			this.btnRemoveAreaFromSel.Click += new System.EventHandler(this.btnRemoveAreaFromSel_Click);
			// 
			// btnSelectArea
			// 
			this.btnSelectArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSelectArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSelectArea.Location = new System.Drawing.Point(9, 16);
			this.btnSelectArea.Name = "btnSelectArea";
			this.btnSelectArea.Size = new System.Drawing.Size(48, 23);
			this.btnSelectArea.TabIndex = 9;
			this.btnSelectArea.Text = "Select";
			this.btnSelectArea.Click += new System.EventHandler(this.btnSelectArea_Click);
			// 
			// btnAddArea
			// 
			this.btnAddArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddArea.Location = new System.Drawing.Point(56, 16);
			this.btnAddArea.Name = "btnAddArea";
			this.btnAddArea.Size = new System.Drawing.Size(57, 23);
			this.btnAddArea.TabIndex = 10;
			this.btnAddArea.Text = "Include";
			this.btnAddArea.Click += new System.EventHandler(this.AddArea_Click);
			// 
			// ctlMoveItems
			// 
			this.ctlMoveItems.Location = new System.Drawing.Point(16, 16);
			this.ctlMoveItems.Name = "ctlMoveItems";
			this.ctlMoveItems.Size = new System.Drawing.Size(136, 72);
			this.ctlMoveItems.TabIndex = 1;
			// 
			// ctlItemZFilter
			// 
			this.ctlItemZFilter.Location = new System.Drawing.Point(12, 46);
			this.ctlItemZFilter.Name = "ctlItemZFilter";
			this.ctlItemZFilter.Size = new System.Drawing.Size(152, 45);
			this.ctlItemZFilter.TabIndex = 12;
			// 
			// MovePanel
			// 
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.btnTeleport);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnWipeArea);
			this.Controls.Add(this.fraSelection);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Name = "MovePanel";
			this.Size = new System.Drawing.Size(192, 360);
			this.fraSelection.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
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
            this.ctlMoveItems.OnMoveItems = (MoveControlMockup.MoveItemsEvent) Delegate.Combine(this.ctlMoveItems.OnMoveItems, new MoveControlMockup.MoveItemsEvent(this.OnMoveItems));
            this.ctlMoveItems.OnNudgeItems = (MoveControlMockup.NudgeItemsEvent) Delegate.Combine(this.ctlMoveItems.OnNudgeItems, new MoveControlMockup.NudgeItemsEvent(this.OnNudgeItems));
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

        private void OnSelection(SelectItemsResponse response)
        {
            this._lastResponse = response;
            this._busy = false;
        }

        private void RemoveAreaFromSelection(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Area, multiple);
            if (response != null)
            {
                this.RemoveItemsFromSel(response.ItemSerials);
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void RemoveItemFromSelection(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Item, multiple);
            if (response != null)
            {
                this.RemoveItemsFromSel(response.ItemSerials);
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void RemoveItemsFromSel(int[] serials)
        {
            ArrayList list = new ArrayList(this._serials);
            for (int i = 0; i < serials.Length; i++)
            {
                if (list.IndexOf(serials[i]) != -1)
                {
                    list.Remove(serials[i]);
                }
            }
            if (list.Count > 0)
            {
                this._serials = (int[]) list.ToArray(typeof(int));
            }
            else
            {
                this._serials = null;
            }
        }

        private void SelectArea(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            this._serials = null;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Area, multiple);
            if (response != null)
            {
                this._serials = response.ItemSerials;
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private SelectItemsResponse SelectItems(SelectTypes type, bool multiple)
        {
            SelectItemsRequestArgs args = new SelectItemsRequestArgs(type);
            args.Multiple = multiple;
            if (type == SelectTypes.Area)
            {
                if (this.ctlItemZFilter.UseMaxZ)
                {
                    args.UseMaxZ = true;
                    args.MaxZ = this.ctlItemZFilter.MaxZ;
                }
                if (this.ctlItemZFilter.UseMinZ)
                {
                    args.UseMinZ = true;
                    args.MinZ = this.ctlItemZFilter.MinZ;
                }
            }
            this._lastResponse = null;
            ItemSelector selector = new ItemSelector();
            selector.OnSelection = (ItemSelector.ItemsSelectedtEvent) Delegate.Combine(selector.OnSelection, new ItemSelector.ItemsSelectedtEvent(this.OnSelection));
            selector.SelectItems(args, true);
            this.WaitForSelection();
            selector.OnSelection = (ItemSelector.ItemsSelectedtEvent) Delegate.Remove(selector.OnSelection, new ItemSelector.ItemsSelectedtEvent(this.OnSelection));
            return this._lastResponse;
        }

        private void SelectSingleItem(bool multiple)
        {
            Cursor.Current = Cursors.WaitCursor;
            this._serials = null;
            SelectItemsResponse response = this.SelectItems(SelectTypes.Item, multiple);
            if (response != null)
            {
                this._serials = response.ItemSerials;
            }
            this.UpdateSelectionCount();
            this.UpdateControlStates();
            Cursor.Current = Cursors.Default;
        }

        private void UpdateControlStates()
        {
            if (!this._busy)
            {
                this.btnWipeArea.Enabled = true;
                this.btnTeleport.Enabled = true;
                this.btnRemove.Enabled = true;
            }
            else
            {
                this.btnWipeArea.Enabled = false;
                this.btnTeleport.Enabled = false;
                this.btnRemove.Enabled = false;
            }
            if ((!this._busy && (this.ItemCount > 0)) && (Connection.IsConnected && !Connection.IsBusy))
            {
                this.ctlMoveItems.Enabled = true;
                this.btnClearSel.Enabled = true;
                this.btnWipeSel.Enabled = true;
                this.btnAddItemToSel.Enabled = this.ItemCount > 0;
                this.btnAddArea.Enabled = this.ItemCount > 0;
                this.btnRemoveAreaFromSel.Enabled = this.ItemCount > 0;
                this.btnRemoveItemFromSel.Enabled = this.ItemCount > 0;
                this.btnExtract.Enabled = this.ItemCount > 0;
            }
            else
            {
                this.ctlMoveItems.Enabled = false;
                this.btnClearSel.Enabled = false;
                this.btnWipeSel.Enabled = false;
                this.btnAddItemToSel.Enabled = false;
                this.btnAddArea.Enabled = false;
                this.btnRemoveAreaFromSel.Enabled = false;
                this.btnRemoveItemFromSel.Enabled = false;
                this.btnExtract.Enabled = false;
                this.btnSelectArea.Enabled = false;
                this.btnSelectItem.Enabled = false;
            }
            if ((!this._busy && !Connection.IsBusy) && Connection.IsConnected)
            {
                this.btnSelectArea.Enabled = true;
                this.btnSelectItem.Enabled = true;
            }
            else
            {
                this.btnAddArea.Enabled = false;
                this.btnAddItemToSel.Enabled = false;
            }
        }

        private void UpdateSelectionCount()
        {
            this.fraSelection.Text = string.Format("Selected: {0} items", this.ItemCount);
        }

        private void WaitForSelection()
        {
            this._busy = true;
            while (this._busy)
            {
                Application.DoEvents();
                Thread.Sleep(50);
            }
        }

        private int ItemCount
        {
            get
            {
                if (this._serials == null)
                {
                    return 0;
                }
                return this._serials.Length;
            }
        }
    }
}

