using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using static LeagueBackgrounds.Properties.Settings;

#if DEBUG
using System.Diagnostics;
#endif

namespace LeagueBackgrounds
{
    public partial class MainForm : Form
    {
        #region BackgroundWorkers
        private readonly BackgroundWorker _copyWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private readonly BackgroundWorker _checkWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        #endregion

        #region Init
        public MainForm()
        {
            #region Load last or Default Destinations
            if (string.IsNullOrEmpty(Default.LeagueFolder))
                Default.LeagueFolder = Static.FindLeagueOfLegends();
            if (string.IsNullOrEmpty(Default.DestinationFolder))
#if DEBUG
                Properties.Settings.Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Backgrounds\\League Of Legends";
#else
                Default.DestinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\League Of Legends BGs";
#endif
            Default.Save();
            #endregion

            #region Adding EventHandlers to BackgroundWorkers
            _copyWorker.DoWork += CopyWorkerDoWork;
            _copyWorker.ProgressChanged += WorkerProgressChanged;
            _copyWorker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            _checkWorker.DoWork += CheckWorker_DoWork;
            _checkWorker.ProgressChanged += WorkerProgressChanged;
            _checkWorker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            #endregion

            InitializeComponent();

            #region DefaultValues
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);
            #endregion
        }
        #endregion

        #region ButtonClicks
        private void Options_Button_Click(object sender, EventArgs e)
        {
            if (new Options().ShowDialog() == DialogResult.Retry) RunCheckWorker();
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
            Default.LeagueFolder = LeagueFolder_TextBox.Text;
            Default.Save();
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
            Default.DestinationFolder = DestinationFolder_TextBox.Text;
            Default.Save();
        }

        private void Export_Button_Click(object sender, EventArgs e)
        {
            DisableMainForm();
            Default.LeagueFolder = LeagueFolder_TextBox.Text;
            Default.DestinationFolder = DestinationFolder_TextBox.Text.TrimEnd('\\');
            Default.Save();
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

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            _copyWorker.CancelAsync();
            lock (_checkWorker)
            {
                _checkWorker.CancelAsync();
            }
        }
        #endregion

        private void RunCheckWorker()
        {
            DisableMainForm();

            Default.DestinationFolder = DestinationFolder_TextBox.Text;
            Default.Save();
            //_checkWorker.RunWorkerAsync();
            try
            {
                var images = Directory.GetFiles(Default.DestinationFolder.TrimEnd('\\'), "*.jpg");
                const string pattern = ".+_[Ss]plash_[2-9]+\\.jpg";

                var splashArts = (from image in images select Regex.Match(Path.GetFileName(image) ?? string.Empty, pattern) into match where match.Success select match.Value).ToList();
                Output_ProgressBar.Maximum = splashArts.Count * 2;
                lock (_checkWorker)
                {
                    _checkWorker.RunWorkerAsync(splashArts);
                }
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

        #region BackgroundWorkers Methods

        private int _counter;
        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((_copyWorker.CancellationPending && _copyWorker.IsBusy) || (_checkWorker.CancellationPending && _checkWorker.IsBusy)) return;
            if(e.ProgressPercentage > Output_ProgressBar.Maximum)
            {
                Output_ProgressBar.Maximum = (int)((Output_ProgressBar.Maximum+1)*1.5);
            }
            Output_ProgressBar.Value = e.ProgressPercentage;
            var percent = (decimal)e.ProgressPercentage/Output_ProgressBar.Maximum;
            // GC after every 10 %
            var p = (int)(percent*100);
            if (p > _counter+10)
            {
                _counter += 10;
                GC.Collect();
            }
            else if (p < _counter)
            {
                _counter = p;
            }
            Text = $"[{percent.ToString("P")}] League of Legends Backgrounds Exporter";
            if (e.UserState != null) Output_TextBox.AppendText((string)e.UserState);
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            TaskbarProgress.SetValue(Handle, e.ProgressPercentage, Output_ProgressBar.Maximum);
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _counter = 0;
            Output_ProgressBar.Value = 0;
            Export_Button.Visible = true;
            Cancel_Button.Visible = false;
            Options_Button.Enabled = true;
            DestinationFolder_Button.Enabled = true;
            DestinationFolder_TextBox.Enabled = true;
            LeagueFolder_Button.Enabled = true;
            LeagueFolder_TextBox.Enabled = true;
            Output_ProgressBar.Value = Output_ProgressBar.Maximum;
            Text = @"League of Legends Backgrounds Exporter";
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
#if DEBUG
            catch (ObjectDisposedException ex)
            {
                Debug.WriteLine(ex);
#else
            catch (ObjectDisposedException)
            {
#endif
            }
            GC.Collect();
            UseWaitCursor = false;
        }

        private void CheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var splashArts = e.Argument as List<string>;
            if (splashArts == null) return;

            lock (_checkWorker)
            {
                _checkWorker.ReportProgress(0);
            }
            var count = 0;
            var destinationPath = Default.DestinationFolder.EndsWith("\\")
                                ? Default.DestinationFolder
                                : Default.DestinationFolder + "\\";

            var tempList = new Queue<Tuple<string, Color[]>>();
            // Add every SpalshArt to list
            Parallel.ForEach(splashArts, art =>
            {
                if (_checkWorker.CancellationPending) return;
                // ReSharper disable once AccessToModifiedClosure
                lock (_checkWorker)
                {
                    _checkWorker.ReportProgress(count++);
                }
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
                    tempList.Enqueue(new Tuple<string, Color[]>(art, temp));
                }
                bmp.Dispose();
            });

            lock (_checkWorker)
            {
                _checkWorker.ReportProgress(count);
            }
            var tmp = Static.GetIgnoreList();
            while (tempList.Count > 0)
            {
                if (_checkWorker.CancellationPending) return;
                lock (_checkWorker)
                {
                    _checkWorker.ReportProgress(count++);
                }
                var champ = tempList.Dequeue();
                var remaining = tempList.ToList();
                Parallel.ForEach(remaining, bitmap =>
                {
                    if (_checkWorker.CancellationPending) return;
                    lock (champ)
                    {
                        // Come on riot ... Addding duplicates to same champion ...
                        //if (bitmap.Item1.StartsWith(champ1.Item1.Split('_')[0])) continue;
                        if (!Static.CompareSplash(champ.Item2, bitmap.Item2)) return;
                        File.Delete(destinationPath + bitmap.Item1);
                        lock (tmp) if (!tmp.Contains(bitmap.Item1)) tmp.Add(bitmap.Item1);
                        _checkWorker.ReportProgress(count, $"{champ.Item1}\t is duplicate of\t {bitmap.Item1}{Environment.NewLine}");
                    }
                });
            }
            var ignore = tmp.Where(r => !string.IsNullOrWhiteSpace(r)).Aggregate(string.Empty, (current, r) => $"{current}{r}\r\n");
            ignore = ignore.Trim();
            if (ignore.Length == 0) ignore = null;
            Default.IgnoreList = ignore;
            Default.Save();
        }
        
