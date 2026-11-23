using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace SpleeterGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Windows 7 / 8 don't offer TLS 1.2 to .NET by default; the update check needs it.
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }
            catch (Exception)
            {
            }

            if (!SettingsAreReadable(args)) return;

            Application.Run(new Form1(args));
        }

        /// <summary>
        /// .NET keeps user settings in a folder per version, so every update would otherwise start
        /// with an empty output folder, English, and no core path. Bring the previous version's along once.
        /// </summary>
        private static void carry_over_settings()
        {
            try
            {
                if (Properties.Settings.Default.settings_upgraded) return;
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.settings_upgraded = true;
                Properties.Settings.Default.Save();
            }
            catch (ConfigurationException)
            {
                throw;      // a damaged file: handled by the caller
            }
            catch (Exception)
            {
                // settings could not be saved (read-only profile...): not worth stopping for
            }
        }

        /// <summary>
        /// A damaged user.config (power cut or crash while saving) makes every settings access throw,
        /// which used to stop the app from starting at all. Throw the bad file away and start again
        /// with default settings instead.
        /// </summary>
        private static bool SettingsAreReadable(string[] args)
        {
            try
            {
                string unused = Properties.Settings.Default.language;
                carry_over_settings();
                return true;
            }
            catch (ConfigurationException ex)
            {
                string file = null;
                for (Exception e = ex; e != null && file == null; e = e.InnerException)
                {
                    ConfigurationErrorsException cee = e as ConfigurationErrorsException;
                    if (cee != null && !string.IsNullOrEmpty(cee.Filename)) file = cee.Filename;
                }

                // Only ever remove the per-user settings file, never the app's own .config.
                bool is_user_config = file != null && File.Exists(file) &&
                    string.Equals(Path.GetFileName(file), "user.config", StringComparison.OrdinalIgnoreCase);
                if (!is_user_config)
                {
                    MessageBox.Show("SpleeterGUI could not read its settings:\r\n\r\n" + ex.Message, "SpleeterGUI");
                    return true;    // the form copes with settings errors on its own
                }

                try
                {
                    File.Delete(file);
                    // The configuration system caches the failure, so a fresh process is needed.
                    Process.Start(Application.ExecutablePath, string.Join(" ", args.Select(Form1.quote_arg)));
                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("SpleeterGUI could not read its settings. Please delete this file and start again:\r\n\r\n" + file, "SpleeterGUI");
                    return true;
                }
            }
        }
    }
}
