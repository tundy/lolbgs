using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace lolbgs
{

    class Check
    {
        private readonly Dictionary<String,List<string>> _duplicates = new Dictionary<string, List<string>>();
        private readonly List<String> _images;
        private readonly String _destinationPath;
        private readonly MyThread _t;

        internal Check(MyThread t, String destinationPath, List<String> images)
        {
            _destinationPath = destinationPath;
            _images = images;
            _t = t;
        }

        internal void Run()
        {
            var count = 1;

            for (var index = 0; index < _images.Count; index++)
            {
                var champ1 = _images[index];
                _images.RemoveAt(index--);
                var a = new Bitmap(_destinationPath + champ1);
                for (var i = 0; i < _images.Count; i++)
                {
                    var champ2 = _images[i];
                    if (champ2.StartsWith(champ1.Split('_')[0])) continue;
                    var b = new Bitmap(_destinationPath + champ2);
                    if (!Compare(a, b))
                    {
                        b.Dispose();
                        continue;
                    }
                    b.Dispose();
                    if (_duplicates.ContainsKey(champ1))
                        _duplicates[champ1].Add(champ2);
                    else
                        _duplicates.Add(champ1, new List<string> {champ2});

                    _images.RemoveAt(i--);
                    _t.OnJobDone(champ2 + " is duplicate of " + champ1 + Environment.NewLine, count);
                }
                a.Dispose();
                _t.OnJobDone(champ1 + " comparing done " + Environment.NewLine, count++);
            }

            var text = string.Empty;
            _t.OnJobDone(Environment.NewLine + "Found this duplicates:" + Environment.NewLine, --count);
            foreach (var duplicate in _duplicates)
                foreach (var img in duplicate.Value)
                    text += img + " is duplicate of " + duplicate.Key + Environment.NewLine;
            _t.OnJobDone(text);

            var tmp = Settings.GetIgnoreList();
            foreach (var duplicate in _duplicates)
                foreach (var img in duplicate.Value)
                {
                    File.Delete(_destinationPath + img);
                    if (!tmp.Contains(img))
                        tmp.Add(img);
                }

            var ignore = string.Empty;
            foreach (var r in tmp)
            {
                if (!string.IsNullOrEmpty(r))
                    ignore += r + "\r\n";
            }
            ignore = ignore.Trim();
            if (ignore.Length == 0) ignore = null;
            Properties.Settings.Default.IgnoreList = ignore;
            Properties.Settings.Default.Save();

            _t.OnJobFinished();
        }

        internal static bool Compare(Bitmap imageA, Bitmap imageB)
        {
            if (imageA.Size != imageB.Size)
                return false;
            for (var x = 1; x < imageA.Width; x+=200)
                for (var y = 1; y < imageA.Height; y+=100)
                    if (imageA.GetPixel(x, y) != imageB.GetPixel(x, y))
                        return false;
            return true;
        }
    }
}
