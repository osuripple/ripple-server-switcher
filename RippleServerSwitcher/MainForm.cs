using System;
using System.Drawing;
using System.Windows.Forms;
using RippleServerSwitcher.Properties;
using System.Security.Principal;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RippleServerSwitcher
{
    public partial class MainForm : Form
    {
        private string _bottomText = "";
        private string bottomText
        {
            get { return _bottomText; }
            set
            {
                bool hasError = bottomErrorText.Length > 0;
                if (value.Length > 0 || hasError)
                {
                    bottomIconsPanel.Hide();
                    bottomStatusPanel.Show();
                }
                else
                {
                    bottomIconsPanel.Show();
                    bottomStatusPanel.Hide();
                }
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
        private Color emergencyMessagePanelBaseColor
        {
            get => emergencyMessage.Type == EmergencyMessageType.Warning ? Color.FromArgb(54, 14, 16) : Color.FromArgb(19, 78, 125);
        }

        private Color emergencyMessagePanelBorderColor
        {
            get => emergencyMessage.Type == EmergencyMessageType.Warning ? Color.FromArgb(74, 38, 39) : Color.FromArgb(12, 48, 80);
        }

        private string iconInfoText
        {
            get { return iconInfoLabel.Text; }
            set { iconInfoLabel.Text = value; }
        }
        private EmergencyMessage _emergencyMessage = new EmptyEmergencyMessage();
        private EmergencyMessage emergencyMessage
        {
            get { return _emergencyMessage; }
            set
            {
                _emergencyMessage = value ?? new EmptyEmergencyMessage();
                if (emergencyMessage.Empty)
                {
                    if (bottomPanel.Visible)
                    {
                        bottomPanel.Hide();
                        Height -= bottomPanel.Height - (Height - bottomPanel.Location.Y) + 3;
                    }
                }
                else
                {
                    messageLabel.Text = String.Format("{0}{1}", emergencyMessage.Message.Substring(0, Math.Min(32, emergencyMessage.Message.Length)), emergencyMessage.Message.Length > 32 ? "..." : "");
                    messageIcon.Image = emergencyMessage.Type == EmergencyMessageType.Warning ? Resources.Warning : Resources.Info;
                    if (!bottomPanel.Visible)
                    {
                        Height += bottomPanel.Height - (Height - bottomPanel.Location.Y) + 3;
                        bottomPanel.Show();
                    }
                }
            }
        }
        private string _bottomErrorText = "";
        private string bottomErrorText
        {
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
        private Exception latestException
        {
            get { return _latestException; }
            set
            {
                if (!(value is HumanReadableException) && switcher.Settings.ReportCrashStatus != ReportCrashStatus.NO)
                    switcher.RavenClient.Capture(new SharpRaven.Data.SentryEvent(value));
                bottomErrorText = value is HumanReadableException ? ((HumanReadableException)value).UIMessage : "An unhandled exception has occurred.";
                _latestException = value;
            }
        }

        private bool _servicesUp = true;
        private bool servicesUp
        {
            get { return _servicesUp; }
            set
            {
                _servicesUp = value;
                serverStatusIcon.Image = servicesUp ? Resources.Game : Resources.Warning;
                if (!servicesUp && emergencyMessage.Empty)
                {
                    emergencyMessage = new EmergencyMessage {
                        Type = EmergencyMessageType.Warning,
                        Title = "Services down!",
                        Message = "Some services are experiencing issues. Please check the status page at https://status.ripple.moe for more information."
                    };
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            bottomPanel.BackColor = emergencyMessagePanelBaseColor;
            foreach (Control childControl in bottomPanel.Controls)
                childControl.Click += BottomPanel_Click;

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
                bottomIconsPanel.Hide();

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
                }
                catch
                {
                    latestException = new HumanReadableException("Error while checking for updates.");
                }

                inspectButton.Enabled = true;
                updateStatus();

                bottomText = "Fetching messages...";
                emergencyMessage = await Pipoli.FetchEmergencyMessage();

                bottomText = "Checking server status...";
                var statusDict = await Pipoli.FetchServicesStatus();
                bool allUp = true;
                foreach (KeyValuePair<string, ServiceStatus> entry in statusDict)
                {
                    if (!entry.Value.Up && !entry.Value.Secondary)
                    {
                        allUp = false;
                        break;
                    }
                }

                servicesUp = allUp;
                bottomText = "";
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

        private void inspectButton_click(object sender, EventArgs e) => new SettingsForm().ShowDialog();

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

        private void bottomIconHover(string s)
        {
            iconInfoText = s;
            Cursor = Cursors.Hand;
        }

        private void DiscordIcon_MouseEnter(object sender, EventArgs e) => bottomIconHover("Discord Server.");
        private void DiscordIcon_Click(object sender, EventArgs e) => Process.Start("https://discord.ripple.moe");
        private void ServerStatusIcon_MouseEnter(object sender, EventArgs e) => bottomIconHover(servicesUp ? "Server status. Everything is up!" : "Servers currently offline!");
        private void ServerStatusIcon_Click(object sender, EventArgs e) => Process.Start("https://status.ripple.moe");
        private void bottomIconReset(object sender, EventArgs e)
        {
            iconInfoText = "";
            Cursor = Cursors.Default;
        }
        private void BottomPanel_Paint(object sender, PaintEventArgs e) => ControlPaint.DrawBorder(e.Graphics, bottomPanel.ClientRectangle, emergencyMessagePanelBorderColor, ButtonBorderStyle.Solid);
        private void BottomPanel_MouseEnter(object sender, EventArgs e) => bottomPanel.BackColor = ControlPaint.Light(emergencyMessagePanelBaseColor);
        private void BottomPanel_MouseLeave(object sender, EventArgs e)
        {
            if (bottomPanel.ClientRectangle.Contains(bottomPanel.PointToClient(MousePosition)))
                return;
            bottomPanel.BackColor = emergencyMessagePanelBaseColor;
        }
        private void BottomPanel_Click(object sender, EventArgs e) => MessageBox.Show(emergencyMessage.Message, emergencyMessage.Title, MessageBoxButtons.OK, emergencyMessage.Type == EmergencyMessageType.Warning ? MessageBoxIcon.Warning : MessageBoxIcon.None);
    }
}
