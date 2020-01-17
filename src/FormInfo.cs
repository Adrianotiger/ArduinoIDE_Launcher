using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();

            //var ver1 = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
            //var ver2 = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        }

        private void FormInfo_Load(object sender, EventArgs e)
        {
            label1.Text = "Framework: ";
            label2.Text = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

            label3.Text = "For OS: ";
            label4.Text = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

            label5.Text = "Framework version: ";
            label6.Text = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion(); 

            label7.Text = "System/Process architecture: ";
            label8.Text = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString() + " / " + System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString();

            label9.Text = "Application version: ";
            label10.Text = Application.ProductVersion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = (sender as LinkLabel).Text
            };

            Process.Start(startInfo);
        }
    }
}
