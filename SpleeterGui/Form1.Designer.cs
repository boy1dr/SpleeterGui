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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.stems2 = new System.Windows.Forms.RadioButton();
            this.stems4 = new System.Windows.Forms.RadioButton();
            this.stems5 = new System.Windows.Forms.RadioButton();
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
            this.chkRecombine = new System.Windows.Forms.CheckBox();
            this.pnlRecombine = new System.Windows.Forms.Panel();
            this.chkRPartOther = new System.Windows.Forms.CheckBox();
            this.chkRPartPiano = new System.Windows.Forms.CheckBox();
            this.chkRPartDrums = new System.Windows.Forms.CheckBox();
            this.chkRPartBass = new System.Windows.Forms.CheckBox();
            this.chkRPartVocal = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMaxLength = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.duration = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRecombine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.duration)).BeginInit();
            this.SuspendLayout();
            // 
            // stems2
            // 
            this.stems2.Location = new System.Drawing.Point(0, 0);
            this.stems2.Name = "stems2";
            this.stems2.Size = new System.Drawing.Size(104, 24);
            this.stems2.TabIndex = 36;
            // 
            // stems4
            // 
            this.stems4.Location = new System.Drawing.Point(0, 0);
            this.stems4.Name = "stems4";
            this.stems4.Size = new System.Drawing.Size(104, 24);
            this.stems4.TabIndex = 35;
            // 
            // stems5
            // 
            this.stems5.Location = new System.Drawing.Point(0, 0);
            this.stems5.Name = "stems5";
            this.stems5.Size = new System.Drawing.Size(104, 24);
            this.stems5.TabIndex = 34;
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
            this.mnuLanguage.Image = global::SpleeterGui.Properties.Resources._649656_global_32x32;
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
            this.setPythonPathToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.setPythonPathToolStripMenuItem1.Text = "Set python path";
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
            this.spleeterupgradeToolStripMenuItem.Name = "spleeterupgradeToolStripMenuItem";
            this.spleeterupgradeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.spleeterupgradeToolStripMenuItem.Text = "Spleeter core &upgrade";
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
            this.txt_output_directory.Location = new System.Drawing.Point(118, 67);
            this.txt_output_directory.Name = "txt_output_directory";
            this.txt_output_directory.Size = new System.Drawing.Size(364, 22);
            this.txt_output_directory.TabIndex = 9;
            // 
            // chkFullBandwidth
            // 
            this.chkFullBandwidth.AccessibleDescription = "enable high quality mode (may increase noise)";
            this.chkFullBandwidth.AccessibleName = "full bandwidth";
            this.chkFullBandwidth.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.chkFullBandwidth.AutoSize = true;
            this.chkFullBandwidth.Checked = true;
            this.chkFullBandwidth.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.textBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Location = new System.Drawing.Point(4, 208);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(492, 229);
            this.textBox1.TabIndex = 16;
            // 
            // lblDroptext
            // 
            this.lblDroptext.AutoSize = true;
            this.lblDroptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroptext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDroptext.Location = new System.Drawing.Point(43, 103);
            this.lblDroptext.Name = "lblDroptext";
            this.lblDroptext.Size = new System.Drawing.Size(412, 24);
            this.lblDroptext.TabIndex = 13;
            this.lblDroptext.Text = "Drop your music file(s) here to begin processing";
            // 
            // lblSlogan1
            // 
            this.lblSlogan1.AutoSize = true;
            this.lblSlogan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlogan1.ForeColor = System.Drawing.Color.Gray;
            this.lblSlogan1.Location = new System.Drawing.Point(308, 56);
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
            this.lblSlogan2.Location = new System.Drawing.Point(308, 75);
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
            this.parts_btn5.AccessibleDescription = "Separate song in 4 parts";
            this.parts_btn5.AccessibleName = "five parts";
            this.parts_btn5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.parts_btn5.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.parts_label.AutoSize = true;
            this.parts_label.Location = new System.Drawing.Point(293, 135);
            this.parts_label.Name = "parts_label";
            this.parts_label.Size = new System.Drawing.Size(122, 13);
            this.parts_label.TabIndex = 6;
            this.parts_label.Text = "Vocal + Accompaniment";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.progress_txt.Location = new System.Drawing.Point(61, 165);
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
            this.pnlMain.Controls.Add(this.duration);
            this.pnlMain.Controls.Add(this.lblSeconds);
            this.pnlMain.Controls.Add(this.lblMaxLength);
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
            // chkRecombine
            // 
            this.chkRecombine.AutoSize = true;
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
            this.pnlRecombine.Controls.Add(this.chkRPartOther);
            this.pnlRecombine.Controls.Add(this.chkRecombine);
            this.pnlRecombine.Controls.Add(this.chkRPartPiano);
            this.pnlRecombine.Controls.Add(this.chkRPartDrums);
            this.pnlRecombine.Controls.Add(this.chkRPartBass);
            this.pnlRecombine.Controls.Add(this.chkRPartVocal);
            this.pnlRecombine.Location = new System.Drawing.Point(23, 162);
            this.pnlRecombine.Name = "pnlRecombine";
            this.pnlRecombine.Size = new System.Drawing.Size(493, 20);
            this.pnlRecombine.TabIndex = 39;
            // 
            // chkRPartOther
            // 
            this.chkRPartOther.AutoSize = true;
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
            this.chkRPartVocal.Location = new System.Drawing.Point(24, 26);
            this.chkRPartVocal.Name = "chkRPartVocal";
            this.chkRPartVocal.Size = new System.Drawing.Size(53, 17);
            this.chkRPartVocal.TabIndex = 1;
            this.chkRPartVocal.Text = "Vocal";
            this.chkRPartVocal.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = "Spleeter logo";
            this.pictureBox1.AccessibleName = "Spleeter logo";
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(279, 71);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // lblMaxLength
            // 
            this.lblMaxLength.AutoSize = true;
            this.lblMaxLength.Location = new System.Drawing.Point(15, 39);
            this.lblMaxLength.Name = "lblMaxLength";
            this.lblMaxLength.Size = new System.Drawing.Size(109, 13);
            this.lblMaxLength.TabIndex = 17;
            this.lblMaxLength.Text = "Maximum song length";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(176, 39);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 18;
            this.lblSeconds.Text = "seconds";
            // 
            // duration
            // 
            this.duration.Location = new System.Drawing.Point(124, 37);
            this.duration.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.duration.Name = "duration";
            this.duration.Size = new System.Drawing.Size(49, 20);
            this.duration.TabIndex = 19;
            this.duration.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.duration.ValueChanged += new System.EventHandler(this.duration_ValueChanged);
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
            this.Controls.Add(this.stems5);
            this.Controls.Add(this.stems4);
            this.Controls.Add(this.stems2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SpleeterGUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlRecombine.ResumeLayout(false);
            this.pnlRecombine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.duration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton stems2;
        private System.Windows.Forms.RadioButton stems4;
        private System.Windows.Forms.RadioButton stems5;
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
    }
}

