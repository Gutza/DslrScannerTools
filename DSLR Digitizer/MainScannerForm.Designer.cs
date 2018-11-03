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
            this.navigationGroup = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.commPortGroupbox = new System.Windows.Forms.GroupBox();
            this.btnRefreshCommPortList = new System.Windows.Forms.Button();
            this.commPortCombo = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.GroupBox();
            this.tbScanLog = new System.Windows.Forms.TextBox();
            this.tbMessageLog = new System.Windows.Forms.TextBox();
            this.iconRight = new DSLR_Digitizer.ScannerIcon();
            this.iconDown = new DSLR_Digitizer.ScannerIcon();
            this.iconLeft = new DSLR_Digitizer.ScannerIcon();
            this.iconUp = new DSLR_Digitizer.ScannerIcon();
            this.iconStop = new DSLR_Digitizer.ScannerIcon();
            this.navigationGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.commPortGroupbox.SuspendLayout();
            this.logBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).BeginInit();
            this.SuspendLayout();
            // 
            // navigationGroup
            // 
            this.navigationGroup.Controls.Add(this.panel1);
            this.navigationGroup.Enabled = false;
            this.navigationGroup.Location = new System.Drawing.Point(12, 77);
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
            // commPortGroupbox
            // 
            this.commPortGroupbox.Controls.Add(this.btnRefreshCommPortList);
            this.commPortGroupbox.Controls.Add(this.commPortCombo);
            this.commPortGroupbox.Controls.Add(this.lblComPort);
            this.commPortGroupbox.Location = new System.Drawing.Point(18, 13);
            this.commPortGroupbox.Name = "commPortGroupbox";
            this.commPortGroupbox.Size = new System.Drawing.Size(326, 48);
            this.commPortGroupbox.TabIndex = 3;
            this.commPortGroupbox.TabStop = false;
            this.commPortGroupbox.Text = "COM port selector";
            // 
            // btnRefreshCommPortList
            // 
            this.btnRefreshCommPortList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCommPortList.Location = new System.Drawing.Point(245, 16);
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
            this.commPortCombo.Size = new System.Drawing.Size(175, 21);
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
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Controls.Add(this.tbScanLog);
            this.logBox.Controls.Add(this.tbMessageLog);
            this.logBox.Location = new System.Drawing.Point(12, 426);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(776, 324);
            this.logBox.TabIndex = 4;
            this.logBox.TabStop = false;
            this.logBox.Text = "Log";
            // 
            // tbScanLog
            // 
            this.tbScanLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScanLog.Font = new System.Drawing.Font("Liberation Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScanLog.Location = new System.Drawing.Point(648, 16);
            this.tbScanLog.Multiline = true;
            this.tbScanLog.Name = "tbScanLog";
            this.tbScanLog.ReadOnly = true;
            this.tbScanLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbScanLog.Size = new System.Drawing.Size(125, 305);
            this.tbScanLog.TabIndex = 1;
            // 
            // tbMessageLog
            // 
            this.tbMessageLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbMessageLog.Location = new System.Drawing.Point(3, 16);
            this.tbMessageLog.Multiline = true;
            this.tbMessageLog.Name = "tbMessageLog";
            this.tbMessageLog.ReadOnly = true;
            this.tbMessageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageLog.Size = new System.Drawing.Size(645, 305);
            this.tbMessageLog.TabIndex = 0;
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
            // 
            // MainScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 762);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.commPortGroupbox);
            this.Controls.Add(this.navigationGroup);
            this.Name = "MainScannerForm";
            this.Text = "Das Scannerwerkschtungapp";
            this.navigationGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.commPortGroupbox.ResumeLayout(false);
            this.commPortGroupbox.PerformLayout();
            this.logBox.ResumeLayout(false);
            this.logBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).EndInit();
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
    }
}

