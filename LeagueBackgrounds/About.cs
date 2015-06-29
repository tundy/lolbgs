using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                System.Diagnostics.Process.Start("mailto:tunder.matus+tundy@gmail.com?subject=League Of Legends Backgrounds Exporter&body=Version: " + ProductVersion);

            }
            catch
            {
                Contact.LinkClicked -= Contact_LinkClicked;
                Contact.Text = @"mailto:tunder.matus+tundy@gmail.com";
            }
        }
    }
}
