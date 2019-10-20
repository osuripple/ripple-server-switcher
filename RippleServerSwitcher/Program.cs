using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpRaven;

namespace RippleServerSwitcher
{
    static class Program
    {
        private static string guid = ((GuidAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly int VersionNumber = Convert.ToInt32(Version.Replace(".", string.Empty));
        private const string sentryDSN = "https://ee05f6bf42c9433c9dfd2bc97a2ee895@sentry.nyodev.xyz/5";
        public static RavenClient RavenClient = new RavenClient(sentryDSN);
        public static Switcher Switcher = new Switcher();
        
        public static bool ReportExceptions
        {
            get
            {
                try
                {
                    return Switcher.Settings.ReportCrashStatus != ReportCrashStatus.NO;
                }
                catch
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception e = (Exception)args.ExceptionObject;
                Logger.Error(e, "Unhandled exception");
                if (ReportExceptions)
                    RavenClient.Capture(new SharpRaven.Data.SentryEvent(e));
                MessageBox.Show("Unhandled exception: " + args.ExceptionObject + "\n\n" + (ReportExceptions ? "The error has been reported to Ripple." : "Please report the error to Ripple."), "Oh no!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            };
            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                Logger.Info("Shutting down");
                NLog.LogManager.Shutdown();
            };

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") {
                FileName = "RippleServerSwitcher.log", Layout = "${longdate}:${level}:${message} ${exception:format=ToString}",
                DeleteOldFileOnStartup = true
            };
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
            NLog.LogManager.Configuration = config;

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool firstProcess = true;
            using (Mutex mutex = new Mutex(true, guid, out firstProcess))
            {
                if (firstProcess)
                {
                    Logger.Info("Application started");
                    Application.Run(new MainForm());
                }
                else
                {
                    MessageBox.Show("Ripple Server Switcher is already running", "Opsie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
