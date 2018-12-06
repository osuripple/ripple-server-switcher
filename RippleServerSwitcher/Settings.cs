using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    enum ReportCrashStatus
    {
        NO,
        YES,
        NOT_ASKED
    }

    class Settings
    {
        public List<HostsEntry> IPsBackup = new List<HostsEntry>();
        public int LatestChangelog = 0;
        public ReportCrashStatus ReportCrashStatus = ReportCrashStatus.NOT_ASKED;

        public async Task Save()
        {
            Directory.CreateDirectory(SettingsManager.Folder);
            using(StreamWriter writer = new StreamWriter(SettingsManager.FilePath))
                await writer.WriteAsync(JsonConvert.SerializeObject(this));
        }
    }
}
