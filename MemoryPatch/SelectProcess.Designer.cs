namespace MemoryPatch
{
    partial class SelectProcess
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
            this.lstProcess = new System.Windows.Forms.ListBox();
            this.lstModules = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkUserOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstProcess
            // 
            this.lstProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstProcess.FormattingEnabled = true;
            this.lstProcess.HorizontalScrollbar = true;
            this.lstProcess.ItemHeight = 20;
            this.lstProcess.Location = new System.Drawing.Point(0, 0);
            this.lstProcess.Name = "lstProcess";
            this.lstProcess.Size = new System.Drawing.Size(336, 564);
            this.lstProcess.Sorted = true;
            this.lstProcess.TabIndex = 0;
            this.lstProcess.SelectedIndexChanged += new System.EventHandler(this.lstProcess_SelectedIndexChanged);
            // 
            // lstModules
            // 
            this.lstModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstModules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstModules.FormattingEnabled = true;
            this.lstModules.HorizontalScrollbar = true;
            this.lstModules.ItemHeight = 20;
            this.lstModules.Location = new System.Drawing.Point(342, 0);
            this.lstModules.Name = "lstModules";
            this.lstModules.Size = new System.Drawing.Size(352, 564);
            this.lstModules.TabIndex = 0;
            this.lstModules.SelectedIndexChanged += new System.EventHandler(this.lstModules_SelectedIndexChanged);
            this.lstModules.DoubleClick += new System.EventHandler(this.lstModules_DoubleClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(619, 579);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0, 579);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkUserOnly
            // 
            this.chkUserOnly.AutoSize = true;
            this.chkUserOnly.Checked = true;
            this.chkUserOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserOnly.Location = new System.Drawing.Point(182, 570);
            this.chkUserOnly.Name = "chkUserOnly";
            this.chkUserOnly.Size = new System.Drawing.Size(154, 17);
            this.chkUserOnly.TabIndex = 2;
            this.chkUserOnly.Text = "Show User Processes Only";
            this.chkUserOnly.UseVisualStyleBackColor = true;
            this.chkUserOnly.CheckedChanged += new System.EventHandler(this.chkUserOnly_CheckedChanged);
            // 
            // SelectProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 614);
            this.Controls.Add(this.chkUserOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstModules);
            this.Controls.Add(this.lstProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectProcess";
            this.Text = "SelectProcess";
            this.Load += new System.EventHandler(this.SelectProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstProcess;
        private System.Windows.Forms.ListBox lstModules;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkUserOnly;
    }
}