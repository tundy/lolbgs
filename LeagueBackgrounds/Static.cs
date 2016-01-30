using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace LeagueBackgrounds
{
    static internal class Static
    {
        static internal List<string> GetIgnoreList()
        {
            var result = new List<string>();
            var settings = Properties.Settings.Default.IgnoreList;
            if (string.IsNullOrEmpty(settings)) return result;
            settings = settings.Replace("\r\n", "\n");
            var tmp = settings.Split('\n');
            result.AddRange(tmp);
            return result;
        }

        static internal List<string> GetChampsList()
        {
            var result = new List<string>();
            var settings = Properties.Settings.Default.Champs;
            if (string.IsNullOrEmpty(settings)) return result;
            settings = settings.Replace("\r\n", "\n");
            var tmp = settings.Split('\n');
            result.AddRange(tmp);
            return result;
        }

        static internal string FindLeagueOfLegends()
        {
            var temp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\Wow6432Node\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Riot Games\RADS", "LOCALROOTFOLDER", null)
                    ?? (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\RIOT GAMES\RADS", "LOCALROOTFOLDER", null);
            return temp != null ? Directory.GetParent(temp).ToString() : @"C:\Riot Games\League of Legends";
        }
        
        static internal string GetRadPath()
        {
            var source = Properties.Settings.Default.LeagueFolder + "\\RADS\\projects\\lol_air_client\\releases\\";
            var directories = Directory.GetDirectories(source);
            return directories[0] != null ? directories[0] + "\\deploy\\assets\\images\\champions\\" : null;
        }

        static internal bool IsEmptySplash(Bitmap splash)
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

        /*static internal bool CompareSplash(Bitmap imageA, Bitmap imageB)
        {
            if (imageA.Size != imageB.Size) return false;
            for (var x = 1; x < imageA.Width; x += 200)
                for (var y = 1; y < imageA.Height; y += 100)
                    if (imageA.GetPixel(x, y) != imageB.GetPixel(x, y))
                        return false;
            return true;
        }
        static internal bool CompareSplash(List<Color> imageA, List<Color> imageB)
        {
            if (imageA.Count != imageB.Count) return false;
            return !imageA.Where((t, i) => t != imageB[i]).Any();
        }*/

        static internal bool CompareSplash(Color[] imageA, Color[] imageB)
        {
            if (imageA.GetLength(0) != imageB.GetLength(0)) return false;
            return !imageA.Where((t, i) => t != imageB[i]).Any();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsActive(IntPtr handle)
        {
            var activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        /*[DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hWnd);*/

        /*static bool IsWinVistaOrHigher()
        {
            OperatingSystem os = Environment.OSVersion;
            return (os.Platform == PlatformID.Win32NT) && (os.Version.Major >= 6);
        }*/
    }
}
