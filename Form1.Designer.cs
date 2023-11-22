namespace kmumed_pk_grouper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.loggingDelayNum = new System.Windows.Forms.NumericUpDown();
            this.Label_DBFilePath = new System.Windows.Forms.Label();
            this.dbFilePathTB = new System.Windows.Forms.TextBox();
            this.radioButton_mode1 = new System.Windows.Forms.RadioButton();
            this.Label_Modes = new System.Windows.Forms.Label();
            this.radioButton_mode2 = new System.Windows.Forms.RadioButton();
            this.radioButton_mode3 = new System.Windows.Forms.RadioButton();
            this.teamCountLabel = new System.Windows.Forms.Label();
            this.teamCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.totalPopulationNumeric = new System.Windows.Forms.NumericUpDown();
            this.TotalPopulationLabel = new System.Windows.Forms.Label();
            this.totalPopulationUnitLabel = new System.Windows.Forms.Label();
            this.populationPerTeamLabel1 = new System.Windows.Forms.Label();
            this.populationPerTeamNum1 = new System.Windows.Forms.NumericUpDown();
            this.ModePanel = new System.Windows.Forms.Panel();
            this.considerSurgicalCopyLabel = new System.Windows.Forms.Label();
            this.considerSurgicalCopyPanel = new System.Windows.Forms.Panel();
            this.considerSurgicalCopyNoRB = new System.Windows.Forms.RadioButton();
            this.considerSurgicalCopyYesRB = new System.Windows.Forms.RadioButton();
            this.populationPerTeamLabel2 = new System.Windows.Forms.Label();
            this.populationPerTeamNum2 = new System.Windows.Forms.NumericUpDown();
            this.genderRatioPanel = new System.Windows.Forms.Panel();
            this.genderRatio_AllRandRB = new System.Windows.Forms.RadioButton();
            this.genderRatio_NoHalfAndSingle = new System.Windows.Forms.RadioButton();
            this.genderRatio_NoHalfOrMoreRB = new System.Windows.Forms.RadioButton();
            this.genderRatio_UniformRB = new System.Windows.Forms.RadioButton();
            this.genderRatio_NoSingleRB = new System.Windows.Forms.RadioButton();
            this.genderRatioLabel = new System.Windows.Forms.Label();
            this.loggingLabel = new System.Windows.Forms.Label();
            this.loggingOnOffPanel = new System.Windows.Forms.Panel();
            this.loggingOffRB = new System.Windows.Forms.RadioButton();
            this.loggingOnRB = new System.Windows.Forms.RadioButton();
            this.StartButton = new System.Windows.Forms.Button();
            this.settingPanel = new System.Windows.Forms.Panel();
            this.whenNoSinglePanel = new System.Windows.Forms.Panel();
            this.whenNoSingleNoMoreThanHalfRB = new System.Windows.Forms.RadioButton();
            this.whenNoSingleRandomRB = new System.Windows.Forms.RadioButton();
            this.whenNoSingleLabel = new System.Windows.Forms.Label();
            this.logSettingsPanel = new System.Windows.Forms.Panel();
            this.loggingUntilCycleLabel = new System.Windows.Forms.Label();
            this.loggingDelayLabel = new System.Windows.Forms.Label();
            this.loggingUntilCycleNum = new System.Windows.Forms.NumericUpDown();
            this.logPanel = new System.Windows.Forms.Panel();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.loggingDelayNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teamCountNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalPopulationNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationPerTeamNum1)).BeginInit();
            this.ModePanel.SuspendLayout();
            this.considerSurgicalCopyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.populationPerTeamNum2)).BeginInit();
            this.genderRatioPanel.SuspendLayout();
            this.loggingOnOffPanel.SuspendLayout();
            this.settingPanel.SuspendLayout();
            this.whenNoSinglePanel.SuspendLayout();
            this.logSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loggingUntilCycleNum)).BeginInit();
            this.logPanel.SuspendLayout();
            this.logGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loggingDelayNum
            // 
            resources.ApplyResources(this.loggingDelayNum, "loggingDelayNum");
            this.loggingDelayNum.DecimalPlaces = 1;
            this.loggingDelayNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.loggingDelayNum.Name = "loggingDelayNum";
            this.loggingDelayNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_DBFilePath
            // 
            resources.ApplyResources(this.Label_DBFilePath, "Label_DBFilePath");
            this.Label_DBFilePath.Name = "Label_DBFilePath";
            // 
            // dbFilePathTB
            // 
            resources.ApplyResources(this.dbFilePathTB, "dbFilePathTB");
            this.dbFilePathTB.Name = "dbFilePathTB";
            this.dbFilePathTB.ReadOnly = true;
            this.dbFilePathTB.Click += new System.EventHandler(this.dbFilePath_Click);
            this.dbFilePathTB.TextChanged += new System.EventHandler(this.dbFilePathTB_TextChanged);
            // 
            // radioButton_mode1
            // 
            resources.ApplyResources(this.radioButton_mode1, "radioButton_mode1");
            this.radioButton_mode1.Name = "radioButton_mode1";
            this.radioButton_mode1.TabStop = true;
            this.radioButton_mode1.Tag = "31";
            this.radioButton_mode1.UseVisualStyleBackColor = true;
            this.radioButton_mode1.CheckedChanged += new System.EventHandler(this.ModeSelected);
            // 
            // Label_Modes
            // 
            resources.ApplyResources(this.Label_Modes, "Label_Modes");
            this.Label_Modes.Name = "Label_Modes";
            // 
            // radioButton_mode2
            // 
            resources.ApplyResources(this.radioButton_mode2, "radioButton_mode2");
            this.radioButton_mode2.Name = "radioButton_mode2";
            this.radioButton_mode2.TabStop = true;
            this.radioButton_mode2.Tag = "32";
            this.radioButton_mode2.UseVisualStyleBackColor = true;
            this.radioButton_mode2.CheckedChanged += new System.EventHandler(this.ModeSelected);
            // 
            // radioButton_mode3
            // 
            resources.ApplyResources(this.radioButton_mode3, "radioButton_mode3");
            this.radioButton_mode3.Name = "radioButton_mode3";
            this.radioButton_mode3.TabStop = true;
            this.radioButton_mode3.Tag = "41";
            this.radioButton_mode3.UseVisualStyleBackColor = true;
            this.radioButton_mode3.CheckedChanged += new System.EventHandler(this.ModeSelected);
            // 
            // teamCountLabel
            // 
            resources.ApplyResources(this.teamCountLabel, "teamCountLabel");
            this.teamCountLabel.Name = "teamCountLabel";
            // 
            // teamCountNumeric
            // 
            resources.ApplyResources(this.teamCountNumeric, "teamCountNumeric");
            this.teamCountNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.teamCountNumeric.Name = "teamCountNumeric";
            this.teamCountNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.teamCountNumeric.ValueChanged += new System.EventHandler(this.totalPopulationNumeric_ValueChanged);
            // 
            // totalPopulationNumeric
            // 
            resources.ApplyResources(this.totalPopulationNumeric, "totalPopulationNumeric");
            this.totalPopulationNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.totalPopulationNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.totalPopulationNumeric.Name = "totalPopulationNumeric";
            this.totalPopulationNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.totalPopulationNumeric.ValueChanged += new System.EventHandler(this.totalPopulationNumeric_ValueChanged);
            // 
            // TotalPopulationLabel
            // 
            resources.ApplyResources(this.TotalPopulationLabel, "TotalPopulationLabel");
            this.TotalPopulationLabel.Name = "TotalPopulationLabel";
            // 
            // totalPopulationUnitLabel
            // 
            resources.ApplyResources(this.totalPopulationUnitLabel, "totalPopulationUnitLabel");
            this.totalPopulationUnitLabel.Name = "totalPopulationUnitLabel";
            // 
            // populationPerTeamLabel1
            // 
            resources.ApplyResources(this.populationPerTeamLabel1, "populationPerTeamLabel1");
            this.populationPerTeamLabel1.Name = "populationPerTeamLabel1";
            // 
            // populationPerTeamNum1
            // 
            resources.ApplyResources(this.populationPerTeamNum1, "populationPerTeamNum1");
            this.populationPerTeamNum1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.populationPerTeamNum1.Name = "populationPerTeamNum1";
            this.populationPerTeamNum1.ReadOnly = true;
            this.populationPerTeamNum1.TabStop = false;
            this.populationPerTeamNum1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ModePanel
            // 
            resources.ApplyResources(this.ModePanel, "ModePanel");
            this.ModePanel.Controls.Add(this.radioButton_mode3);
            this.ModePanel.Controls.Add(this.radioButton_mode2);
            this.ModePanel.Controls.Add(this.radioButton_mode1);
            this.ModePanel.Name = "ModePanel";
            // 
            // considerSurgicalCopyLabel
            // 
            resources.ApplyResources(this.considerSurgicalCopyLabel, "considerSurgicalCopyLabel");
            this.considerSurgicalCopyLabel.Name = "considerSurgicalCopyLabel";
            // 
            // considerSurgicalCopyPanel
            // 
            resources.ApplyResources(this.considerSurgicalCopyPanel, "considerSurgicalCopyPanel");
            this.considerSurgicalCopyPanel.Controls.Add(this.considerSurgicalCopyNoRB);
            this.considerSurgicalCopyPanel.Controls.Add(this.considerSurgicalCopyYesRB);
            this.considerSurgicalCopyPanel.Name = "considerSurgicalCopyPanel";
            // 
            // considerSurgicalCopyNoRB
            // 
            resources.ApplyResources(this.considerSurgicalCopyNoRB, "considerSurgicalCopyNoRB");
            this.considerSurgicalCopyNoRB.Checked = true;
            this.considerSurgicalCopyNoRB.Name = "considerSurgicalCopyNoRB";
            this.considerSurgicalCopyNoRB.TabStop = true;
            this.considerSurgicalCopyNoRB.Tag = "n";
            this.considerSurgicalCopyNoRB.UseVisualStyleBackColor = true;
            this.considerSurgicalCopyNoRB.CheckedChanged += new System.EventHandler(this.consigderSurgicalCopySelected);
            // 
            // considerSurgicalCopyYesRB
            // 
            resources.ApplyResources(this.considerSurgicalCopyYesRB, "considerSurgicalCopyYesRB");
            this.considerSurgicalCopyYesRB.Name = "considerSurgicalCopyYesRB";
            this.considerSurgicalCopyYesRB.Tag = "y";
            this.considerSurgicalCopyYesRB.UseVisualStyleBackColor = true;
            this.considerSurgicalCopyYesRB.CheckedChanged += new System.EventHandler(this.consigderSurgicalCopySelected);
            // 
            // populationPerTeamLabel2
            // 
            resources.ApplyResources(this.populationPerTeamLabel2, "populationPerTeamLabel2");
            this.populationPerTeamLabel2.Name = "populationPerTeamLabel2";
            // 
            // populationPerTeamNum2
            // 
            resources.ApplyResources(this.populationPerTeamNum2, "populationPerTeamNum2");
            this.populationPerTeamNum2.Name = "populationPerTeamNum2";
            this.populationPerTeamNum2.ReadOnly = true;
            this.populationPerTeamNum2.TabStop = false;
            // 
            // genderRatioPanel
            // 
            resources.ApplyResources(this.genderRatioPanel, "genderRatioPanel");
            this.genderRatioPanel.Controls.Add(this.genderRatio_AllRandRB);
            this.genderRatioPanel.Controls.Add(this.genderRatio_NoHalfAndSingle);
            this.genderRatioPanel.Controls.Add(this.genderRatio_NoHalfOrMoreRB);
            this.genderRatioPanel.Controls.Add(this.genderRatio_UniformRB);
            this.genderRatioPanel.Controls.Add(this.genderRatio_NoSingleRB);
            this.genderRatioPanel.Name = "genderRatioPanel";
            // 
            // genderRatio_AllRandRB
            // 
            resources.ApplyResources(this.genderRatio_AllRandRB, "genderRatio_AllRandRB");
            this.genderRatio_AllRandRB.Checked = true;
            this.genderRatio_AllRandRB.Name = "genderRatio_AllRandRB";
            this.genderRatio_AllRandRB.TabStop = true;
            this.genderRatio_AllRandRB.Tag = "0";
            this.genderRatio_AllRandRB.UseVisualStyleBackColor = true;
            this.genderRatio_AllRandRB.CheckedChanged += new System.EventHandler(this.GenderRatioSelected);
            // 
            // genderRatio_NoHalfAndSingle
            // 
            resources.ApplyResources(this.genderRatio_NoHalfAndSingle, "genderRatio_NoHalfAndSingle");
            this.genderRatio_NoHalfAndSingle.Name = "genderRatio_NoHalfAndSingle";
            this.genderRatio_NoHalfAndSingle.Tag = "4";
            this.genderRatio_NoHalfAndSingle.UseVisualStyleBackColor = true;
            this.genderRatio_NoHalfAndSingle.CheckedChanged += new System.EventHandler(this.GenderRatioSelected);
            // 
            // genderRatio_NoHalfOrMoreRB
            // 
            resources.ApplyResources(this.genderRatio_NoHalfOrMoreRB, "genderRatio_NoHalfOrMoreRB");
            this.genderRatio_NoHalfOrMoreRB.Name = "genderRatio_NoHalfOrMoreRB";
            this.genderRatio_NoHalfOrMoreRB.Tag = "2";
            this.genderRatio_NoHalfOrMoreRB.UseVisualStyleBackColor = true;
            this.genderRatio_NoHalfOrMoreRB.CheckedChanged += new System.EventHandler(this.GenderRatioSelected);
            // 
            // genderRatio_UniformRB
            // 
            resources.ApplyResources(this.genderRatio_UniformRB, "genderRatio_UniformRB");
            this.genderRatio_UniformRB.Name = "genderRatio_UniformRB";
            this.genderRatio_UniformRB.Tag = "1";
            this.genderRatio_UniformRB.UseVisualStyleBackColor = true;
            this.genderRatio_UniformRB.CheckedChanged += new System.EventHandler(this.GenderRatioSelected);
            // 
            // genderRatio_NoSingleRB
            // 
            resources.ApplyResources(this.genderRatio_NoSingleRB, "genderRatio_NoSingleRB");
            this.genderRatio_NoSingleRB.Name = "genderRatio_NoSingleRB";
            this.genderRatio_NoSingleRB.Tag = "3";
            this.genderRatio_NoSingleRB.UseVisualStyleBackColor = true;
            this.genderRatio_NoSingleRB.CheckedChanged += new System.EventHandler(this.GenderRatioSelected);
            // 
            // genderRatioLabel
            // 
            resources.ApplyResources(this.genderRatioLabel, "genderRatioLabel");
            this.genderRatioLabel.Name = "genderRatioLabel";
            // 
            // loggingLabel
            // 
            resources.ApplyResources(this.loggingLabel, "loggingLabel");
            this.loggingLabel.Name = "loggingLabel";
            // 
            // loggingOnOffPanel
            // 
            resources.ApplyResources(this.loggingOnOffPanel, "loggingOnOffPanel");
            this.loggingOnOffPanel.Controls.Add(this.loggingOffRB);
            this.loggingOnOffPanel.Controls.Add(this.loggingOnRB);
            this.loggingOnOffPanel.Name = "loggingOnOffPanel";
            // 
            // loggingOffRB
            // 
            resources.ApplyResources(this.loggingOffRB, "loggingOffRB");
            this.loggingOffRB.Name = "loggingOffRB";
            this.loggingOffRB.Tag = "n";
            this.loggingOffRB.UseVisualStyleBackColor = true;
            this.loggingOffRB.CheckedChanged += new System.EventHandler(this.LoggingSelected);
            // 
            // loggingOnRB
            // 
            resources.ApplyResources(this.loggingOnRB, "loggingOnRB");
            this.loggingOnRB.Checked = true;
            this.loggingOnRB.Name = "loggingOnRB";
            this.loggingOnRB.TabStop = true;
            this.loggingOnRB.Tag = "y";
            this.loggingOnRB.UseVisualStyleBackColor = true;
            this.loggingOnRB.CheckedChanged += new System.EventHandler(this.LoggingSelected);
            // 
            // StartButton
            // 
            resources.ApplyResources(this.StartButton, "StartButton");
            this.StartButton.Name = "StartButton";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // settingPanel
            // 
            resources.ApplyResources(this.settingPanel, "settingPanel");
            this.settingPanel.Controls.Add(this.whenNoSinglePanel);
            this.settingPanel.Controls.Add(this.logSettingsPanel);
            this.settingPanel.Controls.Add(this.StartButton);
            this.settingPanel.Controls.Add(this.loggingLabel);
            this.settingPanel.Controls.Add(this.loggingOnOffPanel);
            this.settingPanel.Name = "settingPanel";
            // 
            // whenNoSinglePanel
            // 
            resources.ApplyResources(this.whenNoSinglePanel, "whenNoSinglePanel");
            this.whenNoSinglePanel.Controls.Add(this.whenNoSingleNoMoreThanHalfRB);
            this.whenNoSinglePanel.Controls.Add(this.whenNoSingleRandomRB);
            this.whenNoSinglePanel.Controls.Add(this.whenNoSingleLabel);
            this.whenNoSinglePanel.Name = "whenNoSinglePanel";
            // 
            // whenNoSingleNoMoreThanHalfRB
            // 
            resources.ApplyResources(this.whenNoSingleNoMoreThanHalfRB, "whenNoSingleNoMoreThanHalfRB");
            this.whenNoSingleNoMoreThanHalfRB.Name = "whenNoSingleNoMoreThanHalfRB";
            this.whenNoSingleNoMoreThanHalfRB.Tag = "n";
            this.whenNoSingleNoMoreThanHalfRB.UseVisualStyleBackColor = true;
            // 
            // whenNoSingleRandomRB
            // 
            resources.ApplyResources(this.whenNoSingleRandomRB, "whenNoSingleRandomRB");
            this.whenNoSingleRandomRB.Checked = true;
            this.whenNoSingleRandomRB.Name = "whenNoSingleRandomRB";
            this.whenNoSingleRandomRB.TabStop = true;
            this.whenNoSingleRandomRB.Tag = "y";
            this.whenNoSingleRandomRB.UseVisualStyleBackColor = true;
            this.whenNoSingleRandomRB.CheckedChanged += new System.EventHandler(this.whenNoSingleRandomRB_CheckedChanged);
            // 
            // whenNoSingleLabel
            // 
            resources.ApplyResources(this.whenNoSingleLabel, "whenNoSingleLabel");
            this.whenNoSingleLabel.Name = "whenNoSingleLabel";
            // 
            // logSettingsPanel
            // 
            resources.ApplyResources(this.logSettingsPanel, "logSettingsPanel");
            this.logSettingsPanel.Controls.Add(this.loggingUntilCycleLabel);
            this.logSettingsPanel.Controls.Add(this.loggingDelayLabel);
            this.logSettingsPanel.Controls.Add(this.loggingDelayNum);
            this.logSettingsPanel.Controls.Add(this.loggingUntilCycleNum);
            this.logSettingsPanel.Name = "logSettingsPanel";
            // 
            // loggingUntilCycleLabel
            // 
            resources.ApplyResources(this.loggingUntilCycleLabel, "loggingUntilCycleLabel");
            this.loggingUntilCycleLabel.Name = "loggingUntilCycleLabel";
            // 
            // loggingDelayLabel
            // 
            resources.ApplyResources(this.loggingDelayLabel, "loggingDelayLabel");
            this.loggingDelayLabel.Name = "loggingDelayLabel";
            // 
            // loggingUntilCycleNum
            // 
            resources.ApplyResources(this.loggingUntilCycleNum, "loggingUntilCycleNum");
            this.loggingUntilCycleNum.Name = "loggingUntilCycleNum";
            this.loggingUntilCycleNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // logPanel
            // 
            resources.ApplyResources(this.logPanel, "logPanel");
            this.logPanel.Controls.Add(this.logGroupBox);
            this.logPanel.Name = "logPanel";
            // 
            // logGroupBox
            // 
            resources.ApplyResources(this.logGroupBox, "logGroupBox");
            this.logGroupBox.Controls.Add(this.logRichTextBox);
            this.logGroupBox.Name = "logGroupBox";
            this.logGroupBox.TabStop = false;
            // 
            // logRichTextBox
            // 
            resources.ApplyResources(this.logRichTextBox, "logRichTextBox");
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logPanel);
            this.Controls.Add(this.genderRatioPanel);
            this.Controls.Add(this.genderRatioLabel);
            this.Controls.Add(this.considerSurgicalCopyPanel);
            this.Controls.Add(this.considerSurgicalCopyLabel);
            this.Controls.Add(this.populationPerTeamNum2);
            this.Controls.Add(this.populationPerTeamNum1);
            this.Controls.Add(this.populationPerTeamLabel2);
            this.Controls.Add(this.totalPopulationNumeric);
            this.Controls.Add(this.populationPerTeamLabel1);
            this.Controls.Add(this.totalPopulationUnitLabel);
            this.Controls.Add(this.TotalPopulationLabel);
            this.Controls.Add(this.teamCountNumeric);
            this.Controls.Add(this.teamCountLabel);
            this.Controls.Add(this.Label_Modes);
            this.Controls.Add(this.Label_DBFilePath);
            this.Controls.Add(this.dbFilePathTB);
            this.Controls.Add(this.ModePanel);
            this.Controls.Add(this.settingPanel);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.loggingDelayNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teamCountNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalPopulationNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationPerTeamNum1)).EndInit();
            this.ModePanel.ResumeLayout(false);
            this.ModePanel.PerformLayout();
            this.considerSurgicalCopyPanel.ResumeLayout(false);
            this.considerSurgicalCopyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.populationPerTeamNum2)).EndInit();
            this.genderRatioPanel.ResumeLayout(false);
            this.genderRatioPanel.PerformLayout();
            this.loggingOnOffPanel.ResumeLayout(false);
            this.loggingOnOffPanel.PerformLayout();
            this.settingPanel.ResumeLayout(false);
            this.settingPanel.PerformLayout();
            this.whenNoSinglePanel.ResumeLayout(false);
            this.whenNoSinglePanel.PerformLayout();
            this.logSettingsPanel.ResumeLayout(false);
            this.logSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loggingUntilCycleNum)).EndInit();
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.logGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label Label_DBFilePath;
        private TextBox dbFilePathTB;
        private RadioButton radioButton_mode1;
        private Label Label_Modes;
        private RadioButton radioButton_mode2;
        private RadioButton radioButton_mode3;
        private Label teamCountLabel;
        private NumericUpDown teamCountNumeric;
        private NumericUpDown totalPopulationNumeric;
        private Label TotalPopulationLabel;
        private Label totalPopulationUnitLabel;
        private Label populationPerTeamLabel1;
        private NumericUpDown populationPerTeamNum1;
        private Panel ModePanel;
        private Label considerSurgicalCopyLabel;
        private Panel considerSurgicalCopyPanel;
        private RadioButton considerSurgicalCopyNoRB;
        private RadioButton considerSurgicalCopyYesRB;
        private Label populationPerTeamLabel2;
        private NumericUpDown populationPerTeamNum2;
        private Panel genderRatioPanel;
        private RadioButton genderRatio_AllRandRB;
        private RadioButton genderRatio_NoSingleRB;
        private Label genderRatioLabel;
        private RadioButton genderRatio_NoHalfOrMoreRB;
        private RadioButton genderRatio_UniformRB;
        private RadioButton genderRatio_NoHalfAndSingle;
        private Label loggingLabel;
        private Panel loggingOnOffPanel;
        private RadioButton loggingOffRB;
        private RadioButton loggingOnRB;
        private Button StartButton;
        private Panel settingPanel;
        private Panel logPanel;
        private GroupBox logGroupBox;
        private RichTextBox logRichTextBox;
        private Panel logSettingsPanel;
        private Label loggingUntilCycleLabel;
        private Label loggingDelayLabel;
        private NumericUpDown loggingUntilCycleNum;
        private NumericUpDown loggingDelayNum;
        private Label whenNoSingleLabel;
        private Panel whenNoSinglePanel;
        private RadioButton whenNoSingleNoMoreThanHalfRB;
        private RadioButton whenNoSingleRandomRB;
    }
}