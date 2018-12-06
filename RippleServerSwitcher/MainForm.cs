using System;
using System.Drawing;
using System.Windows.Forms;
using RippleServerSwitcher.Properties;
using System.Security.Principal;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

namespace RippleServerSwitcher
{
    public partial class MainForm : Form
    {
        private string _bottomText = "";
        private string bottomText {
            get { return _bottomText; }
            set {
                bool hasError = bottomErrorText.Length > 0;
                if (value.Length > 0 || hasError)
                    bottomStatusPanel.Show();
                else
                    bottomStatusPanel.Hide();
                if (hasError)
                {
                    new ToolTip().SetToolTip(this.bottomStatusLabel, value);
                    bottomStatusPicture.Image = Resources.Warning;
                }
                else
                    bottomStatusPicture.Image = Resources.Loading;
                
                _bottomText = value;
                bottomStatusLabel.Text = hasError ? bottomErrorText : bottomText;
            }
        }
        private string _bottomErrorText = "";
        private string bottomErrorText {
            get { return _bottomErrorText; }
            set
            {
                _latestException = null;
                _bottomErrorText = value;
                bottomText = value;
            }
        }
        private Switcher switcher = Program.Switcher;
        private Exception _latestException;
        private Exception latestException {
            get { return _latestException; }
            set
            {
                if (!(value is HumanReadableException) && switcher.Settings.ReportCrashStatus != ReportCrashStatus.NO)
                    switcher.RavenClient.Capture(new SharpRaven.Data.SentryEvent(value));
                bottomErrorText = value is HumanReadableException ? ((HumanReadableException)value).UIMessage : "An unhandled exception has occurred.";
                _latestException = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            
            new ToolTip().SetToolTip(this.switchButton, "Switch between osu! and ripple");
        }

        private static bool isRunningAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            if (!isRunningAsAdmin())
            {
                MessageBox.Show("Please run the application as administator.");
                Environment.Exit(0);
            }

            bottomText = "Updating updater...";
            updateUpdateUnpacker();

            try
            {
                bottomText = "Fetching servers IPs...";
                updateStatus();

                switcher.Settings = await SettingsManager.Load();

                if (switcher.Settings.ReportCrashStatus == ReportCrashStatus.NOT_ASKED)
                {
                    DialogResult result = MessageBox.Show(
                         "Do you want to help ripple make the server switcher better by submitting " +
                         "crash reports to our error reporting service?\n" +
                         "(You can always toggle this setting from the \"Inspect\" menu).",
                         "Error reporting",
                         MessageBoxButtons.YesNo
                    );
                    switcher.Settings.ReportCrashStatus = result == DialogResult.Yes ? ReportCrashStatus.YES : ReportCrashStatus.NO;
                    await switcher.Settings.Save();
                }

                if (switcher.Settings.LatestChangelog != Program.VersionNumber)
                {
                    new ChangelogForm().ShowDialog();
                    switcher.Settings.LatestChangelog = Program.VersionNumber;
                    await switcher.Settings.Save();
                }

                try
                {
                    await switcher.Initialize();
                }
                catch (HumanReadableException ex)
                {
                    latestException = ex;
                }

                bottomText = "Checking updates...";
                try
                {
                    await switcher.Updater.CheckUpdates();
                    if (switcher.Updater.NewUpdateAvailable)
                    {
                        DialogResult result = MessageBox.Show("There's a new update available. Do you want to download it now?", "Updater", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                            new UpdaterForm().ShowDialog();
                    }
                    bottomText = "";
                }
                catch
                {
                    latestException = new HumanReadableException("Error while checking for updates.");
                }

                inspectButton.Enabled = true;
                updateStatus();
            }
            catch (Exception ex)
            {
                latestException = ex;
            }
        }

        private void updateStatus()
        {
            bool ripple = switcher.IsConnectedToRipple();
            statusLabel.Text = String.Format("You are connected to {0}", ripple ? "ripple" : "osu!");
            switchButton.Text = String.Format("Switch to {0}", ripple ? "osu!" : "ripple");
            switchButton.Font = new Font(switchButton.Font, ripple ? FontStyle.Regular : FontStyle.Bold);
        }

        private async void switchButton_Click(object sender, EventArgs e)
        {
            bottomText = "Switching servers...";
            switchButton.Enabled = false;
            try
            {
                if (switcher.IsConnectedToRipple())
                    await switcher.DisconnectFromRipple();
                else
                {
                    if (!switcher.CertificateManager.IsCertificateInstalled())
                    {
                        MessageBox.Show("Please click 'Yes' on the next message that will appear on your screen to install the HTTPs certificate. It's needed to connect to ripple.");
                    }
                    await switcher.ConnectToRipple();
                }
                bottomText = "";
                updateStatus();
            }
            catch (Exception ex)
            {
                latestException = ex;
            }
            finally
            {
                switchButton.Enabled = true;
            }
        }

        private void bottomStatusLabel_DoubleClick(object sender, EventArgs e)
        {
            if (latestException == null || (latestException is HumanReadableException && ((HumanReadableException)latestException).AdditionalInfo.Length == 0))
                return;
            string s;
            if (latestException is HumanReadableException)
            {
                s = ((HumanReadableException)latestException).AdditionalInfo;
            }
            else
            {
                s = latestException.ToString();
                Clipboard.SetText(s);
                s += "\n\n(Exception copied to clipboard)";
            }
                
            MessageBox.Show(s);
        }

        private async void closeButton_Click(object sender, EventArgs e)
        {
            await switcher.Settings.Save();
            Application.Exit();
        }
        
        private void aboutButton_Click(object sender, EventArgs e) => new AboutForm().ShowDialog();

        private void inspectButton_click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void updateUpdateUnpacker()
        {
            if (!File.Exists("UpdateUnpacker.exe.new"))
                return;
            foreach (Process p in Process.GetProcessesByName("UpdateUnpacker"))
            {
                try
                {
                    p.Kill();
                }
                catch (Win32Exception)
                {
                    throw new HumanReadableException("Cannot kill updater", "The update unpacker is running but the switcher wasn't able to kill it. Please close it manually or restart your computer.");
                }
            }

            try
            {
                if (File.Exists("UpdateUnpacker.exe"))
                    File.Delete("UpdateUnpacker.exe");
                File.Move("UpdateUnpacker.exe.new", "UpdateUnpacker.exe");
                File.Delete("UpdateUnpacker.exe.new");
            }
            catch (UnauthorizedAccessException)
            {
                throw new HumanReadableException("Cannot replace updater", "Unhautorized access. Maybe UpdateUnpacker.exe is in read only mode?");
            }
        }
    }
}
