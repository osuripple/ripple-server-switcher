using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    static class Program
    {
        public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static int VersionNumber { get { return Convert.ToInt32(Version.Replace(".", string.Empty)); } }
        public static Switcher Switcher = new Switcher();

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                bool reported = false;
                MessageBox.Show("Unhandled exception: " + args.ExceptionObject + "\n\n" + (reported ? "The error has been reported to Ripple." : "Please report the error to Ripple."), "Oh no!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            };

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
