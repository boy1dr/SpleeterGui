namespace SpleeterGui
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPythonPathToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpFAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSpleeterGUIUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spleeterupgradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.spleeterGithubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makenItSoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveTo = new System.Windows.Forms.Button();
            this.txt_output_directory = new System.Windows.Forms.TextBox();
            this.chkFullBandwidth = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblDroptext = new System.Windows.Forms.Label();
            this.lblSlogan1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblSlogan2 = new System.Windows.Forms.Label();
            this.lblPartsTitle = new System.Windows.Forms.Label();
            this.parts_btn2 = new System.Windows.Forms.Button();
            this.parts_btn4 = new System.Windows.Forms.Button();
            this.parts_btn5 = new System.Windows.Forms.Button();
            this.parts_label = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progress_txt = new System.Windows.Forms.Label();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.duration = new System.Windows.Forms.NumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblMaxLength = new System.Windows.Forms.Label();
            this.chkRecombine = new System.Windows.Forms.CheckBox();
            this.pnlRecombine = new System.Windows.Forms.Panel();
            this.chkRPartOther = new System.Windows.Forms.CheckBox();
            this.chkRPartPiano = new System.Windows.Forms.CheckBox();
            this.chkRPartDrums = new System.Windows.Forms.CheckBox();
            this.chkRPartBass = new System.Windows.Forms.CheckBox();
            this.chkRPartVocal = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cmbCodec = new System.Windows.Forms.ComboBox();
            this.flowParts = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.duration)).BeginInit();
            this.pnlRecombine.SuspendLayout();
            this.flowOptions.SuspendLayout();
            this.flowParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuLanguage,
            this.advancedToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(522, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.Name = "mnuLanguage";
            this.mnuLanguage.Size = new System.Drawing.Size(91, 24);
            this.mnuLanguage.Text = "Language";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPythonPathToolStripMenuItem1});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // setPythonPathToolStripMenuItem1
            // 
            this.setPythonPathToolStripMenuItem1.Name = "setPythonPathToolStripMenuItem1";
            this.setPythonPathToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.setPythonPathToolStripMenuItem1.Text = "Set SpleeterGUI path";
            this.setPythonPathToolStripMenuItem1.Click += new System.EventHandler(this.setPythonPathToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpFAQToolStripMenuItem,
            this.checkSpleeterGUIUpdateToolStripMenuItem,
            this.spleeterupgradeToolStripMenuItem,
            this.toolStripMenuItem3,
            this.spleeterGithubPageToolStripMenuItem,
            this.makenItSoToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpFAQToolStripMenuItem
            // 
            this.helpFAQToolStripMenuItem.Name = "helpFAQToolStripMenuItem";
            this.helpFAQToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.helpFAQToolStripMenuItem.Text = "&Help && FAQ";
            this.helpFAQToolStripMenuItem.Click += new System.EventHandler(this.helpFAQToolStripMenuItem_Click);
            // 
            // checkSpleeterGUIUpdateToolStripMenuItem
            // 
            this.checkSpleeterGUIUpdateToolStripMenuItem.Name = "checkSpleeterGUIUpdateToolStripMenuItem";
            this.checkSpleeterGUIUpdateToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.checkSpleeterGUIUpdateToolStripMenuItem.Text = "Check &SpleeterGUI version";
            this.checkSpleeterGUIUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkSpleeterGUIUpdateToolStripMenuItem_Click);
            // 
            // spleeterupgradeToolStripMenuItem
            // 
            this.spleeterupgradeToolStripMenuItem.Enabled = false;
            this.spleeterupgradeToolStripMenuItem.Name = "spleeterupgradeToolStripMenuItem";
            this.spleeterupgradeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.spleeterupgradeToolStripMenuItem.Text = "Spleeter core &upgrade";
            this.spleeterupgradeToolStripMenuItem.Visible = false;
            this.spleeterupgradeToolStripMenuItem.Click += new System.EventHandler(this.spleeterupgradeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
            // 
            // spleeterGithubPageToolStripMenuItem
            // 
            this.spleeterGithubPageToolStripMenuItem.Name = "spleeterGithubPageToolStripMenuItem";
            this.spleeterGithubPageToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.spleeterGithubPageToolStripMenuItem.Text = "SpleeterGUI &Github page";
            this.spleeterGithubPageToolStripMenuItem.Click += new System.EventHandler(this.spleeterGithubPageToolStripMenuItem_Click);
            // 
            // makenItSoToolStripMenuItem1
            // 
            this.makenItSoToolStripMenuItem1.Enabled = false;
            this.makenItSoToolStripMenuItem1.Name = "makenItSoToolStripMenuItem1";
            this.makenItSoToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.makenItSoToolStripMenuItem1.Text = "&Maken It So";
            this.makenItSoToolStripMenuItem1.Click += new System.EventHandler(this.makenItSoToolStripMenuItem1_Click);
            // 
            // btnSaveTo
            // 
            this.btnSaveTo.AccessibleDescription = "Choose folder to save separated files to";
            this.btnSaveTo.AccessibleName = "Save to";
            this.btnSaveTo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveTo.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnSaveTo.ForeColor = System.Drawing.Color.Black;
            this.btnSaveTo.Location = new System.Drawing.Point(15, 67);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new System.Drawing.Size(97, 22);
            this.btnSaveTo.TabIndex = 8;
            this.btnSaveTo.Text = "Save to";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new System.EventHandler(this.Button2_Click);
            // 
            // txt_output_directory
            // 
            this.txt_output_directory.AccessibleDescription = "shows output directory location";
            this.txt_output_directory.AccessibleName = "output directory display";
            this.txt_output_directory.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.txt_output_directory.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txt_output_directory.ForeColor = System.Drawing.Color.Black;
            this.txt_output_directory.Location = new System.Drawing.Point(118, 67);
            this.txt_output_directory.Name = "txt_output_directory";
            this.txt_output_directory.Size = new System.Drawing.Size(286, 22);
            this.txt_output_directory.TabIndex = 9;
            this.txt_output_directory.Leave += new System.EventHandler(this.txt_output_directory_Leave);
            // 
            // chkFullBandwidth
            // 
            this.chkFullBandwidth.AccessibleDescription = "enable high quality mode (may increase noise)";
            this.chkFullBandwidth.AccessibleName = "full bandwidth";
            this.chkFullBandwidth.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.chkFullBandwidth.AutoSize = true;
            this.chkFullBandwidth.Checked = true;
            this.chkFullBandwidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFullBandwidth.ForeColor = System.Drawing.Color.Black;
            this.chkFullBandwidth.Location = new System.Drawing.Point(15, 12);
            this.chkFullBandwidth.Name = "chkFullBandwidth";
            this.chkFullBandwidth.Size = new System.Drawing.Size(191, 17);
            this.chkFullBandwidth.TabIndex = 7;
            this.chkFullBandwidth.Text = "Full bandwidth (16Khz High quality)";
            this.chkFullBandwidth.UseVisualStyleBackColor = true;
            this.chkFullBandwidth.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AccessibleDescription = "spleeter debug output";
            this.textBox1.AccessibleName = "Console output";
            this.textBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.textBox1.Location = new System.Drawing.Point(4, 208);
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Multiline = true;
            this.textBox1.ReadOnly = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(492, 229);
            this.textBox1.TabIndex = 16;
            // 
            // lblDroptext
            // 
            this.lblDroptext.AutoEllipsis = true;
            this.lblDroptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroptext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDroptext.Location = new System.Drawing.Point(4, 92);
            this.lblDroptext.Name = "lblDroptext";
            this.lblDroptext.Size = new System.Drawing.Size(492, 44);
            this.lblDroptext.TabIndex = 13;
            this.lblDroptext.Text = "Drop your music file(s) here to begin processing";
            this.lblDroptext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSlogan1
            // 
            this.lblSlogan1.AutoSize = true;
            this.lblSlogan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlogan1.ForeColor = System.Drawing.Color.Gray;
            this.lblSlogan1.Location = new System.Drawing.Point(286, 56);
            this.lblSlogan1.Name = "lblSlogan1";
            this.lblSlogan1.Size = new System.Drawing.Size(171, 18);
            this.lblSlogan1.TabIndex = 0;
            this.lblSlogan1.Text = "Music source separation";
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleDescription = "Shows overall process completion";
            this.progressBar1.AccessibleName = "Progress Bar";
            this.progressBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.progressBar1.Location = new System.Drawing.Point(4, 185);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(491, 17);
            this.progressBar1.TabIndex = 11;
            // 
            // lblSlogan2
            // 
            this.lblSlogan2.AutoSize = true;
            this.lblSlogan2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlogan2.ForeColor = System.Drawing.Color.Gray;
            this.lblSlogan2.Location = new System.Drawing.Point(286, 75);
            this.lblSlogan2.Name = "lblSlogan2";
            this.lblSlogan2.Size = new System.Drawing.Size(174, 18);
            this.lblSlogan2.TabIndex = 1;
            this.lblSlogan2.Text = "Windows desktop bundle";
            // 
            // lblPartsTitle
            // 
            this.lblPartsTitle.AutoSize = true;
            this.lblPartsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPartsTitle.Location = new System.Drawing.Point(19, 129);
            this.lblPartsTitle.Name = "lblPartsTitle";
            this.lblPartsTitle.Size = new System.Drawing.Size(148, 24);
            this.lblPartsTitle.TabIndex = 2;
            this.lblPartsTitle.Text = "Parts to separate";
            // 
            // parts_btn2
            // 
            this.parts_btn2.AccessibleDescription = "Separate song in 2 parts";
            this.parts_btn2.AccessibleName = "Two parts";
            this.parts_btn2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.parts_btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.parts_btn2.ForeColor = System.Drawing.Color.Black;
            this.parts_btn2.Location = new System.Drawing.Point(173, 127);
            this.parts_btn2.Name = "parts_btn2";
            this.parts_btn2.Size = new System.Drawing.Size(34, 29);
            this.parts_btn2.TabIndex = 3;
            this.parts_btn2.Text = "2";
            this.parts_btn2.UseVisualStyleBackColor = false;
            this.parts_btn2.Click += new System.EventHandler(this.parts_btn2_Click);
            // 
            // parts_btn4
            // 
            this.parts_btn4.AccessibleDescription = "Separate song in 4 parts";
            this.parts_btn4.AccessibleName = "four parts";
            this.parts_btn4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.parts_btn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.parts_btn4.ForeColor = System.Drawing.Color.Black;
            this.parts_btn4.Location = new System.Drawing.Point(213, 127);
            this.parts_btn4.Name = "parts_btn4";
            this.parts_btn4.Size = new System.Drawing.Size(34, 29);
            this.parts_btn4.TabIndex = 4;
            this.parts_btn4.Text = "4";
            this.parts_btn4.UseVisualStyleBackColor = true;
            this.parts_btn4.Click += new System.EventHandler(this.parts_btn4_Click);
            // 
            // parts_btn5
            // 
            this.parts_btn5.AccessibleDescription = "Separate song in 5 parts";
            this.parts_btn5.AccessibleName = "five parts";
            this.parts_btn5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.parts_btn5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.parts_btn5.ForeColor = System.Drawing.Color.Black;
            this.parts_btn5.Location = new System.Drawing.Point(253, 127);
            this.parts_btn5.Name = "parts_btn5";
            this.parts_btn5.Size = new System.Drawing.Size(34, 29);
            this.parts_btn5.TabIndex = 5;
            this.parts_btn5.Text = "5";
            this.parts_btn5.UseVisualStyleBackColor = true;
            this.parts_btn5.Click += new System.EventHandler(this.parts_btn5_Click);
            // 
            // parts_label
            // 
            this.parts_label.AccessibleDescription = "displays separated parts names";
            this.parts_label.AccessibleName = "parts description";
            this.parts_label.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.parts_label.AutoEllipsis = true;
            this.parts_label.ForeColor = System.Drawing.Color.Black;
            this.parts_label.Location = new System.Drawing.Point(293, 127);
            this.parts_label.Name = "parts_label";
            this.parts_label.Size = new System.Drawing.Size(223, 29);
            this.parts_label.TabIndex = 6;
            this.parts_label.Text = "Vocal + Accompaniment";
            this.parts_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Black;
            this.lblProgress.Location = new System.Drawing.Point(1, 165);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(61, 17);
            this.lblProgress.TabIndex = 10;
            this.lblProgress.Text = "Progress";
            // 
            // progress_txt
            // 
            this.progress_txt.AutoSize = true;
            this.progress_txt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progress_txt.ForeColor = System.Drawing.Color.Black;
            this.progress_txt.Location = new System.Drawing.Point(68, 165);
            this.progress_txt.Name = "progress_txt";
            this.progress_txt.Size = new System.Drawing.Size(29, 17);
            this.progress_txt.TabIndex = 12;
            this.progress_txt.Text = "idle";
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.AccessibleDescription = "Choose music files to begin processing";
            this.btnSelectFiles.AccessibleName = "Choose music files";
            this.btnSelectFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFiles.AutoSize = true;
            this.btnSelectFiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectFiles.ForeColor = System.Drawing.Color.Black;
            this.btnSelectFiles.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnSelectFiles.Location = new System.Drawing.Point(187, 137);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(124, 23);
            this.btnSelectFiles.TabIndex = 15;
            this.btnSelectFiles.Text = "Or select music file(s)";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "Music Files|*.mp3;*.wav;*.ogg;*.m4a;*.wma;*.flac|All files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.flowOptions);
            this.pnlMain.Controls.Add(this.btnOpenFolder);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.chkFullBandwidth);
            this.pnlMain.Controls.Add(this.btnSelectFiles);
            this.pnlMain.Controls.Add(this.btnSaveTo);
            this.pnlMain.Controls.Add(this.textBox1);
            this.pnlMain.Controls.Add(this.txt_output_directory);
            this.pnlMain.Controls.Add(this.progress_txt);
            this.pnlMain.Controls.Add(this.lblDroptext);
            this.pnlMain.Controls.Add(this.lblProgress);
            this.pnlMain.Controls.Add(this.progressBar1);
            this.pnlMain.Location = new System.Drawing.Point(12, 182);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(504, 445);
            this.pnlMain.TabIndex = 37;
            // 
            // duration
            // 
            this.duration.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.duration.Location = new System.Drawing.Point(112, 4);
            this.duration.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.duration.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.duration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.duration.Name = "duration";
            this.duration.Size = new System.Drawing.Size(56, 20);
            this.duration.TabIndex = 19;
            this.duration.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.duration.ValueChanged += new System.EventHandler(this.duration_ValueChanged);
            // 
            // lblSeconds
            // 
            this.lblSeconds.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Margin = new System.Windows.Forms.Padding(3, 0, 18, 0);
            this.lblSeconds.Location = new System.Drawing.Point(176, 39);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 18;
            this.lblSeconds.Text = "seconds";
            // 
            // lblMaxLength
            // 
            this.lblMaxLength.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMaxLength.AutoSize = true;
            this.lblMaxLength.Margin = new System.Windows.Forms.Padding(0);
            this.lblMaxLength.ForeColor = System.Drawing.Color.Black;
            this.lblMaxLength.Location = new System.Drawing.Point(15, 39);
            this.lblMaxLength.Name = "lblMaxLength";
            this.lblMaxLength.Size = new System.Drawing.Size(109, 13);
            this.lblMaxLength.TabIndex = 17;
            this.lblMaxLength.Text = "Maximum song length";
            // 
            // chkRecombine
            // 
            this.chkRecombine.AutoSize = true;
            this.chkRecombine.ForeColor = System.Drawing.Color.Black;
            this.chkRecombine.Location = new System.Drawing.Point(3, 3);
            this.chkRecombine.Name = "chkRecombine";
            this.chkRecombine.Size = new System.Drawing.Size(247, 17);
            this.chkRecombine.TabIndex = 38;
            this.chkRecombine.Text = "Recombine (Merge output parts back together)";
            this.chkRecombine.UseVisualStyleBackColor = true;
            this.chkRecombine.CheckedChanged += new System.EventHandler(this.chkRecombine_CheckedChanged);
            // 
            // pnlRecombine
            // 
            this.pnlRecombine.Controls.Add(this.chkRecombine);
            this.pnlRecombine.Controls.Add(this.flowParts);
            this.pnlRecombine.Location = new System.Drawing.Point(23, 162);
            this.pnlRecombine.Name = "pnlRecombine";
            this.pnlRecombine.Size = new System.Drawing.Size(493, 14);
            this.pnlRecombine.TabIndex = 39;
            // 
            // chkRPartOther
            // 
            this.chkRPartOther.AutoSize = true;
            this.chkRPartOther.ForeColor = System.Drawing.Color.Black;
            this.chkRPartOther.Location = new System.Drawing.Point(255, 26);
            this.chkRPartOther.Name = "chkRPartOther";
            this.chkRPartOther.Size = new System.Drawing.Size(52, 17);
            this.chkRPartOther.TabIndex = 5;
            this.chkRPartOther.Text = "Other";
            this.chkRPartOther.UseVisualStyleBackColor = true;
            // 
            // chkRPartPiano
            // 
            this.chkRPartPiano.AutoSize = true;
            this.chkRPartPiano.ForeColor = System.Drawing.Color.Black;
            this.chkRPartPiano.Location = new System.Drawing.Point(197, 26);
            this.chkRPartPiano.Name = "chkRPartPiano";
            this.chkRPartPiano.Size = new System.Drawing.Size(53, 17);
            this.chkRPartPiano.TabIndex = 4;
            this.chkRPartPiano.Text = "Piano";
            this.chkRPartPiano.UseVisualStyleBackColor = true;
            // 
            // chkRPartDrums
            // 
            this.chkRPartDrums.AutoSize = true;
            this.chkRPartDrums.ForeColor = System.Drawing.Color.Black;
            this.chkRPartDrums.Location = new System.Drawing.Point(136, 26);
            this.chkRPartDrums.Name = "chkRPartDrums";
            this.chkRPartDrums.Size = new System.Drawing.Size(56, 17);
            this.chkRPartDrums.TabIndex = 3;
            this.chkRPartDrums.Text = "Drums";
            this.chkRPartDrums.UseVisualStyleBackColor = true;
            // 
            // chkRPartBass
            // 
            this.chkRPartBass.AutoSize = true;
            this.chkRPartBass.ForeColor = System.Drawing.Color.Black;
            this.chkRPartBass.Location = new System.Drawing.Point(82, 26);
            this.chkRPartBass.Name = "chkRPartBass";
            this.chkRPartBass.Size = new System.Drawing.Size(49, 17);
            this.chkRPartBass.TabIndex = 2;
            this.chkRPartBass.Text = "Bass";
            this.chkRPartBass.UseVisualStyleBackColor = true;
            // 
            // chkRPartVocal
            // 
            this.chkRPartVocal.AutoSize = true;
            this.chkRPartVocal.ForeColor = System.Drawing.Color.Black;
            this.chkRPartVocal.Location = new System.Drawing.Point(24, 26);
            this.chkRPartVocal.Name = "chkRPartVocal";
            this.chkRPartVocal.Size = new System.Drawing.Size(53, 17);
            this.chkRPartVocal.TabIndex = 1;
            this.chkRPartVocal.Text = "Vocal";
            this.chkRPartVocal.UseVisualStyleBackColor = true;
            //
            // btnOpenFolder
            //
            this.btnOpenFolder.AccessibleDescription = "Open the folder the separated files are saved to";
            this.btnOpenFolder.AccessibleName = "Open folder";
            this.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnOpenFolder.ForeColor = System.Drawing.Color.Black;
            this.btnOpenFolder.Location = new System.Drawing.Point(410, 67);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(85, 22);
            this.btnOpenFolder.TabIndex = 10;
            this.btnOpenFolder.Text = "Open folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            //
            // btnCancel
            //
            this.btnCancel.AccessibleDescription = "Stop processing and clear the queue";
            this.btnCancel.AccessibleName = "Cancel";
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(410, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 22);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // flowOptions
            //
            this.flowOptions.Controls.Add(this.lblMaxLength);
            this.flowOptions.Controls.Add(this.duration);
            this.flowOptions.Controls.Add(this.lblSeconds);
            this.flowOptions.Controls.Add(this.lblFormat);
            this.flowOptions.Controls.Add(this.cmbCodec);
            this.flowOptions.Location = new System.Drawing.Point(15, 33);
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size(481, 28);
            this.flowOptions.TabIndex = 17;
            this.flowOptions.WrapContents = false;
            //
            // lblFormat
            //
            this.lblFormat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFormat.AutoSize = true;
            this.lblFormat.ForeColor = System.Drawing.Color.Black;
            this.lblFormat.Margin = new System.Windows.Forms.Padding(0);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(71, 13);
            this.lblFormat.TabIndex = 20;
            this.lblFormat.Text = "Output format";
            //
            // cmbCodec
            //
            this.cmbCodec.AccessibleDescription = "File format of the separated parts";
            this.cmbCodec.AccessibleName = "Output format";
            this.cmbCodec.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodec.FormattingEnabled = true;
            this.cmbCodec.Items.AddRange(new object[] {
            "wav",
            "mp3",
            "flac",
            "ogg",
            "m4a",
            "wma"});
            this.cmbCodec.Name = "cmbCodec";
            this.cmbCodec.Size = new System.Drawing.Size(58, 21);
            this.cmbCodec.TabIndex = 21;
            this.cmbCodec.SelectedIndexChanged += new System.EventHandler(this.cmbCodec_SelectedIndexChanged);
            //
            // flowParts
            //
            this.flowParts.AutoSize = true;
            this.flowParts.Controls.Add(this.chkRPartVocal);
            this.flowParts.Controls.Add(this.chkRPartBass);
            this.flowParts.Controls.Add(this.chkRPartDrums);
            this.flowParts.Controls.Add(this.chkRPartPiano);
            this.flowParts.Controls.Add(this.chkRPartOther);
            this.flowParts.Location = new System.Drawing.Point(21, 23);
            this.flowParts.Name = "flowParts";
            this.flowParts.Size = new System.Drawing.Size(300, 23);
            this.flowParts.TabIndex = 39;
            this.flowParts.WrapContents = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = "Spleeter logo";
            this.pictureBox1.AccessibleName = "Spleeter logo";
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.pictureBox1.Location = new System.Drawing.Point(9, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(522, 628);
            this.Controls.Add(this.pnlRecombine);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.parts_label);
            this.Controls.Add(this.parts_btn5);
            this.Controls.Add(this.parts_btn4);
            this.Controls.Add(this.parts_btn2);
            this.Controls.Add(this.lblPartsTitle);
            this.Controls.Add(this.lblSlogan2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblSlogan1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SpleeterGUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.duration)).EndInit();
            this.flowOptions.ResumeLayout(false);
            this.flowOptions.PerformLayout();
            this.flowParts.ResumeLayout(false);
            this.flowParts.PerformLayout();
            this.pnlRecombine.ResumeLayout(false);
            this.pnlRecombine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnSaveTo;
        private System.Windows.Forms.TextBox txt_output_directory;
        private System.Windows.Forms.CheckBox chkFullBandwidth;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem helpFAQToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spleeterGithubPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makenItSoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Label lblDroptext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSlogan1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblSlogan2;
        private System.Windows.Forms.Label lblPartsTitle;
        private System.Windows.Forms.Button parts_btn2;
        private System.Windows.Forms.Button parts_btn4;
        private System.Windows.Forms.Button parts_btn5;
        private System.Windows.Forms.Label parts_label;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label progress_txt;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem spleeterupgradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkSpleeterGUIUpdateToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.CheckBox chkRecombine;
        private System.Windows.Forms.Panel pnlRecombine;
        private System.Windows.Forms.CheckBox chkRPartOther;
        private System.Windows.Forms.CheckBox chkRPartPiano;
        private System.Windows.Forms.CheckBox chkRPartDrums;
        private System.Windows.Forms.CheckBox chkRPartBass;
        private System.Windows.Forms.CheckBox chkRPartVocal;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPythonPathToolStripMenuItem1;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblMaxLength;
        private System.Windows.Forms.NumericUpDown duration;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowOptions;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbCodec;
        private System.Windows.Forms.FlowLayoutPanel flowParts;
    }
}

