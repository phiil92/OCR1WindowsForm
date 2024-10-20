namespace OCR1WindowsForm
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
            this.lblfolderpath = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblfolderpath
            // 
            this.lblfolderpath.AutoSize = true;
            this.lblfolderpath.Location = new System.Drawing.Point(12, 9);
            this.lblfolderpath.Name = "lblfolderpath";
            this.lblfolderpath.Size = new System.Drawing.Size(94, 13);
            this.lblfolderpath.TabIndex = 0;
            this.lblfolderpath.Text = "Ordner Auswählen";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(12, 25);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(300, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(318, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(110, 51);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(109, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 110);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status: Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 153);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.lblfolderpath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfolderpath;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblStatus;
    }
}

