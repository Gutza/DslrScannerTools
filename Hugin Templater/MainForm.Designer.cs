namespace Hugin_Templater
{
    partial class MainForm
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
            this.lblTemplateFile = new System.Windows.Forms.Label();
            this.tbTemplateFile = new System.Windows.Forms.TextBox();
            this.btnTemplateFile = new System.Windows.Forms.Button();
            this.ptoOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnTiffFolder = new System.Windows.Forms.Button();
            this.tbTiffFolder = new System.Windows.Forms.TextBox();
            this.lbTiffFolder = new System.Windows.Forms.Label();
            this.tiffOpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTemplateFile
            // 
            this.lblTemplateFile.AutoSize = true;
            this.lblTemplateFile.Location = new System.Drawing.Point(12, 15);
            this.lblTemplateFile.Name = "lblTemplateFile";
            this.lblTemplateFile.Size = new System.Drawing.Size(92, 13);
            this.lblTemplateFile.TabIndex = 0;
            this.lblTemplateFile.Text = "Template PTO file";
            // 
            // tbTemplateFile
            // 
            this.tbTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTemplateFile.Location = new System.Drawing.Point(110, 12);
            this.tbTemplateFile.Name = "tbTemplateFile";
            this.tbTemplateFile.Size = new System.Drawing.Size(521, 20);
            this.tbTemplateFile.TabIndex = 1;
            // 
            // btnTemplateFile
            // 
            this.btnTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateFile.Location = new System.Drawing.Point(637, 10);
            this.btnTemplateFile.Name = "btnTemplateFile";
            this.btnTemplateFile.Size = new System.Drawing.Size(25, 23);
            this.btnTemplateFile.TabIndex = 2;
            this.btnTemplateFile.Text = "...";
            this.btnTemplateFile.UseVisualStyleBackColor = true;
            this.btnTemplateFile.Click += new System.EventHandler(this.btnTemplateFile_Click);
            // 
            // ptoOpenFileDialog
            // 
            this.ptoOpenFileDialog.Filter = "PTO files|*.pto|All files|*.*";
            // 
            // btnTiffFolder
            // 
            this.btnTiffFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiffFolder.Location = new System.Drawing.Point(637, 36);
            this.btnTiffFolder.Name = "btnTiffFolder";
            this.btnTiffFolder.Size = new System.Drawing.Size(25, 23);
            this.btnTiffFolder.TabIndex = 5;
            this.btnTiffFolder.Text = "...";
            this.btnTiffFolder.UseVisualStyleBackColor = true;
            this.btnTiffFolder.Click += new System.EventHandler(this.btnTiffFolder_Click);
            // 
            // tbTiffFolder
            // 
            this.tbTiffFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTiffFolder.Location = new System.Drawing.Point(79, 38);
            this.tbTiffFolder.Name = "tbTiffFolder";
            this.tbTiffFolder.Size = new System.Drawing.Size(552, 20);
            this.tbTiffFolder.TabIndex = 4;
            // 
            // lbTiffFolder
            // 
            this.lbTiffFolder.AutoSize = true;
            this.lbTiffFolder.Location = new System.Drawing.Point(12, 41);
            this.lbTiffFolder.Name = "lbTiffFolder";
            this.lbTiffFolder.Size = new System.Drawing.Size(61, 13);
            this.lbTiffFolder.TabIndex = 3;
            this.lbTiffFolder.Text = "TIFF Folder";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(12, 366);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(650, 23);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Generate PTO";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 450);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnTiffFolder);
            this.Controls.Add(this.tbTiffFolder);
            this.Controls.Add(this.lbTiffFolder);
            this.Controls.Add(this.btnTemplateFile);
            this.Controls.Add(this.tbTemplateFile);
            this.Controls.Add(this.lblTemplateFile);
            this.Name = "MainForm";
            this.Text = "Hugin Templater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTemplateFile;
        private System.Windows.Forms.TextBox tbTemplateFile;
        private System.Windows.Forms.Button btnTemplateFile;
        private System.Windows.Forms.OpenFileDialog ptoOpenFileDialog;
        private System.Windows.Forms.Button btnTiffFolder;
        private System.Windows.Forms.TextBox tbTiffFolder;
        private System.Windows.Forms.Label lbTiffFolder;
        private System.Windows.Forms.FolderBrowserDialog tiffOpenFolderDialog;
        private System.Windows.Forms.Button btnProcess;
    }
}

