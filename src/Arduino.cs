using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    class Arduino
    {
        public static readonly string PreferencesFileName = "Preferences.txt";
        public static readonly string PreferencesFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string PreferencesSketchBookParam = "sketchbook.path";
        public static readonly List<string> PreferencesApplicationGroups = new List<string> { "CUSTOM", "PROJECT" };
        public static readonly string IDE_PARAM_KEY = "**#ide_param:";

        public static class PreferencesColor
        {
            public static readonly Color Void = Color.White;
            public static readonly Color Disabled = Color.FromArgb(200, 200, 200);
            public static readonly Color Intendical = Color.FromArgb(200, 255, 200);
            public static readonly Color DifferentFile = Color.FromArgb(255, 255, 200);
            public static readonly Color DifferentIno = Color.FromArgb(250, 230, 200);
        };

        private readonly FileSystemWatcher Watcher = new FileSystemWatcher();
        private Action<int> WatcherCallback = null;

        public Arduino()
        {

        }

        public string GetPreferencesDefaultFullPath()
        {
            string path = PreferencesFilePath;
            if (PreferencesFilePath.Contains("Roaming"))
            {
                path = PreferencesFilePath.Replace("Roaming", "Local");
            }
            path = Path.Combine(path, "Arduino15");
            return Path.Combine(path, PreferencesFileName);
        }

        public Dictionary<string, Settings> LoadPreferences()
        {
            Dictionary<string, Settings> settings = new Dictionary<string, Settings>(StringComparer.InvariantCulture);

            if(!File.Exists(GetPreferencesDefaultFullPath()))
            {
                Program.ErrorLog.Add("[E|LoadPreferences] Preferences file '" + GetPreferencesDefaultFullPath() + "' not found.");
                return settings;
            }
            try
            {
                var lines = File.ReadAllLines(GetPreferencesDefaultFullPath());

                for (var j = 0; j < lines.Length; j++)
                {
                    if (lines[j].IndexOf("=") > 1)
                    {
                        var param = lines[j].Substring(0, lines[j].IndexOf("="));
                        var paramValue = lines[j].Substring(lines[j].IndexOf("=") + 1);
                        settings.Add(param, new Settings
                        {
                            Value = paramValue,
                            Parameter = param
                        });
                    }
                    else
                    {
                        Program.ErrorLog.Add("[E|LoadPreferences] Error reading line '" + lines[j] + "'");
                    }
                }
            }
            catch(Exception ex)
            {
                Program.ErrorLog.Add("[E|LoadPreferences] " + ex.Message);
            }
            return settings;
        }

        public List<ListViewItem> FindSketches(string path)
        {
            List<ListViewItem> itemsList = new List<ListViewItem>();

            if (path == null || path == "")
            {
                Program.ErrorLog.Add("Unable to find Arduino Sketch Path");
                return itemsList;
            }

            for (var j = 0; j < Properties.Settings1.Default.RecentNum; j++)
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
            }

            var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            for (var j = 0; j < dirs.Length; j++)
            {
                var dir = Path.GetFileName(dirs[j]);
                if (File.Exists(Path.Combine(dirs[j], dir + ".ino")))
                {
                    var lvi = new ListViewItem(dir)
                    {
                        Tag = dirs[j],
                        Group = new ListViewGroup("SketchFolder"),// listView1.Groups["SketchFolder"];
                        ImageKey = "icon_loading"
                    };
                    itemsList.Add(lvi);
                }
            }

            return itemsList;
        }

        public void StartPreferencesWatcher(Action<int> callback = null)
        {
            if (callback != null)
            {
                Watcher.Path = PreferencesFilePath;
                Watcher.Filter = PreferencesFileName;
                Watcher.NotifyFilter = NotifyFilters.LastWrite;
                Watcher.Changed += Watcher_Changed; ;
                WatcherCallback = callback;
            }
            Watcher.EnableRaisingEvents = true;
        }

        public void StopPreferencesWatcher()
        {
            Watcher.EnableRaisingEvents = false;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            WatcherCallback(0);
        }

        public Dictionary<string, string> GetCustomPreferencesFile(string projectDirectory)
        {
            var ret = new Dictionary<string, string>();
            var filePath = Path.GetFullPath(projectDirectory);
            if(File.Exists(Path.Combine(filePath, "." + PreferencesFileName)))
            {
                List<string> lines = new List<string>(File.ReadAllLines(Path.Combine(filePath, "." + PreferencesFileName)));
                lines.ForEach(s =>
                    {
                        if(s.Contains("="))
                        {
                            var pair = s.Split('=', 2);
                            ret.Add(pair[0], pair[1]);
                        }
                    });
            }
            return ret;
        }

        public Dictionary<string, string> GetCustomPreferencesIno(string projectFilePath)
        {
            var ret = new Dictionary<string, string>();
            if (File.Exists(projectFilePath))
            {
                List<string> lines = new List<string>(File.ReadAllLines(projectFilePath));
                lines.ForEach(s =>
                {
                    if (s.Contains(IDE_PARAM_KEY) && s.Contains("=") && s.IndexOf("=") > s.IndexOf(IDE_PARAM_KEY))
                    {
                        var pair = s.Substring(s.IndexOf(IDE_PARAM_KEY) + IDE_PARAM_KEY.Length).Split("=");
                        ret.Add(pair[0], pair[1]);
                    }
                });
            }
            return ret;
        }

        public bool LaunchArduinoIDE(Dictionary<string, string> customPreferences, Dictionary<string, Settings> settingsList, string projectPath)
        {
            bool successfull = true;
            using (Process pr = new Process())
            {
                try
                {

                    var project = Path.GetFileName(projectPath) + ".ino";

                    if (customPreferences.Count == 0)
                    {
                        pr.StartInfo = new ProcessStartInfo
                        {
                            FileName = Properties.Settings1.Default.ArduinoFolder,
                            Arguments = " " + Path.Combine(Properties.Settings1.Default.ArduinoFolder, project) + ""
                        };
                    }
                    else
                    {
                        var preference = "";
                        foreach (var sl in settingsList.Values)
                        {
                            var p = "";
                            if (sl.IsIgnoring) continue;
                            p += sl.Parameter;

                            if (sl.LVItem.Checked)
                            {
                                if (sl.Parameter == "sketchbook.path" && sl.LVItem.SubItems[2].Text.StartsWith(".."))
                                {
                                    p += "=" + Path.Combine(projectPath, sl.LVItem.SubItems[2].Text) + "\r\n";
                                }
                                else
                                {
                                    p += "=" + sl.LVItem.SubItems[2].Text + "\r\n";
                                }
                            }
                            else
                                p += "=" + sl.LVItem.SubItems[1].Text + "\r\n";
                            preference += p;
                        }

                        var tempFile = Path.Combine(Path.GetTempPath(), project + "." + Arduino.PreferencesFileName);
                        File.WriteAllText(tempFile, preference);

                        pr.StartInfo = new ProcessStartInfo
                        {
                            FileName = Properties.Settings1.Default.ArduinoFolder,
                            Arguments = " --preferences-file " + tempFile + " " + Path.Combine(projectPath, project) + ""
                        };
                    }

                    pr.Start();
                }
                catch (Exception ex)
                {
                    Program.ErrorLog.Add("[E|LaunchArduinoIDE]" + ex.Message);
                    successfull = false;
                }
            }

            return successfull;
        }

        public void OpenSketchFolder(string sketchPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = sketchPath,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
    }
}
