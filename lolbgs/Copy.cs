using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace lolbgs
{
    internal class Copy
    {
        private readonly List<String> _images;
        private readonly String _destinationPath;
        private readonly MyThread _t;

        internal Copy(MyThread t, String destinationPath, List<String> images)
        {
            _destinationPath = destinationPath;
            _images = images;
            _t = t;
        }

        internal void Run()
        {
            Directory.CreateDirectory(_destinationPath);
            var sourcePath = Settings.GetRadPath();
            const string pattern = "(.+)_[Ss]plash_[0-9]+\\.jpg";
            var temp = string.Empty;
            foreach (var champ in _images)
            {
                var match = Regex.Match(champ, pattern);
                if (match.Success)
                    temp = match.Groups[1].Value;
                if (!Settings.GetIgnoreList().Contains(champ, StringComparer.OrdinalIgnoreCase) && !Settings.GetChampsList().Contains(temp, StringComparer.OrdinalIgnoreCase))
                {
                    if (IsEmptySplash(new Bitmap(sourcePath + champ)))
                    {
                        try
                        {
                            File.Copy(sourcePath + champ, _destinationPath + champ, true);
                            _t.OnCopyDone("File copied from " + sourcePath + champ + " to " + _destinationPath + champ +
                                            " successfully!\r\n");
                        }
                        catch
                        {
                            _t.OnCopyDone("Failed to copy from " + sourcePath + champ + " to " + _destinationPath +
                                            champ + " !\r\n");
                        }
                    }
                    else
                    {
                        _t.OnCopyDone("Ignoring [not finished] " + champ + "\r\n");
                    }
                }
                else
                {
                    _t.OnCopyDone("Ignoring " + champ + "\r\n");
                }
            }
            _t.OnCopyFinished();
        }
        internal static bool IsEmptySplash(Bitmap splash)
        {
            if (splash.Size != new Size(1215, 717)) return false;
            var color = Color.FromArgb(125, 125, 125);
            for (var x = 0; x < 32; ++x)
            {
                for (var y = 0; y < 32; ++y)
                {
                    if (splash.GetPixel(x, y) == color) continue;
                    return true;
                }
            }
            return false;
        }
    }
}
