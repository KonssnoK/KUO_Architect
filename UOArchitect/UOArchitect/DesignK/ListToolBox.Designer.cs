namespace UOArchitect
{
	partial class ListToolBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tree = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_destroy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tree
			// 
			this.tree.Location = new System.Drawing.Point(0, 30);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(195, 391);
			this.tree.TabIndex = 3;
			this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Drag me!";
			// 
			// btn_destroy
			// 
			this.btn_destroy.Location = new System.Drawing.Point(139, 3);
			this.btn_destroy.Name = "btn_destroy";
			this.btn_destroy.Size = new System.Drawing.Size(53, 23);
			this.btn_destroy.TabIndex = 5;
			this.btn_destroy.Text = "Destroy";
			this.btn_destroy.UseVisualStyleBackColor = true;
			this.btn_destroy.Click += new System.EventHandler(this.btn_destroy_Click);
			// 
			// ListToolBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.btn_destroy);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tree);
			this.Name = "ListToolBox";
			this.Size = new System.Drawing.Size(195, 421);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_destroy;
	}
}
