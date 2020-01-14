using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Settings> SettingList;
        private readonly Arduino ArduinoIDE = new Arduino();
        private bool PreferencesFilled = false;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = ArduinoIDE.GetPreferencesDefaultFullPath();
            ButtonSavePref.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(ArduinoIDE.GetPreferencesDefaultFullPath()))
            {
                MessageBox.Show("Unable to find Arduino parameters on " + ArduinoIDE.GetPreferencesDefaultFullPath(), "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Enabled = false;

            progressBar1.Visible = true;
            progressBar1.Value = 10;
            PreferencesFilled = false;

            if (Properties.Settings1.Default.ArduinoFolder.Length > 10)
                button3.Text = Properties.Settings1.Default.ArduinoFolder;

            Task.Run(async () =>
            {
                await Task.Delay(50); // leave time to show form.

                SettingList = ArduinoIDE.LoadPreferences();

                this.Invoke(new Action(() =>
                {
                    InitApplication();
                }));

                this.Invoke(new Action(() => { progressBar1.Value = 30; }));

                do {
                    await Task.Delay(20);
                    if (SettingList.ContainsKey(Arduino.PreferencesSketchBookParam)) break;
                } while (!PreferencesFilled);

                var sketches = ArduinoIDE.FindSketches(SettingList[Arduino.PreferencesSketchBookParam]?.Value);

                this.Invoke(new Action(() => { progressBar1.Value = 60; }));

                sketches.ForEach(lv =>
                {
                    UpdateIcon(lv);
                });

                this.Invoke(new Action(() =>
                {
                    FillSketches(sketches);
                }));

                this.Invoke(new Action(() => { progressBar1.Value = 100; }));

                await Task.Delay(500);

                this.Invoke(new Action(() => {
                    ArduinoIDE.StartPreferencesWatcher(WatcherChanged); 
                    progressBar1.Visible = false;
                    this.Enabled = true;
                }));
            });
        }
        
        private void InitApplication()
        {
            ListViewSketches.SmallImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(16, 16)
            };

            var resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
            if (resourceSet != null)
            {
                foreach (DictionaryEntry entry in resourceSet)
                {
                    //only get images
                    if (entry.Value is Bitmap value)
                    {
                        ListViewSketches.SmallImageList.Images.Add((string)entry.Key, value);
                    }
                }
            }
            
            FillPreferences();
        }

        private void FillSketches(List<ListViewItem> items)
        {
            items.ForEach(i =>
            {
                i.Group = ListViewSketches.Groups[i.Group.Header];
            });
            ListViewSketches.Items.AddRange(items.ToArray());
        }
        
        private void UpdateIcon(ListViewItem lvi)
        {
            var path = lvi.Tag.ToString();
            var ino = Path.GetFileName(path).ToString();
            var filePath = Path.Combine(path, ino + ".ino");
            //var prefPath = Path.Combine(path, "." + Arduino.PreferencesFileName);
            lvi.SubItems.Clear();
            lvi.Text = ino;
            var paramsFile = ArduinoIDE.GetCustomPreferencesFile(path);
            var paramsIno = ArduinoIDE.GetCustomPreferencesIno(filePath);

            if (paramsFile.Count == 0 && paramsIno.Count == 0)
            {
                lvi.ImageKey = "icon_noparam";
                lvi.SubItems.Add("-");
            }
            else
            {
                if(paramsIno.ContainsKey("board"))
                {
                    lvi.SubItems.Add(paramsIno["board"]);
                    if(paramsFile.Count > 0)
                        lvi.ImageKey = "icon_hybridparam";
                    else
                        lvi.ImageKey = "icon_newparam";
                }
                else if(paramsFile.ContainsKey("board"))
                {
                    lvi.SubItems.Add(paramsFile["board"]);
                    if (paramsIno.Count > 0)
                        lvi.ImageKey = "icon_hybridparam";
                    else
                        lvi.ImageKey = "icon_warning";
                }
                else
                {
                    lvi.SubItems.Add("-");

                    if (paramsFile.Count > 0 && paramsIno.Count > 0)
                        lvi.ImageKey = "icon_hybridparam";
                    else if(paramsIno.Count > 0)
                        lvi.ImageKey = "icon_newparam";
                    else
                        lvi.ImageKey = "icon_warning";
                }
            }
            lvi.SubItems.Add(File.GetLastWriteTime(filePath).ToString());
        }

        private void WatcherChanged(int status)
        {
            ArduinoIDE.StopPreferencesWatcher();

            this.Invoke(new Action(() =>
            {
                if (MessageBox.Show("Preferences changed, do you want reload the file?", "Arduino Preferences", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SettingList = ArduinoIDE.LoadPreferences();
                    FillPreferences();
                    ListViewSketches.SelectedItems.Clear();
                }
                ArduinoIDE.StartPreferencesWatcher();
            }));
        }

        private void FillPreferences()
        {
            listView2.Items.Clear();
            listView2.Groups.Clear();
            Arduino.PreferencesApplicationGroups.ForEach(g => { listView2.Groups.Add(g, g); });

            foreach (var s in SettingList.Values)
            {
                if (s.Group != "" && listView2.Groups[s.Group] == null)
                {
                    listView2.Groups.Add(s.Group, s.Group);
                }

                var lvi = new ListViewItem(s.SubParameter);
                lvi.SubItems.Add(s.Value);
                lvi.SubItems.Add("");

                lvi.Group = listView2.Groups[s.Group];
                lvi.Name = s.Parameter;
                lvi.Checked = s.CheckedDefault;

                s.LVItem = listView2.Items.Add(lvi);
            };

            PreferencesFilled = true;
        }

        private void BlankListView2()
        {
            foreach (var sl in SettingList.Values)
            {
                if (sl.IsIgnoring)
                    sl.LVItem.BackColor = Arduino.PreferencesColor.Disabled;
                else
                    sl.LVItem.BackColor = Arduino.PreferencesColor.Void;
                sl.LVItem.SubItems[2].Text = "";
            };
        }

        private void AddRecent(string recent)
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
                            Properties.Settings1.Default["Recent" + k.ToString("D2")] = Properties.Settings1.Default["Recent" + (k-1).ToString("D2")];
                        }
                        Properties.Settings1.Default["Recent00"] = recent;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                for (var k = Properties.Settings1.Default.RecentNum - 1; k > 0; k--)
                {
                    Properties.Settings1.Default["Recent" + k.ToString("D2")] = Properties.Settings1.Default["Recent" + (k - 1).ToString("D2")];
                }
                Properties.Settings1.Default["Recent00"] = recent;
                if (Properties.Settings1.Default.RecentNum < 16) Properties.Settings1.Default.RecentNum++;
            }
            Properties.Settings1.Default.Save();
        }


        private void ButtonFindArduino_Click(object sender, EventArgs e)
        {
            using var fo = new OpenFileDialog
            {
                Filter = "?rduino.exe|Arduino",
                FileName = "*rduino.exe",
                InitialDirectory = button3.Text,
                CheckFileExists = true
            };
            if (fo.ShowDialog() == DialogResult.OK)
            {
                button3.Text = fo.FileName;
                Properties.Settings1.Default.ArduinoFolder = fo.FileName;
                Properties.Settings1.Default.Save();
            }
        }

        private void ButtonImportIno_Click(object sender, EventArgs e)
        {
            using var fd = new OpenFileDialog
            {
                Filter = "*.ino|ino project",
                Title = "Open Arduino Project",
                CheckFileExists = true,
                FileName = "*.ino",
                Multiselect = false
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                var fn = Path.GetFileName(fd.FileName);

                var lvi = new ListViewItem(fn)
                {
                    Tag = Path.GetDirectoryName(fd.FileName),
                    Group = ListViewSketches.Groups["Recent"],
                    ImageKey = "icon_loading"
                };
                ListViewSketches.Items.Add(lvi);

                AddRecent(fd.FileName);

                UpdateIcon(lvi);
            }
        }

        private void ButtonSavePref_Click(object sender, EventArgs e)
        {
            if (ListViewSketches.SelectedItems.Count > 0)
            {
                var preference = "";
                for (var j = 0; j < listView2.Items.Count; j++)
                {
                    if (listView2.Items[j].Checked)
                    {
                        var p = "";
                        if (listView2.Items[j].Group == null || Arduino.PreferencesApplicationGroups.Contains(listView2.Items[j].Group.Header))
                        {
                            p += listView2.Items[j].Text;
                        }
                        else
                        {
                            p += listView2.Items[j].Group.Name + listView2.Items[j].Text;
                        }
                        p += "=" + listView2.Items[j].SubItems[1].Text + "\n";
                        preference += p;
                    }
                }
                File.WriteAllText(Path.Combine(ListViewSketches.SelectedItems[0].Tag.ToString(), "." + Arduino.PreferencesFileName), preference);
                UpdateIcon(ListViewSketches.SelectedItems[0]);
                ListViewSketches.SelectedItems.Clear();
            }
        }

        private void ListViewSketches_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlankListView2();
            if (ListViewSketches.SelectedItems.Count > 0)
            {
                var lvi = ListViewSketches.SelectedItems[0];
                var pathIno = Path.Combine(lvi.Tag.ToString(), Path.GetFileName(lvi.Tag.ToString()) + ".ino");

                var paramsFile = ArduinoIDE.GetCustomPreferencesFile(lvi.Tag.ToString());
                var paramsIno = ArduinoIDE.GetCustomPreferencesIno(pathIno);

                foreach(var p in paramsFile)
                {
                    if(SettingList.ContainsKey(p.Key))
                    {
                        if (SettingList[p.Key].LVItem.SubItems[1].Text == p.Value)
                            SettingList[p.Key].LVItem.BackColor = Arduino.PreferencesColor.Intendical;
                        else
                            SettingList[p.Key].LVItem.BackColor = Arduino.PreferencesColor.DifferentFile;
                        SettingList[p.Key].LVItem.SubItems[2].Text = p.Value;
                    }
                }

                foreach (var p in paramsIno)
                {
                    if (SettingList.ContainsKey(p.Key))
                    {
                        if (SettingList[p.Key].LVItem.SubItems[1].Text == p.Value)
                            SettingList[p.Key].LVItem.BackColor = Arduino.PreferencesColor.Intendical;
                        else
                            SettingList[p.Key].LVItem.BackColor = Arduino.PreferencesColor.DifferentIno;
                        SettingList[p.Key].LVItem.SubItems[2].Text = p.Value;
                    }
                }

                ButtonSavePref.Enabled = true;
            }
            else
            {
                ButtonSavePref.Enabled = false;
            }
        }

        private void ListViewSketches_DoubleClick(object sender, EventArgs e)
        {
            if (ListViewSketches.SelectedItems.Count > 0)
            {
                ArduinoIDE.StopPreferencesWatcher();

                var lvi = ListViewSketches.SelectedItems[0];
                var projectPath = lvi.Tag.ToString();
                var pathIno = Path.Combine(lvi.Tag.ToString(), Path.GetFileName(projectPath) + ".ino");
                var success = true;

                if(CheckBoxOptionStartDefault.Checked)
                {
                    success = ArduinoIDE.LaunchArduinoIDE(new Dictionary<string, string>(), null, projectPath);
                }
                else
                {
                    var paramsFile = ArduinoIDE.GetCustomPreferencesFile(lvi.Tag.ToString());
                    var paramsIno = ArduinoIDE.GetCustomPreferencesIno(pathIno);

                    paramsFile.ToList().ForEach(x => { if (!paramsIno.ContainsKey(x.Key)) paramsIno.Add(x.Key, x.Value); });

                    success = ArduinoIDE.LaunchArduinoIDE(paramsIno, SettingList, projectPath);
                }

                if (success)
                {
                    AddRecent(pathIno);
                    bool found = false;

                    for (var j = 0; j < ListViewSketches.Groups["Recent"].Items.Count; j++)
                    {
                        if (ListViewSketches.Groups["Recent"].Items[j].Tag.ToString() == projectPath)
                        {
                            found = true;
                            UpdateIcon(ListViewSketches.SelectedItems[0]);
                            ListViewSketches.SelectedItems.Clear();
                            ListViewSketches.Groups["Recent"].Items[j].Selected = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        ListViewSketches.SelectedItems[0].Group = ListViewSketches.Groups["Recent"];
                        UpdateIcon(ListViewSketches.SelectedItems[0]);
                    }
                }
                else
                { 
                    if(MessageBox.Show("Unable to launch Arduino IDE: \n Be sure the Arduino folder is selected", "Arduino launcher", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                        ButtonFindArduino_Click(sender, e);
                }

                ArduinoIDE.StartPreferencesWatcher();
            }
        }

    }
}
