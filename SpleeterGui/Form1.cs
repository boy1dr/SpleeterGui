using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

// 2019 Maken it so.
// https://www.youtube.com/c/makenitso


namespace SpleeterGui
{
    public partial class Form1 : Form
    {
        private bool expert_mode = true;
        private string stem_count = "2";
        private string mask_extension = "zeros";

        public Form1()
        {
            InitializeComponent();
            load_stuff();
        }
        public void load_stuff()
        {
            txt_output_directory.Text = Properties.Settings.Default.output_location;

            python_path_txt.Text = Properties.Settings.Default.python_path;
            if (python_path_txt.Text == "")
            {
                python_path_txt.Text = @"python\python.exe";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void AboutSpleeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/deezer/spleeter");
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txt_input_song.Text = openFileDialog1.FileName.ToString();
        }
       

        private void Button2_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_output_directory.Text = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                txt_output_directory.Text = "";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (txt_input_song.Text == "")
            {
                MessageBox.Show("Please select an input file");
                return;
            }
            if (txt_output_directory.Text == "")
            {
                MessageBox.Show("Please select an output directory");
                return;
            }
            stem_count = "2";
             if (stems2.Checked)
            {
                stem_count = "2";
            }
            else if (stems4.Checked)
            {
                stem_count = "4";
            }
            else if (stems5.Checked)
            {
                stem_count = "5";
            }
            System.IO.File.WriteAllText(@"config.json", get_config_string());
            runCmd( @"/k "+ build_command() );
        }
        public void runCmd(String command)
        {
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();
        }

        private void MakenItSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/user/mitchellcj/videos");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.output_location = txt_output_directory.Text;
            Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggle_expert();
        }
        public void toggle_expert()
        {
            expert_mode = !expert_mode;
            if (expert_mode)
            {
                Form1.ActiveForm.Width = 980;
                menuStrip1.Items[1].Text = "&Expert Mode";
            }
            else
            {
                Form1.ActiveForm.Width = 610;
                menuStrip1.Items[1].Text = "Simpl&e Mode";
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            toggle_expert();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(build_command());
        }
        private string build_command()
        {
            String the_command = python_path_txt.Text + @" -m spleeter separate -i " + (char)34 + txt_input_song.Text + (char)34 + " -o " + (char)34 + txt_output_directory.Text + (char)34 + " -p config.json";
            return the_command;
        }

        private void python_path_set_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            python_path_txt.Text = openFileDialog2.FileName.ToString();
            Properties.Settings.Default.python_path = python_path_txt.Text;
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

        private void install_spleeter_btn_Click(object sender, EventArgs e)
        {
            runCmd( python_path_txt.Text + @" -m pip install spleeter");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://makenweb.com/spleeter_help.php");
        }
    }
}
