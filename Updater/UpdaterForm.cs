using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Updater
{
    public partial class UpdaterForm : Form
    {
        private Updater updater = new Updater();

        public UpdaterForm()
        {
            InitializeComponent();
        }

        private async void UpdaterForm_Shown(object sender, EventArgs e)
        {
            try
            {
                await updater.CheckUpdates();
                if (!updater.NewUpdateAvailable)
                {
                    progressBar.Value = 100;
                    throw new UpdateAbortedException { Info = "Ripple Server Switcher is already up to date!" };
                }

                /*DialogResult result = MessageBox.Show("There's a new update available. Do you want to download it now?", "Updater", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                    throw new UpdateAbortedException();*/

                statusLabel.Text = "Downloading new executable";
                string newExePath = await updater.DownloadLatestVersion(
                    new DownloadProgressChangedEventHandler(
                        (object _, DownloadProgressChangedEventArgs e_) =>
                        { progressBar.Value = e_.ProgressPercentage; }
                    )
                );

                statusLabel.Text = "Checking hashes";
                try
                {
                    if (!updater.CheckHash(newExePath))
                        throw new UpdateAbortedException { Info = "Error: Hashes don't match.", Code = -2 };

                    statusLabel.Text = "Updating";
                    File.Replace(newExePath, "RippleServerSwitcher.exe", "RippleServerSwitcher.exe.bak");
                }
                finally
                {
                    File.Delete(newExePath);
                }

                Process.Start(@"RippleServerSwitcher.exe");
            }
            catch (UpdateAbortedException ex)
            {
                MessageBox.Show(ex.Info);
                Environment.Exit(ex.Code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unhandled error:\n{0}.\n\nUpdate aborted.", ex.ToString()));
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
