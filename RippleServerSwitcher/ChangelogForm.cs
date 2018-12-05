using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class ChangelogForm : Form
    {
        private readonly Dictionary<string, string[]> entries = new Dictionary<string, string[]>
        {
            {
                "v2.0.1.4",
                new string[]
                {
                    "Correctly handle empty config file"
                }
            },
            {
                "v2.0.1.3",
                new string[]
                {
                    "Re-parse the hosts file after writing it",
                    "Check response content when testing the connection towards ripple"
                }
            },
            {
                "v2.0.1.2",
                new string[]
                {
                    "Fix hosts file not being closed correctly after creating it"
                }
            },
            {
                "v2.0.1.1",
                new string[] 
                {
                    "Fix server switcher not working if hosts file doesn't exist"
                }
            },
            {
                "v2.0.1.0",
                new string[]
                {
                    "Add new c*.ppy.sh domains to hardcoded fallback domains",
                    "Ported to .NET Framework 4.5.0",
                    "Show unhandled exceptions on screen",
                }
            },
            {
                "v2.0.0.0",
                new string[]
                {
                    "Total refactoring",
                    "Made the server switcher easier to use",
                    "Get redirections remotely",
                    "Add inspect mode",
                    "Add updater",
                    "Add support for high DPI screens",
                }
            }
        };

        public ChangelogForm() => InitializeComponent();
        private void closeButton_Click(object sender, EventArgs e) => Close();

        private void ChangelogForm_Shown(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string[]> entry in entries)
            {
                if (changelogTextBox.Text.Length > 0)
                    changelogTextBox.Text += Environment.NewLine + Environment.NewLine;
                changelogTextBox.Text += entry.Key + ":" + Environment.NewLine + "- ";
                changelogTextBox.Text += String.Join(Environment.NewLine + "- ", entry.Value);
            }
        }
    }
}
