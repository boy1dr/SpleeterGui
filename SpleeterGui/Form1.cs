using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SpleeterGui
{
    public partial class Form1 : Form
    {
        private const string UrlGithub = "https://github.com/boy1dr/SpleeterGui";
        private const string UrlHelp = "https://makenweb.com/spleeter_help.php";
        private const string UrlYoutube = "https://www.youtube.com/user/mitchellcj/videos";
        private const string UrlVersionCheck = "https://raw.githubusercontent.com/boy1dr/SpleeterGui/master/SpleeterGui/Properties/AssemblyInfo.cs";

        // Extensions picked up when a whole folder is dropped on the window.
        private static readonly string[] AudioExtensions = {
            ".mp3", ".wav", ".ogg", ".m4a", ".wma", ".flac", ".aac", ".aif", ".aiff", ".opus", ".mp4", ".mka", ".webm"
        };

        // Folder the exe lives in. NOT the current directory: that is wrong whenever the app is
        // started from a shortcut, "Open with", a terminal, or after a file dialog changed it.
        private static readonly string appDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/');
        // Always-writable per-user folder (the install folder often is not writable).
        private static readonly string dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpleeterGUI");

        private string stem_count = "2";
        private string mask_extension = "average";

        private string storage = "";        // the "SpleeterGUI core" folder: holds python\, ffmpeg.exe and pretrained_models\
        private string path_python = "";    // folder that contains python.exe
        private Version spleeter_version = null;
        // True when python is the self-contained one that ships with SpleeterGUI (<core>\python\python.exe),
        // false when the user pointed the app at a python of their own.
        private bool bundled_python = true;

        // Batch state. Only touched on the UI thread.
        private readonly Queue<string> queue = new Queue<string>();
        private Process current_process = null;
        private bool busy = false;
        private volatile bool cancelling = false;    // also read by the process-exit callback
        private int songs_total = 0;
        private int songs_done = 0;
        private int songs_failed = 0;
        private string batch_output_dir = "";
        private string batch_codec = "wav";
        private bool upgrading = false;             // pip is running instead of a batch
        private string pending_hint = null;         // plain-language explanation of the current song's error, if recognised
        private readonly HashSet<string> batch_song_names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private bool legacy_cli = false;            // spleeter older than 2.1: input files are given with -i
        private bool cli_mismatch = false;          // spleeter rejected the way the input file was passed
        private string transient_output = null;     // output folder given with -o on the command line: used, not remembered

        private string gui_version = "";
        private bool loaded = false;
        private int base_main_top;
        private int base_client_height;

        private readonly Dictionary<string, string> langStr = new Dictionary<string, string>();

        // Files (or folders) given on the command line: "Open with", dropping songs on the exe icon,
        // or  SpleeterGui.exe [-o <output folder>] <song> [<song> ...]
        private readonly string[] startup_args;

        public Form1() : this(new string[0])
        {
        }

        public Form1(string[] args)
        {
            startup_args = args ?? new string[0];
            InitializeComponent();

            Icon icon = Assets.LoadIcon("form_icon.ico");
            if (icon != null) this.Icon = icon;
            pictureBox1.Image = Assets.LoadImage("spleeter_logo.png");
            mnuLanguage.Image = Assets.LoadImage("language.jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                remove_stale_configs();
                LoadStuff();
                process_startup_args();
            }
            catch (Exception ex)
            {
                // Never let a startup problem kill the window without telling the user why.
                textBox1.AppendText("Startup error: " + ex.Message + "\r\n");
            }
        }

        public void LoadStuff()
        {
            //program startup - initialise things
            if (!loaded)
            {
                loaded = true;

                // Remember the designed (already DPI-scaled) layout so update_checks() never needs pixel constants.
                base_main_top = pnlMain.Top;
                base_client_height = this.ClientSize.Height;

                gui_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text = "SpleeterGUI " + gui_version;

                txt_output_directory.Text = Properties.Settings.Default.output_location;

                int saved_duration = Properties.Settings.Default.duration;
                duration.Value = Math.Max(duration.Minimum, Math.Min(duration.Maximum, saved_duration));

                int codec_index = cmbCodec.Items.IndexOf(Properties.Settings.Default.codec);
                cmbCodec.SelectedIndex = codec_index >= 0 ? codec_index : 0;

                highlight_stem_button();
                update_checks();
                get_languages();
                update_language(Properties.Settings.Default.language);
            }

            resolve_core_paths();
            show_startup_text();
        }

        private void process_startup_args()
        {
            var files = new List<string>();
            for (int i = 0; i < startup_args.Length; i++)
            {
                string arg = startup_args[i];
                if ((arg == "-o" || arg == "--output") && i + 1 < startup_args.Length)
                {
                    string dir = startup_args[++i].Trim().Trim('"');
                    try
                    {
                        dir = Path.GetFullPath(dir);
                    }
                    catch (Exception)
                    {
                    }
                    transient_output = dir;
                    txt_output_directory.Text = dir;
                }
                else
                {
                    files.Add(arg);
                }
            }
            if (files.Count > 0) enqueue_files(files);
        }

        // ------------------------------------------------------------------
        //  Locating python / spleeter
        // ------------------------------------------------------------------

        private void resolve_core_paths()
        {
            // Candidates, in order: the folder chosen in Advanced > Set SpleeterGUI path,
            // the folder the exe is in (v2.9.5 layout), then %APPDATA%\SpleeterGUI (v2.9.1 and older installers).
            var candidates = new List<string>();
            string custom = (Properties.Settings.Default.path_python ?? "").Trim();
            if (custom != "") candidates.Add(custom);
            candidates.Add(appDir);
            candidates.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SpleeterGUI"));

            storage = appDir;
            path_python = Path.Combine(appDir, "python");
            bundled_python = true;

            foreach (string dir in candidates)
            {
                try
                {
                    if (File.Exists(Path.Combine(dir, "python", "python.exe")))
                    {
                        storage = dir;
                        path_python = Path.Combine(dir, "python");
                        break;
                    }
                    if (File.Exists(Path.Combine(dir, "python.exe")))
                    {
                        // The user pointed straight at a python folder (their own python with spleeter installed).
                        path_python = dir;
                        bundled_python = false;
                        string parent = Path.GetDirectoryName(dir.TrimEnd('\\', '/'));
                        bool parent_is_core = parent != null &&
                            (File.Exists(Path.Combine(parent, "ffmpeg.exe")) || Directory.Exists(Path.Combine(parent, "pretrained_models")));
                        storage = parent_is_core ? parent : appDir;
                        // ...unless it is the "python" folder of a core after all
                        if (parent_is_core) bundled_python = true;
                        break;
                    }
                }
                catch (Exception)
                {
                    // malformed path in settings - ignore and try the next candidate
                }
            }

            spleeter_version = find_spleeter_version();
            // Version unknown (no pip metadata)? The old argparse command line lived in spleeter\commands.
            legacy_cli = spleeter_version != null
                ? spleeter_version < new Version(2, 1)
                : site_dirs().Any(d => Directory.Exists(Path.Combine(d, "spleeter", "commands")));
        }

        private string[] site_dirs()
        {
            // embedded python, a normal install / conda env, and a venv (python.exe sits in Scripts\)
            return new[]
            {
                path_python,
                Path.Combine(path_python, "Lib", "site-packages"),
                Path.Combine(path_python, "..", "Lib", "site-packages"),
            };
        }

        private string python_exe()
        {
            return Path.Combine(path_python, "python.exe");
        }

        private Version find_spleeter_version()
        {
            // pip leaves a "spleeter-<version>.dist-info" folder next to the package.
            foreach (string place in site_dirs())
            {
                try
                {
                    if (!Directory.Exists(place)) continue;
                    foreach (string dir in Directory.GetDirectories(place, "spleeter-*", SearchOption.TopDirectoryOnly))
                    {
                        Match m = Regex.Match(Path.GetFileName(dir), @"^spleeter-(\d+(\.\d+){1,3})");
                        Version v;
                        if (m.Success && Version.TryParse(m.Groups[1].Value, out v)) return v;
                    }
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        private void show_startup_text()
        {
            string txt = L("LoadStuff_textBox1").Replace("[NL]", "\r\n");
            if (!File.Exists(python_exe()))
            {
                textBox1.Text = txt + "...\r\n\r\n" + python_missing_message() + "\r\n";
            }
            else if (spleeter_version != null)
            {
                textBox1.Text = txt + " [spleeter-" + spleeter_version + "]\r\n";
            }
            else
            {
                textBox1.Text = txt + " [?]\r\n";
            }
        }

        private string python_missing_message()
        {
            return L("python_missing") + "\r\n  " + python_exe() + "\r\n" + L("python_missing_hint");
        }

        // ------------------------------------------------------------------
        //  Languages
        // ------------------------------------------------------------------

        // English texts built in, so a missing or incomplete language file can never crash the app.
        private static readonly Dictionary<string, string> defaultStr = new Dictionary<string, string>
        {
            { "LoadStuff_textBox1", "Help: " + UrlHelp + "[NL]Issues: " + UrlGithub + "/issues[NL][NL]Showing spleeter version" },
            { "Form1_DragDrop_msgbox", "Please select an output directory" },
            { "starting", "Starting" },
            { "starting_all", "Starting processing of all songs" },
            { "songs_remaining", "songs remaining" },
            { "working", "Working" },
            { "processing", "Processing" },
            { "idle", "idle" },
            { "finished", "Finished processing all songs" },
            { "run_complete", "Run complete" },
            { "vocal_accompaniment", "Vocal + Accompaniment" },
            { "vocal_bass_drums_other", "Vocal + Bass + Drums + Other" },
            { "vocal_bass_drums_piano_other", "Vocal + Bass + Drums + Piano + Other" },
            { "output_message", "Please select an output directory" },
            { "run_update", "Running update command" },
            { "run_update_b", "This might take a while, wait for the beep" },
            { "version", "Version" },
            { "is_available", "is available" },
            { "latest", "You are using the latest version" },
            { "unable", "Unable get new version data, check internet connection" },
            { "set_output", "Choose output location" },
            { "set_python_path", "Choose location of python.exe" },
            // added in 2.9.6
            { "python_missing", "Could not find python.exe. Looked for:" },
            { "python_missing_hint", "Use Advanced > Set SpleeterGUI path to choose the folder that contains the \"python\" folder." },
            { "python_not_in_folder", "That folder does not contain python.exe or a \"python\" folder with python.exe in it." },
            { "output_unusable", "The output directory cannot be created or written to:" },
            { "saved_to", "Saved to" },
            { "song_failed", "FAILED - spleeter exited with error code" },
            { "no_output", "Spleeter finished but no output was written. Expected:" },
            { "recombining", "Recombining" },
            { "recombine_failed", "Recombine failed (ffmpeg error code" },
            { "added_to_queue", "Added to queue" },
            { "no_audio_files", "No music files found." },
            { "cancelled", "Cancelled" },
            { "failed_count", "failed" },
            { "confirm_exit", "Songs are still being processed. Stop and exit?" },
            { "no_pip", "This python does not include pip, so the spleeter core cannot be upgraded from here." },
            { "truncated_hint", "Note: only the first {0} seconds of each song are processed (\"Maximum song length\")." },
            { "same_name_note", "Another song in this batch has the same name, so this one is saved as \"{0}\"." },
            { "hint_model", "What this means: the model in this folder is incomplete (a download was interrupted). Delete the folder and try again; it is downloaded again automatically:" },
            { "hint_memory", "What this means: the computer ran out of memory. Try a shorter \"Maximum song length\", fewer parts, or close other programs." },
            { "hint_download", "What this means: the model could not be downloaded. Check the internet connection (or proxy / firewall) and try again. Models are fetched from github.com/deezer/spleeter and stored in:" },
            { "hint_dll", "What this means: a file of the Spleeter core could not be loaded. Antivirus software may have removed it, or the Microsoft Visual C++ Redistributable (x64) is missing. Reinstalling SpleeterGUI usually fixes it." },
            { "hint_ffmpeg", "What this means: ffmpeg.exe / ffprobe.exe were not found. They belong in the SpleeterGUI core folder, next to the \"python\" folder." },
            { "hint_no_spleeter", "What this means: spleeter is not installed in this python:" },
            { "hint_bad_file", "What this means: this file could not be read as audio. It may be damaged, or not a music file." },
            { "scanning", "Looking for music files..." },
            { "model_busy", "The model in this folder is incomplete, and another SpleeterGUI window is open that may be downloading it right now. Try again when that has finished (or close the other window):" },
            { "model_incomplete", "The model in this folder is incomplete, so it is downloaded again:" },
        };

        private string L(string key)
        {
            string value;
            if (langStr.TryGetValue(key, out value) && !string.IsNullOrEmpty(value)) return value;
            if (defaultStr.TryGetValue(key, out value)) return value;
            return key;
        }

        private static string languages_dir()
        {
            return Path.Combine(appDir, "languages");
        }

        void get_languages()
        {
            //find and load language files in to menu toolstrip
            if (!Directory.Exists(languages_dir())) return;

            var items = new List<ToolStripItem>();
            foreach (string fileName in Directory.GetFiles(languages_dir(), "*.xml").OrderBy(f => f, StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(fileName);
                    XmlNodeList nodes = xmldoc.GetElementsByTagName("language");
                    string lang_text = nodes.Count > 0 ? nodes[0].InnerText.Trim() : name;

                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = lang_text + " (" + name + ")";
                    item.Tag = name;
                    item.Click += new EventHandler(LanguageItemClickHandler);
                    items.Add(item);
                }
                catch (Exception)
                {
                    // a broken language file just doesn't get listed
                }
            }
            this.mnuLanguage.DropDownItems.AddRange(items.ToArray());
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
            // English is applied first so anything a translation lacks still gets a proper label.
            langStr.Clear();
            if (string.IsNullOrEmpty(lang_name)) lang_name = "english";

            bool ok = true;
            if (!string.Equals(lang_name, "english", StringComparison.OrdinalIgnoreCase))
            {
                apply_language_file("english");
                ok = apply_language_file(lang_name);
                if (!ok) lang_name = "english";
            }
            else
            {
                apply_language_file("english");
            }

            if (Properties.Settings.Default.language != lang_name)
            {
                Properties.Settings.Default.language = lang_name;
                save_settings();
            }

            foreach (ToolStripMenuItem item in mnuLanguage.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = string.Equals(item.Tag as string, lang_name, StringComparison.OrdinalIgnoreCase);
            }

            // texts that depend on state rather than on a single control
            update_parts_label();
            fit_texts();
            if (!busy) progress_txt.Text = L("idle");
            btnSelectFiles.Left = Math.Max(0, (pnlMain.ClientSize.Width - btnSelectFiles.Width) / 2);
        }

        private bool apply_language_file(string lang_name)
        {
            string file = Path.Combine(languages_dir(), lang_name + ".xml");
            if (!File.Exists(file)) return false;

            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(file);
            }
            catch (Exception)
            {
                return false;
            }

            foreach (XmlNode node in xmldoc.GetElementsByTagName("item"))  //load control texts
            {
                string control_name = attr(node, "control");
                string control_label = node.InnerText.Trim();
                if (control_name == "" || control_label == "") continue;

                // The stem buttons keep their digits: several translations spell the number out,
                // which never fitted on the buttons.
                if (control_name.StartsWith("parts_btn", StringComparison.OrdinalIgnoreCase)) continue;

                Control[] found = Controls.Find(control_name, true);
                if (found.Length > 0) found[0].Text = literal_ampersand(control_label);
            }
            foreach (XmlNode node in xmldoc.GetElementsByTagName("menu"))  //load menu texts
            {
                string control_name = attr(node, "control");
                string control_label = literal_ampersand(node.InnerText.Trim());
                if (control_name == "" || control_label == "") continue;

                foreach (ToolStripMenuItem item in menuStrip1.Items.OfType<ToolStripMenuItem>())
                {
                    if (item.Name == control_name) item.Text = control_label;
                    foreach (ToolStripMenuItem subitem in item.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        if (subitem.Name == control_name) subitem.Text = control_label;
                    }
                }
            }
            foreach (XmlNode node in xmldoc.GetElementsByTagName("lang"))  //load all the program texts
            {
                string control_name = attr(node, "control");
                string control_label = node.InnerText.Trim();
                if (control_name == "" || control_label == "") continue;
                langStr[control_name] = control_label;
            }
            return true;
        }

        private static string literal_ampersand(string text)
        {
            // "Help & FAQ": an ampersand followed by a space is meant literally, not as an access key marker
            return Regex.Replace(text, @"(?<!&)&(?=\s)", "&&");
        }

        private static string attr(XmlNode node, string name)
        {
            XmlAttribute a = node.Attributes == null ? null : node.Attributes[name];
            return a == null ? "" : a.Value.Trim();
        }

        private static void save_settings()
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                // a locked/corrupt user.config must not take the app down
            }
        }

        // ------------------------------------------------------------------
        //  Queueing songs
        // ------------------------------------------------------------------

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //music files have been dropped on the app, start processing them
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length == 0) return;

            // Return from the drop handler first: Explorer stays frozen until we do,
            // and enqueue_files() may show a message box.
            BeginInvoke((Action)(() => enqueue_files(files)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //choose a song(s) to spleet
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                enqueue_files(openFileDialog2.FileNames);
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            // handled in button1_Click once the dialog has closed
        }

        private static List<string> expand_inputs(IEnumerable<string> paths, string output_dir)
        {
            // files are taken as they are; folders contribute the music files inside them
            var result = new List<string>();
            foreach (string raw in paths)
            {
                try
                {
                    // Full paths only: python is started in a different working directory, so a relative
                    // path (possible on the command line) would otherwise point somewhere else.
                    string path = Path.GetFullPath(raw);
                    if (Directory.Exists(path))
                    {
                        add_music_files(path, output_dir, result);
                    }
                    else if (File.Exists(path))
                    {
                        result.Add(path);
                    }
                }
                catch (Exception)
                {
                    // not a usable path - skip it
                }
            }
            return result;
        }

        private static void add_music_files(string folder, string output_dir, List<string> result)
        {
            // Walks the tree by hand so that one unreadable sub folder doesn't lose the whole drop.
            try
            {
                // when the dropped folder is the output folder itself, results of earlier runs in it are not songs
                bool is_output = output_dir != null && string.Equals(folder.TrimEnd('\\'), output_dir, StringComparison.OrdinalIgnoreCase);
                result.AddRange(Directory.GetFiles(folder)
                    .Where(f => AudioExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                    .Where(f => !(is_output && Path.GetFileNameWithoutExtension(f).EndsWith("_recombined", StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(f => f, StringComparer.OrdinalIgnoreCase));

                foreach (string sub in Directory.GetDirectories(folder).OrderBy(d => d, StringComparer.OrdinalIgnoreCase))
                {
                    // don't follow junctions / symlinks: they can loop back on themselves
                    if ((File.GetAttributes(sub) & FileAttributes.ReparsePoint) != 0) continue;
                    // nor the output folder: its stems from an earlier run would be separated again
                    if (output_dir != null && string.Equals(sub.TrimEnd('\\'), output_dir, StringComparison.OrdinalIgnoreCase)) continue;
                    if (is_output && holds_stems(sub)) continue;
                    add_music_files(sub, output_dir, result);
                }
            }
            catch (Exception)
            {
                // unreadable folder - skip it
            }
        }

        /// <summary>True for a folder that spleeter wrote: vocals.* next to accompaniment.* or drums.*</summary>
        private static bool holds_stems(string dir)
        {
            try
            {
                var names = new HashSet<string>(Directory.GetFiles(dir).Select(Path.GetFileNameWithoutExtension), StringComparer.OrdinalIgnoreCase);
                return names.Contains("vocals") && (names.Contains("accompaniment") || names.Contains("drums"));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool prepare_output_directory(out string output_dir)
        {
            output_dir = txt_output_directory.Text.Trim().Trim('"');
            if (output_dir == "")
            {
                MessageBox.Show(this, L("output_message"), this.Text);
                return false;
            }
            try
            {
                output_dir = Path.GetFullPath(output_dir);
                Directory.CreateDirectory(output_dir);
                if (!is_writable(output_dir)) throw new UnauthorizedAccessException("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, (L("output_unusable") + "\r\n" + output_dir + "\r\n\r\n" + ex.Message).Trim(), this.Text);
                return false;
            }
            if (!is_transient(output_dir) && Properties.Settings.Default.output_location != output_dir)
            {
                Properties.Settings.Default.output_location = output_dir;
                save_settings();
            }
            txt_output_directory.Text = output_dir;
            return true;
        }

        private bool is_transient(string dir)
        {
            return transient_output != null &&
                string.Equals(dir.TrimEnd('\\', '/'), transient_output.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase);
        }

        private int scans_running = 0;
        private int scan_epoch = 0;     // Cancel moves this on, so scans still under way are dropped

        private async void enqueue_files(IEnumerable<string> paths)
        {
            if (upgrading)
            {
                // the core is being replaced right now; songs can't be queued behind that
                System.Media.SystemSounds.Exclamation.Play();
                return;
            }

            // Dropped folders are searched in the background: a big or network folder must not freeze the window.
            string[] inputs = paths.ToArray();
            string exclude = null;
            try
            {
                string typed = txt_output_directory.Text.Trim().Trim('"');
                if (typed != "") exclude = Path.GetFullPath(typed).TrimEnd('\\');
            }
            catch (Exception)
            {
            }

            List<string> files;
            int epoch = scan_epoch;
            scans_running++;
            if (!busy) progress_txt.Text = L("scanning");
            try
            {
                files = await Task.Run(() => expand_inputs(inputs, exclude));
            }
            catch (Exception)
            {
                files = new List<string>();
            }
            finally
            {
                scans_running--;
            }
            if (IsDisposed) return;
            if (!busy && scans_running == 0) progress_txt.Text = L("idle");
            if (epoch != scan_epoch) return;    // cancelled while the folder was being searched

            try
            {
                start_or_extend_batch(files);
            }
            catch (Exception ex)
            {
                log("Error: " + ex.Message);
            }
        }

        private void start_or_extend_batch(List<string> files)
        {
            if (upgrading) return;
            if (files.Count == 0)
            {
                MessageBox.Show(this, L("no_audio_files"), this.Text);
                return;
            }

            if (busy)
            {
                // already running: just add to the end of the queue
                foreach (string file in files)
                {
                    queue.Enqueue(file);
                    log(L("added_to_queue") + ": " + file);
                }
                songs_total += files.Count;
                update_progress();
                return;
            }

            string output_dir;
            if (!prepare_output_directory(out output_dir)) return;

            if (!File.Exists(python_exe()))
            {
                textBox1.Text = python_missing_message() + "\r\n";
                MessageBox.Show(this, python_missing_message(), this.Text);
                return;
            }

            batch_output_dir = output_dir;
            batch_codec = cmbCodec.SelectedItem as string ?? "wav";
            foreach (string file in files) queue.Enqueue(file);
            songs_total = files.Count;
            songs_done = 0;
            songs_failed = 0;
            cancelling = false;
            batch_song_names.Clear();
            set_busy(true);

            textBox1.Text = "";
            log(L("starting_all"));
            log(L("truncated_hint").Replace("{0}", ((int)duration.Value).ToString()));
            progress_txt.Text = L("starting") + "..." + queue.Count + " " + L("songs_remaining");
            update_progress();
            next_song();
        }

        private void set_busy(bool value)
        {
            busy = value;
            btnCancel.Enabled = value;
            // these describe the whole batch, so they can't change half way through
            cmbCodec.Enabled = !value;
            btnSaveTo.Enabled = !value;
            txt_output_directory.ReadOnly = value;
            setPythonPathToolStripMenuItem1.Enabled = !value;
            parts_btn2.Enabled = !value;
            parts_btn4.Enabled = !value;
            parts_btn5.Enabled = !value;
        }

        private void update_progress()
        {
            progressBar1.Maximum = Math.Max(1, songs_total);
            progressBar1.Value = Math.Min(progressBar1.Maximum, songs_done);
            if (busy)
            {
                progress_txt.Text = L("working") + "..." + (songs_total - songs_done) + " " + L("songs_remaining");
            }
        }

        // ------------------------------------------------------------------
        //  Running spleeter
        // ------------------------------------------------------------------

        private void next_song()
        {
            //begins the spleeting function on the next song in the queue
            if (cancelling || queue.Count == 0)
            {
                finish_batch();
                return;
            }

            string filename = queue.Dequeue();
            update_progress();
            log("");
            log(L("processing") + " " + filename);

            // spleeter names the output folder after the song, so "CD1\01 Intro.mp3" and "CD2\01 Intro.mp3"
            // would silently overwrite each other. A name already used in this batch gets " (2)", " (3)"...
            string song_name = Path.GetFileNameWithoutExtension(filename);
            string out_name = song_name;
            for (int n = 2; !batch_song_names.Add(out_name); n++) out_name = song_name + " (" + n + ")";
            if (out_name != song_name) log(L("same_name_note").Replace("{0}", out_name));

            run_song(filename, out_name, false);
        }

        private string config_path()
        {
            // one file per running copy of the app, so two windows can't swap each other's settings
            return Path.Combine(dataDir, "config-" + Process.GetCurrentProcess().Id + ".json");
        }

        private void delete_config()
        {
            try
            {
                File.Delete(config_path());
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// A copy of the app that crashed or was killed could not remove its config-&lt;pid&gt;.json.
        /// Clear out the ones whose window is gone.
        /// </summary>
        private void remove_stale_configs()
        {
            try
            {
                if (!Directory.Exists(dataDir)) return;
                string app_name;
                using (Process me = Process.GetCurrentProcess()) app_name = me.ProcessName;

                foreach (string file in Directory.GetFiles(dataDir, "config-*.json"))
                {
                    int pid;
                    if (!int.TryParse(Path.GetFileNameWithoutExtension(file).Substring("config-".Length), out pid)) continue;

                    bool alive = false;
                    try
                    {
                        using (Process owner = Process.GetProcessById(pid))
                        {
                            alive = string.Equals(owner.ProcessName, app_name, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                    catch (ArgumentException)
                    {
                        // no process with that id any more
                    }
                    if (!alive) File.Delete(file);
                }
            }
            catch (Exception)
            {
                // only housekeeping
            }
        }

        private void run_song(string filename, string out_name, bool is_retry)
        {
            pending_hint = null;
            cli_mismatch = false;

            string problem = model_problem();
            if (problem != null)
            {
                log(problem);
                fail_song_later();
                return;
            }

            string config_file = config_path();
            try
            {
                Directory.CreateDirectory(dataDir);
                File.WriteAllText(config_file, get_config_string());
            }
            catch (Exception ex)
            {
                log("Error: " + ex.Message);
                fail_song_later();
                return;
            }

            // -X utf8: song names with accents / non-latin scripts must survive the trip through python's
            // stdout. (The environment variables for this are ignored by the embedded python of older cores.)
            var args = new List<string> { "-X", "utf8", "-W", "ignore", "-m", "spleeter", "separate" };
            args.AddRange(new[] { "-o", batch_output_dir });
            args.AddRange(new[] { "-d", ((int)duration.Value).ToString() });
            args.AddRange(new[] { "-p", config_file });
            if (batch_codec != "wav")
            {
                // spleeter's default bitrate is 128k, which is audibly poor for stems
                args.AddRange(new[] { "-c", batch_codec });
                if (batch_codec != "flac") args.AddRange(new[] { "-b", "256k" });
            }
            if (out_name != Path.GetFileNameWithoutExtension(filename))
            {
                // a python format string: literal braces in the name have to be doubled
                args.AddRange(new[] { "-f", out_name.Replace("{", "{{").Replace("}", "}}") + "/{instrument}.{codec}" });
            }
            // spleeter 2.1 turned the input file into a positional argument; older versions need -i
            if (legacy_cli) args.Add("-i");
            args.Add(filename);

            // Remember what is already in the song's folder: only files written by this run count as output.
            string song_dir = Path.Combine(batch_output_dir, out_name);
            Dictionary<string, DateTime> before = snapshot(song_dir);

            // If the guess about the command line turns out wrong the song is simply run again the
            // other way, so spleeter's "usage: ..." complaint about the first try is not worth showing.
            hide_cli_usage = !is_retry;
            in_usage_text = false;

            bool started = start_process(python_exe(), args, true, (exit_code) =>
            {
                hide_cli_usage = false;
                if (cancelling)
                {
                    song_finished(false);
                    return;
                }
                if (cli_mismatch && !is_retry)
                {
                    // the other generation of the spleeter command line is installed: switch and try once more
                    legacy_cli = !legacy_cli;
                    run_song(filename, out_name, true);
                    return;
                }

                List<string> written = snapshot(song_dir).Where(f =>
                {
                    DateTime old;
                    return !before.TryGetValue(f.Key, out old) || old != f.Value;
                }).Select(f => f.Key).ToList();

                if (exit_code != 0)
                {
                    log(L("song_failed") + " " + exit_code + ": " + filename);
                    if (pending_hint != null) log(pending_hint);
                    song_finished(false);
                }
                else if (written.Count == 0)
                {
                    // spleeter reports some errors without an error code
                    log(L("no_output") + " " + song_dir);
                    if (pending_hint != null) log(pending_hint);
                    song_finished(false);
                }
                else
                {
                    log(L("saved_to") + ": " + song_dir);
                    recombine(out_name, written, song_finished);
                }
            });

            if (!started)
            {
                hide_cli_usage = false;
                // nothing will work without python: drop the rest of the queue instead of failing once per song
                songs_failed += 1 + queue.Count;
                songs_done += 1 + queue.Count;
                queue.Clear();
                finish_batch();
            }
        }

        /// <summary>The stem files (of the batch's format) in a song's output folder, with their timestamps.</summary>
        private Dictionary<string, DateTime> snapshot(string song_dir)
        {
            var result = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            try
            {
                if (Directory.Exists(song_dir))
                {
                    foreach (string file in Directory.GetFiles(song_dir, "*." + batch_codec))
                    {
                        result[Path.GetFileName(file)] = File.GetLastWriteTimeUtc(file);
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// For failures before python is even started. Going through the message loop keeps a long queue
        /// that fails song after song (unwritable profile, broken model folder) from nesting call upon call.
        /// </summary>
        private void fail_song_later()
        {
            BeginInvoke((Action)(() =>
            {
                if (busy) song_finished(false);
            }));
        }

        private void song_finished(bool success)
        {
            songs_done++;
            if (!success) songs_failed++;
            update_progress();
            next_song();
        }

        private void finish_batch()
        {
            if (cancelling)
            {
                queue.Clear();
                log("");
                log(L("cancelled"));
            }
            else
            {
                log("");
                string summary = L("finished");
                if (songs_failed > 0) summary += " (" + songs_failed + " " + L("failed_count") + ")";
                log(summary);
            }

            delete_config();
            current_process = null;
            cancelling = false;
            set_busy(false);
            progress_txt.Text = L("idle");
            progressBar1.Maximum = Math.Max(1, songs_total);
            progressBar1.Value = progressBar1.Maximum;

            if (songs_failed > 0) System.Media.SystemSounds.Exclamation.Play();
            else System.Media.SystemSounds.Beep.Play();
        }

        private void recombine(string out_name, ICollection<string> written, Action<bool> done)
        {
            //runs ffmpeg to mix the chosen stems back together, then calls done(success)
            var parts = new List<string>();
            if (chkRecombine.Checked)
            {
                if (chkRPartVocal.Checked) parts.Add("vocals");
                if (chkRPartBass.Checked) parts.Add("bass");
                if (chkRPartDrums.Checked) parts.Add("drums");
                if (chkRPartPiano.Checked && stem_count == "5") parts.Add("piano");
                if (chkRPartOther.Checked) parts.Add("other");
            }
            // only stems this run has just written, never leftovers from an earlier one
            parts = parts.Where(p => written.Contains(p + "." + batch_codec, StringComparer.OrdinalIgnoreCase)).ToList();
            if (parts.Count == 0)
            {
                done(true);
                return;
            }

            string ffmpeg = find_ffmpeg();
            if (ffmpeg == null)
            {
                log(L("hint_ffmpeg"));
                done(false);
                return;
            }

            // amix divides the level by the number of inputs, so each input is boosted first
            string song_dir = Path.Combine(batch_output_dir, out_name);
            var args = new List<string> { "-y", "-hide_banner", "-loglevel", "error" };
            var filter = new StringBuilder();
            var labels = new StringBuilder();
            for (int i = 0; i < parts.Count; i++)
            {
                args.AddRange(new[] { "-i", Path.Combine(song_dir, parts[i] + "." + batch_codec) });
                filter.Append("[" + i + "]volume=" + parts.Count + "[a" + i + "];");
                labels.Append("[a" + i + "]");
            }
            args.AddRange(new[] { "-filter_complex", filter.ToString() + labels + "amix=inputs=" + parts.Count + ":duration=longest" });
            if (batch_codec != "wav" && batch_codec != "flac") args.AddRange(new[] { "-b:a", "256k" });
            string target = Path.Combine(batch_output_dir, out_name + "_recombined." + batch_codec);
            args.Add(target);

            log(L("recombining") + " " + string.Join(" + ", parts));
            bool started = start_process(ffmpeg, args, false, (exit_code) =>
            {
                if (exit_code == 0) log(L("saved_to") + ": " + target);
                else if (!cancelling) log(L("recombine_failed") + " " + exit_code + ")");
                done(exit_code == 0);
            });
            if (!started) done(false);
        }

        private string find_ffmpeg()
        {
            // the core folder first, then wherever spleeter itself would find it
            var dirs = new List<string> { storage, path_python, appDir };
            dirs.AddRange((Environment.GetEnvironmentVariable("PATH") ?? "").Split(';'));
            foreach (string dir in dirs)
            {
                try
                {
                    string clean = dir.Trim().Trim('"');
                    if (clean == "") continue;
                    string file = Path.Combine(clean, "ffmpeg.exe");
                    if (File.Exists(file)) return file;
                }
                catch (Exception)
                {
                    // malformed PATH entry
                }
            }
            return null;
        }

        /// <summary>
        /// Where spleeter is told to keep its models, or null to leave it at its default
        /// (the pretrained_models folder of the core).
        /// </summary>
        private string model_path_override()
        {
            // Models are downloaded on first use. If the core folder can't be written to
            // (e.g. under Program Files) keep them in the per-user data folder instead.
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MODEL_PATH")) &&
                !Directory.Exists(Path.Combine(storage, "pretrained_models", stem_count + "stems")) &&
                !is_writable(storage))
            {
                return Path.Combine(dataDir, "pretrained_models");
            }
            return null;
        }

        private string model_dir()
        {
            string root = model_path_override() ?? Environment.GetEnvironmentVariable("MODEL_PATH");
            if (string.IsNullOrEmpty(root)) root = Path.Combine(storage, "pretrained_models");
            return Path.Combine(root, stem_count + "stems");
        }

        /// <summary>
        /// Spleeter only downloads a model when its folder does not exist at all. A folder left behind by an
        /// interrupted download (or one that has lost files) is therefore never repaired: old spleeter versions
        /// fail with "Can't load save_path when it is None", newer ones write noise without any error.
        /// Such a folder is removed here so the model is fetched again.
        /// Returns null when the song can go ahead, otherwise the message to show.
        /// </summary>
        private string model_problem()
        {
            string dir = model_dir();
            try
            {
                if (!Directory.Exists(dir)) return null;    // not there yet: spleeter downloads it

                bool complete = File.Exists(Path.Combine(dir, "checkpoint")) &&
                    Directory.GetFiles(dir, "*.index").Length > 0 &&
                    Directory.GetFiles(dir, "*.data-*").Length > 0;
                if (complete) return null;

                // Only ever remove what is recognisably the remains of a model, never anything else.
                bool only_leftovers = Directory.GetDirectories(dir).Length == 0 && Directory.GetFiles(dir).All(f =>
                {
                    string name = Path.GetFileName(f);
                    return name == ".probe" || name == "checkpoint" || name.StartsWith("model.", StringComparison.OrdinalIgnoreCase);
                });
                if (!only_leftovers) return L("hint_model") + "\r\n  " + dir;

                // A second SpleeterGUI window may be downloading this very model: its folder is empty
                // until the download is unpacked. Taking it away now would break that download.
                if (other_window_open() && DateTime.UtcNow - Directory.GetLastWriteTimeUtc(dir) < TimeSpan.FromHours(1))
                {
                    return L("model_busy") + "\r\n  " + dir;
                }

                log(L("model_incomplete") + " " + dir);
                Directory.Delete(dir, true);
                return null;
            }
            catch (Exception)
            {
                return L("hint_model") + "\r\n  " + dir;
            }
        }

        private static bool other_window_open()
        {
            try
            {
                using (Process me = Process.GetCurrentProcess())
                {
                    Process[] all = Process.GetProcessesByName(me.ProcessName);
                    foreach (Process p in all) p.Dispose();
                    return all.Length > 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hide_cli_usage = false;
        private bool in_usage_text = false;

        /// <summary>True for the lines of spleeter's "usage: ... / error: missing argument" complaint.</summary>
        private bool is_cli_usage_line(string line)
        {
            if (line.StartsWith("usage:", StringComparison.OrdinalIgnoreCase))
            {
                in_usage_text = true;
                return true;
            }
            if (in_usage_text && line.StartsWith(" ")) return true;    // the usage text continues
            in_usage_text = false;
            return line.StartsWith("Try '") || line.Contains("required: -i") || line.Contains("Missing argument 'FILES");
        }

        /// <summary>Turns the python errors people report most into something they can act on.</summary>
        private void note_error_hint(string line)
        {
            if (line.Contains("required: -i") || line.Contains("Missing argument 'FILES")) cli_mismatch = true;

            if (line.Contains("save_path when it is None"))
            {
                pending_hint = L("hint_model") + "\r\n  " + model_dir();
            }
            else if (line.Contains("ResourceExhaustedError") || line.Contains("OOM when allocating") || line.Contains("MemoryError") ||
                     line.Contains("Unable to allocate") || line.Contains("bad allocation"))
            {
                pending_hint = L("hint_memory");
            }
            else if (line.Contains("httpx.") || line.Contains("getaddrinfo failed") || line.Contains("ConnectError") ||
                     line.Contains("ConnectTimeout") || line.Contains("ReadTimeout") || line.Contains("ProxyError"))
            {
                pending_hint = L("hint_download") + "\r\n  " + model_dir();
            }
            else if (line.Contains("DLL load failed"))
            {
                pending_hint = L("hint_dll");
            }
            else if (line.Contains("An error occurs with ffprobe"))
            {
                pending_hint = L("hint_bad_file");
            }
            else if (line.Contains("binary not found"))
            {
                pending_hint = L("hint_ffmpeg");
            }
            else if (line.Contains("No module named spleeter") || line.Contains("No module named 'spleeter'"))
            {
                pending_hint = L("hint_no_spleeter") + "\r\n  " + path_python;
            }
        }

        /// <summary>
        /// Starts a hidden child process, streams its output to the log, and calls on_exit(exit code)
        /// on the UI thread when it has finished. Returns false if it could not be started.
        /// </summary>
        private bool start_process(string exe, IEnumerable<string> args, bool filter_output, Action<int> on_exit)
        {
            ProcessStartInfo psi = new ProcessStartInfo(exe, string.Join(" ", args.Select(quote_arg)));
            psi.WorkingDirectory = Directory.Exists(storage) ? storage : appDir;
            psi.UseShellExecute = false;
            psi.ErrorDialog = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.StandardErrorEncoding = Encoding.UTF8;

            psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";
            psi.EnvironmentVariables["PYTHONUTF8"] = "1";
            // A PYTHONHOME left behind by some other python makes this one load the wrong standard library.
            psi.EnvironmentVariables.Remove("PYTHONHOME");
            if (bundled_python)
            {
                // The bundled python is self-contained. Without this it also picks up packages from
                // %APPDATA%\Python\PythonXY\site-packages and PYTHONPATH, which take priority over its own
                // and break spleeter as soon as one of them is a different version (click, numpy, pandas...).
                psi.EnvironmentVariables["PYTHONNOUSERSITE"] = "1";
                psi.EnvironmentVariables.Remove("PYTHONPATH");
            }
            // spleeter shells out to ffmpeg/ffprobe: make sure the bundled ones are found.
            psi.EnvironmentVariables["PATH"] = storage + ";" + path_python + ";" + Environment.GetEnvironmentVariable("PATH");
            string models = model_path_override();
            if (models != null) psi.EnvironmentVariables["MODEL_PATH"] = models;

            Process process = new Process();
            process.StartInfo = psi;
            process.EnableRaisingEvents = true;
            DataReceivedEventHandler handler = (s, e) =>
            {
                if (string.IsNullOrEmpty(e.Data)) return;
                string line = e.Data.TrimEnd('\r', '\n');
                bool show = !filter_output || txt_check(line);   //Please don't email Deezer about problems with this GUI app.
                on_ui(() =>
                {
                    note_error_hint(line);
                    if (hide_cli_usage && is_cli_usage_line(line)) return;
                    if (show) log(line);
                });
            };
            process.OutputDataReceived += handler;
            process.ErrorDataReceived += handler;
            process.Exited += (s, e) =>
            {
                int exit_code = -1;
                try
                {
                    // Lets the async output readers drain before we report. Not when cancelling: a child
                    // that survived the kill would keep the pipes open and the window "busy" for good.
                    if (!cancelling) process.WaitForExit();
                    exit_code = process.ExitCode;
                }
                catch (Exception)
                {
                }
                on_ui(() =>
                {
                    if (current_process == process) current_process = null;
                    process.Dispose();
                    on_exit(exit_code);
                });
            };

            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                process.Dispose();
                log("Error: " + ex.Message);
                log(exe);
                if (string.Equals(Path.GetFileName(exe), "python.exe", StringComparison.OrdinalIgnoreCase))
                {
                    log(L("python_missing_hint"));
                }
                return false;
            }
            current_process = process;
            try
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (InvalidOperationException)
            {
                // it has already gone; the Exited handler reports that
            }
            return true;
        }

        /// <summary>Quotes one command line argument the way the Windows C runtime parses it.</summary>
        internal static string quote_arg(string arg)
        {
            if (arg.Length > 0 && arg.IndexOfAny(new[] { ' ', '\t', '"' }) < 0) return arg;

            var sb = new StringBuilder("\"");
            int backslashes = 0;
            foreach (char c in arg)
            {
                if (c == '\\')
                {
                    backslashes++;
                }
                else if (c == '"')
                {
                    sb.Append('\\', backslashes * 2 + 1);
                    sb.Append('"');
                    backslashes = 0;
                }
                else
                {
                    sb.Append('\\', backslashes);
                    sb.Append(c);
                    backslashes = 0;
                }
            }
            // backslashes right before the closing quote must be doubled ("D:\" would otherwise swallow the quote)
            sb.Append('\\', backslashes * 2);
            sb.Append('"');
            return sb.ToString();
        }

        private static bool is_writable(string dir)
        {
            try
            {
                string probe = Path.Combine(dir, ".write_test_" + Guid.NewGuid().ToString("N"));
                File.WriteAllText(probe, "");
                File.Delete(probe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void on_ui(Action action)
        {
            // the process callbacks arrive on worker threads, possibly after the window has gone
            try
            {
                if (IsDisposed || !IsHandleCreated) return;
                BeginInvoke(action);
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void log(string line)
        {
            textBox1.AppendText(line + "\r\n");
        }

        bool txt_check(string txt)  //prevent output
        {
            bool allow = true;
            if (txt.IndexOf("Author-email") != -1) { allow = false; }
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

        private void kill_current_process()
        {
            Process process = current_process;
            if (process == null) return;
            try
            {
                // /T also takes down the ffmpeg children python has started
                ProcessStartInfo psi = new ProcessStartInfo("taskkill.exe", "/PID " + process.Id + " /T /F");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                using (Process killer = Process.Start(psi))
                {
                    killer.WaitForExit(5000);
                }
            }
            catch (Exception)
            {
                try { process.Kill(); } catch (Exception) { }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!busy) return;
            cancelling = true;
            scan_epoch++;
            btnCancel.Enabled = false;
            queue.Clear();
            if (current_process != null) kill_current_process();   // its exit handler finishes the batch
            else finish_batch();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy && e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show(this, L("confirm_exit"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            // never leave python running in the background with no window
            cancelling = true;
            queue.Clear();
            kill_current_process();
            delete_config();
        }

        // ------------------------------------------------------------------
        //  Buttons, menus and options
        // ------------------------------------------------------------------

        private void Button2_Click(object sender, EventArgs e)
        {
            //prompt user for output folder
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = true;
                dialog.Description = L("set_output");
                string current = txt_output_directory.Text.Trim();
                if (current != "" && Directory.Exists(current)) dialog.SelectedPath = current;

                // Cancel keeps whatever folder was already chosen.
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    // a folder search that was still running may have started a batch while the dialog
                    // was open; the output folder of a running batch can't change any more
                    if (busy) return;
                    transient_output = null;    // a folder picked by hand is remembered
                    txt_output_directory.Text = dialog.SelectedPath;
                    Properties.Settings.Default.output_location = dialog.SelectedPath;
                    save_settings();
                }
            }
        }

        private void txt_output_directory_Leave(object sender, EventArgs e)
        {
            // a path typed or pasted by hand is remembered too
            string typed = txt_output_directory.Text.Trim().Trim('"');
            if (!busy && !is_transient(typed) && Properties.Settings.Default.output_location != typed)
            {
                Properties.Settings.Default.output_location = typed;
                save_settings();
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string dir = txt_output_directory.Text.Trim().Trim('"');
            if (dir == "" || !Directory.Exists(dir))
            {
                MessageBox.Show(this, L("output_message"), this.Text);
                return;
            }
            try
            {
                // explorer.exe explicitly: a bare path could start "Output.exe" sitting next to the "Output" folder
                // Always quoted: explorer also splits its argument at commas ("D:\Stems,2024").
                // A drive root keeps its backslash and needs no quotes.
                string full = Path.GetFullPath(dir);
                Process.Start("explorer.exe", full.Length > 3 ? "\"" + full.TrimEnd('\\') + "\"" : full);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, dir + "\r\n\r\n" + ex.Message, this.Text);
            }
        }

        private void setPythonPathToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //prompt user for python path
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = storage;
                dialog.Description = L("set_python_path");
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                if (busy) return;   // a batch started while the dialog was open: not under its feet

                string chosen = dialog.SelectedPath;
                if (!File.Exists(Path.Combine(chosen, "python.exe")) && !File.Exists(Path.Combine(chosen, "python", "python.exe")))
                {
                    MessageBox.Show(this, L("python_not_in_folder") + "\r\n" + chosen, this.Text);
                    return;
                }
                Properties.Settings.Default.path_python = chosen;
                save_settings();
                LoadStuff();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();   // goes through Form1_FormClosing, unlike Application.Exit()
        }

        private string get_config_string()
        {
            //reads the JSON config file for the current stem mode
            string file = Path.Combine(appDir, "configs", stem_count + "stems.json");
            string readText = File.Exists(file) ? File.ReadAllText(file) : default_config(stem_count);
            if (mask_extension == "average")
            {
                readText = readText.Replace("\"zeros\"", "\"average\"");
            }
            return readText;
        }

        private static string default_config(string stems)
        {
            // configs\<n>stems.json is also embedded in the exe, in case the folder next to it has gone missing
            return Assets.LoadText("configs." + stems + "stems.json") ?? "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //sets the full bandwidth mode (16Khz)
            mask_extension = chkFullBandwidth.Checked ? "average" : "zeros";
        }

        private void cmbCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loaded) return;
            string codec = cmbCodec.SelectedItem as string ?? "wav";
            if (Properties.Settings.Default.codec != codec)
            {
                Properties.Settings.Default.codec = codec;
                save_settings();
            }
        }

        private void open_external(string target)
        {
            try
            {
                Process.Start(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, target + "\r\n\r\n" + ex.Message, this.Text);
            }
        }

        private void spleeterGithubPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - opens SpleeterGUI github page in a browser window
            open_external(UrlGithub);
        }

        private void makenItSoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //help - open's the Maken it so youtube channel in a browser window
            open_external(UrlYoutube);
        }

        private void helpFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - open's the SpleeterGUI help page in a browser window
            open_external(UrlHelp);
        }

        private void set_stem_count(string count)
        {
            stem_count = count;
            highlight_stem_button();
            update_parts_label();
            update_checks();
        }

        private Font stem_font_regular;
        private Font stem_font_bold;

        private void highlight_stem_button()
        {
            // make the chosen stem count obvious
            if (stem_font_bold == null)
            {
                stem_font_regular = parts_btn2.Font;
                stem_font_bold = new Font(parts_btn2.Font, FontStyle.Bold);
            }
            foreach (Button button in new[] { parts_btn2, parts_btn4, parts_btn5 })
            {
                if (button.Name.EndsWith(stem_count))
                {
                    button.BackColor = Color.FromArgb(204, 228, 247);
                    button.Font = stem_font_bold;
                }
                else
                {
                    button.UseVisualStyleBackColor = true;
                    button.Font = stem_font_regular;
                }
            }
        }

        private void update_parts_label()
        {
            switch (stem_count)
            {
                case "4": parts_label.Text = L("vocal_bass_drums_other"); break;
                case "5": parts_label.Text = L("vocal_bass_drums_piano_other"); break;
                default: parts_label.Text = L("vocal_accompaniment"); break;
            }
            fit_text(parts_label, parts_label.Width, parts_label.Height, false);
        }

        private readonly Dictionary<Control, Font> base_font = new Dictionary<Control, Font>();
        private readonly Dictionary<Control, Font> fitted_font = new Dictionary<Control, Font>();

        /// <summary>
        /// Translations can be a lot longer than the English text. Rather than letting a label run
        /// under its neighbours or get cut off, step its font down until the text fits.
        /// </summary>
        private void fit_text(Control c, int max_width, int max_height, bool single_line)
        {
            Font designed;
            if (!base_font.TryGetValue(c, out designed))
            {
                designed = c.Font;
                base_font[c] = designed;
            }

            TextFormatFlags flags = TextFormatFlags.NoPrefix | (single_line ? TextFormatFlags.SingleLine : TextFormatFlags.WordBreak);
            float size = designed.SizeInPoints;
            Font chosen = designed;
            while (true)
            {
                Font probe = size == designed.SizeInPoints ? designed : new Font(designed.FontFamily, size, designed.Style);
                Size needed = TextRenderer.MeasureText(c.Text, probe, new Size(max_width, int.MaxValue), flags);
                if ((needed.Width <= max_width && needed.Height <= max_height) || size <= 7f)
                {
                    chosen = probe;
                    break;
                }
                if (probe != designed) probe.Dispose();
                size -= 0.5f;
            }

            Font previous;
            fitted_font.TryGetValue(c, out previous);
            c.Font = chosen;
            if (chosen != designed) fitted_font[c] = chosen; else fitted_font.Remove(c);
            if (previous != null && previous != chosen) previous.Dispose();
        }

        private Font designed_font(Control c)
        {
            Font designed;
            if (!base_font.TryGetValue(c, out designed))
            {
                designed = c.Font;
                base_font[c] = designed;
            }
            return designed;
        }

        /// <summary>Gives a control its designed font, or a smaller copy of it.</summary>
        private void use_font_size(Control c, float size)
        {
            Font designed = designed_font(c);
            Font previous;
            fitted_font.TryGetValue(c, out previous);
            if (size >= designed.SizeInPoints)
            {
                c.Font = designed;
                fitted_font.Remove(c);
            }
            else if (previous == null || previous.SizeInPoints != size)
            {
                Font smaller = new Font(designed.FontFamily, size, designed.Style);
                c.Font = smaller;
                fitted_font[c] = smaller;
            }
            else
            {
                return;     // already at that size
            }
            if (previous != null) previous.Dispose();
        }

        /// <summary>Steps the font of several controls down together until they fit.</summary>
        private void fit_group(Control[] controls, float min_size, Func<bool> fits)
        {
            float size = controls.Max(c => designed_font(c).SizeInPoints);
            while (true)
            {
                foreach (Control c in controls) use_font_size(c, size);
                if (fits() || size <= min_size) break;
                size -= 0.5f;
            }
        }

        private void fit_texts()
        {
            fit_text(lblPartsTitle, parts_btn2.Left - lblPartsTitle.Left - 2, parts_btn2.Height, true);
            fit_text(lblDroptext, lblDroptext.Width, lblDroptext.Height, false);
            lblPartsTitle.Top = parts_btn2.Top + (parts_btn2.Height - lblPartsTitle.Height) / 2;

            // "Maximum song length [600] seconds   Output format [wav]" has to stay on one row
            fit_group(new Control[] { lblMaxLength, lblSeconds, lblFormat }, 6.5f, () =>
            {
                flowOptions.PerformLayout();
                return flowOptions.GetPreferredSize(Size.Empty).Width <= flowOptions.Width;
            });

            // the two lines next to the logo must not run out of the window
            int right_edge = ClientSize.Width - 6 * DeviceDpi / 96;
            fit_group(new Control[] { lblSlogan1, lblSlogan2 }, 8f, () =>
                lblSlogan1.Left + lblSlogan1.GetPreferredSize(Size.Empty).Width <= right_edge &&
                lblSlogan2.Left + lblSlogan2.GetPreferredSize(Size.Empty).Width <= right_edge);

            fit_group(new Control[] { btnCancel }, 7f, () => btnCancel.GetPreferredSize(Size.Empty).Width <= btnCancel.Width);
            fit_output_row();
        }

        private Rectangle designed_save_to, designed_output_box, designed_open_folder;

        /// <summary>
        /// [Save to] [output folder] [Open folder]: a translated button text can be much longer than
        /// the English one. The buttons take the room they need from the folder box (up to a third
        /// of it), and only then is their text made smaller.
        /// </summary>
        private void fit_output_row()
        {
            if (designed_output_box.IsEmpty)
            {
                designed_save_to = btnSaveTo.Bounds;
                designed_output_box = txt_output_directory.Bounds;
                designed_open_folder = btnOpenFolder.Bounds;
            }
            int gap_left = designed_output_box.Left - designed_save_to.Right;
            int gap_right = designed_open_folder.Left - designed_output_box.Right;
            int spare = designed_output_box.Width / 3;

            Func<int> save_growth = () => Math.Max(0, btnSaveTo.GetPreferredSize(Size.Empty).Width - designed_save_to.Width);
            Func<int> open_growth = () => Math.Max(0, btnOpenFolder.GetPreferredSize(Size.Empty).Width - designed_open_folder.Width);
            fit_group(new Control[] { btnSaveTo, btnOpenFolder }, 7f, () => save_growth() + open_growth() <= spare);

            int grow_save = save_growth();
            int grow_open = open_growth();
            if (grow_save + grow_open > spare)
            {
                // still too long at the smallest font: share out what there is
                grow_save = (int)((long)spare * grow_save / (grow_save + grow_open));
                grow_open = spare - grow_save;
            }

            btnSaveTo.Bounds = new Rectangle(designed_save_to.Left, designed_save_to.Top, designed_save_to.Width + grow_save, designed_save_to.Height);
            btnOpenFolder.Bounds = new Rectangle(designed_open_folder.Left - grow_open, designed_open_folder.Top, designed_open_folder.Width + grow_open, designed_open_folder.Height);
            int box_left = btnSaveTo.Right + gap_left;
            txt_output_directory.Bounds = new Rectangle(box_left, designed_output_box.Top, btnOpenFolder.Left - gap_right - box_left, designed_output_box.Height);
        }

        private void parts_btn2_Click(object sender, EventArgs e)
        {
            if (!busy) set_stem_count("2");
        }

        private void parts_btn4_Click(object sender, EventArgs e)
        {
            if (!busy) set_stem_count("4");
        }

        private void parts_btn5_Click(object sender, EventArgs e)
        {
            if (!busy) set_stem_count("5");
        }

        private void spleeterupgradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - spleeter core upgrade
            if (busy) return;
            if (!File.Exists(python_exe()))
            {
                MessageBox.Show(this, python_missing_message(), this.Text);
                return;
            }
            bool has_pip = Directory.Exists(Path.Combine(path_python, "Lib", "site-packages", "pip")) || Directory.Exists(Path.Combine(path_python, "pip"));
            if (!has_pip)
            {
                MessageBox.Show(this, L("no_pip"), this.Text);
                return;
            }

            textBox1.Text = L("run_update") + "\r\n" + L("run_update_b") + "\r\n\r\n";
            songs_total = 1;
            songs_done = 0;
            songs_failed = 0;
            set_busy(true);
            upgrading = true;
            // numpy<2: a plain upgrade pulls in numpy 2, which the tensorflow that spleeter needs cannot load
            bool started = start_process(python_exe(), new[] { "-X", "utf8", "-W", "ignore", "-m", "pip", "install", "--upgrade", "spleeter", "numpy<2" }, true, (exit_code) =>
            {
                log("");
                log(L("run_complete") + (exit_code == 0 ? "" : " (error code " + exit_code + ")"));
                upgrading = false;
                set_busy(false);
                cancelling = false;
                System.Media.SystemSounds.Beep.Play();
                resolve_core_paths();
            });
            if (!started)
            {
                upgrading = false;
                set_busy(false);
            }
        }

        private async void checkSpleeterGUIUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help - check SpleeterGUI version
            checkSpleeterGUIUpdateToolStripMenuItem.Enabled = false;
            try
            {
                string source;
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    source = await client.GetStringAsync(UrlVersionCheck);
                }

                Match m = Regex.Match(source, @"^\[assembly:\s*AssemblyVersion\(""([0-9.]+)""\)", RegexOptions.Multiline);
                Version remote, local;
                if (m.Success && Version.TryParse(m.Groups[1].Value, out remote) && Version.TryParse(gui_version, out local))
                {
                    if (remote > local) MessageBox.Show(this, L("version") + " " + remote + " " + L("is_available"), this.Text);
                    else MessageBox.Show(this, L("latest"), this.Text);
                }
                else
                {
                    MessageBox.Show(this, L("unable"), this.Text);
                }
            }
            catch (Exception)
            {
                if (!IsDisposed) MessageBox.Show(this, L("unable"), this.Text);
            }
            finally
            {
                if (!IsDisposed) checkSpleeterGUIUpdateToolStripMenuItem.Enabled = true;
            }
        }

        private void chkRecombine_CheckedChanged(object sender, EventArgs e)
        {
            update_checks();
        }

        private void update_checks()
        {
            //update the user interface based on the chosen stem count
            bool can_recombine = stem_count != "2";
            if (!can_recombine && chkRecombine.Checked)
            {
                chkRecombine.Checked = false;   // re-enters via chkRecombine_CheckedChanged
                return;
            }
            chkRecombine.Enabled = can_recombine;

            bool expanded = can_recombine && chkRecombine.Checked;
            if (!expanded)
            {
                chkRPartVocal.Checked = false;
                chkRPartBass.Checked = false;
                chkRPartDrums.Checked = false;
                chkRPartPiano.Checked = false;
                chkRPartOther.Checked = false;
            }
            chkRPartPiano.Enabled = stem_count == "5";
            if (!chkRPartPiano.Enabled) chkRPartPiano.Checked = false;

            if (!loaded) return;

            // Sizes are derived from the (DPI-scaled) controls themselves instead of pixel constants.
            int extra = expanded ? flowParts.Bottom - chkRecombine.Bottom : 0;
            flowParts.Visible = expanded;
            pnlRecombine.Height = (expanded ? flowParts.Bottom : chkRecombine.Bottom) + 2;
            pnlMain.Top = base_main_top + extra;
            this.ClientSize = new Size(this.ClientSize.Width, base_client_height + extra);
        }

        private void duration_ValueChanged(object sender, EventArgs e)
        {
            if (!loaded) return;
            Properties.Settings.Default.duration = Convert.ToInt32(duration.Value);
            save_settings();
        }
    }
}
