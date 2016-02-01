using System;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            Duplicates.DialogResult = DialogResult.Retry;
            Cancel.DialogResult = DialogResult.Cancel;
        }

        private void Close_Click(object sender, System.EventArgs e)
        {
            Dispose(true);
        }

        private void About_Click(object sender, System.EventArgs e)
        {
            new About().ShowDialog();
        }

        private void IgnoreList_Click(object sender, System.EventArgs e)
        {
            new IgnoreList().ShowDialog();
        }

        private void Allowed_Click(object sender, System.EventArgs e)
        {
            new Champs().ShowDialog();
            GC.Collect();
        }

        private void Duplicates_Click(object sender, System.EventArgs e)
        {
            Dispose(true);
        }
    }
}
