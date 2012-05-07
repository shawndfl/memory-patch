namespace ZsnesPlugins
{
    partial class EarthBound
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMaxHp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMaxHp
            // 
            this.btnMaxHp.Location = new System.Drawing.Point(56, 62);
            this.btnMaxHp.Name = "btnMaxHp";
            this.btnMaxHp.Size = new System.Drawing.Size(111, 23);
            this.btnMaxHp.TabIndex = 0;
            this.btnMaxHp.Text = "Set Hp to Max";
            this.btnMaxHp.UseVisualStyleBackColor = true;
            this.btnMaxHp.Click += new System.EventHandler(this.btnMaxHp_Click);
            // 
            // EarthBound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMaxHp);
            this.Name = "EarthBound";
            this.Size = new System.Drawing.Size(491, 383);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMaxHp;
    }
}
