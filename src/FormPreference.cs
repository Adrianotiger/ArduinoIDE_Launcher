using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArduinoIDE_Launcher
{
    public partial class FormPreference : Form
    {
        public string NewValue { get; private set; } = "";

        public FormPreference(string parameterName, string parameterDefault, string parameterCustom)
        {
            InitializeComponent();

            label4.Text = parameterName;
            textBox2.Text = parameterDefault;
            textBox1.Text = NewValue = parameterCustom;

            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            NewValue = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
