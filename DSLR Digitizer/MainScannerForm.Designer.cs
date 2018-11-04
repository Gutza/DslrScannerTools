namespace DSLR_Digitizer
{
    partial class MainScannerForm
    {
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
            this.components = new System.ComponentModel.Container();
            this.navigationGroup = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconRight = new DSLR_Digitizer.ScannerIcon();
            this.iconDown = new DSLR_Digitizer.ScannerIcon();
            this.iconLeft = new DSLR_Digitizer.ScannerIcon();
            this.iconUp = new DSLR_Digitizer.ScannerIcon();
            this.iconStop = new DSLR_Digitizer.ScannerIcon();
            this.commPortGroupbox = new System.Windows.Forms.GroupBox();
            this.btnRefreshCommPortList = new System.Windows.Forms.Button();
            this.commPortCombo = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.GroupBox();
            this.tbScanLog = new System.Windows.Forms.TextBox();
            this.tbMessageLog = new System.Windows.Forms.TextBox();
            this.sweepSettingsGroup = new System.Windows.Forms.GroupBox();
            this.cbSweepSettings = new System.Windows.Forms.ComboBox();
            this.lblLoadSweep = new System.Windows.Forms.Label();
            this.btnSaveSweepSettings = new System.Windows.Forms.Button();
            this.btnSetSweepHeight = new System.Windows.Forms.Button();
            this.btnSetSweepWidth = new System.Windows.Forms.Button();
            this.btnSetSweepStart = new System.Windows.Forms.Button();
            this.btnSetDslrWH = new System.Windows.Forms.Button();
            this.btnSetDslrHeight = new System.Windows.Forms.Button();
            this.btnSetDslrWidth = new System.Windows.Forms.Button();
            this.btnSetAnchor = new System.Windows.Forms.Button();
            this.shootingGroup = new System.Windows.Forms.GroupBox();
            this.pbHelpSaveLocation = new System.Windows.Forms.PictureBox();
            this.lblShootLocation = new System.Windows.Forms.Label();
            this.btnShootLocation = new System.Windows.Forms.Button();
            this.tbShootLocation = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.navigationGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).BeginInit();
            this.commPortGroupbox.SuspendLayout();
            this.logBox.SuspendLayout();
            this.sweepSettingsGroup.SuspendLayout();
            this.shootingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelpSaveLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // navigationGroup
            // 
            this.navigationGroup.Controls.Add(this.panel1);
            this.navigationGroup.Enabled = false;
            this.navigationGroup.Location = new System.Drawing.Point(12, 66);
            this.navigationGroup.Name = "navigationGroup";
            this.navigationGroup.Size = new System.Drawing.Size(332, 343);
            this.navigationGroup.TabIndex = 2;
            this.navigationGroup.TabStop = false;
            this.navigationGroup.Text = "Navigation";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iconRight);
            this.panel1.Controls.Add(this.iconDown);
            this.panel1.Controls.Add(this.iconLeft);
            this.panel1.Controls.Add(this.iconUp);
            this.panel1.Controls.Add(this.iconStop);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 316);
            this.panel1.TabIndex = 2;
            // 
            // iconRight
            // 
            this.iconRight.IconState = DSLR_Digitizer.ScannerIcon.IconStates.Disabled;
            this.iconRight.ImageActive = global::DSLR_Digitizer.Properties.Resources.hand_drawn_right_red;
            this.iconRight.ImageDefault = global::DSLR_Digitizer.Properties.Resources.hand_drawn_right_empty;
            this.iconRight.ImageDisabled = global::DSLR_Digitizer.Properties.Resources.hand_drawn_right_disabled;
            this.iconRight.ImageHover = global::DSLR_Digitizer.Properties.Resources.hand_drawn_right_white;
            this.iconRight.InitialImage = null;
            this.iconRight.Location = new System.Drawing.Point(215, 109);
            this.iconRight.Name = "iconRight";
            this.iconRight.Size = new System.Drawing.Size(100, 100);
            this.iconRight.TabIndex = 5;
            this.iconRight.TabStop = false;
            this.iconRight.Click += new System.EventHandler(this.iconRight_Click);
            // 
            // iconDown
            // 
            this.iconDown.IconState = DSLR_Digitizer.ScannerIcon.IconStates.Disabled;
            this.iconDown.ImageActive = global::DSLR_Digitizer.Properties.Resources.hand_drawn_down_red;
            this.iconDown.ImageDefault = global::DSLR_Digitizer.Properties.Resources.hand_drawn_down_empty;
            this.iconDown.ImageDisabled = global::DSLR_Digitizer.Properties.Resources.hand_drawn_down_disabled;
            this.iconDown.ImageHover = global::DSLR_Digitizer.Properties.Resources.hand_drawn_down_white;
            this.iconDown.InitialImage = null;
            this.iconDown.Location = new System.Drawing.Point(109, 215);
            this.iconDown.Name = "iconDown";
            this.iconDown.Size = new System.Drawing.Size(100, 100);
            this.iconDown.TabIndex = 4;
            this.iconDown.TabStop = false;
            this.iconDown.Click += new System.EventHandler(this.iconDown_Click);
            // 
            // iconLeft
            // 
            this.iconLeft.IconState = DSLR_Digitizer.ScannerIcon.IconStates.Disabled;
            this.iconLeft.ImageActive = global::DSLR_Digitizer.Properties.Resources.hand_drawn_left_red;
            this.iconLeft.ImageDefault = global::DSLR_Digitizer.Properties.Resources.hand_drawn_left_empty;
            this.iconLeft.ImageDisabled = global::DSLR_Digitizer.Properties.Resources.hand_drawn_left_disabled;
            this.iconLeft.ImageHover = global::DSLR_Digitizer.Properties.Resources.hand_drawn_left_white;
            this.iconLeft.InitialImage = null;
            this.iconLeft.Location = new System.Drawing.Point(0, 109);
            this.iconLeft.Name = "iconLeft";
            this.iconLeft.Size = new System.Drawing.Size(100, 100);
            this.iconLeft.TabIndex = 3;
            this.iconLeft.TabStop = false;
            this.iconLeft.Click += new System.EventHandler(this.iconLeft_Click);
            // 
            // iconUp
            // 
            this.iconUp.IconState = DSLR_Digitizer.ScannerIcon.IconStates.Disabled;
            this.iconUp.ImageActive = global::DSLR_Digitizer.Properties.Resources.hand_drawn_up_red;
            this.iconUp.ImageDefault = global::DSLR_Digitizer.Properties.Resources.hand_drawn_up_empty;
            this.iconUp.ImageDisabled = global::DSLR_Digitizer.Properties.Resources.hand_drawn_up_disabled;
            this.iconUp.ImageHover = global::DSLR_Digitizer.Properties.Resources.hand_drawn_up_white;
            this.iconUp.InitialImage = null;
            this.iconUp.Location = new System.Drawing.Point(109, 3);
            this.iconUp.Name = "iconUp";
            this.iconUp.Size = new System.Drawing.Size(100, 100);
            this.iconUp.TabIndex = 2;
            this.iconUp.TabStop = false;
            this.iconUp.Click += new System.EventHandler(this.iconUp_Click);
            // 
            // iconStop
            // 
            this.iconStop.IconState = DSLR_Digitizer.ScannerIcon.IconStates.Disabled;
            this.iconStop.ImageActive = global::DSLR_Digitizer.Properties.Resources.hand_drawn_circle_green;
            this.iconStop.ImageDefault = global::DSLR_Digitizer.Properties.Resources.hand_drawn_circle_empty;
            this.iconStop.ImageDisabled = global::DSLR_Digitizer.Properties.Resources.hand_drawn_circle_disabled;
            this.iconStop.ImageHover = global::DSLR_Digitizer.Properties.Resources.hand_drawn_circle_white;
            this.iconStop.InitialImage = null;
            this.iconStop.Location = new System.Drawing.Point(109, 109);
            this.iconStop.Name = "iconStop";
            this.iconStop.Size = new System.Drawing.Size(100, 100);
            this.iconStop.TabIndex = 1;
            this.iconStop.TabStop = false;
            this.iconStop.Click += new System.EventHandler(this.iconStop_Click);
            // 
            // commPortGroupbox
            // 
            this.commPortGroupbox.Controls.Add(this.btnRefreshCommPortList);
            this.commPortGroupbox.Controls.Add(this.commPortCombo);
            this.commPortGroupbox.Controls.Add(this.lblComPort);
            this.commPortGroupbox.Location = new System.Drawing.Point(12, 12);
            this.commPortGroupbox.Name = "commPortGroupbox";
            this.commPortGroupbox.Size = new System.Drawing.Size(332, 48);
            this.commPortGroupbox.TabIndex = 3;
            this.commPortGroupbox.TabStop = false;
            this.commPortGroupbox.Text = "COM port selector";
            // 
            // btnRefreshCommPortList
            // 
            this.btnRefreshCommPortList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCommPortList.Location = new System.Drawing.Point(251, 16);
            this.btnRefreshCommPortList.Name = "btnRefreshCommPortList";
            this.btnRefreshCommPortList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshCommPortList.TabIndex = 2;
            this.btnRefreshCommPortList.Text = "Refresh";
            this.btnRefreshCommPortList.UseVisualStyleBackColor = true;
            this.btnRefreshCommPortList.Click += new System.EventHandler(this.btnRefreshCommPortList_Click);
            // 
            // commPortCombo
            // 
            this.commPortCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commPortCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commPortCombo.FormattingEnabled = true;
            this.commPortCombo.Location = new System.Drawing.Point(64, 16);
            this.commPortCombo.Name = "commPortCombo";
            this.commPortCombo.Size = new System.Drawing.Size(181, 21);
            this.commPortCombo.TabIndex = 1;
            this.commPortCombo.SelectedIndexChanged += new System.EventHandler(this.commPortCombo_SelectedIndexChanged);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(6, 19);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(52, 13);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM port";
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Controls.Add(this.tbScanLog);
            this.logBox.Controls.Add(this.tbMessageLog);
            this.logBox.Location = new System.Drawing.Point(12, 415);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(785, 359);
            this.logBox.TabIndex = 4;
            this.logBox.TabStop = false;
            this.logBox.Text = "Log";
            // 
            // tbScanLog
            // 
            this.tbScanLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbScanLog.Font = new System.Drawing.Font("Liberation Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScanLog.Location = new System.Drawing.Point(657, 16);
            this.tbScanLog.Multiline = true;
            this.tbScanLog.Name = "tbScanLog";
            this.tbScanLog.ReadOnly = true;
            this.tbScanLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbScanLog.Size = new System.Drawing.Size(125, 340);
            this.tbScanLog.TabIndex = 1;
            // 
            // tbMessageLog
            // 
            this.tbMessageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessageLog.Location = new System.Drawing.Point(3, 16);
            this.tbMessageLog.Multiline = true;
            this.tbMessageLog.Name = "tbMessageLog";
            this.tbMessageLog.ReadOnly = true;
            this.tbMessageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageLog.Size = new System.Drawing.Size(779, 340);
            this.tbMessageLog.TabIndex = 0;
            // 
            // sweepSettingsGroup
            // 
            this.sweepSettingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sweepSettingsGroup.Controls.Add(this.cbSweepSettings);
            this.sweepSettingsGroup.Controls.Add(this.lblLoadSweep);
            this.sweepSettingsGroup.Controls.Add(this.btnSaveSweepSettings);
            this.sweepSettingsGroup.Controls.Add(this.btnSetSweepHeight);
            this.sweepSettingsGroup.Controls.Add(this.btnSetSweepWidth);
            this.sweepSettingsGroup.Controls.Add(this.btnSetSweepStart);
            this.sweepSettingsGroup.Controls.Add(this.btnSetDslrWH);
            this.sweepSettingsGroup.Controls.Add(this.btnSetDslrHeight);
            this.sweepSettingsGroup.Controls.Add(this.btnSetDslrWidth);
            this.sweepSettingsGroup.Controls.Add(this.btnSetAnchor);
            this.sweepSettingsGroup.Enabled = false;
            this.sweepSettingsGroup.Location = new System.Drawing.Point(350, 12);
            this.sweepSettingsGroup.Name = "sweepSettingsGroup";
            this.sweepSettingsGroup.Size = new System.Drawing.Size(447, 107);
            this.sweepSettingsGroup.TabIndex = 5;
            this.sweepSettingsGroup.TabStop = false;
            this.sweepSettingsGroup.Text = "Sweep settings";
            // 
            // cbSweepSettings
            // 
            this.cbSweepSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSweepSettings.FormattingEnabled = true;
            this.cbSweepSettings.Location = new System.Drawing.Point(116, 77);
            this.cbSweepSettings.Name = "cbSweepSettings";
            this.cbSweepSettings.Size = new System.Drawing.Size(325, 21);
            this.cbSweepSettings.TabIndex = 9;
            this.cbSweepSettings.SelectedIndexChanged += new System.EventHandler(this.cbSweepSettings_SelectedIndexChanged);
            // 
            // lblLoadSweep
            // 
            this.lblLoadSweep.AutoSize = true;
            this.lblLoadSweep.Location = new System.Drawing.Point(6, 80);
            this.lblLoadSweep.Name = "lblLoadSweep";
            this.lblLoadSweep.Size = new System.Drawing.Size(104, 13);
            this.lblLoadSweep.TabIndex = 8;
            this.lblLoadSweep.Text = "Load sweep settings";
            // 
            // btnSaveSweepSettings
            // 
            this.btnSaveSweepSettings.Location = new System.Drawing.Point(341, 48);
            this.btnSaveSweepSettings.Name = "btnSaveSweepSettings";
            this.btnSaveSweepSettings.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSweepSettings.TabIndex = 7;
            this.btnSaveSweepSettings.Text = "Save sweep";
            this.btnSaveSweepSettings.UseVisualStyleBackColor = true;
            this.btnSaveSweepSettings.Click += new System.EventHandler(this.btnSaveSweepSettings_Click);
            // 
            // btnSetSweepHeight
            // 
            this.btnSetSweepHeight.Location = new System.Drawing.Point(235, 48);
            this.btnSetSweepHeight.Name = "btnSetSweepHeight";
            this.btnSetSweepHeight.Size = new System.Drawing.Size(100, 23);
            this.btnSetSweepHeight.TabIndex = 6;
            this.btnSetSweepHeight.Text = "Set sweep height";
            this.btnSetSweepHeight.UseVisualStyleBackColor = true;
            this.btnSetSweepHeight.Click += new System.EventHandler(this.btnSetSweepHeight_Click);
            // 
            // btnSetSweepWidth
            // 
            this.btnSetSweepWidth.Location = new System.Drawing.Point(129, 48);
            this.btnSetSweepWidth.Name = "btnSetSweepWidth";
            this.btnSetSweepWidth.Size = new System.Drawing.Size(100, 23);
            this.btnSetSweepWidth.TabIndex = 5;
            this.btnSetSweepWidth.Text = "Set sweep width";
            this.btnSetSweepWidth.UseVisualStyleBackColor = true;
            this.btnSetSweepWidth.Click += new System.EventHandler(this.btnSetSweepWidth_Click);
            // 
            // btnSetSweepStart
            // 
            this.btnSetSweepStart.Location = new System.Drawing.Point(6, 48);
            this.btnSetSweepStart.Name = "btnSetSweepStart";
            this.btnSetSweepStart.Size = new System.Drawing.Size(117, 23);
            this.btnSetSweepStart.TabIndex = 4;
            this.btnSetSweepStart.Text = "Set sweep start here";
            this.btnSetSweepStart.UseVisualStyleBackColor = true;
            this.btnSetSweepStart.Click += new System.EventHandler(this.btnSetSweepStart_Click);
            // 
            // btnSetDslrWH
            // 
            this.btnSetDslrWH.Enabled = false;
            this.btnSetDslrWH.Location = new System.Drawing.Point(341, 19);
            this.btnSetDslrWH.Name = "btnSetDslrWH";
            this.btnSetDslrWH.Size = new System.Drawing.Size(100, 23);
            this.btnSetDslrWH.TabIndex = 3;
            this.btnSetDslrWH.Text = "Set DSLR W+H";
            this.btnSetDslrWH.UseVisualStyleBackColor = true;
            this.btnSetDslrWH.Click += new System.EventHandler(this.btnSetDslrWH_Click);
            // 
            // btnSetDslrHeight
            // 
            this.btnSetDslrHeight.Enabled = false;
            this.btnSetDslrHeight.Location = new System.Drawing.Point(235, 19);
            this.btnSetDslrHeight.Name = "btnSetDslrHeight";
            this.btnSetDslrHeight.Size = new System.Drawing.Size(100, 23);
            this.btnSetDslrHeight.TabIndex = 2;
            this.btnSetDslrHeight.Text = "Set DSLR height";
            this.btnSetDslrHeight.UseVisualStyleBackColor = true;
            this.btnSetDslrHeight.Click += new System.EventHandler(this.btnSetDslrHeight_Click);
            // 
            // btnSetDslrWidth
            // 
            this.btnSetDslrWidth.Enabled = false;
            this.btnSetDslrWidth.Location = new System.Drawing.Point(129, 19);
            this.btnSetDslrWidth.Name = "btnSetDslrWidth";
            this.btnSetDslrWidth.Size = new System.Drawing.Size(100, 23);
            this.btnSetDslrWidth.TabIndex = 1;
            this.btnSetDslrWidth.Text = "Set DSLR width";
            this.btnSetDslrWidth.UseVisualStyleBackColor = true;
            this.btnSetDslrWidth.Click += new System.EventHandler(this.btnSetDslrWidth_Click);
            // 
            // btnSetAnchor
            // 
            this.btnSetAnchor.Location = new System.Drawing.Point(6, 19);
            this.btnSetAnchor.Name = "btnSetAnchor";
            this.btnSetAnchor.Size = new System.Drawing.Size(117, 23);
            this.btnSetAnchor.TabIndex = 0;
            this.btnSetAnchor.Text = "Set anchor here";
            this.btnSetAnchor.UseVisualStyleBackColor = true;
            this.btnSetAnchor.Click += new System.EventHandler(this.btnLearnShotSize_Click);
            // 
            // shootingGroup
            // 
            this.shootingGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shootingGroup.Controls.Add(this.pbHelpSaveLocation);
            this.shootingGroup.Controls.Add(this.lblShootLocation);
            this.shootingGroup.Controls.Add(this.btnShootLocation);
            this.shootingGroup.Controls.Add(this.tbShootLocation);
            this.shootingGroup.Location = new System.Drawing.Point(350, 125);
            this.shootingGroup.Name = "shootingGroup";
            this.shootingGroup.Size = new System.Drawing.Size(447, 284);
            this.shootingGroup.TabIndex = 6;
            this.shootingGroup.TabStop = false;
            this.shootingGroup.Text = "Shooting";
            // 
            // pbHelpSaveLocation
            // 
            this.pbHelpSaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHelpSaveLocation.Image = global::DSLR_Digitizer.Properties.Resources.question_mark_small;
            this.pbHelpSaveLocation.Location = new System.Drawing.Point(418, 17);
            this.pbHelpSaveLocation.Name = "pbHelpSaveLocation";
            this.pbHelpSaveLocation.Size = new System.Drawing.Size(23, 23);
            this.pbHelpSaveLocation.TabIndex = 3;
            this.pbHelpSaveLocation.TabStop = false;
            this.infoToolTip.SetToolTip(this.pbHelpSaveLocation, "Set this to whatever folder you configured in Canon Utility");
            // 
            // lblShootLocation
            // 
            this.lblShootLocation.AutoSize = true;
            this.lblShootLocation.Location = new System.Drawing.Point(6, 22);
            this.lblShootLocation.Name = "lblShootLocation";
            this.lblShootLocation.Size = new System.Drawing.Size(72, 13);
            this.lblShootLocation.TabIndex = 2;
            this.lblShootLocation.Text = "Save location";
            // 
            // btnShootLocation
            // 
            this.btnShootLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShootLocation.Location = new System.Drawing.Point(385, 17);
            this.btnShootLocation.Name = "btnShootLocation";
            this.btnShootLocation.Size = new System.Drawing.Size(27, 23);
            this.btnShootLocation.TabIndex = 1;
            this.btnShootLocation.Text = "...";
            this.btnShootLocation.UseVisualStyleBackColor = true;
            this.btnShootLocation.Click += new System.EventHandler(this.btnShootLocation_Click);
            // 
            // tbShootLocation
            // 
            this.tbShootLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbShootLocation.Location = new System.Drawing.Point(84, 19);
            this.tbShootLocation.Name = "tbShootLocation";
            this.tbShootLocation.Size = new System.Drawing.Size(295, 20);
            this.tbShootLocation.TabIndex = 0;
            // 
            // infoToolTip
            // 
            this.infoToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // MainScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 786);
            this.Controls.Add(this.shootingGroup);
            this.Controls.Add(this.sweepSettingsGroup);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.commPortGroupbox);
            this.Controls.Add(this.navigationGroup);
            this.MinimumSize = new System.Drawing.Size(825, 825);
            this.Name = "MainScannerForm";
            this.Text = "Das Scannerwerkschtungapp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScannerForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainScannerForm_Shown);
            this.navigationGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).EndInit();
            this.commPortGroupbox.ResumeLayout(false);
            this.commPortGroupbox.PerformLayout();
            this.logBox.ResumeLayout(false);
            this.logBox.PerformLayout();
            this.sweepSettingsGroup.ResumeLayout(false);
            this.sweepSettingsGroup.PerformLayout();
            this.shootingGroup.ResumeLayout(false);
            this.shootingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelpSaveLocation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox navigationGroup;
        private System.Windows.Forms.Panel panel1;
        private ScannerIcon iconRight;
        private ScannerIcon iconDown;
        private ScannerIcon iconLeft;
        private ScannerIcon iconUp;
        private ScannerIcon iconStop;
        private System.Windows.Forms.GroupBox commPortGroupbox;
        private System.Windows.Forms.ComboBox commPortCombo;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Button btnRefreshCommPortList;
        private System.Windows.Forms.GroupBox logBox;
        private System.Windows.Forms.TextBox tbMessageLog;
        private System.Windows.Forms.TextBox tbScanLog;
        private System.Windows.Forms.GroupBox sweepSettingsGroup;
        private System.Windows.Forms.Button btnSetAnchor;
        private System.Windows.Forms.Button btnSetDslrWidth;
        private System.Windows.Forms.Button btnSetDslrHeight;
        private System.Windows.Forms.Button btnSetDslrWH;
        private System.Windows.Forms.Button btnSetSweepHeight;
        private System.Windows.Forms.Button btnSetSweepWidth;
        private System.Windows.Forms.Button btnSetSweepStart;
        private System.Windows.Forms.Button btnSaveSweepSettings;
        private System.Windows.Forms.ComboBox cbSweepSettings;
        private System.Windows.Forms.Label lblLoadSweep;
        private System.Windows.Forms.GroupBox shootingGroup;
        private System.Windows.Forms.Label lblShootLocation;
        private System.Windows.Forms.Button btnShootLocation;
        private System.Windows.Forms.TextBox tbShootLocation;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.PictureBox pbHelpSaveLocation;
        private System.Windows.Forms.ToolTip infoToolTip;
    }
}

