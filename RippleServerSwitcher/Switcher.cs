using Newtonsoft.Json;
using RippleServerSwitcher.Properties;
using SharpRaven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    class InvalidIPException : Exception { }
    class HumanReadableException : Exception
    {
        public string UIMessage;
        public string AdditionalInfo;
        public HumanReadableException(string uiMessage = "", string additionalInfo = "")
        {
            UIMessage = uiMessage;
            AdditionalInfo = additionalInfo;
        }
    }
    class UpdateIPsFailedException : HumanReadableException
    {
        public UpdateIPsFailedException(string message): base(message) {}
    }

    class Switcher
    {
        public static readonly HostsEntry[] FallbackOfflineIPs = new HostsEntry[]
        {
            new HostsEntry{domain="osu.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="c.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="ce.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="a.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="s.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="i.ppy.sh", ip="51.15.223.146"},
            new HostsEntry{domain="bm6.ppy.sh", ip="51.15.223.146"}
        }.Concat(
            (from x in Enumerable.Range(1, 6) select
                new HostsEntry{ domain=String.Format("c{0}.ppy.sh", x), ip="51.15.223.146" }
            ).ToArray()
        ).ToArray();

        private HostsFile hostsFile = new HostsFile();
        private readonly HttpClient httpClient = new HttpClient();
        public List<HostsEntry> RippleHostsEntries = new List<HostsEntry>();
        public CertificateManager CertificateManager = new CertificateManager { Certificate = new X509Certificate2(Resources.Certificate) };
        public Settings Settings = null;
        public RavenClient RavenClient = new RavenClient(Program.SentryDSN);
        public Updater Updater = new Updater();

        public async Task Initialize()
        {
            if (Settings == null)
                Settings = await SettingsManager.Load();
            await UpdateIPs();
            if (IsConnectedToRipple())
                await ConnectToRipple();
        }

        public static bool IsIPv4(string ipString)
        {
            if (ipString.Count(c => c == '.') != 3) return false;
            IPAddress address;
            return IPAddress.TryParse(ipString, out address);
        }

        public bool IsConnectedToRipple()
        {
            hostsFile.semaphore.Wait();
            try
            {
                // TODO: Check all entries? May screw things up when first launching and the remote IPs have been updated.
                return hostsFile.Entries.Any(x => x is HostsEntry && ((HostsEntry)x).domain.Contains(".ppy.sh"));
            }
            finally
            {
                hostsFile.semaphore.Release();
            }
        }

        public async Task DisconnectFromRipple()
        {
            await hostsFile.Parse();
            hostsFile.semaphore.Wait();
            try
            {
                hostsFile.Entries.RemoveAll(x => x is HostsEntry && ((HostsEntry)x).domain.Contains(".ppy.sh"));
                await hostsFile.Write();
            }
            finally
            {
                hostsFile.semaphore.Release();
            }
            await hostsFile.Parse();
            if (IsConnectedToRipple())
                throw new HumanReadableException("Can't delete entries. Disable antivirus.", "The hosts file looks writable, but Ripple Server Switcher wasn't able to delete the entries from it and switch back to osu!. There is most likely a software blocking access to the hosts file. Please disable your antivirus, third party firewall or any similar software that may block edits on the hosts file.");
        }
        
        public async Task ConnectToRipple()
        {
            CertificateManager.InstallCertificate();
            try
            {
                await UpdateIPs();
            }
            catch (UpdateIPsFailedException) { }

            await hostsFile.Parse();
            hostsFile.semaphore.Wait();
            try
            {
                hostsFile.Entries.RemoveAll(x => x is HostsEntry && ((HostsEntry)x).domain.Contains(".ppy.sh"));
                hostsFile.Entries.AddRange(RippleHostsEntries);
                await hostsFile.Write();
            }
            finally
            {
                hostsFile.semaphore.Release();
            }
            await hostsFile.Parse();
            if (!IsConnectedToRipple())
                throw new HumanReadableException("Can't write entries. Disable antivirus.", "The hosts file looks writable, but Ripple Server Switcher wasn't able to write the required entries to it and switch to ripple. There is most likely a software blocking access to the hosts file. Please disable your antivirus, third party firewall or any similar software that may block edits on the hosts file.");
        }

        public async Task UpdateIPs()
        {
            bool fromInternet = false;
            Dictionary<string, string> redirections = new Dictionary<string, string>();
            try
            {
                using (var result = await httpClient.GetAsync("https://ip.ripple.moe/current.json"))
                {
                    string content = await result.Content.ReadAsStringAsync();
                    redirections = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                }

                RippleHostsEntries.Clear();
                foreach (KeyValuePair<string, string> pair in redirections)
                    RippleHostsEntries.Add(new HostsEntry { domain = pair.Key, ip = pair.Value });

                fromInternet = true;
            }
            catch (Exception ex)
            {
                RippleHostsEntries = Settings.IPsBackup.Count > 0 ? Settings.IPsBackup : FallbackOfflineIPs.ToList();
                throw new UpdateIPsFailedException(String.Format("Couldn't fetch IPs ({0}). Using fallback ones.", ex.Message));
            }

            if (fromInternet)
            {
                Settings.IPsBackup = RippleHostsEntries;
                await Settings.Save();
            }
        }
    }
}
