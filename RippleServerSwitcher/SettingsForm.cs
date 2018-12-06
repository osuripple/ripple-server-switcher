using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class SettingsForm : Form
    {
        private bool testingHosts = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private string uiHostsEntry(HostsEntry e) => String.Format("{0,-12} -> {1}", e.domain, e.ip);

        private void certificateButton_Click(object sender, EventArgs e)
        {
            if (Program.Switcher.CertificateManager.IsCertificateInstalled())
                Program.Switcher.CertificateManager.RemoveCertificates();
            else
                Program.Switcher.CertificateManager.InstallCertificate();
            updateCertificateStatus();
        }

        private void updateErrorReportCheckbox() => errorReportCheckbox.Checked = Program.Switcher.Settings.ReportCrashStatus != ReportCrashStatus.NO;

        private void updateCertificateStatus()
        {
            bool installed = Program.Switcher.CertificateManager.IsCertificateInstalled();
            certificateLabel.Text = installed ? "INSTALLED" : "NOT INSTALLED";
            certificateLabel.ForeColor = installed ? Color.Green : Color.Red;
            certificateButton.Text = (installed ? "Uninstall" : "Install") + " certificate";
        }

        private async Task updateHostsFileStatus()
        {
            HostsFile h = new HostsFile();
            try
            {
                await h.Parse();
                hostsFileReadableLabel.Text = "YES";
                hostsFileReadableLabel.ForeColor = Color.Green;
            }
            catch
            {
                hostsFileReadableLabel.Text = "NO";
                hostsFileReadableLabel.ForeColor = Color.Red;
            }

            ArbitraryHostsEntry testEntry = new ArbitraryHostsEntry(String.Format("# rss test {0}", Guid.NewGuid().ToString()));
            hostsFileWritableLabel.Text = "...";
            hostsFileWritableLabel.ForeColor = Color.Blue;

            try
            {
                h.Entries.Add(testEntry);
                await h.Write();
                await h.Parse();
                if (!h.Entries.Any(x => x is ArbitraryHostsEntry && ((ArbitraryHostsEntry)x).Equals(testEntry)))
                    throw new Exception();
                h.Entries.RemoveAll(x => x.ToString() == testEntry.ToString());
                await h.Write();
                await h.Parse();
                hostsFileWritableLabel.Text = "YES";
                hostsFileWritableLabel.ForeColor = Color.Green;
            }
            catch
            {
                hostsFileWritableLabel.Text = "NO";
                hostsFileWritableLabel.ForeColor = Color.Red;
            }

            bool ripple = Program.Switcher.IsConnectedToRipple();
            hostsFileRippleLabel.Text = ripple ? "YES" : "NO";
            hostsFileRippleLabel.ForeColor = ripple ? Color.Green : Color.Yellow;
            
        }

        private async void SettingsForm_Shown(object sender, EventArgs e)
        {
            foreach (HostsEntry entry in Program.Switcher.RippleHostsEntries)
                currentDomains.Items.Add(uiHostsEntry(entry));
            foreach (HostsEntry entry in Switcher.FallbackOfflineIPs)
                fallbackDomains.Items.Add(uiHostsEntry(entry));
            updateErrorReportCheckbox();
            updateCertificateStatus();
            await updateHostsFileStatus();
            await updateIPServerStatus();
            if (Program.Switcher.IsConnectedToRipple())
                await testRippleConnection();
        }

        private async void hostsHelpButton_Click(object sender, EventArgs e)
        {
            if (testingHosts)
                return;

            try
            {
                testingHosts = true;
                hostsHelpButton.Enabled = false;
                await updateHostsFileStatus();
                hostsHelpButton.Enabled = true;
            }
            finally
            {
                testingHosts = false;
            }
        }

        private async Task updateIPServerStatus()
        {
            try
            {
                await Program.Switcher.UpdateIPs();
                ipServerStatusLabel.Text = "ONLINE";
                ipServerStatusLabel.ForeColor = Color.Green;
            }
            catch
            {
                ipServerStatusLabel.Text = "OFFLINE";
                ipServerStatusLabel.ForeColor = Color.Red;
            }
        }

        private async void testRippleConnectionButton_Click(object sender, EventArgs e)
        {
            if (!Program.Switcher.IsConnectedToRipple())
            {
                MessageBox.Show("You must be connected to Ripple to perform the connection test.");
                return;
            }
            await testRippleConnection();
        }

        private async Task testRippleConnection()
        {
            testRippleConnectionButton.Enabled = false;
            var labels = new Label[] { banchoRippleStatusLabel, banchoOsuStatusLabel, scoresRippleStatusLabel, scoresOsuStatusLabel };
            foreach (Label label in labels)
            {
                label.Text = "...";
                label.ForeColor = Color.White;
            }

            try
            {
                var domains = new string[]
                {
                    "https://c.ripple.moe",
                    "https://c.ppy.sh",
                    "https://ripple.moe/web/",
                    "https://osu.ppy.sh/web/"
                };
                var checkers = domains.Zip(labels, (d, l) => (new ConnectionChecker(d), l));
                foreach ((ConnectionChecker checker, Label label) in checkers)
                {
                    try
                    {
                        await checker.Check();
                        label.Text = "OK";
                        label.ForeColor = Color.Green;
                    }
                    catch (NonOkResponse ex)
                    {
                        label.Text = String.Format("ERROR {0}", ex.StatusCode);
                        label.ForeColor = Color.Red;
                    }
                    catch (CertificateException)
                    {
                        label.Text = "CERT ERROR";
                        label.ForeColor = Color.Red;
                    }
                    catch (NoRedirectionException)
                    {
                        label.Text = "NOT RIPPLE";
                        label.ForeColor = Color.Red;
                    }
                    catch (Exception ex)
                    {
                        label.Text = String.Format("ERROR");
                        label.ForeColor = Color.Red;
                        MessageBox.Show(String.Format("Couldn't connect to {0}:\n\n{1}", checker.Endpoint, ex.ToString()));
                    }
                }
            }
            finally
            {
                testRippleConnectionButton.Enabled = true;
            }
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();

        private async void errorReportCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ReportCrashStatus newStatus = ((CheckBox)sender).Checked ? ReportCrashStatus.YES : ReportCrashStatus.NO;
            Program.Switcher.Settings.ReportCrashStatus = newStatus;
            await Program.Switcher.Settings.Save();
        }
    }

    class ConnectionCheckerException : Exception { }
    class NonOkResponse : ConnectionCheckerException
    {
        public HttpStatusCode StatusCode;
        public NonOkResponse(HttpStatusCode code) => StatusCode = code;
    }
    class CertificateException : ConnectionCheckerException { }
    class NoRedirectionException : ConnectionCheckerException { }

    class ConnectionChecker
    {
        public string Endpoint;
        private readonly HttpClient httpClient = new HttpClient();

        public ConnectionChecker(string endpoint)
        {
            this.Endpoint = endpoint;
        }

        public async Task Check()
        {
            try
            {
                using (HttpResponseMessage result = await httpClient.GetAsync(Endpoint))
                {
                    if (result.StatusCode != HttpStatusCode.OK)
                        throw new NonOkResponse(result.StatusCode);
                    if (!((await result.Content.ReadAsStringAsync()).ToLower().Contains("ripple")))
                        throw new NoRedirectionException();
                }
            }
            catch (HttpRequestException ex) when (ex.InnerException != null && ex.InnerException.InnerException is AuthenticationException)
            {
                throw new CertificateException();
            }
        }
    }
}
