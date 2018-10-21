using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    abstract class BaseHostsEntry
    {
        public abstract override string ToString();
    }

    class ArbitraryHostsEntry : BaseHostsEntry
    {
        public string content;
        public override string ToString() => content;

        public override bool Equals(object obj)
        {
            var other = obj as ArbitraryHostsEntry;
            if (other == null)
                return false;
            return content.Equals(other.content);
        }

        public override int GetHashCode() => content.GetHashCode();
    }

    class HostsEntry : BaseHostsEntry
    {
        public string ip;
        public string domain;

        public override string ToString() => $"{ip}\t{domain}";

        public HostsEntry() { }

        public HostsEntry(string ip, string domain)
        {
            this.ip = ip;
            this.domain = domain;
        }

        public override bool Equals(object obj)
        {
            var other = obj as HostsEntry;
            if (other == null)
                return false;
            return ip.Equals(other.ip) && domain.Equals(other.domain);
        }

        public override int GetHashCode() => ToString().GetHashCode();
    }

    class HostsFile
    {
        public static Regex HostEntryRegex = new Regex(@"^\s*(?<address>\S+)\s+(?<name>\S+)\s*($|#)", RegexOptions.Compiled);
        private string hostsFilePath;
        public List<BaseHostsEntry> Entries = new List<BaseHostsEntry>();
        public SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public HostsFile(string path = null)
        {
            hostsFilePath = path ?? Environment.GetEnvironmentVariable("windir") + "\\system32\\drivers\\etc\\hosts";
            Task.Run(() => Parse());
        }
        
        public async Task Parse()
        {
            await semaphore.WaitAsync();
            try
            {
                Entries.Clear();
                using (StreamReader reader = new StreamReader(hostsFilePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.StartsWith("#") || line.Length == 0)
                        {
                            Entries.Add(new ArbitraryHostsEntry { content=line });
                            continue;
                        }
                        Match m = HostEntryRegex.Match(line);
                        if (m.Success)
                            Entries.Add(new HostsEntry { ip = m.Groups["address"].Value, domain = m.Groups["name"].Value });
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task Write(int maxRetries=3, int retryDelay=1000)
        {
            FileInfo fileInfo = new FileInfo(hostsFilePath);
            if (fileInfo.IsReadOnly)
                fileInfo.IsReadOnly = false;
            int retries = 0;

            try
            {
                FileStream fs = new FileStream(hostsFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                using (StreamReader reader = new StreamReader(fs))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        string line;
                        while ((line = await reader.ReadLineAsync()) != null)
                            if (!line.Contains(".ppy.sh"))
                                await writer.WriteLineAsync(line);
                        foreach (BaseHostsEntry entry in Entries)
                            await writer.WriteLineAsync(entry.ToString());
                    }
                }
            }
            catch (IOException) when (retries <= maxRetries)
            {
                await Task.Delay(retryDelay);
                retries++;
            }
            catch (UnauthorizedAccessException)
            {
                throw new HumanReadableException("Access denied. Turn off your antivirus.", "Cannot write the hosts file. This is often caused by the antivirus. Please try turning off your antivirus and try again.");
            }
        }
    }
}
