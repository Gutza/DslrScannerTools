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
            this.pnlNavigation = new System.Windows.Forms.Panel();
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
            this.tbMessageLog = new System.Windows.Forms.TextBox();
            this.pnlScannerLog = new System.Windows.Forms.Panel();
            this.tbScannerLog = new System.Windows.Forms.TextBox();
            this.pnlLogOptions = new System.Windows.Forms.Panel();
            this.cbHidePositionDatagrams = new System.Windows.Forms.CheckBox();
            this.sweepSettingsGroup = new System.Windows.Forms.GroupBox();
            this.btnDeleteSweepSettings = new System.Windows.Forms.Button();
            this.btnHuginTemplate = new System.Windows.Forms.Button();
            this.tbHuginTemplate = new System.Windows.Forms.TextBox();
            this.lbHuginTemplate = new System.Windows.Forms.Label();
            this.cbSweepSettings = new System.Windows.Forms.ComboBox();
            this.lblLoadSweep = new System.Windows.Forms.Label();
            this.btnSaveSweepSettings = new System.Windows.Forms.Button();
            this.btnSetNegativeSize = new System.Windows.Forms.Button();
            this.btnGoToOrigin = new System.Windows.Forms.Button();
            this.btnSetDslrSize = new System.Windows.Forms.Button();
            this.btnResetOrigin = new System.Windows.Forms.Button();
            this.shootingGroup = new System.Windows.Forms.GroupBox();
            this.btnNextFrame = new System.Windows.Forms.Button();
            this.btnResetFilm = new System.Windows.Forms.Button();
            this.btnStartSweep = new System.Windows.Forms.Button();
            this.btnNextSweepStep = new System.Windows.Forms.Button();
            this.btnResetSweep = new System.Windows.Forms.Button();
            this.pbHelpSaveLocation = new System.Windows.Forms.PictureBox();
            this.lblShootLocation = new System.Windows.Forms.Label();
            this.btnShootLocation = new System.Windows.Forms.Button();
            this.tbShootLocation = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openPtoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.timSweep = new System.Windows.Forms.Timer(this.components);
            this.btnGoToMidFrame = new System.Windows.Forms.Button();
            this.navigationGroup.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).BeginInit();
            this.commPortGroupbox.SuspendLayout();
            this.logBox.SuspendLayout();
            this.pnlScannerLog.SuspendLayout();
            this.pnlLogOptions.SuspendLayout();
            this.sweepSettingsGroup.SuspendLayout();
            this.shootingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelpSaveLocation)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationGroup
            // 
            this.navigationGroup.Controls.Add(this.pnlNavigation);
            this.navigationGroup.Enabled = false;
            this.navigationGroup.Location = new System.Drawing.Point(12, 66);
            this.navigationGroup.Name = "navigationGroup";
            this.navigationGroup.Size = new System.Drawing.Size(332, 343);
            this.navigationGroup.TabIndex = 2;
            this.navigationGroup.TabStop = false;
            this.navigationGroup.Text = "Navigation";
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.Controls.Add(this.iconRight);
            this.pnlNavigation.Controls.Add(this.iconDown);
            this.pnlNavigation.Controls.Add(this.iconLeft);
            this.pnlNavigation.Controls.Add(this.iconUp);
            this.pnlNavigation.Controls.Add(this.iconStop);
            this.pnlNavigation.Location = new System.Drawing.Point(6, 19);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(318, 316);
            this.pnlNavigation.TabIndex = 2;
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
            this.logBox.Controls.Add(this.tbMessageLog);
            this.logBox.Controls.Add(this.pnlScannerLog);
            this.logBox.Location = new System.Drawing.Point(12, 415);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(785, 346);
            this.logBox.TabIndex = 4;
            this.logBox.TabStop = false;
            this.logBox.Text = "Log";
            // 
            // tbMessageLog
            // 
            this.tbMessageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessageLog.Location = new System.Drawing.Point(3, 16);
            this.tbMessageLog.Multiline = true;
            this.tbMessageLog.Name = "tbMessageLog";
            this.tbMessageLog.ReadOnly = true;
            this.tbMessageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageLog.Size = new System.Drawing.Size(579, 327);
            this.tbMessageLog.TabIndex = 0;
            // 
            // pnlScannerLog
            // 
            this.pnlScannerLog.Controls.Add(this.tbScannerLog);
            this.pnlScannerLog.Controls.Add(this.pnlLogOptions);
            this.pnlScannerLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlScannerLog.Location = new System.Drawing.Point(582, 16);
            this.pnlScannerLog.Name = "pnlScannerLog";
            this.pnlScannerLog.Size = new System.Drawing.Size(200, 327);
            this.pnlScannerLog.TabIndex = 2;
            // 
            // tbScannerLog
            // 
            this.tbScannerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScannerLog.Font = new System.Drawing.Font("Liberation Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScannerLog.Location = new System.Drawing.Point(0, 0);
            this.tbScannerLog.Multiline = true;
            this.tbScannerLog.Name = "tbScannerLog";
            this.tbScannerLog.ReadOnly = true;
            this.tbScannerLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbScannerLog.Size = new System.Drawing.Size(200, 301);
            this.tbScannerLog.TabIndex = 2;
            // 
            // pnlLogOptions
            // 
            this.pnlLogOptions.Controls.Add(this.cbHidePositionDatagrams);
            this.pnlLogOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogOptions.Location = new System.Drawing.Point(0, 301);
            this.pnlLogOptions.Name = "pnlLogOptions";
            this.pnlLogOptions.Size = new System.Drawing.Size(200, 26);
            this.pnlLogOptions.TabIndex = 3;
            // 
            // cbHidePositionDatagrams
            // 
            this.cbHidePositionDatagrams.AutoSize = true;
            this.cbHidePositionDatagrams.Checked = true;
            this.cbHidePositionDatagrams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHidePositionDatagrams.Location = new System.Drawing.Point(6, 6);
            this.cbHidePositionDatagrams.Name = "cbHidePositionDatagrams";
            this.cbHidePositionDatagrams.Size = new System.Drawing.Size(139, 17);
            this.cbHidePositionDatagrams.TabIndex = 0;
            this.cbHidePositionDatagrams.Text = "Hide position datagrams";
            this.cbHidePositionDatagrams.UseVisualStyleBackColor = true;
            this.cbHidePositionDatagrams.CheckedChanged += new System.EventHandler(this.cbHidePositionDatagrams_CheckedChanged);
            // 
            // sweepSettingsGroup
            // 
            this.sweepSettingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sweepSettingsGroup.Controls.Add(this.btnDeleteSweepSettings);
            this.sweepSettingsGroup.Controls.Add(this.btnHuginTemplate);
            this.sweepSettingsGroup.Controls.Add(this.tbHuginTemplate);
            this.sweepSettingsGroup.Controls.Add(this.lbHuginTemplate);
            this.sweepSettingsGroup.Controls.Add(this.cbSweepSettings);
            this.sweepSettingsGroup.Controls.Add(this.lblLoadSweep);
            this.sweepSettingsGroup.Controls.Add(this.btnSaveSweepSettings);
            this.sweepSettingsGroup.Controls.Add(this.btnSetNegativeSize);
            this.sweepSettingsGroup.Controls.Add(this.btnGoToOrigin);
            this.sweepSettingsGroup.Controls.Add(this.btnSetDslrSize);
            this.sweepSettingsGroup.Controls.Add(this.btnResetOrigin);
            this.sweepSettingsGroup.Enabled = false;
            this.sweepSettingsGroup.Location = new System.Drawing.Point(350, 12);
            this.sweepSettingsGroup.Name = "sweepSettingsGroup";
            this.sweepSettingsGroup.Size = new System.Drawing.Size(447, 105);
            this.sweepSettingsGroup.TabIndex = 5;
            this.sweepSettingsGroup.TabStop = false;
            this.sweepSettingsGroup.Text = "Sweep settings";
            // 
            // btnDeleteSweepSettings
            // 
            this.btnDeleteSweepSettings.Location = new System.Drawing.Point(374, 75);
            this.btnDeleteSweepSettings.Name = "btnDeleteSweepSettings";
            this.btnDeleteSweepSettings.Size = new System.Drawing.Size(67, 23);
            this.btnDeleteSweepSettings.TabIndex = 12;
            this.btnDeleteSweepSettings.Text = "Delete";
            this.btnDeleteSweepSettings.UseVisualStyleBackColor = true;
            // 
            // btnHuginTemplate
            // 
            this.btnHuginTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuginTemplate.Location = new System.Drawing.Point(414, 48);
            this.btnHuginTemplate.Name = "btnHuginTemplate";
            this.btnHuginTemplate.Size = new System.Drawing.Size(27, 23);
            this.btnHuginTemplate.TabIndex = 4;
            this.btnHuginTemplate.Text = "...";
            this.btnHuginTemplate.UseVisualStyleBackColor = true;
            this.btnHuginTemplate.Click += new System.EventHandler(this.btnHuginTemplate_Click);
            // 
            // tbHuginTemplate
            // 
            this.tbHuginTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHuginTemplate.Location = new System.Drawing.Point(140, 50);
            this.tbHuginTemplate.Name = "tbHuginTemplate";
            this.tbHuginTemplate.Size = new System.Drawing.Size(268, 20);
            this.tbHuginTemplate.TabIndex = 11;
            // 
            // lbHuginTemplate
            // 
            this.lbHuginTemplate.AutoSize = true;
            this.lbHuginTemplate.Location = new System.Drawing.Point(6, 53);
            this.lbHuginTemplate.Name = "lbHuginTemplate";
            this.lbHuginTemplate.Size = new System.Drawing.Size(128, 13);
            this.lbHuginTemplate.TabIndex = 10;
            this.lbHuginTemplate.Text = "Hugin panorama template";
            // 
            // cbSweepSettings
            // 
            this.cbSweepSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSweepSettings.FormattingEnabled = true;
            this.cbSweepSettings.Location = new System.Drawing.Point(83, 77);
            this.cbSweepSettings.Name = "cbSweepSettings";
            this.cbSweepSettings.Size = new System.Drawing.Size(212, 21);
            this.cbSweepSettings.TabIndex = 9;
            this.cbSweepSettings.SelectedIndexChanged += new System.EventHandler(this.cbSweepSettings_SelectedIndexChanged);
            // 
            // lblLoadSweep
            // 
            this.lblLoadSweep.AutoSize = true;
            this.lblLoadSweep.Location = new System.Drawing.Point(6, 80);
            this.lblLoadSweep.Name = "lblLoadSweep";
            this.lblLoadSweep.Size = new System.Drawing.Size(72, 13);
            this.lblLoadSweep.TabIndex = 8;
            this.lblLoadSweep.Text = "Sweep preset";
            // 
            // btnSaveSweepSettings
            // 
            this.btnSaveSweepSettings.Location = new System.Drawing.Point(301, 75);
            this.btnSaveSweepSettings.Name = "btnSaveSweepSettings";
            this.btnSaveSweepSettings.Size = new System.Drawing.Size(67, 23);
            this.btnSaveSweepSettings.TabIndex = 7;
            this.btnSaveSweepSettings.Text = "Save";
            this.btnSaveSweepSettings.UseVisualStyleBackColor = true;
            this.btnSaveSweepSettings.Click += new System.EventHandler(this.btnSaveSweepSettings_Click);
            // 
            // btnSetNegativeSize
            // 
            this.btnSetNegativeSize.Location = new System.Drawing.Point(308, 19);
            this.btnSetNegativeSize.Name = "btnSetNegativeSize";
            this.btnSetNegativeSize.Size = new System.Drawing.Size(133, 23);
            this.btnSetNegativeSize.TabIndex = 5;
            this.btnSetNegativeSize.Text = "Set negative frame size";
            this.btnSetNegativeSize.UseVisualStyleBackColor = true;
            this.btnSetNegativeSize.Click += new System.EventHandler(this.btnSetNegativeSize_Click);
            // 
            // btnGoToOrigin
            // 
            this.btnGoToOrigin.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGoToOrigin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToOrigin.Location = new System.Drawing.Point(98, 19);
            this.btnGoToOrigin.Name = "btnGoToOrigin";
            this.btnGoToOrigin.Size = new System.Drawing.Size(78, 23);
            this.btnGoToOrigin.TabIndex = 4;
            this.btnGoToOrigin.Text = "Go to origin";
            this.btnGoToOrigin.UseVisualStyleBackColor = true;
            this.btnGoToOrigin.Click += new System.EventHandler(this.btnGoToOrigin_Click);
            // 
            // btnSetDslrSize
            // 
            this.btnSetDslrSize.Enabled = false;
            this.btnSetDslrSize.Location = new System.Drawing.Point(182, 19);
            this.btnSetDslrSize.Name = "btnSetDslrSize";
            this.btnSetDslrSize.Size = new System.Drawing.Size(120, 23);
            this.btnSetDslrSize.TabIndex = 3;
            this.btnSetDslrSize.Text = "Set DSLR frame size";
            this.btnSetDslrSize.UseVisualStyleBackColor = true;
            this.btnSetDslrSize.Click += new System.EventHandler(this.btnSetDslrSize_Click);
            // 
            // btnResetOrigin
            // 
            this.btnResetOrigin.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnResetOrigin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetOrigin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnResetOrigin.Location = new System.Drawing.Point(6, 19);
            this.btnResetOrigin.Name = "btnResetOrigin";
            this.btnResetOrigin.Size = new System.Drawing.Size(86, 23);
            this.btnResetOrigin.TabIndex = 0;
            this.btnResetOrigin.Text = "Reset origin";
            this.btnResetOrigin.UseVisualStyleBackColor = false;
            this.btnResetOrigin.Click += new System.EventHandler(this.btnResetOrigin_Click);
            // 
            // shootingGroup
            // 
            this.shootingGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shootingGroup.Controls.Add(this.btnGoToMidFrame);
            this.shootingGroup.Controls.Add(this.btnNextFrame);
            this.shootingGroup.Controls.Add(this.btnResetFilm);
            this.shootingGroup.Controls.Add(this.btnStartSweep);
            this.shootingGroup.Controls.Add(this.btnNextSweepStep);
            this.shootingGroup.Controls.Add(this.btnResetSweep);
            this.shootingGroup.Controls.Add(this.pbHelpSaveLocation);
            this.shootingGroup.Controls.Add(this.lblShootLocation);
            this.shootingGroup.Controls.Add(this.btnShootLocation);
            this.shootingGroup.Controls.Add(this.tbShootLocation);
            this.shootingGroup.Location = new System.Drawing.Point(350, 123);
            this.shootingGroup.Name = "shootingGroup";
            this.shootingGroup.Size = new System.Drawing.Size(447, 286);
            this.shootingGroup.TabIndex = 6;
            this.shootingGroup.TabStop = false;
            this.shootingGroup.Text = "Shooting";
            // 
            // btnNextFrame
            // 
            this.btnNextFrame.Location = new System.Drawing.Point(98, 71);
            this.btnNextFrame.Name = "btnNextFrame";
            this.btnNextFrame.Size = new System.Drawing.Size(83, 23);
            this.btnNextFrame.TabIndex = 8;
            this.btnNextFrame.Text = "Next frame";
            this.btnNextFrame.UseVisualStyleBackColor = true;
            this.btnNextFrame.Click += new System.EventHandler(this.btnNextFrame_Click);
            // 
            // btnResetFilm
            // 
            this.btnResetFilm.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnResetFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilm.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnResetFilm.Location = new System.Drawing.Point(9, 71);
            this.btnResetFilm.Name = "btnResetFilm";
            this.btnResetFilm.Size = new System.Drawing.Size(83, 23);
            this.btnResetFilm.TabIndex = 7;
            this.btnResetFilm.Text = "Reset film";
            this.btnResetFilm.UseVisualStyleBackColor = false;
            this.btnResetFilm.Click += new System.EventHandler(this.btnResetFilm_Click);
            // 
            // btnStartSweep
            // 
            this.btnStartSweep.Enabled = false;
            this.btnStartSweep.Location = new System.Drawing.Point(98, 45);
            this.btnStartSweep.Name = "btnStartSweep";
            this.btnStartSweep.Size = new System.Drawing.Size(83, 23);
            this.btnStartSweep.TabIndex = 6;
            this.btnStartSweep.Text = "Start shooting";
            this.btnStartSweep.UseVisualStyleBackColor = true;
            this.btnStartSweep.Click += new System.EventHandler(this.btnStartSweep_Click);
            // 
            // btnNextSweepStep
            // 
            this.btnNextSweepStep.Enabled = false;
            this.btnNextSweepStep.Location = new System.Drawing.Point(187, 45);
            this.btnNextSweepStep.Name = "btnNextSweepStep";
            this.btnNextSweepStep.Size = new System.Drawing.Size(23, 23);
            this.btnNextSweepStep.TabIndex = 5;
            this.btnNextSweepStep.Text = ">";
            this.btnNextSweepStep.UseVisualStyleBackColor = true;
            this.btnNextSweepStep.Click += new System.EventHandler(this.btnNextSweepStep_Click);
            // 
            // btnResetSweep
            // 
            this.btnResetSweep.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnResetSweep.Enabled = false;
            this.btnResetSweep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetSweep.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnResetSweep.Location = new System.Drawing.Point(9, 45);
            this.btnResetSweep.Name = "btnResetSweep";
            this.btnResetSweep.Size = new System.Drawing.Size(83, 23);
            this.btnResetSweep.TabIndex = 4;
            this.btnResetSweep.Text = "Reset sweep";
            this.btnResetSweep.UseVisualStyleBackColor = false;
            this.btnResetSweep.Click += new System.EventHandler(this.btnResetSweep_Click);
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
            // openPtoFileDialog
            // 
            this.openPtoFileDialog.DefaultExt = "pto";
            this.openPtoFileDialog.Filter = "PTO files|*.pto|All files|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPosition});
            this.statusStrip1.Location = new System.Drawing.Point(0, 764);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(809, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsPosition
            // 
            this.tsPosition.Name = "tsPosition";
            this.tsPosition.Size = new System.Drawing.Size(65, 17);
            this.tsPosition.Text = "(unknown)";
            // 
            // timSweep
            // 
            this.timSweep.Tick += new System.EventHandler(this.timSweep_Tick);
            // 
            // btnGoToMidFrame
            // 
            this.btnGoToMidFrame.Enabled = false;
            this.btnGoToMidFrame.Location = new System.Drawing.Point(9, 100);
            this.btnGoToMidFrame.Name = "btnGoToMidFrame";
            this.btnGoToMidFrame.Size = new System.Drawing.Size(172, 23);
            this.btnGoToMidFrame.TabIndex = 9;
            this.btnGoToMidFrame.Text = "Go to mid frame";
            this.btnGoToMidFrame.UseVisualStyleBackColor = true;
            this.btnGoToMidFrame.Click += new System.EventHandler(this.btnGoToMidFrame_Click);
            // 
            // MainScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 786);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.shootingGroup);
            this.Controls.Add(this.sweepSettingsGroup);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.commPortGroupbox);
            this.Controls.Add(this.navigationGroup);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(825, 825);
            this.Name = "MainScannerForm";
            this.Text = "Das Scannerwerkschtungapp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScannerForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainScannerForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainScannerForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainScannerForm_KeyUp);
            this.navigationGroup.ResumeLayout(false);
            this.pnlNavigation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).EndInit();
            this.commPortGroupbox.ResumeLayout(false);
            this.commPortGroupbox.PerformLayout();
            this.logBox.ResumeLayout(false);
            this.logBox.PerformLayout();
            this.pnlScannerLog.ResumeLayout(false);
            this.pnlScannerLog.PerformLayout();
            this.pnlLogOptions.ResumeLayout(false);
            this.pnlLogOptions.PerformLayout();
            this.sweepSettingsGroup.ResumeLayout(false);
            this.sweepSettingsGroup.PerformLayout();
            this.shootingGroup.ResumeLayout(false);
            this.shootingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelpSaveLocation)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox navigationGroup;
        private System.Windows.Forms.Panel pnlNavigation;
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
        private System.Windows.Forms.GroupBox sweepSettingsGroup;
        private System.Windows.Forms.Button btnResetOrigin;
        private System.Windows.Forms.Button btnSetDslrSize;
        private System.Windows.Forms.Button btnSetNegativeSize;
        private System.Windows.Forms.Button btnGoToOrigin;
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
        private System.Windows.Forms.Button btnHuginTemplate;
        private System.Windows.Forms.TextBox tbHuginTemplate;
        private System.Windows.Forms.Label lbHuginTemplate;
        private System.Windows.Forms.OpenFileDialog openPtoFileDialog;
        private System.Windows.Forms.Button btnDeleteSweepSettings;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsPosition;
        private System.Windows.Forms.Button btnResetSweep;
        private System.Windows.Forms.Button btnNextSweepStep;
        private System.Windows.Forms.Panel pnlScannerLog;
        private System.Windows.Forms.Panel pnlLogOptions;
        private System.Windows.Forms.TextBox tbScannerLog;
        private System.Windows.Forms.CheckBox cbHidePositionDatagrams;
        private System.Windows.Forms.Button btnStartSweep;
        private System.Windows.Forms.Timer timSweep;
        private System.Windows.Forms.Button btnNextFrame;
        private System.Windows.Forms.Button btnResetFilm;
        private System.Windows.Forms.Button btnGoToMidFrame;
    }
}

