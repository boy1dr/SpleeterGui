using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

// 2019 Maken it so.
// https://www.youtube.com/c/makenitso

namespace SpleeterGui
{
    public partial class Form1 : Form
    {
        private string file_list = "";
        private string stem_count = "2";
        private string mask_extension = "zeros";
        private int files_remain = 0;

        public Form1()
        {
            InitializeComponent();
            
        }
        public void LoadStuff()
        {
            panel1.Visible = false;
            txt_output_directory.Text = Properties.Settings.Default.output_location;
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            files_remain = 0;
            foreach (string file in files)
            {
                Console.WriteLine(file);
                file_list += (char)34 + file + (char)34 + " ";
                files_remain++;
            }
            if (txt_output_directory.Text == "")
            {
                MessageBox.Show("Please select an output directory");
                return;
            }
            files_remain = files_remain * Int32.Parse( stem_count);
            progressBar1.Maximum = files_remain + 1;
            files_remain--;
            progress_txt.Text = "Starting..." + files_remain / Int32.Parse(stem_count) + " songs remaining";
            System.IO.File.WriteAllText(@"config.json", get_config_string());
            System.IO.File.WriteAllText(@"spleeter.bat", Properties.Settings.Default.python_path + @" -W ignore -m spleeter separate -i " + file_list  + " -o " + (char)34 + txt_output_directory.Text + (char)34 + " -p config.json" + "\r\n" + "@echo finished");
            textBox1.Text = "Starting\r\n";
            consoleControl1.StartProcess("spleeter.bat", "");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_output_directory.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.output_location = txt_output_directory.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                txt_output_directory.Text = "";
            }
        }

        public void runCmd(String command)
        {
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            while (!cmd.HasExited)
            {
                Application.DoEvents();
            }
            cmd.Close();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.python_path = openFileDialog2.FileName.ToString();
            Properties.Settings.Default.Save();
        }

        private string get_config_string()
        {
            string readText = File.ReadAllText(stem_count + "stems.json");
            if (mask_extension == "average")
            {
                readText = readText.Replace("zeros", "average");
            }
            return readText;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mask_extension = checkBox1.Checked ? "average" : "zeros";
        }

        private void consoleControl1_OnConsoleOutput(object sender, ConsoleControl.ConsoleEventArgs args)
        {
            String theText = args.Content.Replace("\\\\", "\\");
            textBox1.Text = theText + "\n" + textBox1.Text;
            if ((" " + theText).IndexOf("INFO:spleeter:File") > 0 && (" " + theText).IndexOf("written") > 0)
            {
                files_remain--;
            }
            int newval = progressBar1.Maximum - files_remain - 1;
            if(newval> progressBar1.Maximum) { newval = progressBar1.Maximum; }
            progressBar1.Value = progressBar1.Maximum - files_remain - 1;
            if (files_remain > 0)
            {
                progress_txt.Text = "Working..." + ((files_remain / Int32.Parse(stem_count)) + 1) + " songs remaining";
            }
            if (theText == "finished" + "\r\n")
            {
                progress_txt.Text = "idle";
            }
        }

        private void spleeterGithubPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/deezer/spleeter");
        }

        private void makenItSoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/user/mitchellcj/videos");
        }

        private void setPythonLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void runSpleeterInstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runCmd(Properties.Settings.Default.python_path + @" -m pip install spleeter");
        }

        private void stems2_Click(object sender, EventArgs e)
        {
            stem_count = "2";
        }

        private void stems4_Click(object sender, EventArgs e)
        {
            stem_count = "4";
        }

        private void stems5_Click(object sender, EventArgs e)
        {
            stem_count = "5";
        }

        private void showDebugPanelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (showDebugPanelToolStripMenuItem1.Text == "Show debug panel")
            {
                showDebugPanelToolStripMenuItem1.Text = "Hide debug panel";
                panel1.Visible = true;
                Form1.ActiveForm.Height = 560;
            }
            else
            {
                showDebugPanelToolStripMenuItem1.Text = "Show debug panel";
                panel1.Visible = false;
                Form1.ActiveForm.Height = 420;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            this.MinimumSize = new Size(510,420);
        }

        private void helpFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://makenweb.com/spleeter_help.php");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadStuff();
        }

        private void parts_btn2_Click(object sender, EventArgs e)
        {
            parts_label.Text = "Vocal + Accompaniment";
            parts_btn2.UseVisualStyleBackColor = false;
            parts_btn4.UseVisualStyleBackColor = true;
            parts_btn5.UseVisualStyleBackColor = true;
            stem_count = "2";
        }

        private void parts_btn4_Click(object sender, EventArgs e)
        {
            parts_label.Text = "Vocal + Bass + Drums + Other";
            parts_btn2.UseVisualStyleBackColor = true;
            parts_btn4.UseVisualStyleBackColor = false;
            parts_btn5.UseVisualStyleBackColor = true;
            stem_count = "4";
        }

        private void parts_btn5_Click(object sender, EventArgs e)
        {
            parts_label.Text = "Vocal + Bass + Drums + Piano + Other";
            parts_btn2.UseVisualStyleBackColor = true;
            parts_btn4.UseVisualStyleBackColor = true;
            parts_btn5.UseVisualStyleBackColor = false;
            stem_count = "5";
        }

    }
}
