namespace DSLR_Digitizer
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconRight = new DSLR_Digitizer.ScannerIcon();
            this.iconDown = new DSLR_Digitizer.ScannerIcon();
            this.iconLeft = new DSLR_Digitizer.ScannerIcon();
            this.iconUp = new DSLR_Digitizer.ScannerIcon();
            this.iconStop = new DSLR_Digitizer.ScannerIcon();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iconRight);
            this.panel1.Controls.Add(this.iconDown);
            this.panel1.Controls.Add(this.iconLeft);
            this.panel1.Controls.Add(this.iconUp);
            this.panel1.Controls.Add(this.iconStop);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 321);
            this.panel1.TabIndex = 1;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ScannerIcon iconRight;
        private ScannerIcon iconDown;
        private ScannerIcon iconLeft;
        private ScannerIcon iconUp;
        private ScannerIcon iconStop;
    }
}

