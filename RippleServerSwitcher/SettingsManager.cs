using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    public class InvalidSettingsException : Exception { }

    class SettingsManager
    {
        public static string Folder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ripple Server Switcher"; } }
        public static string FilePath { get { return Folder + "\\config"; } }
        
        public static Settings CreateEmpty()
        {
            Settings s = new Settings();
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(s));
            return s;
        }

        public async static Task<Settings> Load()
        {
            Directory.CreateDirectory(Folder);
            if (!File.Exists(FilePath))
                return CreateEmpty();
            string text;
            using(StreamReader reader = new StreamReader(FilePath))
                text = await reader.ReadToEndAsync();
            Settings s;
            try
            {
                s = JsonConvert.DeserializeObject<Settings>(text);
            }
            catch (ArgumentException)
            {
                throw new InvalidSettingsException();
            }
            return s;
        }
    }
}
