namespace MemoryPatch.EditMemory
{
    partial class PluginLoader
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenPath = new System.Windows.Forms.Button();
            this.lstPlugins = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstTypes = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupLoadPlugin = new System.Windows.Forms.GroupBox();
            this.groupPlugin = new System.Windows.Forms.GroupBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupLoadPlugin.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(70, 16);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(372, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plugin Path";
            // 
            // btnOpenPath
            // 
            this.btnOpenPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPath.Location = new System.Drawing.Point(448, 14);
            this.btnOpenPath.Name = "btnOpenPath";
            this.btnOpenPath.Size = new System.Drawing.Size(32, 23);
            this.btnOpenPath.TabIndex = 2;
            this.btnOpenPath.Text = "...";
            this.btnOpenPath.UseVisualStyleBackColor = true;
            this.btnOpenPath.Click += new System.EventHandler(this.btnOpenPath_Click);
            // 
            // lstPlugins
            // 
            this.lstPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPlugins.FormattingEnabled = true;
            this.lstPlugins.Location = new System.Drawing.Point(0, 0);
            this.lstPlugins.Name = "lstPlugins";
            this.lstPlugins.Size = new System.Drawing.Size(214, 329);
            this.lstPlugins.TabIndex = 3;
            this.lstPlugins.SelectedIndexChanged += new System.EventHandler(this.lstPlugins_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstPlugins);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstTypes);
            this.splitContainer1.Size = new System.Drawing.Size(474, 338);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 4;
            // 
            // lstTypes
            // 
            this.lstTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTypes.FormattingEnabled = true;
            this.lstTypes.Location = new System.Drawing.Point(0, 0);
            this.lstTypes.Name = "lstTypes";
            this.lstTypes.Size = new System.Drawing.Size(256, 329);
            this.lstTypes.TabIndex = 4;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(414, 395);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupLoadPlugin
            // 
            this.groupLoadPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLoadPlugin.Controls.Add(this.txtPath);
            this.groupLoadPlugin.Controls.Add(this.label1);
            this.groupLoadPlugin.Controls.Add(this.splitContainer1);
            this.groupLoadPlugin.Controls.Add(this.btnOpenPath);
            this.groupLoadPlugin.Enabled = false;
            this.groupLoadPlugin.Location = new System.Drawing.Point(3, 3);
            this.groupLoadPlugin.Name = "groupLoadPlugin";
            this.groupLoadPlugin.Size = new System.Drawing.Size(486, 386);
            this.groupLoadPlugin.TabIndex = 6;
            this.groupLoadPlugin.TabStop = false;
            this.groupLoadPlugin.Text = "Load Plugin";
            // 
            // groupPlugin
            // 
            this.groupPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPlugin.Location = new System.Drawing.Point(3, 3);
            this.groupPlugin.Name = "groupPlugin";
            this.groupPlugin.Size = new System.Drawing.Size(486, 386);
            this.groupPlugin.TabIndex = 7;
            this.groupPlugin.TabStop = false;
            this.groupPlugin.Text = "Plugin";
            // 
            // PluginLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.groupLoadPlugin);
            this.Controls.Add(this.groupPlugin);
            this.Name = "PluginLoader";
            this.Size = new System.Drawing.Size(492, 426);
            this.Load += new System.EventHandler(this.PluginLoader_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupLoadPlugin.ResumeLayout(false);
            this.groupLoadPlugin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenPath;
        private System.Windows.Forms.ListBox lstPlugins;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstTypes;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupLoadPlugin;
        private System.Windows.Forms.GroupBox groupPlugin;
    }
}
