using System;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            versionLabel.Text = String.Format($"v{Program.Version}");
            aboutText.Text = aboutText.Text.Insert(0, $"© 2015-{Meta.Year}, The Ripple Team{Environment.NewLine}");
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();
    }
}
