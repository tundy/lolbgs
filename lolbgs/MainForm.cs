using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace lolbgs
{
    public partial class MainForm : Form
    {
        private readonly MyThread _t = new MyThread();

        public MainForm()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LeagueFolder))
            {
                var temp = (string) Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
                if (string.IsNullOrEmpty(temp))
                {
                    temp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\Wow6432Node\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
                    if (string.IsNullOrEmpty(temp))
                    {
                        temp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
                        if (string.IsNullOrEmpty(temp))
                        {
                            temp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null);
                            if (string.IsNullOrEmpty(temp))
                            {
                                temp = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null);
                                if (string.IsNullOrEmpty(temp))
                                {
                                    temp = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(temp) == false) Properties.Settings.Default.LeagueFolder = Directory.GetParent(temp).ToString();
            }

            if (Properties.Settings.Default.DestinationFolder == String.Empty)
            {
                Properties.Settings.Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\League Of Legends";
            }

            Properties.Settings.Default.Save();

            InitializeComponent();

            FormClosing += MainForm_FormClosing;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);
            ProgressBar.Step = 1;
            ProgressBar.Value = 0;
            _t.NewCopyOutput += t_NewCopyOutput;
            _t.NewCopyFinished += t_NewCopyFinished;
        }

        void t_NewCopyFinished()
        {
            Output.Invoke((Action)(() =>
            {
                Export.Enabled = true;
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);
            }));
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LeagueFolder = LeagueFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = DestinationFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
        }

        void t_NewCopyOutput(string s, int count)
        {
            Output.Invoke((Action)(() =>
            {
                Output.AppendText(s);
                ProgressBar.Value = count;
                TaskbarProgress.SetValue(Handle, ProgressBar.Value, ProgressBar.Maximum);
            }));
        }

        private void LeagueFolderBrowse_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = LeagueFolderLocation.Text,
                Description = @"Select League of Legends Folder that contains: RADS folder, lol.launcher.exe and so on."
            };
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            LeagueFolderLocation.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.LeagueFolder = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.Save();
        }

        private void BrowseDestinationFolder_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = DestinationFolderLocation.Text,
                Description = @"Select Folder where will be Splash Arts copied."
            };
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            DestinationFolderLocation.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.Save();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            Output.Text = string.Empty;
            Export.Enabled = false;
            ProgressBar.Value = 0;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, ProgressBar.Value, ProgressBar.Maximum);

            var destination = DestinationFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.LeagueFolder = LeagueFolderLocation.Text;
            Properties.Settings.Default.DestinationFolder = destination;
            Properties.Settings.Default.Save();

            try
            {
                var images = Directory.GetFiles(Settings.GetRadPath(), "*.jpg");
                const string pattern = ".+_[Ss]plash_[0-9]+\\.jpg";

                var splashArts = new List<string>();
                foreach (var image in images)
                {
                    var match = Regex.Match(Path.GetFileName(image), pattern);
                    if (match.Success)
                        splashArts.Add(match.Value);
                }
                ProgressBar.Maximum = splashArts.Count;
                TaskbarProgress.SetValue(Handle, ProgressBar.Value, ProgressBar.Maximum);
                destination += "\\";
                _t.Copy(destination, splashArts);
            }
            catch (DirectoryNotFoundException)
            {
                Output.AppendText("Directory not found." + Environment.NewLine);
            }
        }

        private void Options_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LeagueFolder = LeagueFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
            new Options().ShowDialog();
        }
    }
}