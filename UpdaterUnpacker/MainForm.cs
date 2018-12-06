using System;
using System.Windows.Forms;

namespace UpdateUnpacker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Program.UnpackUpdate();
        }
    }
}
