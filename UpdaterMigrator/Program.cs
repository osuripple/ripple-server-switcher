using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace UpdaterMigrator
{
    /*
     * Helper program that migrates from rss 2.1 to rss 2.2
     * rss 2.2 comes with a new updater, this executable will be downloaded by
     * rss 2.1's updater as RippleServerSwitcher.exe and executed afterwards.
     * 
     * This executable will extract UpdateUnpacker.exe and update.zip
     * from its resources and run UpdateUnpacker.exe, which will extract update.zip 
     * (which contains RippleServerSwitcher.exe) and finally run RippleServerSwitcher.exe (rss 2.2)
     */
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string s in new string[]{ "UpdateUnpacker.exe", "update.zip" })
            {
                WriteResourceToFile(string.Format("UpdaterMigrator.Resources.{0}", s), s);
            }
            Process.Start("UpdateUnpacker.exe");
        }

        public static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }

    }
}
