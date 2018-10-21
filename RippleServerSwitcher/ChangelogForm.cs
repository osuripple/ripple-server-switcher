using System;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    public partial class ChangelogForm : Form
    {
        public ChangelogForm() => InitializeComponent();
        private void closeButton_Click(object sender, EventArgs e) => Close();
    }
}
