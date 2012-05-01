namespace MemoryPatch
{
    partial class FrmTest
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
            this.customTreeview1 = new MemoryPatch.CustomTreeview();
            this.SuspendLayout();
            // 
            // customTreeview1
            // 
            this.customTreeview1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customTreeview1.Location = new System.Drawing.Point(12, 12);
            this.customTreeview1.Name = "customTreeview1";
            this.customTreeview1.Size = new System.Drawing.Size(260, 238);
            this.customTreeview1.TabIndex = 0;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.customTreeview1);
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomTreeview customTreeview1;
    }
}