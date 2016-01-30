using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueBackgrounds
{
    public partial class MainForm : Form
    {

        private readonly BackgroundWorker _copyWorker = new BackgroundWorker();
        private readonly BackgroundWorker _checkWorker = new BackgroundWorker();
        public MainForm()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LeagueFolder))
                Properties.Settings.Default.LeagueFolder = Static.FindLeagueOfLegends();
            if (string.IsNullOrEmpty(Properties.Settings.Default.DestinationFolder))
#if DEBUG
                Properties.Settings.Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Backgrounds\\League Of Legends";
#else
                Properties.Settings.Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\League Of Legends BGs";
#endif
            Properties.Settings.Default.Save();

            _copyWorker.WorkerReportsProgress = true;
            _copyWorker.WorkerSupportsCancellation = true;
            _copyWorker.DoWork += CopyWorkerDoWork;
            _copyWorker.ProgressChanged += WorkerProgressChanged;
            _copyWorker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            _checkWorker.WorkerReportsProgress = true;
            _checkWorker.WorkerSupportsCancellation = true;
            _checkWorker.DoWork += CheckWorker_DoWork;
            _checkWorker.ProgressChanged += WorkerProgressChanged;
            _checkWorker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            InitializeComponent();
            FormClosing += MainForm_FormClosing;

            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);

            Options_Button.Click += Options_Button_Click;
        }

        void Options_Button_Click(object sender, EventArgs e)
        {
            Enabled = false;
            var opt = new Options();
            opt.ShowDialog();
            if (opt.DialogResult != DialogResult.Retry)
            {
                Enabled = true;
                return;
            }

            Enabled = true;
            DisableMainForm();

            Properties.Settings.Default.DestinationFolder = DestinationFolder_TextBox.Text;
            Properties.Settings.Default.Save();
            //_checkWorker.RunWorkerAsync();
            try
            {
                var images = Directory.GetFiles(Properties.Settings.Default.DestinationFolder.TrimEnd('\\'), "*.jpg");
                const string pattern = ".+_[Ss]plash_[2-9]+\\.jpg";

                var splashArts = (from image in images select Regex.Match(Path.GetFileName(image)?? string.Empty, pattern) into match where match.Success select match.Value).ToList();
                Output_ProgressBar.Maximum = splashArts.Count*2;
                _checkWorker.RunWorkerAsync(splashArts);
                //_checkWorker.RunWorkerAsync(GetSplashArts(DestinationFolder_TextBox.Text));
            }
            catch (Exception)
            {
                Output_TextBox.AppendText("Directory not found." + Environment.NewLine);
                EnableMainForm();
            }
        }

        /*IEnumerable<string> GetSplashArts(string RadPath)
        {
            const string pattern = ".+_[Ss]plash_[2-9]+\\.jpg";
            /*foreach(var image in Directory.GetFiles(RadPath.TrimEnd('\\'), "*.jpg"))
            {
                var match = Regex.Match(Path.GetFileName(image) ?? string.Empty, pattern);
                if(match.Success)
                {
                    yield return match.Value;
                }
            }*
            return (from image in Directory.GetFiles(RadPath.TrimEnd('\\'), "*.jpg")
                    select Regex.Match(Path.GetFileName(image) ?? string.Empty, pattern) into match where match.Success select match.Value);
        }*/

        private void CheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var splashArts = e.Argument as List<string>;
            if (splashArts == null) return;

            _checkWorker.ReportProgress(0);
            var count = 0;
            var destinationPath = Properties.Settings.Default.DestinationFolder.TrimEnd('\\') + "\\";
            var duplicates = new List<string>();

            var tempList = new List<Tuple<string, Color[]>>();
            Parallel.ForEach(splashArts, art =>
            {
                if (_checkWorker.CancellationPending) return;
                _checkWorker.ReportProgress(count++);
                var bmp = new Bitmap(destinationPath + art);
                var temp = new Color[(1 + bmp.Width / 50) * (1 + bmp.Height / 50)];
                var i = 0;
                for (var x = 0; x < bmp.Width; x += 50)
                {
                    for (var y = 0; y < bmp.Height; y += 50)
                    {
                        temp[i++] = bmp.GetPixel(x, y);
                    }
                }
                lock (tempList)
                {
                    tempList.Add(new Tuple<string, Color[]>(art, temp));
                }
                bmp.Dispose();
            }
            );

            _checkWorker.ReportProgress(count);
            while (tempList.Count > 0)
            {
                if (_checkWorker.CancellationPending) return;
                _checkWorker.ReportProgress(count++);
                var champ = tempList.First();
                tempList.Remove(champ);
                Parallel.ForEach(tempList, bitmap =>
                {
                    if (_checkWorker.CancellationPending) return;
                    lock(champ)
                    {
                        // Just fuck you riot ... Addding duplicates to same champion ...
                        //if (bitmap.Item1.StartsWith(champ1.Item1.Split('_')[0])) continue;
                        if (!Static.CompareSplash(champ.Item2, bitmap.Item2)) return;
                        lock (duplicates)
                        {
                            duplicates.Add(bitmap.Item1);
                            _checkWorker.ReportProgress(count, champ.Item1 + "\t is duplicate of\t " + bitmap.Item1 + Environment.NewLine);
                        }
                    }
                }
                );
            }

            var tmp = Static.GetIgnoreList();
            Parallel.ForEach(duplicates, duplicate =>
            {
                if (_checkWorker.CancellationPending) return;
                File.Delete(destinationPath + duplicate);
                lock(tmp)
                {
                    if (!tmp.Contains(duplicate))
                        tmp.Add(duplicate);
                }
            });

            var ignore = tmp.Where(r => !string.IsNullOrWhiteSpace(r)).Aggregate(string.Empty, (current, r) => current + (r + "\r\n"));
            ignore = ignore.Trim();
            if (ignore.Length == 0) ignore = null;
            Properties.Settings.Default.IgnoreList = ignore;
            Properties.Settings.Default.Save();
        }

        private void LeagueFolder_Button_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = LeagueFolder_TextBox.Text,
                Description = @"Select League of Legends Folder that contains: RADS folder, lol.launcher.exe and so on."
            };

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            LeagueFolder_TextBox.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.LeagueFolder = LeagueFolder_TextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void DestinationFolder_Button_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = DestinationFolder_TextBox.Text,
                Description = @"Select Folder where will be Splash Arts copied."
            };

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            DestinationFolder_TextBox.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = DestinationFolder_TextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _copyWorker.CancelAsync();
            _checkWorker.CancelAsync();
            Properties.Settings.Default.LeagueFolder = LeagueFolder_TextBox.Text.TrimEnd('\\');
            Properties.Settings.Default.DestinationFolder = DestinationFolder_TextBox.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
        }

        private void Export_Button_Click(object sender, EventArgs e)
        {
            DisableMainForm();
            Properties.Settings.Default.LeagueFolder = LeagueFolder_TextBox.Text;
            Properties.Settings.Default.DestinationFolder = DestinationFolder_TextBox.Text.TrimEnd('\\');
            Properties.Settings.Default.Save();
            try
            {
                var images = Directory.GetFiles(Static.GetRadPath(), "*.jpg");
                const string pattern = ".+_[Ss]plash_[0-9]+\\.jpg";

                var splashArts = (from image in images select Regex.Match(Path.GetFileName(image)?? string.Empty, pattern) into match where match.Success select match.Value).ToList();
                Output_ProgressBar.Maximum = splashArts.Count;
                _copyWorker.RunWorkerAsync(splashArts);

            }
            catch (DirectoryNotFoundException)
            {
                Output_TextBox.AppendText("Directory not found." + Environment.NewLine);
                EnableMainForm();
            }
        }

        private void DisableMainForm()
        {
            Cancel_Button.Visible = true;
            Export_Button.Visible = false;
            Options_Button.Enabled = false;
            LeagueFolder_Button.Enabled = false;
            LeagueFolder_TextBox.Enabled = false;
            DestinationFolder_Button.Enabled = false;
            DestinationFolder_TextBox.Enabled = false;
            Output_ProgressBar.Value = 0;
            Output_TextBox.Clear();
            UseWaitCursor = true;
        }

        private void EnableMainForm()
        {
            WorkerRunWorkerCompleted(this, null);
            Output_ProgressBar.Value = 0;
        }
        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Export_Button.Visible = true;
            Cancel_Button.Visible = false;
            Options_Button.Enabled = true;
            DestinationFolder_Button.Enabled = true;
            DestinationFolder_TextBox.Enabled = true;
            LeagueFolder_Button.Enabled = true;
            LeagueFolder_TextBox.Enabled = true;
            Output_ProgressBar.Value = Output_ProgressBar.Maximum;
            Text = @"League of Legends Backgrounds Exporter";
            UseWaitCursor = false;
            try
            {
                if (Static.IsActive(Handle))
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
                    TaskbarProgress.SetValue(Handle, 0, 1);
                }
                else
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);
                }
            }
            catch (ObjectDisposedException)
            {
                //do(nothing);
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((_copyWorker.CancellationPending && _copyWorker.IsBusy) || (_checkWorker.CancellationPending && _checkWorker.IsBusy)) return;
            if(e.ProgressPercentage > Output_ProgressBar.Maximum)
            {
                Output_ProgressBar.Maximum = (int)((Output_ProgressBar.Maximum+1)*1.5);
            }
            Output_ProgressBar.Value = e.ProgressPercentage;
            Text = @"[" + ((decimal)e.ProgressPercentage / Output_ProgressBar.Maximum).ToString("P") + @"] League of Legends Backgrounds Exporter";
            if (e.UserState != null) Output_TextBox.AppendText((string)e.UserState);
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, e.ProgressPercentage, Output_ProgressBar.Maximum);
        }

        private void CopyWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var splashArts = e.Argument as List<string>;
            if (splashArts == null) return;
            _copyWorker.ReportProgress(0);
            var count = 1;
            Directory.CreateDirectory(Properties.Settings.Default.DestinationFolder + "\\");
            var sourcePath = Static.GetRadPath();
            const string pattern = "(.+)_[Ss]plash_[0-9]+\\.jpg";
            var temp = string.Empty;
            // ReSharper disable once PossibleNullReferenceException
            Parallel.ForEach(splashArts, champ =>
            {
                if (_copyWorker.CancellationPending) { return; }
                var match = Regex.Match(champ, pattern);
                if (match.Success) { temp = match.Groups[1].Value; }
                if (!Static.GetIgnoreList().Contains(champ, StringComparer.OrdinalIgnoreCase) &&
                    !Static.GetChampsList().Contains(temp, StringComparer.OrdinalIgnoreCase))
                {
                    if (Static.IsEmptySplash(new Bitmap(sourcePath + champ)))
                    {
                        try
                        {
                            File.Copy(sourcePath + champ, Properties.Settings.Default.DestinationFolder + "\\" + champ,
                                true);
                            _copyWorker.ReportProgress(count++
                                /*, "File copied from " + sourcePath + champ + " to " + Properties.Settings.Default.DestinationFolder + "\\" + champ + " successfully!" + Environment.NewLine*/);
                        }
                        catch
                        {
                            _copyWorker.ReportProgress(count++,
                                "Failed to copy from " + sourcePath + champ + " to " +
                                Properties.Settings.Default.DestinationFolder + "\\" + champ + " !" +
                                Environment.NewLine);
                        }
                    }
                    else
                    {
                        _copyWorker.ReportProgress(count++, "Ignoring (not finished) " + champ + Environment.NewLine);
                    }
                }
                else
                {
                    _copyWorker.ReportProgress(count++, "Ignoring " + champ + Environment.NewLine);
                }
            });
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            _copyWorker.CancelAsync();
            _checkWorker.CancelAsync();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (_copyWorker.IsBusy || _checkWorker.IsBusy) return;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);
        }
    }
}
