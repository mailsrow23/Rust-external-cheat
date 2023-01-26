namespace RyuzakiUI
{
    partial class ConsoleApp
    {

        private System.Windows.Forms.Button _button_StartDump;
        private System.Windows.Forms.Button _button_Clear_Dump;
        private System.Windows.Forms.Button _button_StopDump;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _checkBox_EnableDebugging;
        private System.Windows.Forms.ListBox listBox_Console;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._button_StartDump = new System.Windows.Forms.Button();
            this._button_Clear_Dump = new System.Windows.Forms.Button();
            this._button_StopDump = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._checkBox_EnableDebugging = new System.Windows.Forms.CheckBox();
            this.listBox_Console = new System.Windows.Forms.ListBox();
            this._checkBox_TogglePlayers = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleCrosshair = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleToolCupboard = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleArmoredDoors = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleGarageDoors = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleDoubleMetalDoors = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleMetalDoor = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleBarrels = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleLargeBox = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleMilitaryCrates = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleNormalCrate = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleHemp = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleStone = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleMetal = new System.Windows.Forms.CheckBox();
            this._checkBox_ToggleSulfur = new System.Windows.Forms.CheckBox();
            this.sulfurColorDialog = new System.Windows.Forms.ColorDialog();
            this._checkBox_ToggleAnimals = new System.Windows.Forms.CheckBox();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.office2010SilverTheme1 = new Telerik.WinControls.Themes.Office2010SilverTheme();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGroupBox4 = new Telerik.WinControls.UI.RadGroupBox();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox4)).BeginInit();
            this.radGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            this.radPageViewPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _button_StartDump
            // 
            this._button_StartDump.Location = new System.Drawing.Point(22, 43);
            this._button_StartDump.Name = "_button_StartDump";
            this._button_StartDump.Size = new System.Drawing.Size(75, 23);
            this._button_StartDump.TabIndex = 0;
            this._button_StartDump.Text = "Start";
            this._button_StartDump.UseVisualStyleBackColor = true;
            this._button_StartDump.Click += new System.EventHandler(this._button_StartObjDump_Click);
            // 
            // _button_Clear_Dump
            // 
            this._button_Clear_Dump.Location = new System.Drawing.Point(22, 101);
            this._button_Clear_Dump.Name = "_button_Clear_Dump";
            this._button_Clear_Dump.Size = new System.Drawing.Size(75, 23);
            this._button_Clear_Dump.TabIndex = 2;
            this._button_Clear_Dump.Text = "Clear";
            this._button_Clear_Dump.UseVisualStyleBackColor = true;
            this._button_Clear_Dump.Click += new System.EventHandler(this._button_Clear_Dump_Click);
            // 
            // _button_StopDump
            // 
            this._button_StopDump.Location = new System.Drawing.Point(22, 72);
            this._button_StopDump.Name = "_button_StopDump";
            this._button_StopDump.Size = new System.Drawing.Size(75, 23);
            this._button_StopDump.TabIndex = 3;
            this._button_StopDump.Text = "Stop";
            this._button_StopDump.UseVisualStyleBackColor = true;
            this._button_StopDump.Click += new System.EventHandler(this._button_StopObjDump_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._button_StartDump);
            this.groupBox1.Controls.Add(this._checkBox_EnableDebugging);
            this.groupBox1.Controls.Add(this._button_StopDump);
            this.groupBox1.Controls.Add(this._button_Clear_Dump);
            this.groupBox1.Location = new System.Drawing.Point(3, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 140);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // _checkBox_EnableDebugging
            // 
            this._checkBox_EnableDebugging.AutoSize = true;
            this._checkBox_EnableDebugging.Location = new System.Drawing.Point(22, 20);
            this._checkBox_EnableDebugging.Name = "_checkBox_EnableDebugging";
            this._checkBox_EnableDebugging.Size = new System.Drawing.Size(123, 17);
            this._checkBox_EnableDebugging.TabIndex = 8;
            this._checkBox_EnableDebugging.Text = "Enable Debugging";
            this._checkBox_EnableDebugging.UseVisualStyleBackColor = true;
            this._checkBox_EnableDebugging.CheckedChanged += new System.EventHandler(this._checkBox_EnableDebugging_CheckedChanged);
            // 
            // listBox_Console
            // 
            this.listBox_Console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Console.FormattingEnabled = true;
            this.listBox_Console.Location = new System.Drawing.Point(0, 0);
            this.listBox_Console.Name = "listBox_Console";
            this.listBox_Console.Size = new System.Drawing.Size(450, 304);
            this.listBox_Console.TabIndex = 11;
            // 
            // _checkBox_TogglePlayers
            // 
            this._checkBox_TogglePlayers.AutoSize = true;
            this._checkBox_TogglePlayers.Checked = true;
            this._checkBox_TogglePlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBox_TogglePlayers.Location = new System.Drawing.Point(17, 80);
            this._checkBox_TogglePlayers.Name = "_checkBox_TogglePlayers";
            this._checkBox_TogglePlayers.Size = new System.Drawing.Size(61, 17);
            this._checkBox_TogglePlayers.TabIndex = 14;
            this._checkBox_TogglePlayers.Text = "Players";
            this._checkBox_TogglePlayers.UseVisualStyleBackColor = true;
            this._checkBox_TogglePlayers.CheckedChanged += new System.EventHandler(this._checkBox_TogglePlayers_CheckedChanged);
            // 
            // _checkBox_ToggleCrosshair
            // 
            this._checkBox_ToggleCrosshair.AutoSize = true;
            this._checkBox_ToggleCrosshair.Location = new System.Drawing.Point(17, 55);
            this._checkBox_ToggleCrosshair.Name = "_checkBox_ToggleCrosshair";
            this._checkBox_ToggleCrosshair.Size = new System.Drawing.Size(74, 17);
            this._checkBox_ToggleCrosshair.TabIndex = 13;
            this._checkBox_ToggleCrosshair.Text = "Crosshair";
            this._checkBox_ToggleCrosshair.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleToolCupboard
            // 
            this._checkBox_ToggleToolCupboard.AutoSize = true;
            this._checkBox_ToggleToolCupboard.Location = new System.Drawing.Point(17, 30);
            this._checkBox_ToggleToolCupboard.Name = "_checkBox_ToggleToolCupboard";
            this._checkBox_ToggleToolCupboard.Size = new System.Drawing.Size(102, 17);
            this._checkBox_ToggleToolCupboard.TabIndex = 12;
            this._checkBox_ToggleToolCupboard.Text = "Tool Cupboard";
            this._checkBox_ToggleToolCupboard.UseVisualStyleBackColor = true;
            this._checkBox_ToggleToolCupboard.CheckedChanged += new System.EventHandler(this._checkBox_ToggleToolCupboard_CheckedChanged);
            // 
            // _checkBox_ToggleArmoredDoors
            // 
            this._checkBox_ToggleArmoredDoors.AutoSize = true;
            this._checkBox_ToggleArmoredDoors.Location = new System.Drawing.Point(15, 105);
            this._checkBox_ToggleArmoredDoors.Name = "_checkBox_ToggleArmoredDoors";
            this._checkBox_ToggleArmoredDoors.Size = new System.Drawing.Size(104, 17);
            this._checkBox_ToggleArmoredDoors.TabIndex = 11;
            this._checkBox_ToggleArmoredDoors.Text = "Armored Doors";
            this._checkBox_ToggleArmoredDoors.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleGarageDoors
            // 
            this._checkBox_ToggleGarageDoors.AutoSize = true;
            this._checkBox_ToggleGarageDoors.Location = new System.Drawing.Point(15, 80);
            this._checkBox_ToggleGarageDoors.Name = "_checkBox_ToggleGarageDoors";
            this._checkBox_ToggleGarageDoors.Size = new System.Drawing.Size(97, 17);
            this._checkBox_ToggleGarageDoors.TabIndex = 10;
            this._checkBox_ToggleGarageDoors.Text = "Garage Doors";
            this._checkBox_ToggleGarageDoors.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleDoubleMetalDoors
            // 
            this._checkBox_ToggleDoubleMetalDoors.AutoSize = true;
            this._checkBox_ToggleDoubleMetalDoors.Location = new System.Drawing.Point(15, 55);
            this._checkBox_ToggleDoubleMetalDoors.Name = "_checkBox_ToggleDoubleMetalDoors";
            this._checkBox_ToggleDoubleMetalDoors.Size = new System.Drawing.Size(130, 17);
            this._checkBox_ToggleDoubleMetalDoors.TabIndex = 9;
            this._checkBox_ToggleDoubleMetalDoors.Text = "Double Metal Doors";
            this._checkBox_ToggleDoubleMetalDoors.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleMetalDoor
            // 
            this._checkBox_ToggleMetalDoor.AutoSize = true;
            this._checkBox_ToggleMetalDoor.Location = new System.Drawing.Point(15, 30);
            this._checkBox_ToggleMetalDoor.Name = "_checkBox_ToggleMetalDoor";
            this._checkBox_ToggleMetalDoor.Size = new System.Drawing.Size(121, 17);
            this._checkBox_ToggleMetalDoor.TabIndex = 8;
            this._checkBox_ToggleMetalDoor.Text = "Sheet Metal Doors";
            this._checkBox_ToggleMetalDoor.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleBarrels
            // 
            this._checkBox_ToggleBarrels.AutoSize = true;
            this._checkBox_ToggleBarrels.Location = new System.Drawing.Point(22, 105);
            this._checkBox_ToggleBarrels.Name = "_checkBox_ToggleBarrels";
            this._checkBox_ToggleBarrels.Size = new System.Drawing.Size(61, 17);
            this._checkBox_ToggleBarrels.TabIndex = 7;
            this._checkBox_ToggleBarrels.Text = "Barrels";
            this._checkBox_ToggleBarrels.UseVisualStyleBackColor = true;
            // 
            // _checkBox_ToggleLargeBox
            // 
            this._checkBox_ToggleLargeBox.AutoSize = true;
            this._checkBox_ToggleLargeBox.Location = new System.Drawing.Point(22, 80);
            this._checkBox_ToggleLargeBox.Name = "_checkBox_ToggleLargeBox";
            this._checkBox_ToggleLargeBox.Size = new System.Drawing.Size(124, 17);
            this._checkBox_ToggleLargeBox.TabIndex = 6;
            this._checkBox_ToggleLargeBox.Text = "Large Wooden Box";
            this._checkBox_ToggleLargeBox.UseVisualStyleBackColor = true;
            this._checkBox_ToggleLargeBox.CheckedChanged += new System.EventHandler(this._checkBox_ToggleLargeBox_CheckedChanged);
            // 
            // _checkBox_ToggleMilitaryCrates
            // 
            this._checkBox_ToggleMilitaryCrates.AutoSize = true;
            this._checkBox_ToggleMilitaryCrates.Checked = true;
            this._checkBox_ToggleMilitaryCrates.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBox_ToggleMilitaryCrates.Location = new System.Drawing.Point(22, 55);
            this._checkBox_ToggleMilitaryCrates.Name = "_checkBox_ToggleMilitaryCrates";
            this._checkBox_ToggleMilitaryCrates.Size = new System.Drawing.Size(99, 17);
            this._checkBox_ToggleMilitaryCrates.TabIndex = 5;
            this._checkBox_ToggleMilitaryCrates.Text = "Military Crates";
            this._checkBox_ToggleMilitaryCrates.UseVisualStyleBackColor = true;
            this._checkBox_ToggleMilitaryCrates.CheckedChanged += new System.EventHandler(this._checkBox_ToggleMilitaryCrates_CheckedChanged);
            // 
            // _checkBox_ToggleNormalCrate
            // 
            this._checkBox_ToggleNormalCrate.AutoSize = true;
            this._checkBox_ToggleNormalCrate.Checked = true;
            this._checkBox_ToggleNormalCrate.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBox_ToggleNormalCrate.Location = new System.Drawing.Point(22, 30);
            this._checkBox_ToggleNormalCrate.Name = "_checkBox_ToggleNormalCrate";
            this._checkBox_ToggleNormalCrate.Size = new System.Drawing.Size(106, 17);
            this._checkBox_ToggleNormalCrate.TabIndex = 4;
            this._checkBox_ToggleNormalCrate.Text = "Wooden Crates";
            this._checkBox_ToggleNormalCrate.UseVisualStyleBackColor = true;
            this._checkBox_ToggleNormalCrate.CheckedChanged += new System.EventHandler(this._checkBox_ToggleNormalCrate_CheckedChanged);
            // 
            // _checkBox_ToggleHemp
            // 
            this._checkBox_ToggleHemp.AutoSize = true;
            this._checkBox_ToggleHemp.Location = new System.Drawing.Point(18, 105);
            this._checkBox_ToggleHemp.Name = "_checkBox_ToggleHemp";
            this._checkBox_ToggleHemp.Size = new System.Drawing.Size(56, 17);
            this._checkBox_ToggleHemp.TabIndex = 3;
            this._checkBox_ToggleHemp.Text = "Hemp";
            this._checkBox_ToggleHemp.UseVisualStyleBackColor = true;
            this._checkBox_ToggleHemp.CheckedChanged += new System.EventHandler(this._checkBox_ToggleHemp_CheckedChanged);
            // 
            // _checkBox_ToggleStone
            // 
            this._checkBox_ToggleStone.AutoSize = true;
            this._checkBox_ToggleStone.Location = new System.Drawing.Point(18, 80);
            this._checkBox_ToggleStone.Name = "_checkBox_ToggleStone";
            this._checkBox_ToggleStone.Size = new System.Drawing.Size(56, 17);
            this._checkBox_ToggleStone.TabIndex = 2;
            this._checkBox_ToggleStone.Text = "Stone";
            this._checkBox_ToggleStone.UseVisualStyleBackColor = true;
            this._checkBox_ToggleStone.CheckedChanged += new System.EventHandler(this._checkBox_ToggleStone_CheckedChanged);
            // 
            // _checkBox_ToggleMetal
            // 
            this._checkBox_ToggleMetal.AutoSize = true;
            this._checkBox_ToggleMetal.Location = new System.Drawing.Point(18, 55);
            this._checkBox_ToggleMetal.Name = "_checkBox_ToggleMetal";
            this._checkBox_ToggleMetal.Size = new System.Drawing.Size(77, 17);
            this._checkBox_ToggleMetal.TabIndex = 1;
            this._checkBox_ToggleMetal.Text = "Metal Ore";
            this._checkBox_ToggleMetal.UseVisualStyleBackColor = true;
            this._checkBox_ToggleMetal.CheckedChanged += new System.EventHandler(this._checkBox_ToggleMetal_CheckedChanged);
            // 
            // _checkBox_ToggleSulfur
            // 
            this._checkBox_ToggleSulfur.AutoSize = true;
            this._checkBox_ToggleSulfur.Location = new System.Drawing.Point(18, 30);
            this._checkBox_ToggleSulfur.Name = "_checkBox_ToggleSulfur";
            this._checkBox_ToggleSulfur.Size = new System.Drawing.Size(79, 17);
            this._checkBox_ToggleSulfur.TabIndex = 0;
            this._checkBox_ToggleSulfur.Text = "Sulfur Ore";
            this._checkBox_ToggleSulfur.UseVisualStyleBackColor = true;
            this._checkBox_ToggleSulfur.CheckedChanged += new System.EventHandler(this._checkBox_ToggleSulfur_CheckedChanged);
            // 
            // _checkBox_ToggleAnimals
            // 
            this._checkBox_ToggleAnimals.AutoSize = true;
            this._checkBox_ToggleAnimals.Checked = true;
            this._checkBox_ToggleAnimals.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBox_ToggleAnimals.Location = new System.Drawing.Point(17, 105);
            this._checkBox_ToggleAnimals.Name = "_checkBox_ToggleAnimals";
            this._checkBox_ToggleAnimals.Size = new System.Drawing.Size(66, 17);
            this._checkBox_ToggleAnimals.TabIndex = 15;
            this._checkBox_ToggleAnimals.Text = "Animals";
            this._checkBox_ToggleAnimals.UseVisualStyleBackColor = true;
            this._checkBox_ToggleAnimals.CheckedChanged += new System.EventHandler(this._checkBox_ToggleAnimals_CheckedChanged);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this._checkBox_ToggleSulfur);
            this.radGroupBox1.Controls.Add(this._checkBox_ToggleMetal);
            this.radGroupBox1.Controls.Add(this._checkBox_ToggleStone);
            this.radGroupBox1.Controls.Add(this._checkBox_ToggleHemp);
            this.radGroupBox1.HeaderText = "Resources";
            this.radGroupBox1.Location = new System.Drawing.Point(161, 21);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(121, 137);
            this.radGroupBox1.TabIndex = 13;
            this.radGroupBox1.Text = "Resources";
            this.radGroupBox1.ThemeName = "Office2010Silver";
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this._checkBox_ToggleNormalCrate);
            this.radGroupBox2.Controls.Add(this._checkBox_ToggleMilitaryCrates);
            this.radGroupBox2.Controls.Add(this._checkBox_ToggleLargeBox);
            this.radGroupBox2.Controls.Add(this._checkBox_ToggleBarrels);
            this.radGroupBox2.HeaderText = "Loot Storage";
            this.radGroupBox2.Location = new System.Drawing.Point(288, 21);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Size = new System.Drawing.Size(158, 137);
            this.radGroupBox2.TabIndex = 14;
            this.radGroupBox2.Text = "Loot Storage";
            this.radGroupBox2.ThemeName = "Office2010Silver";
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox3.Controls.Add(this._checkBox_ToggleMetalDoor);
            this.radGroupBox3.Controls.Add(this._checkBox_ToggleDoubleMetalDoors);
            this.radGroupBox3.Controls.Add(this._checkBox_ToggleGarageDoors);
            this.radGroupBox3.Controls.Add(this._checkBox_ToggleArmoredDoors);
            this.radGroupBox3.HeaderText = "Doors";
            this.radGroupBox3.Location = new System.Drawing.Point(3, 163);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Size = new System.Drawing.Size(163, 137);
            this.radGroupBox3.TabIndex = 15;
            this.radGroupBox3.Text = "Doors";
            this.radGroupBox3.ThemeName = "Office2010Silver";
            // 
            // radGroupBox4
            // 
            this.radGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox4.Controls.Add(this._checkBox_ToggleAnimals);
            this.radGroupBox4.Controls.Add(this._checkBox_ToggleToolCupboard);
            this.radGroupBox4.Controls.Add(this._checkBox_TogglePlayers);
            this.radGroupBox4.Controls.Add(this._checkBox_ToggleCrosshair);
            this.radGroupBox4.HeaderText = "Misc.";
            this.radGroupBox4.Location = new System.Drawing.Point(172, 164);
            this.radGroupBox4.Name = "radGroupBox4";
            this.radGroupBox4.Size = new System.Drawing.Size(151, 137);
            this.radGroupBox4.TabIndex = 16;
            this.radGroupBox4.Text = "Misc.";
            this.radGroupBox4.ThemeName = "Office2010Silver";
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.radPageViewPage1);
            this.radPageView1.Controls.Add(this.radPageViewPage2);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Location = new System.Drawing.Point(0, 0);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.radPageViewPage1;
            this.radPageView1.Size = new System.Drawing.Size(474, 356);
            this.radPageView1.TabIndex = 17;
            this.radPageView1.Text = "radPageView1";
            this.radPageView1.ThemeName = "Office2010Silver";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.groupBox1);
            this.radPageViewPage1.Controls.Add(this.radGroupBox4);
            this.radPageViewPage1.Controls.Add(this.radGroupBox1);
            this.radPageViewPage1.Controls.Add(this.radGroupBox3);
            this.radPageViewPage1.Controls.Add(this.radGroupBox2);
            this.radPageViewPage1.ItemSize = new System.Drawing.SizeF(62F, 30F);
            this.radPageViewPage1.Location = new System.Drawing.Point(12, 40);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(450, 304);
            this.radPageViewPage1.Text = "Alarms";
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.Controls.Add(this.listBox_Console);
            this.radPageViewPage2.ItemSize = new System.Drawing.SizeF(84F, 30F);
            this.radPageViewPage2.Location = new System.Drawing.Point(12, 40);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(450, 304);
            this.radPageViewPage2.Text = "Debugging";
            // 
            // ConsoleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 356);
            this.Controls.Add(this.radPageView1);
            this.Name = "ConsoleApp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ryuzaki PC Alarm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleApp_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            this.radGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox4)).EndInit();
            this.radGroupBox4.ResumeLayout(false);
            this.radGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            this.radPageViewPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox _checkBox_ToggleSulfur;
        private System.Windows.Forms.CheckBox _checkBox_ToggleMetal;
        private System.Windows.Forms.CheckBox _checkBox_ToggleStone;
        private System.Windows.Forms.CheckBox _checkBox_ToggleNormalCrate;
        private System.Windows.Forms.CheckBox _checkBox_ToggleHemp;
        private System.Windows.Forms.CheckBox _checkBox_ToggleMilitaryCrates;
        private System.Windows.Forms.CheckBox _checkBox_ToggleLargeBox;
        private System.Windows.Forms.CheckBox _checkBox_ToggleBarrels;
        private System.Windows.Forms.CheckBox _checkBox_ToggleArmoredDoors;
        private System.Windows.Forms.CheckBox _checkBox_ToggleGarageDoors;
        private System.Windows.Forms.CheckBox _checkBox_ToggleDoubleMetalDoors;
        private System.Windows.Forms.CheckBox _checkBox_ToggleMetalDoor;
        private System.Windows.Forms.CheckBox _checkBox_ToggleToolCupboard;
        private System.Windows.Forms.CheckBox _checkBox_ToggleCrosshair;
        private System.Windows.Forms.ColorDialog sulfurColorDialog;
        private System.Windows.Forms.CheckBox _checkBox_TogglePlayers;
        private System.Windows.Forms.CheckBox _checkBox_ToggleAnimals;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.Themes.Office2010SilverTheme office2010SilverTheme1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox4;
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
    }
}