using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    public class Updater
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public string Endpoint = "https://ip.ripple.moe/updater_v2.json";
        private readonly HttpClient httpClient = new HttpClient();
        private UpdateInfo info;
        public int AssemblyVersion;
        public bool NewUpdateAvailable { get { return info.Version > AssemblyVersion; } }

        public Updater()
        {
            try
            {
                AssemblyVersion = Convert.ToInt32(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Replace(".", string.Empty));
            }
            catch
            {
                AssemblyVersion = 0;
            }
        }

        public async Task CheckUpdates()
        {
            Logger.Info("Checking for updates");
            using (var result = await httpClient.GetAsync(Endpoint))
            {
                string content = await result.Content.ReadAsStringAsync();
                info = JsonConvert.DeserializeObject<UpdateInfo>(content);
            }
        }

        public async Task<string> DownloadLatestVersion(DownloadProgressChangedEventHandler progressHandler)
        {
            Logger.Info("Downloading new rss version");
            if (info == null)
                await CheckUpdates();
            string destination = Path.GetTempPath() + Guid.NewGuid().ToString();
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += progressHandler;
                wc.Credentials = CredentialCache.DefaultNetworkCredentials;
                await wc.DownloadFileTaskAsync(new Uri(info.URL), destination);
            }
            return destination;
        }

        public bool CheckHash(string path)
        {
            if (!File.Exists(path))
                return false;
            string hash;
            using (FileStream reader = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var buf = new BufferedStream(reader, 1024 * 32))
                {
                    var sha = new SHA256Managed();
                    byte[] checksum = sha.ComputeHash(buf);
                    hash = BitConverter.ToString(checksum).Replace("-", String.Empty);
                }
            }
            return hash.Equals(info.Hash);
        }
    }
}
