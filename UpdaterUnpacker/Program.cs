using Ionic.Zip;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UpdateUnpacker
{
    class HumanReadableException : Exception
    {
        public string CustomMessage;
        public HumanReadableException(string message) => CustomMessage = message;
    }

    class CriticalErrorException : HumanReadableException
    {
        public CriticalErrorException(string message) : base(message) { }
    }

    class WarningException : HumanReadableException
    {
        public WarningException(string message) : base(message) { }
    }

    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        public static void UnpackUpdate()
        {
            try
            {
                if (!File.Exists(@"update.zip"))
                    throw new WarningException("No updates to unpack.");
                
                foreach (Process p in Process.GetProcessesByName("RippleServerSwitcher"))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Win32Exception)
                    {
                        throw new CriticalErrorException("The server switcher is currently running and the updater wasn't able to close it. Please close the server switcher and try again.");
                    }
                }


                try
                {
                    using (ZipFile zip = ZipFile.Read("update.zip"))
                    {
                        foreach (ZipEntry e in zip)
                        {
                            if (e.FileName == "UpdateUnpacker.exe")
                            {
                                using (FileStream fs = File.Create("UpdateUnpacker.exe.new"))
                                {
                                    e.Extract(fs);
                                }
                            }
                            else
                            {
                                e.Extract(ExtractExistingFileAction.OverwriteSilently);
                            }
                        }
                    }
                    File.Delete("update.zip");
                    if (!File.Exists("RippleServerSwitcher.exe"))
                        throw new WarningException("Could not find RippleServerSwitcher.exe");
                }
                catch (UnauthorizedAccessException)
                {
                    throw new CriticalErrorException(
                        "The updater doesn't have enough permissions to extract the update or some files that need to be overwritten are in use.\n" +
                        "Please make sure the folder is not set as 'read only', that the switcher is closed and that you actually have enough " +
                        "permission to write to the folder where the switcher is.\n" +
                        "If have no idea how to slove this, you can re-download the most up-to-date version of the switcher from https://ripple.moe"
                    );
                }
                catch (Exception ex) when (ex is InvalidDataException || ex is NotSupportedException)
                {
                    throw new WarningException(
                        "The update file seems invalid or corrupted. Please open the server switcher and re-download the update."
                    );
                }
                Process.Start("RippleServerSwitcher.exe");
            }
            catch (CriticalErrorException ex)
            {
                MessageBox.Show(ex.CustomMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WarningException ex)
            {
                MessageBox.Show(ex.CustomMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(
                    "An unhandled error has occurred.\n\n"
                    + ex.ToString() +
                    "\n\nDo you want to copy the exception to your clipboard before exiting the application?",
                    "Error",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error
                ) == DialogResult.Yes)
                {
                    Clipboard.SetText(ex.ToString());
                    MessageBox.Show("Exception copied to clipboard. The application will now close.");
                }
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
