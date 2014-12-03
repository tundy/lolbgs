using System;
using System.Collections.Generic;
using System.IO;

namespace lolbgs
{
    static internal class Settings
    {
        static internal List<String> GetIgnoreList()
        {
            var result = new List<String>();
            var settings = Properties.Settings.Default.IgnoreList;
            if (string.IsNullOrEmpty(settings)) return result;
            settings = settings.Replace("\r\n", "\n");
            var tmp = settings.Split('\n');
            result.AddRange(tmp);
            return result;
        }

        static internal List<String> GetChampsList()
        {
            var result = new List<String>();
            var settings = Properties.Settings.Default.Champs;
            if (string.IsNullOrEmpty(settings)) return result;
            settings = settings.Replace("\r\n", "\n");
            var tmp = settings.Split('\n');
            result.AddRange(tmp);
            return result;
        }

        static internal string GetRadPath()
        {

            var source = Properties.Settings.Default.LeagueFolder + "\\RADS\\projects\\lol_air_client\\releases\\";
            var directories = Directory.GetDirectories(source);
            if (directories[0] != null)
                source = directories[0] + "\\deploy\\assets\\images\\champions\\";
            else
                return null;
            return source;
        }
    }
}
