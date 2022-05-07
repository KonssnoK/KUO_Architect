namespace UOArchitect.Toolbox.TabPanels {
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;
	using UOArchitect;

	public class OptionsTab : UserControl {
		private CheckBox chkAutoDetect;
		private Container components = null;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label1 = new Label();
		private Label label2;
		private Label label3;
		private TextBox txtClientDirectory;
		private TextBox txtMultiIdx;
		private TextBox txtMultiMul;
		private GroupBox groupBox3;
		private TextBox txt_SA_path;
		private Label label4;
		private TextBox txt_SAexe;
		private Button btn_Set;
		private Button btn_default;
		private FolderBrowserDialog folderBrowserDialog1;
		private Button btn_folder;
		private Button btn_clear;
		private TextBox txtPrefix;

		public OptionsTab() {
			this.InitializeComponent();
		}

		#region Designer
		protected override void Dispose(bool disposing) {
			if (disposing && (this.components != null)) {
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtClientDirectory = new System.Windows.Forms.TextBox();
			this.chkAutoDetect = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMultiMul = new System.Windows.Forms.TextBox();
			this.txtMultiIdx = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txt_SAexe = new System.Windows.Forms.TextBox();
			this.txt_SA_path = new System.Windows.Forms.TextBox();
			this.btn_Set = new System.Windows.Forms.Button();
			this.btn_default = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.btn_folder = new System.Windows.Forms.Button();
			this.btn_clear = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "RunUO Command Prefix:";
			// 
			// txtPrefix
			// 
			this.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPrefix.Location = new System.Drawing.Point(136, 8);
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(32, 20);
			this.txtPrefix.TabIndex = 1;
			this.txtPrefix.Leave += new System.EventHandler(this.txtPrefix_Leave);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtClientDirectory);
			this.groupBox1.Controls.Add(this.chkAutoDetect);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(8, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 80);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "UO 2D Client Directory";
			// 
			// txtClientDirectory
			// 
			this.txtClientDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtClientDirectory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtClientDirectory.Enabled = false;
			this.txtClientDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClientDirectory.Location = new System.Drawing.Point(8, 48);
			this.txtClientDirectory.Name = "txtClientDirectory";
			this.txtClientDirectory.Size = new System.Drawing.Size(160, 20);
			this.txtClientDirectory.TabIndex = 4;
			this.txtClientDirectory.Leave += new System.EventHandler(this.txtClientDirectory_Leave);
			// 
			// chkAutoDetect
			// 
			this.chkAutoDetect.Checked = true;
			this.chkAutoDetect.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkAutoDetect.Location = new System.Drawing.Point(8, 24);
			this.chkAutoDetect.Name = "chkAutoDetect";
			this.chkAutoDetect.Size = new System.Drawing.Size(160, 16);
			this.chkAutoDetect.TabIndex = 3;
			this.chkAutoDetect.Text = "Auto-detect (Use Registry)";
			this.chkAutoDetect.CheckedChanged += new System.EventHandler(this.chkAutoDetect_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.txtMultiMul);
			this.groupBox2.Controls.Add(this.txtMultiIdx);
			this.groupBox2.Location = new System.Drawing.Point(8, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 80);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Multi Patch Target";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Multi.mul";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Multi.idx";
			// 
			// txtMultiMul
			// 
			this.txtMultiMul.BackColor = System.Drawing.SystemColors.Window;
			this.txtMultiMul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMultiMul.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtMultiMul.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMultiMul.Location = new System.Drawing.Point(56, 48);
			this.txtMultiMul.Name = "txtMultiMul";
			this.txtMultiMul.Size = new System.Drawing.Size(112, 20);
			this.txtMultiMul.TabIndex = 6;
			this.txtMultiMul.TextChanged += new System.EventHandler(this.txtMultiMul_TextChanged);
			// 
			// txtMultiIdx
			// 
			this.txtMultiIdx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMultiIdx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMultiIdx.Location = new System.Drawing.Point(56, 24);
			this.txtMultiIdx.Name = "txtMultiIdx";
			this.txtMultiIdx.Size = new System.Drawing.Size(112, 20);
			this.txtMultiIdx.TabIndex = 5;
			this.txtMultiIdx.TextChanged += new System.EventHandler(this.txtMultiIdx_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.txt_SAexe);
			this.groupBox3.Controls.Add(this.txt_SA_path);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(8, 214);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(176, 78);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "UO SA Client Directory";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Exe:";
			// 
			// txt_SAexe
			// 
			this.txt_SAexe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_SAexe.Location = new System.Drawing.Point(41, 45);
			this.txt_SAexe.Name = "txt_SAexe";
			this.txt_SAexe.Size = new System.Drawing.Size(129, 20);
			this.txt_SAexe.TabIndex = 5;
			// 
			// txt_SA_path
			// 
			this.txt_SA_path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_SA_path.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txt_SA_path.Enabled = false;
			this.txt_SA_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_SA_path.Location = new System.Drawing.Point(6, 19);
			this.txt_SA_path.Name = "txt_SA_path";
			this.txt_SA_path.Size = new System.Drawing.Size(164, 20);
			this.txt_SA_path.TabIndex = 4;
			// 
			// btn_Set
			// 
			this.btn_Set.Location = new System.Drawing.Point(8, 298);
			this.btn_Set.Name = "btn_Set";
			this.btn_Set.Size = new System.Drawing.Size(95, 23);
			this.btn_Set.TabIndex = 5;
			this.btn_Set.Text = "Set Custom .exe";
			this.btn_Set.UseVisualStyleBackColor = true;
			this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
			// 
			// btn_default
			// 
			this.btn_default.Location = new System.Drawing.Point(109, 298);
			this.btn_default.Name = "btn_default";
			this.btn_default.Size = new System.Drawing.Size(75, 23);
			this.btn_default.TabIndex = 6;
			this.btn_default.Text = "Default exe";
			this.btn_default.UseVisualStyleBackColor = true;
			this.btn_default.Click += new System.EventHandler(this.btn_default_Click);
			// 
			// btn_folder
			// 
			this.btn_folder.Location = new System.Drawing.Point(8, 327);
			this.btn_folder.Name = "btn_folder";
			this.btn_folder.Size = new System.Drawing.Size(116, 23);
			this.btn_folder.TabIndex = 7;
			this.btn_folder.Text = "Set custom SA folder";
			this.btn_folder.UseVisualStyleBackColor = true;
			this.btn_folder.Click += new System.EventHandler(this.btn_folder_Click);
			// 
			// btn_clear
			// 
			this.btn_clear.Location = new System.Drawing.Point(130, 327);
			this.btn_clear.Name = "btn_clear";
			this.btn_clear.Size = new System.Drawing.Size(54, 23);
			this.btn_clear.TabIndex = 8;
			this.btn_clear.Text = "Clear";
			this.btn_clear.UseVisualStyleBackColor = true;
			this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
			// 
			// OptionsTab
			// 
			this.Controls.Add(this.btn_clear);
			this.Controls.Add(this.btn_folder);
			this.Controls.Add(this.btn_default);
			this.Controls.Add(this.btn_Set);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtPrefix);
			this.Controls.Add(this.label1);
			this.Name = "OptionsTab";
			this.Size = new System.Drawing.Size(192, 360);
			this.Disposed += new System.EventHandler(this.OptionsTab_Disposed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		void OptionsTab_Disposed(object sender, EventArgs e) {
			using (FileStream fs = new FileStream("settings.bin", FileMode.Create)) {
				using (BinaryWriter writer = new BinaryWriter(fs)) {
					writer.Write((string)Config.MLClientDirectory);
					writer.Write((string)Config.SAClientDirectory);
					writer.Write((string)txt_SAexe.Text);
				}
			}
		}
		#endregion

		private const string SETTINGS_BIN = "settings.bin";

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if ((Config.AutoDetectClientDirectory || (Config.MLClientDirectory == string.Empty)) || !Directory.Exists(Config.MLClientDirectory)) {
				this.chkAutoDetect.Checked = true;
			} else {
				this.txtClientDirectory.Enabled = true;
				this.chkAutoDetect.Checked = false;
				this.txtClientDirectory.Focus();
			}
			string baseexe = Utility.UOSA_CLIENT;
			if (File.Exists(SETTINGS_BIN)) {
				try {
					using (FileStream fs = new FileStream(SETTINGS_BIN, FileMode.Open)) {
						using (BinaryReader reader = new BinaryReader(fs)) {
							Config.MLClientDirectory = reader.ReadString();
							Config.SAClientDirectory = reader.ReadString();
							baseexe = reader.ReadString();
						}
					}
					ClientUtility.CustomClientPath = Config.SAClientDirectory + txt_SA_path.Text.Trim();
				} catch(Exception ex) {
					MessageBox.Show(string.Format("Error loading {0}. {1}", SETTINGS_BIN, ex.Message));
				}
			}

			this.txtClientDirectory.Text = Config.MLClientDirectory;
			this.txt_SA_path.Text = Config.SAClientDirectory;
			this.txt_SAexe.Text = baseexe;

			this.txtPrefix.Text = Config.CommandPrefix;
			this.txtMultiIdx.Text = Config.MultiIdxTarget;
			this.txtMultiMul.Text = Config.MultiMulTarget;
			txt_SA_path.Enabled = false;
		}

		private void txtClientDirectory_Leave(object sender, EventArgs e) {
			string path = this.txtClientDirectory.Text.Trim();
			if (path == string.Empty) {
				this.chkAutoDetect.Checked = true;
			} else if (!Directory.Exists(path)) {
				MessageBox.Show(string.Format("The directory {0} does not exist.", path));
				this.txtClientDirectory.Focus();
			} else {
				this.chkAutoDetect.Checked = false;
				Config.MLClientDirectory = path;
			}
		}

		private void txtMultiIdx_TextChanged(object sender, EventArgs e) {
			Config.MultiIdxTarget = this.txtMultiIdx.Text.Trim();
			if (File.Exists(Config.MultiIdxTarget)) {
				this.txtMultiIdx.BackColor = Color.Green;
			} else {
				this.txtMultiIdx.BackColor = Color.Red;
			}
		}

		private void txtMultiMul_TextChanged(object sender, EventArgs e) {
			Config.MultiMulTarget = this.txtMultiMul.Text.Trim();
			if (File.Exists(Config.MultiMulTarget)) {
				this.txtMultiMul.BackColor = Color.Green;
			} else {
				this.txtMultiMul.BackColor = Color.Red;
			}
		}

		private void txtPrefix_Leave(object sender, EventArgs e) {
			if (this.txtPrefix.Text.Trim() == string.Empty) {
				Config.CommandPrefix = "[";
				this.txtPrefix.Text = Config.CommandPrefix;
			} else {
				Config.CommandPrefix = this.txtPrefix.Text.Trim();
			}
		}

		private void chkAutoDetect_CheckedChanged(object sender, EventArgs e) {
			if (this.chkAutoDetect.Checked) {
				this.txtClientDirectory.Enabled = false;
				this.txtClientDirectory.Text = "";
				Config.AutoDetectClientDirectory = true;
			} else {
				Config.AutoDetectClientDirectory = false;
				this.txtClientDirectory.Enabled = true;
			}
		}

		private void btn_Set_Click(object sender, EventArgs e) {
			if (Config.SAClientDirectory == null) {
				ClientUtility.CustomClientPath = Config.MLClientDirectory + txt_SAexe.Text.Trim();
				MessageBox.Show("Warning: no SA installation found, ML directory will be used.");
			} else
				ClientUtility.CustomClientPath = Config.SAClientDirectory + txt_SAexe.Text.Trim();
			MessageBox.Show("Custom exe set.");
		}

		private void btn_default_Click(object sender, EventArgs e) {
			if (Config.SAClientDirectory == null) {
				ClientUtility.CustomClientPath = Config.MLClientDirectory + Utility.UOML_CLIENT;
				MessageBox.Show("ML: Client.exe restored.");
				txt_SAexe.Text = Utility.UOML_CLIENT;
			} else {
				ClientUtility.CustomClientPath = Config.SAClientDirectory + Utility.UOSA_CLIENT;
				MessageBox.Show("UOSA.exe restored.");
				txt_SAexe.Text = Utility.UOSA_CLIENT;
			}
		}

		private void btn_folder_Click(object sender, EventArgs e) {
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				Config.SAClientDirectory = folderBrowserDialog1.SelectedPath;
			txt_SA_path.Text = Config.SAClientDirectory;
		}

		private void btn_clear_Click(object sender, EventArgs e) {
			try {
				File.Delete("settings.bin");
				MessageBox.Show("Settings.bin deleted!");
			} catch { }
		}
	}
}