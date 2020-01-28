using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    class Settings
    {
        public static void Init()
        {
            if (!Properties.Settings1.Default.SettingsUpdated)
            {
                Properties.Settings1.Default.Upgrade();
                Properties.Settings1.Default.SettingsUpdated = true;
                Properties.Settings1.Default.Save();
            }
        }

        private static string FindArduinoFolderFromRegistry()
        {
            string arduinoIDEPath = null;
            var obj = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(".ino");
            if (obj != null && obj.GetValue("") != null)
            {
                var key = obj.GetValue("").ToString();
                var obj2 = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(key);
                var cmd = obj2.OpenSubKey("shell\\open\\command");
                if (cmd != null && cmd.GetValue("") != null)
                {
                    var path = cmd.GetValue("").ToString();
                    path = path.Trim('"');
                    path = path.Substring(0, path.IndexOf('"'));
                    arduinoIDEPath = path;
                }
            }
            return arduinoIDEPath;
        }

        public static string GetArduinoFolder()
        {
            if (Properties.Settings1.Default.ArduinoFolder.Length > 10)
                return Properties.Settings1.Default.ArduinoFolder;
            else
                return FindArduinoFolderFromRegistry();
        }

        public static void SetArduinoFolder(string path)
        {
            Properties.Settings1.Default.ArduinoFolder = path;
            Properties.Settings1.Default.Save();
        }

        public static void AddToRecent(string recent)
        {
            var found = false;
            for (var j = 0; j < Properties.Settings1.Default.RecentNum; j++)
            {
                if (recent == Properties.Settings1.Default["Recent" + j.ToString("D2")].ToString())
                {
                    if (j > 0)
                    {
                        for (var k = j; k > 0; k--)
                        {
                            Properties.Settings1.Default["Recent" + k.ToString("D2")] = Properties.Settings1.Default["Recent" + (k - 1).ToString("D2")];
                        }
                        Properties.Settings1.Default["Recent00"] = recent;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                for (var k = Properties.Settings1.Default.RecentNum; k > 0; k--)
                {
                    Properties.Settings1.Default["Recent" + k.ToString("D2")] = Properties.Settings1.Default["Recent" + (k - 1).ToString("D2")];
                }
                Properties.Settings1.Default["Recent00"] = recent;
                if (Properties.Settings1.Default.RecentNum < 16) Properties.Settings1.Default.RecentNum++;
            }
            Properties.Settings1.Default.Save();
        }

        public static List<ListViewItem> GetRecentSketches()
        {
            List<int> removeSketches = new List<int>();
            List<ListViewItem> itemsList = new List<ListViewItem>();
            var recentCount = Properties.Settings1.Default.RecentNum;
            for (var j = 0; j < recentCount; j++)
            {
                var full = Properties.Settings1.Default["Recent" + j.ToString("D2")].ToString();
                var dir = Path.GetFileName(Path.GetDirectoryName(full));
                if (File.Exists(full))
                {
                    var lvi = new ListViewItem(dir)
                    {
                        Tag = Path.GetDirectoryName(full),
                        Group = new ListViewGroup("Recent"),// listView1.Groups["Recent"];
                        ImageKey = "icon_loading"
                    };
                    itemsList.Add(lvi);
                }
                else
                {
                    removeSketches.Add(j);
                }
            }
            removeSketches.Reverse();
            removeSketches.ForEach(i => RemoveRecentSketch(i));
            return itemsList;
        }

        public static void RemoveRecentSketch(int index)
        {
            var recentCount = Properties.Settings1.Default.RecentNum - 1;
            for (var j = index; j < recentCount; j++)
            {
                Properties.Settings1.Default["Recent" + j.ToString("D2")] = Properties.Settings1.Default["Recent" + (j+1).ToString("D2")].ToString();
            }
            Properties.Settings1.Default.RecentNum = recentCount;
            Properties.Settings1.Default.Save();
        }
    }
}