using System.Windows.Forms;

namespace lolbgs
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, System.EventArgs e)
        {
            Close();
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
        }
    }
}
