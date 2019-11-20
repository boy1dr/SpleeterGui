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
            this.button1 = new System.Windows.Forms.Button();
            this.stems1 = new System.Windows.Forms.RadioButton();
            this.stems2 = new System.Windows.Forms.RadioButton();
            this.stems4 = new System.Windows.Forms.RadioButton();
            this.stems5 = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSpleeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.makenItSoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_input_song = new System.Windows.Forms.TextBox();
            this.txt_output_directory = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.button1.Location = new System.Drawing.Point(462, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Choose song";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stems1
            // 
            this.stems1.AutoSize = true;
            this.stems1.Location = new System.Drawing.Point(28, 143);
            this.stems1.Name = "stems1";
            this.stems1.Size = new System.Drawing.Size(74, 17);
            this.stems1.TabIndex = 4;
            this.stems1.Text = "Vocal only";
            this.stems1.UseVisualStyleBackColor = true;
            // 
            // stems2
            // 
            this.stems2.AutoSize = true;
            this.stems2.Checked = true;
            this.stems2.Location = new System.Drawing.Point(28, 166);
            this.stems2.Name = "stems2";
            this.stems2.Size = new System.Drawing.Size(140, 17);
            this.stems2.TabIndex = 5;
            this.stems2.TabStop = true;
            this.stems2.Text = "Vocal + Accompaniment";
            this.stems2.UseVisualStyleBackColor = true;
            // 
            // stems4
            // 
            this.stems4.AutoSize = true;
            this.stems4.Location = new System.Drawing.Point(28, 189);
            this.stems4.Name = "stems4";
            this.stems4.Size = new System.Drawing.Size(167, 17);
            this.stems4.TabIndex = 6;
            this.stems4.Text = "Vocal + Bass + Drums + Other";
            this.stems4.UseVisualStyleBackColor = true;
            // 
            // stems5
            // 
            this.stems5.AutoSize = true;
            this.stems5.Location = new System.Drawing.Point(28, 212);
            this.stems5.Name = "stems5";
            this.stems5.Size = new System.Drawing.Size(206, 17);
            this.stems5.TabIndex = 7;
            this.stems5.Text = "Vocal + Bass + Drums + Piano + Other";
            this.stems5.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(597, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSpleeterToolStripMenuItem,
            this.toolStripMenuItem2,
            this.makenItSoToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // aboutSpleeterToolStripMenuItem
            // 
            this.aboutSpleeterToolStripMenuItem.Name = "aboutSpleeterToolStripMenuItem";
            this.aboutSpleeterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.aboutSpleeterToolStripMenuItem.Text = "Spleeter Github page";
            this.aboutSpleeterToolStripMenuItem.Click += new System.EventHandler(this.AboutSpleeterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
            // 
            // makenItSoToolStripMenuItem
            // 
            this.makenItSoToolStripMenuItem.Name = "makenItSoToolStripMenuItem";
            this.makenItSoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.makenItSoToolStripMenuItem.Text = "Maken It So";
            this.makenItSoToolStripMenuItem.Click += new System.EventHandler(this.MakenItSoToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "mp3 Files|*.mp3|Wav Files|*.wav|FLAC files|*.flac";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.button2.Location = new System.Drawing.Point(462, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 22);
            this.button2.TabIndex = 10;
            this.button2.Text = "Save To";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button3.Location = new System.Drawing.Point(423, 186);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 43);
            this.button3.TabIndex = 11;
            this.button3.Text = "Process";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.Location = new System.Drawing.Point(20, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Choose output type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Input song";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.Location = new System.Drawing.Point(20, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Output directory";
            // 
            // txt_input_song
            // 
            this.txt_input_song.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txt_input_song.Location = new System.Drawing.Point(114, 44);
            this.txt_input_song.Name = "txt_input_song";
            this.txt_input_song.Size = new System.Drawing.Size(342, 22);
            this.txt_input_song.TabIndex = 15;
            // 
            // txt_output_directory
            // 
            this.txt_output_directory.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txt_output_directory.Location = new System.Drawing.Point(114, 80);
            this.txt_output_directory.Name = "txt_output_directory";
            this.txt_output_directory.Size = new System.Drawing.Size(342, 22);
            this.txt_output_directory.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 261);
            this.Controls.Add(this.txt_output_directory);
            this.Controls.Add(this.txt_input_song);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.stems5);
            this.Controls.Add(this.stems4);
            this.Controls.Add(this.stems2);
            this.Controls.Add(this.stems1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SpleeterGUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton stems1;
        private System.Windows.Forms.RadioButton stems2;
        private System.Windows.Forms.RadioButton stems4;
        private System.Windows.Forms.RadioButton stems5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSpleeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem makenItSoToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_input_song;
        private System.Windows.Forms.TextBox txt_output_directory;
    }
}

