using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
                var temp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\Wow6432Node\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
                Properties.Settings.Default.LeagueFolder = temp != null ? Directory.GetParent(temp).ToString() : @"C:\Riot Games\League of Legends";
            }

            if (string.IsNullOrEmpty(Properties.Settings.Default.DestinationFolder))
                Properties.Settings.Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\League Of Legends";
            Properties.Settings.Default.Save();

            InitializeComponent();

            FormClosing += MainForm_FormClosing;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);
            ProgressBar.Step = 1;
            ProgressBar.Value = 0;
            _t.NewJobOutput += t_NewJobOutput;
            _t.NewJobFinished += t_JobFinished;
        }

        void t_JobFinished()
        {
            Output.Invoke((Action)(() =>
            {
                Export.Enabled = true;
                Options.Enabled = true;
                BrowseDestinationFolder.Enabled = true;
                BrowseLeagueFolder.Enabled = true;
                DestinationFolderLocation.Enabled = true;
                LeagueFolderLocation.Enabled = true;
                ProgressBar.Value = ProgressBar.Maximum;
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);
                Cursor.Current = Cursors.Default;
            }));
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LeagueFolder = LeagueFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = DestinationFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
        }

        void t_NewJobOutput(string s, int count)
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
            ResetOutput();

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
// ReSharper disable once AssignNullToNotNullAttribute
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

        private void ResetOutput()
        {
            Cursor.Current = Cursors.WaitCursor;
            _t.ResetCounter();
            Output.Text = string.Empty;
            Export.Enabled = false;
            Options.Enabled = false;
            BrowseDestinationFolder.Enabled = false;
            BrowseLeagueFolder.Enabled = false;
            DestinationFolderLocation.Enabled = false;
            LeagueFolderLocation.Enabled = false;
            ProgressBar.Value = 0;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, ProgressBar.Value, ProgressBar.Maximum);
        }

        private void Options_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LeagueFolder = LeagueFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
            var opt = new Options();
            opt.ShowDialog();
            if (opt.DialogResult != DialogResult.Retry) return;

            var destination = DestinationFolderLocation.Text.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = destination;
            Properties.Settings.Default.Save(); try
            {
                var images = Directory.GetFiles(destination, "*.jpg");
                const string pattern = ".+_[Ss]plash_[2-9]+\\.jpg";

                var splashArts = new List<string>();
                foreach (var image in images)
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var match = Regex.Match(Path.GetFileName(image), pattern);
                    if (match.Success)
                        splashArts.Add(match.Value);
                } 

                ProgressBar.Maximum = splashArts.Count;
                TaskbarProgress.SetValue(Handle, ProgressBar.Value, ProgressBar.Maximum);
                destination += "\\";

                var stop = new Stopwatch();
                double time;
                if (splashArts.Count >= 2)
                {
                    var a = new Bitmap(destination + splashArts[0]);
                    stop.Start();
                    var b = new Bitmap(destination + splashArts[1]);
                    Check.Compare(a, b);
                    b.Dispose();
                    a.Dispose();
                    stop.Stop();
                    time = stop.Elapsed.TotalSeconds;
                }
                else
                {
                    time = 1/120;
                }

                var text = string.Empty;
                var seconds = (splashArts.Count + 1)*splashArts.Count/(double)2 * time;
                if (seconds > 60)
                    text += (seconds / 60).ToString("0.#") + @" minute(s) ";
                else
                    text += seconds.ToString("0.#") + @" second(s) ";
                text += " +/- Half that time.";
                var result =
                    MessageBox.Show(
                        @"Do you want to check all images?" + Environment.NewLine +
                        @" It will take approximately " + text + Environment.NewLine +
                        @"After it is done, it will add duplicated images into IgnoreList and autodelete them.",
                        @"Check for duplicates", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
                ResetOutput();
                _t.CheckDuplicates(destination, splashArts);
            }
            catch (Exception)
            {
                Output.AppendText("Directory not found." + Environment.NewLine);
            }
        }
    }
}