        private void CopyWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var splashArts = e.Argument as List<string>;
            if (splashArts == null) return;
            _copyWorker.ReportProgress(0);
            var count = 1;
            Directory.CreateDirectory(Default.DestinationFolder + "\\");
            var sourcePath = Static.GetRadPath();
            const string pattern = "(.+)_[Ss]plash_[0-9]+\\.jpg";
            var ignoreList = Static.GetIgnoreList();
            var champsList = Static.GetChampsList();
            Parallel.ForEach(splashArts, champ =>
            {
                if (_copyWorker.CancellationPending)
                {
                    return;
                }
                var match = Regex.Match(champ, pattern);
                string temp;
                if (match.Success)
                {
                    temp = match.Groups[1].Value;
                }
                else
                {
                    _copyWorker.ReportProgress(count, $"{sourcePath}{champ} Is not a Splash Art !{Environment.NewLine}");
                    return;
                }
#if DEBUG
                var a = ignoreList.Contains(champ, StringComparer.OrdinalIgnoreCase);
                var b = champsList.Contains(temp, StringComparer.OrdinalIgnoreCase);
                if (!a && !b)
#else
                if (!ignoreList.Contains(champ, StringComparer.OrdinalIgnoreCase) && !champsList.Contains(temp, StringComparer.OrdinalIgnoreCase))
#endif
                {
#if DEBUG
                        try
                    {
#endif
                        if (!Static.IsEmptySplash(new Bitmap(sourcePath + champ)))
                        {
                            try
                            {
                                File.Copy(sourcePath + champ, Default.DestinationFolder + "\\" + champ, true);
                                _copyWorker.ReportProgress(count++
                                    //, $@"File copied from {sourcePath}{champ} to {Properties.Settings.Default.DestinationFolder}\{champ} successfully!{Environment.NewLine}"
                                    );
                            }
                            catch
                            {
                                _copyWorker.ReportProgress(count++
                                    , $@"Failed to copy from {sourcePath}{champ} to {Default.DestinationFolder}\{champ} !{Environment.NewLine}"
                                    );
                            }
                        }
                        else
                        {
                            _copyWorker.ReportProgress(count++, $"Ignoring (not finished) {champ}{Environment.NewLine}");
                        }
#if DEBUG
                    }
                    catch (Exception)
                    {
                        throw;
                    }
#endif
                }
                else
                {
                    _copyWorker.ReportProgress(count++, $"Ignoring {champ}{Environment.NewLine}");
                }
            });
        }
#endregion

        private void DisableMainForm()
        {
            UseWaitCursor = true;
            Cancel_Button.Visible = true;
            Export_Button.Visible = false;
            Options_Button.Enabled = false;
            LeagueFolder_Button.Enabled = false;
            LeagueFolder_TextBox.Enabled = false;
            DestinationFolder_Button.Enabled = false;
            DestinationFolder_TextBox.Enabled = false;
            Output_ProgressBar.Value = 0;
            Output_TextBox.Clear();
        }

        private void EnableMainForm()
        {
            WorkerRunWorkerCompleted(this, null);
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (_copyWorker.IsBusy || _checkWorker.IsBusy) return;
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            TaskbarProgress.SetValue(Handle, 0, 1);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _copyWorker.CancelAsync();
            lock (_checkWorker)
            {
                _checkWorker.CancelAsync();
            }
            Default.LeagueFolder = LeagueFolder_TextBox.Text.TrimEnd('\\');
            Default.DestinationFolder = DestinationFolder_TextBox.Text.TrimEnd('\\');
            Default.Save();
        }
    }
}
