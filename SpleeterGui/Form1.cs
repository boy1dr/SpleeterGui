using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace SpleeterGui
{
    public partial class Form1 : Form
    {
        private string stem_count = "2";
        private string mask_extension = "average";
        private string storage = "";

        private string path_python = "";    //needs to be the SpleeterGUI folder, not python
        
        private string current_songname = "";
        private int files_remain = 0;
        private List<string> files_to_process = new List<string>();
        private Boolean run_silent = true;
        private String gui_version = "";
        IDictionary<string, string> langStr = new Dictionary<string, string>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadStuff();
        }

        public void LoadStuff()
        {
            //program startup - initialise things
            txt_output_directory.Text = Properties.Settings.Default.output_location;

            if (Properties.Settings.Default.path_python == "")
            {
                path_python = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SpleeterGUI\python";
                storage = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SpleeterGUI";
            }
            else
            {
                path_python = Properties.Settings.Default.path_python + @"\python";
                storage = Properties.Settings.Default.path_python;
            }

            
            gui_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            String version = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            this.Text = "SpleeterGUI " + version;

            
           

            duration.Value = Properties.Settings.Default.duration;

            update_checks();
            get_languages();
            update_language(Properties.Settings.Default.language);

            string txt = langStr["LoadStuff_textBox1"];
            txt = txt.Replace("[NL]", "\r\n");
            textBox1.Text = txt + "...\r\n";
            run_cmd("pip show spleeter");
        }

        void get_languages()
        {
            //find and load language files in to menu toolstrip
            ToolStrip language_menu = new ToolStrip();
            var enviroment = System.Environment.CurrentDirectory;
            string[] fileEntries = Directory.GetFiles(enviroment + "\\languages");


            ToolStripMenuItem[] items = new ToolStripMenuItem[ fileEntries.Length ];
            int i = 0;
            foreach (string fileName in fileEntries) {
                string name = Path.GetFileName(fileName);
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;
                FileStream fs = new FileStream(enviroment + "\\languages\\" + name, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode = xmldoc.GetElementsByTagName("language");
                string lang_text = xmlnode[0].ChildNodes.Item(0).InnerText.Trim();

                items[i] = new ToolStripMenuItem();
                items[i].Text = lang_text + " (" + name.Replace(".xml","") + ")";
                items[i].Tag = name.Replace(".xml", "");
                items[i].Click += new EventHandler(LanguageItemClickHandler);
                i++;
            }
            this.mnuLanguage.DropDownItems.AddRange(items);
        }

        private void LanguageItemClickHandler(object sender, EventArgs e)
        {
            //a language is chosen by the user, load it up
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            update_language(clickedItem.Tag.ToString());
        }

        void update_language(string lang_name)
        {
            // Read the XML language files, iterate through menu's & controls and update labels.
            Properties.Settings.Default.language = lang_name;
            Properties.Settings.Default.Save();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            string control_name = null;
            string control_label = null;
            var enviroment = System.Environment.CurrentDirectory;
            FileStream fs = new FileStream(enviroment + "\\languages\\" + lang_name + ".xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("item");  //load control texts
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_label = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_name = xmlnode[i].Attributes["control"].InnerText;

                Control ctn = Controls.Find(control_name, true)[0];
                ctn.Text = control_label;
            }
            xmlnode = xmldoc.GetElementsByTagName("menu");  //load menu texts
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_label = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_name = xmlnode[i].Attributes["control"].InnerText;

                foreach (ToolStripMenuItem item in menuStrip1.Items) { 
                    if (item.Name == control_name) { 
                        item.Text = control_label;
                    }
                    foreach (ToolStripMenuItem subitem in item.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        if (subitem.Name == control_name)
                        {
                            subitem.Text = control_label;
                        }
                    }
                }
            }
            xmlnode = xmldoc.GetElementsByTagName("lang");  //load all the program texts
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_label = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                control_name = xmlnode[i].Attributes["control"].InnerText;
                langStr[control_name] = control_label;
            }
            progress_txt.Text = langStr["idle"];
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //music files have been dropped on the app, start processing them
            if (files_remain == 0)
            {
                textBox1.Text = "";
                if (txt_output_directory.Text == "")
                {
                    MessageBox.Show(langStr["output_message"]);
                    return;
                }

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                files_remain = 0;
                foreach (string file in files)
                {
                    files_to_process.Add(file);
                    files_remain++;
                }
                textBox1.AppendText(langStr["starting_all"] + "\r\n");
                progressBar1.Maximum = files_remain + 1;
                progressBar1.Value = 0; 
                progress_txt.Text = langStr["starting"] + "..." + files_remain + " " + langStr["songs_remaining"];
                next_song();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
        }
   
        private void next_song()
        {
            //begins the spleeting function on the next song in the queue
            if (files_remain > 0)
            {
                run_silent = false;
                //string pyPath = storage + @"\python\python.exe";
                string pyPath = path_python + @"\python.exe";
                
                string filename = files_to_process[0];
                
                progressBar1.Value = progressBar1.Value + 1;
                System.IO.File.WriteAllText(storage + @"\config.json", get_config_string());
                textBox1.AppendText(langStr["processing"] + " " + filename + "\r\n");
                progress_txt.Text = langStr["working"] + "..." + files_remain + " "+ langStr["songs_remaining"];

                ProcessStartInfo processStartInfo = new ProcessStartInfo(pyPath, @" -W ignore -m spleeter separate  -o " + (char)34 + txt_output_directory.Text + (char)34 + " -d " + (duration.Value).ToString() + " -p " + (char)34 + storage + @"\config.json" + (char)34 + " " + (char)34 + filename + (char)34);

                processStartInfo.WorkingDirectory = storage;

                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;

                files_to_process.Remove(filename);

                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(ProcessExited);
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
                try
                {
                    bool processStarted = process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    current_songname = Path.GetFileNameWithoutExtension(filename);
                }
                catch
                {
                    MessageBox.Show("Error: unable to find python.exe");
                }
            }
            else
            {
                current_songname = "";
                progress_txt.Text = langStr["idle"];
                textBox1.AppendText(langStr["finished"] + "\r\n");
                progressBar1.Value = progressBar1.Maximum;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void run_cmd(String cmd)
        {
            //general function for executing python commands.
            try
            {
                ProcessStartInfo processStartInfo;
                string pyPath = path_python + @"\python.exe";

                processStartInfo = new ProcessStartInfo(pyPath, @" -W ignore -m " + cmd);
                processStartInfo.WorkingDirectory = storage;

                processStartInfo.UseShellExecute = false;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(ProcessExited);
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);

                bool processStarted = process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch
            {
                MessageBox.Show("Unable to find python.exe");
            }
        }
        private void run_recombine(String args)
        {
            //executes the ffmpeg comand to recombine the output stems
            ProcessStartInfo processStartInfo = new ProcessStartInfo(storage + @"\ffmpeg.exe", args);
            processStartInfo.WorkingDirectory = storage;

            processStartInfo.UseShellExecute = false;
            processStartInfo.ErrorDialog = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(run_recombineExited);
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
            bool processStarted = process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            //output handler called by run_cmd
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    if (txt_check(e.Data))   //Please don't email Deezer about problems with this GUI app.
                    {
                        textBox1.AppendText(e.Data.TrimEnd('\r', '\n') + "\r\n");
                    }
                }
            }));
        }
        bool txt_check(string txt)  //prevent output
        {
            bool allow = true;
            if (txt.IndexOf("Author-email") !=-1){ allow = false; }
            if (txt.IndexOf("Summary:") != -1) { allow = false; }
            if (txt.IndexOf("source separation library") != -1) { allow = false; }
            if (txt.IndexOf("models based on") != -1) { allow = false; }
            if (txt.IndexOf("Home-page:") != -1) { allow = false; }
            if (txt.IndexOf("Author:") != -1) { allow = false; }
            if (txt.IndexOf("License:") != -1) { allow = false; }
            if (txt.IndexOf("Location:") != -1) { allow = false; }
            if (txt.IndexOf("Requires:") != -1) { allow = false; }
            if (txt.IndexOf("Required-by:") != -1) { allow = false; }
            return allow;
        }
        void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            //handles errors from the run_cmd functions
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    textBox1.AppendText(e.Data.TrimEnd('\r', '\n') + "\r\n");
                }
            }));
        }
        private void run_recombineExited(object sender, EventArgs e)
        {
            //cleanup function called by run_recombine
            Invoke((Action)(() =>
            {
                //do nothing
            }));
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            //called by run_cmd when thread exits after spleeting a song. runs the recombine (if enabled) and starts processing next song in queue.
            Invoke((Action)(() =>
            {
                //recombine audio (if enabled)
                if (
                    current_songname!="" &&
                    chkRecombine.Checked == true && (
                    chkRPartVocal.Checked ||
                    chkRPartBass.Checked ||
                    chkRPartDrums.Checked ||
                    chkRPartPiano.Checked ||
                    chkRPartOther.Checked)
                    )
                {
                    String recomnbine_command = "";
                    int input_count = 0;
                    if (chkRPartVocal.Checked) { input_count++; recomnbine_command += " -i " + (char)34 + txt_output_directory.Text + @"\" + current_songname + @"\vocals.wav" + (char)34; }
                    if (chkRPartBass.Checked) { input_count++; recomnbine_command += " -i " + (char)34 + txt_output_directory.Text + @"\" + current_songname + @"\bass.wav" + (char)34; }
                    if (chkRPartDrums.Checked) { input_count++; recomnbine_command += " -i " + (char)34 + txt_output_directory.Text + @"\" + current_songname + @"\drums.wav" + (char)34; }
                    if (chkRPartPiano.Checked) { input_count++; recomnbine_command += " -i " + (char)34 + txt_output_directory.Text + @"\" + current_songname + @"\piano.wav" + (char)34; }
                    if (chkRPartOther.Checked) { input_count++; recomnbine_command += " -i " + (char)34 + txt_output_directory.Text + @"\" + current_songname + @"\other.wav" + (char)34; }
                    if (recomnbine_command != "")
                    {
                        String filter_a = "";
                        String filter_b = "";
                        for (int i = 0; i < input_count; i++)
                        {
                            filter_a += "["+i+"]volume=" + input_count + "["+((char)97+i) +"];";
                            filter_b += "[" + ((char)97 + i) + "]";
                        }
                        recomnbine_command = recomnbine_command + " -filter_complex " + (char)34 + filter_a + filter_b + "amix=inputs=" + input_count.ToString() + ":duration =longest" + (char)34 + " " + (char)34 + txt_output_directory.Text + @"\" + current_songname + "_recombined.wav" + (char)34;
                        run_recombine(recomnbine_command);
                    }
                }

                files_remain--;
                if (files_remain > -1) { 
                    //start processing the next song
                    next_song();
                }
                if (files_remain < 0) files_remain = 0;
                
                if (!run_silent)
                {
                    textBox1.AppendText("\r\n" + langStr["run_complete"] + "\r\n");
                    System.Media.SystemSounds.Beep.Play();
                }
            }));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //prompt user for output folder
            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = langStr["set_output"];
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
        private void setPythonPathToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //prompt user for python path
            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = storage;
            folderBrowserDialog1.Description = langStr["set_python_path"];
            folderBrowserDialog1.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                path_python = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.path_python = path_python;
                Properties.Settings.Default.Save();
                LoadStuff();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string get_config_string()
        {
            //reads the JSON config file for the current stem mode
            var enviroment = System.Environment.CurrentDirectory;
            string readText = File.ReadAllText(enviroment + @"\configs\" + stem_count + "stems.json");
            if (mask_extension == "average")
            {
                readText = readText.Replace("zeros", "average");
            }
            return readText;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //sets the full bandwidth mode (16Khz)
            mask_extension = chkFullBandwidth.Checked ? "average" : "zeros";
        }

        private void spleeterGithubPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - opens SpleeterGUI github page in a browser window
            System.Diagnostics.Process.Start("https://github.com/boy1dr/SpleeterGui");
        }

        private void makenItSoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //help - open's the Maken it so youtube channel in a browser window
            System.Diagnostics.Process.Start("https://www.youtube.com/user/mitchellcj/videos");
        }

        private void helpFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - open's the SpleeterGUI help page in a browser window
            System.Diagnostics.Process.Start("https://makenweb.com/spleeter_help.php");
        }

        private void parts_btn2_Click(object sender, EventArgs e)
        {
            //set the stem mode to 2
            parts_label.Text = langStr["vocal_accompaniment"];
            parts_btn2.UseVisualStyleBackColor = false;
            parts_btn4.UseVisualStyleBackColor = true;
            parts_btn5.UseVisualStyleBackColor = true;
            stem_count = "2";
            update_checks();
        }

        private void parts_btn4_Click(object sender, EventArgs e)
        {
            //set the stem mode to 4
            parts_label.Text = langStr["vocal_bass_drums_other"];
            parts_btn2.UseVisualStyleBackColor = true;
            parts_btn4.UseVisualStyleBackColor = false;
            parts_btn5.UseVisualStyleBackColor = true;
            stem_count = "4";
            update_checks();
        }

        private void parts_btn5_Click(object sender, EventArgs e)
        {
            //set the stem mode to 5
            parts_label.Text = langStr["vocal_bass_drums_piano_other"];
            parts_btn2.UseVisualStyleBackColor = true;
            parts_btn4.UseVisualStyleBackColor = true;
            parts_btn5.UseVisualStyleBackColor = false;
            stem_count = "5";
            update_checks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //choose a song(s) to spleet
            if (files_remain == 0)
            {
                openFileDialog2.ShowDialog();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            //files chosen, start spleeting
            if (files_remain == 0)
            {
                if (txt_output_directory.Text == "")
                {
                    MessageBox.Show(langStr["output_message"]);
                    return;
                }
                textBox1.Text = "";
                files_remain = 0;
                foreach (String file in openFileDialog2.FileNames)
                {
                    files_to_process.Add(file);
                    files_remain++;
                }
                textBox1.AppendText(langStr["starting_all"] + "\r\n");
                progressBar1.Maximum = files_remain + 1;
                progressBar1.Value = 0;
                progress_txt.Text = langStr["starting"] + "..." + files_remain + " " + langStr["songs_remaining"];
                next_song();
            }
        }

        private void spleeterupgradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - spleeter core upgrade
            run_silent = false;
            current_songname = "";
            textBox1.Text = langStr["run_update"] + "\r\n" + langStr["run_update_b"] + "\r\n\r\n";
            run_cmd("pip install --upgrade spleeter");
        }

        private void checkSpleeterGUIUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - check SpleeterGUI version
            WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/boy1dr/SpleeterGui/master/SpleeterGui/Properties/AssemblyInfo.cs");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            int posStart=0;
            int posEnd=0;
            String version_check = "";
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            if (html != "")
            {
                posStart = html.IndexOf("\n[assembly: AssemblyVersion(");
                if (posStart > 0)
                {
                    posStart+= 29;
                    posEnd = html.IndexOf('"', posStart);
                    if (posEnd > 0)
                    {
                        version_check = html.Substring(posStart, posEnd - posStart);
                        if(version_check!="" && version_check != gui_version)
                        {
                            MessageBox.Show(langStr["version"] + " " + version_check + " " + langStr["is_available"]);
                        }
                        else
                        {
                            MessageBox.Show(langStr["latest"]);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(langStr["unable"]);
            }
        }

        private void chkRecombine_CheckedChanged(object sender, EventArgs e)
        {
            update_checks();
        }

        private void update_checks()
        {
            //update the user interface based on the chosen stem count
            chkRPartVocal.Checked = false;
            chkRPartBass.Checked = false;
            chkRPartDrums.Checked = false;
            chkRPartPiano.Checked = false;
            chkRPartOther.Checked = false;

            if (stem_count == "2")
            {
                chkRecombine.Checked = false;
                chkRecombine.Enabled = false;
                pnlRecombine.Height = 20;
                pnlMain.Location = new Point(12, 182);
                this.Height = 677;
            }
            else
            {
                chkRecombine.Enabled = true;

                if (chkRecombine.Checked)
                {
                    pnlRecombine.Height = 50;
                    pnlMain.Location = new Point(12, 202);
                    this.Height = 697;
                }
                else
                {
                    pnlRecombine.Height = 20;
                    pnlMain.Location = new Point(12, 182);
                    this.Height = 677;

                    chkRPartVocal.Checked = false;
                    chkRPartBass.Checked = false;
                    chkRPartDrums.Checked = false;
                    chkRPartPiano.Checked = false;
                    chkRPartOther.Checked = false;
                }
                switch (stem_count)
                {
                    case "4":
                        chkRPartVocal.Enabled = true;
                        chkRPartBass.Enabled = true;
                        chkRPartDrums.Enabled = true;
                        chkRPartPiano.Enabled = false;
                        chkRPartOther.Enabled = true;
                        break;
                    case "5":
                        chkRPartVocal.Enabled = true;
                        chkRPartBass.Enabled = true;
                        chkRPartDrums.Enabled = true;
                        chkRPartPiano.Enabled = true;
                        chkRPartOther.Enabled = true;
                        break;
                }
            }
        }

        private void duration_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.duration = Convert.ToInt32(duration.Value);
            Properties.Settings.Default.Save();
        }
    }
}
