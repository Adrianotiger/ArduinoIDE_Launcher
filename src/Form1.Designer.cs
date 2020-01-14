using System.Drawing;

namespace ArduinoIDE_Launcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("PROJECT", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("CUSTOM", System.Windows.Forms.HorizontalAlignment.Right);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Recent", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Sketch Folder", System.Windows.Forms.HorizontalAlignment.Left);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ListViewPreferences = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.ListViewSketches = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.ButtonImportIno = new System.Windows.Forms.Button();
            this.ButtonSavePref = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.CheckBoxOptionStartDefault = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonSaveCustom = new System.Windows.Forms.Button();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(608, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(405, 23);
            this.textBox1.TabIndex = 1;
            // 
            // ListViewPreferences
            // 
            this.ListViewPreferences.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ListViewPreferences.CheckBoxes = true;
            this.ListViewPreferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6});
            this.ListViewPreferences.FullRowSelect = true;
            this.ListViewPreferences.GridLines = true;
            listViewGroup5.Header = "PROJECT";
            listViewGroup5.Name = "PROJECT";
            listViewGroup6.Header = "CUSTOM";
            listViewGroup6.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            listViewGroup6.Name = "CUSTOM";
            this.ListViewPreferences.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6});
            this.ListViewPreferences.HideSelection = false;
            this.ListViewPreferences.LabelWrap = false;
            this.ListViewPreferences.Location = new System.Drawing.Point(485, 103);
            this.ListViewPreferences.MultiSelect = false;
            this.ListViewPreferences.Name = "ListViewPreferences";
            this.ListViewPreferences.Size = new System.Drawing.Size(528, 461);
            this.ListViewPreferences.TabIndex = 3;
            this.ListViewPreferences.UseCompatibleStateImageBehavior = false;
            this.ListViewPreferences.View = System.Windows.Forms.View.Details;
            this.ListViewPreferences.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewPreferences_ItemCheck);
            this.ListViewPreferences.SelectedIndexChanged += new System.EventHandler(this.ListViewPreferences_SelectedIndexChanged);
            this.ListViewPreferences.DoubleClick += new System.EventHandler(this.ListViewPreferences_DoubleClick);
            this.ListViewPreferences.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewPreferences_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Param";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "New";
            this.columnHeader6.Width = 150;
            // 
            // ListViewSketches
            // 
            this.ListViewSketches.AllowColumnReorder = true;
            this.ListViewSketches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.ListViewSketches.FullRowSelect = true;
            this.ListViewSketches.GridLines = true;
            listViewGroup7.Header = "Recent";
            listViewGroup7.Name = "Recent";
            listViewGroup8.Header = "Sketch Folder";
            listViewGroup8.Name = "SketchFolder";
            this.ListViewSketches.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup7,
            listViewGroup8});
            this.ListViewSketches.HideSelection = false;
            this.ListViewSketches.Location = new System.Drawing.Point(13, 41);
            this.ListViewSketches.MultiSelect = false;
            this.ListViewSketches.Name = "ListViewSketches";
            this.ListViewSketches.Size = new System.Drawing.Size(466, 523);
            this.ListViewSketches.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListViewSketches.TabIndex = 5;
            this.ListViewSketches.UseCompatibleStateImageBehavior = false;
            this.ListViewSketches.View = System.Windows.Forms.View.Details;
            this.ListViewSketches.SelectedIndexChanged += new System.EventHandler(this.ListViewSketches_SelectedIndexChanged);
            this.ListViewSketches.DoubleClick += new System.EventHandler(this.ListViewSketches_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Project Name";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Board";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date";
            this.columnHeader5.Width = 130;
            // 
            // ButtonImportIno
            // 
            this.ButtonImportIno.Location = new System.Drawing.Point(13, 6);
            this.ButtonImportIno.Name = "ButtonImportIno";
            this.ButtonImportIno.Size = new System.Drawing.Size(104, 24);
            this.ButtonImportIno.TabIndex = 6;
            this.ButtonImportIno.Text = "Import .ino";
            this.ButtonImportIno.UseVisualStyleBackColor = true;
            this.ButtonImportIno.Click += new System.EventHandler(this.ButtonImportIno_Click);
            // 
            // ButtonSavePref
            // 
            this.ButtonSavePref.Location = new System.Drawing.Point(623, 575);
            this.ButtonSavePref.Name = "ButtonSavePref";
            this.ButtonSavePref.Size = new System.Drawing.Size(224, 24);
            this.ButtonSavePref.TabIndex = 6;
            this.ButtonSavePref.Text = "Save current preferences to project";
            this.ButtonSavePref.UseVisualStyleBackColor = true;
            this.ButtonSavePref.Click += new System.EventHandler(this.ButtonSavePref_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 30);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(466, 10);
            this.progressBar1.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(608, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(402, 28);
            this.button3.TabIndex = 8;
            this.button3.Text = "Click to set Arduino Path";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ButtonFindArduino_Click);
            // 
            // CheckBoxOptionStartDefault
            // 
            this.CheckBoxOptionStartDefault.AutoSize = true;
            this.CheckBoxOptionStartDefault.Location = new System.Drawing.Point(13, 579);
            this.CheckBoxOptionStartDefault.Name = "CheckBoxOptionStartDefault";
            this.CheckBoxOptionStartDefault.Size = new System.Drawing.Size(304, 19);
            this.CheckBoxOptionStartDefault.TabIndex = 9;
            this.CheckBoxOptionStartDefault.Text = "Start project with the default Arduino IDE parameters";
            this.CheckBoxOptionStartDefault.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(485, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Preferences location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Arduino path:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(485, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Arduino IDE settings:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(582, 575);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "💾";
            // 
            // ButtonSaveCustom
            // 
            this.ButtonSaveCustom.Location = new System.Drawing.Point(853, 575);
            this.ButtonSaveCustom.Name = "ButtonSaveCustom";
            this.ButtonSaveCustom.Size = new System.Drawing.Size(160, 24);
            this.ButtonSaveCustom.TabIndex = 6;
            this.ButtonSaveCustom.Text = "Save new preferences";
            this.ButtonSaveCustom.UseVisualStyleBackColor = true;
            this.ButtonSaveCustom.Click += new System.EventHandler(this.ButtonSaveCustom_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 609);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckBoxOptionStartDefault);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ButtonImportIno);
            this.Controls.Add(this.ListViewSketches);
            this.Controls.Add(this.ListViewPreferences);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ButtonSavePref);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ButtonSaveCustom);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arduino IDE Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView ListViewPreferences;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView ListViewSketches;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button ButtonImportIno;
        private System.Windows.Forms.Button ButtonSavePref;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox CheckBoxOptionStartDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButtonSaveCustom;
    }
}

