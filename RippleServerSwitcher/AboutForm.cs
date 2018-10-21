using System;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            versionLabel.Text = String.Format("v{0}", Program.Version);
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();
    }
}
