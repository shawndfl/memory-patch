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
            this.btnMaxPP = new System.Windows.Forms.Button();
            this.btnResetStatus = new System.Windows.Forms.Button();
            this.lbEnemy1Hp = new System.Windows.Forms.Label();
            this.btnEnemy1Hp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMaxHp
            // 
            this.btnMaxHp.Location = new System.Drawing.Point(3, 14);
            this.btnMaxHp.Name = "btnMaxHp";
            this.btnMaxHp.Size = new System.Drawing.Size(111, 23);
            this.btnMaxHp.TabIndex = 0;
            this.btnMaxHp.Text = "Set Hp to Max";
            this.btnMaxHp.UseVisualStyleBackColor = true;
            this.btnMaxHp.Click += new System.EventHandler(this.btnMaxHp_Click);
            // 
            // btnMaxPP
            // 
            this.btnMaxPP.Location = new System.Drawing.Point(3, 43);
            this.btnMaxPP.Name = "btnMaxPP";
            this.btnMaxPP.Size = new System.Drawing.Size(111, 23);
            this.btnMaxPP.TabIndex = 0;
            this.btnMaxPP.Text = "Set PP to Max";
            this.btnMaxPP.UseVisualStyleBackColor = true;
            this.btnMaxPP.Click += new System.EventHandler(this.btnMaxPP_Click);
            // 
            // btnResetStatus
            // 
            this.btnResetStatus.Location = new System.Drawing.Point(3, 72);
            this.btnResetStatus.Name = "btnResetStatus";
            this.btnResetStatus.Size = new System.Drawing.Size(111, 23);
            this.btnResetStatus.TabIndex = 0;
            this.btnResetStatus.Text = "Reset Status";
            this.btnResetStatus.UseVisualStyleBackColor = true;
            this.btnResetStatus.Click += new System.EventHandler(this.btnResetStatus_Click);
            // 
            // lbEnemy1Hp
            // 
            this.lbEnemy1Hp.AutoSize = true;
            this.lbEnemy1Hp.Location = new System.Drawing.Point(3, 119);
            this.lbEnemy1Hp.Name = "lbEnemy1Hp";
            this.lbEnemy1Hp.Size = new System.Drawing.Size(56, 13);
            this.lbEnemy1Hp.TabIndex = 1;
            this.lbEnemy1Hp.Text = "Enemy Hp";
            // 
            // btnEnemy1Hp
            // 
            this.btnEnemy1Hp.Location = new System.Drawing.Point(118, 114);
            this.btnEnemy1Hp.Name = "btnEnemy1Hp";
            this.btnEnemy1Hp.Size = new System.Drawing.Size(96, 23);
            this.btnEnemy1Hp.TabIndex = 2;
            this.btnEnemy1Hp.Text = "All Enemy Hp = 0";
            this.btnEnemy1Hp.UseVisualStyleBackColor = true;
            this.btnEnemy1Hp.Click += new System.EventHandler(this.btnEnemy1Hp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enemy Hp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enemy Hp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Enemy Hp";
            // 
            // EarthBound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnemy1Hp);
            this.Controls.Add(this.lbEnemy1Hp);
            this.Controls.Add(this.btnResetStatus);
            this.Controls.Add(this.btnMaxPP);
            this.Controls.Add(this.btnMaxHp);
            this.Name = "EarthBound";
            this.Size = new System.Drawing.Size(491, 383);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMaxHp;
        private System.Windows.Forms.Button btnMaxPP;
        private System.Windows.Forms.Button btnResetStatus;
        private System.Windows.Forms.Label lbEnemy1Hp;
        private System.Windows.Forms.Button btnEnemy1Hp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
