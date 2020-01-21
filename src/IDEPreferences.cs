using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    class IDEPreferences
    {
        private readonly List<string> CheckStandard = new List<string> { "board", "programmer", "software", "target_package", "target_platform" };
        private readonly List<string> CheckGroups = new List<string> { "CUSTOM" };
        private readonly List<string> IgnoredParams = new List<string> { "last.", "recent.sketches" };

        private String _param = "";
        public ListViewItem LVItem { get; set; } = null;
        public String Value { get; set; } = "";
            
        public String Group { get; private set; } = "";
        public String Parameter
        {
            get
            {
                return _param;
            }
            set
            {
                if (value.StartsWith("custom_")) Group = "CUSTOM";
                else if (value.Contains(".")) Group = value.Substring(0, value.IndexOf("."));
                _param = value;
            }
        }

        public String SubParameter
        {
            get
            {
                if (Parameter.Contains(".")) return Parameter.Substring(Parameter.IndexOf(".") + 1);
                else return Parameter;
            }
        }

        public bool CheckedDefault { get
            {
                if (Group != "") return (CheckGroups.Contains(Group));
                else return CheckStandard.Contains(Parameter);
            } 
        }

        public bool IsIgnoring
        {
            get
            {
                for(var j=0;j<IgnoredParams.Count;j++)
                {
                    if (Parameter.StartsWith(IgnoredParams[j])) return true;
                }
                return false;
            }
        }

    }
}
