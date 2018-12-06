using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class UpdaterForm : Form
    {
        public UpdaterForm()
        {
            InitializeComponent();
        }

        private async void UpdaterForm_Shown(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "Downloading update";
                string newExePath = await Program.Switcher.Updater.DownloadLatestVersion(
                    new DownloadProgressChangedEventHandler(
                        (object _, DownloadProgressChangedEventArgs e_) =>
                        { progressBar.Value = e_.ProgressPercentage; }
                    )
                );

                statusLabel.Text = "Checking hashes";
                try
                {
                    if (!Program.Switcher.Updater.CheckHash(newExePath))
                        throw new UpdateAbortedException { Info = "Error: Hashes don't match.", Code = -2 };

                    statusLabel.Text = "Updating";
                    if (File.Exists("update.zip"))
                        File.Delete("update.zip");
                    File.Move(newExePath, "update.zip");
                }
                finally
                {
                    File.Delete(newExePath);
                }
                Process.Start("UpdateUnpacker.exe");
            }
            catch (UpdateAbortedException ex)
            {
                MessageBox.Show(ex.Info, "Update aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(ex.Code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unhandled error:\n{0}.\n\nUpdate aborted.", ex.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            Environment.Exit(0);
        }
    }

    class UpdateAbortedException : Exception
    {
        public string Info = "";
        public int Code = 0;
    }

}
