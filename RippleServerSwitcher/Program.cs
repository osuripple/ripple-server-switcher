using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    static class Program
    {
        private static string guid = ((GuidAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value;

        public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly int VersionNumber = Convert.ToInt32(Version.Replace(".", string.Empty));
        public const string SentryDSN = "https://ca538e69c0bc48658e58c7227383a3aa@pew.nyodev.xyz/21";
        public static Switcher Switcher = new Switcher();

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                bool report;
                try
                {
                    report = Switcher.Settings.ReportCrashStatus != ReportCrashStatus.NO;
                }
                catch
                {
                    report = true;
                }
                if (report)
                    Switcher.RavenClient.Capture(new SharpRaven.Data.SentryEvent((Exception)args.ExceptionObject));
                MessageBox.Show("Unhandled exception: " + args.ExceptionObject + "\n\n" + (report ? "The error has been reported to Ripple." : "Please report the error to Ripple."), "Oh no!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            };

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool firstProcess = true;
            using (Mutex mutex = new Mutex(true, guid, out firstProcess))
            {
                if (firstProcess)
                    Application.Run(new MainForm());
                else
                    MessageBox.Show("Ripple Server Switcher is already running", "Opsie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
