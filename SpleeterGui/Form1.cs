using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 2019 Maken it so.
// https://www.youtube.com/c/makenitso


namespace SpleeterGui
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            load_stuff();
        }
        public void load_stuff()
        {
            txt_output_directory.Text = Properties.Settings.Default.output_location;
            
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
            string stems = "1";
            if (stems1.Checked)
            {
                stems = "1";
            }else if (stems2.Checked)
            {
                stems = "2";
            }
            else if (stems4.Checked)
            {
                stems = "4";
            }
            else if (stems5.Checked)
            {
                stems = "5";
            }
            runCmd(@"/k python\\python -m spleeter separate -i " + (char)34 + txt_input_song.Text + (char)34 +" -o " + (char)34 + txt_output_directory.Text + (char)34+" -p spleeter:"+ stems+"stems");
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
    }
}
