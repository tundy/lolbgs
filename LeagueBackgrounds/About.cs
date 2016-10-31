using System.Diagnostics;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Version.Text = ProductVersion;
        }

        private void Contact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(
                    "mailto:tunder.matus+lolBackgrounds@gmail.com?subject=League Of Legends Backgrounds Exporter&body=Version: " +
                    ProductVersion);
            }
            catch
            {
                Contact.LinkClicked -= Contact_LinkClicked;
                Contact.Text = @"mailto:tunder.matus+lolBackgrounds@gmail.com";
            }
        }
    }
}