namespace MemoryPatch
{
    partial class FrmMain
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

            if (disposing)
            {
                //if (_process != null && !_process.HasExited)
                //{
                //    _process.Kill();
                //}

                SaveConfig();
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
            this.btnSelectProcess = new System.Windows.Forms.Button();
            this.lbActiveProcess = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRun = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.openAddressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAddressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchControl1 = new MemoryPatch.SearchControl();
            this.memoryPatchControl1 = new MemoryPatch.Editing_Memory.MemoryPatchControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectProcess
            // 
            this.btnSelectProcess.Location = new System.Drawing.Point(-1, 27);
            this.btnSelectProcess.Name = "btnSelectProcess";
            this.btnSelectProcess.Size = new System.Drawing.Size(115, 23);
            this.btnSelectProcess.TabIndex = 0;
            this.btnSelectProcess.Text = "Select Process";
            this.btnSelectProcess.UseVisualStyleBackColor = true;
            this.btnSelectProcess.Click += new System.EventHandler(this.btnSelectProcess_Click);
            // 
            // lbActiveProcess
            // 
            this.lbActiveProcess.AutoSize = true;
            this.lbActiveProcess.Location = new System.Drawing.Point(241, 27);
            this.lbActiveProcess.Name = "lbActiveProcess";
            this.lbActiveProcess.Size = new System.Drawing.Size(33, 13);
            this.lbActiveProcess.TabIndex = 1;
            this.lbActiveProcess.Text = "None";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 700;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(120, 27);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(115, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run Process";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(771, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectProcessToolStripMenuItem,
            this.toolStripMenuItem3,
            this.openAddressesToolStripMenuItem,
            this.saveAddressesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openSearchToolStripMenuItem,
            this.saveSearchToolStripMenuItem,
            this.toolStripMenuItem1,
            this.importToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectProcessToolStripMenuItem
            // 
            this.selectProcessToolStripMenuItem.Name = "selectProcessToolStripMenuItem";
            this.selectProcessToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.selectProcessToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.selectProcessToolStripMenuItem.Text = "Select Process";
            this.selectProcessToolStripMenuItem.Click += new System.EventHandler(this.selectProcessToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(213, 6);
            // 
            // openAddressesToolStripMenuItem
            // 
            this.openAddressesToolStripMenuItem.Name = "openAddressesToolStripMenuItem";
            this.openAddressesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openAddressesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openAddressesToolStripMenuItem.Text = "Open Addresses";
            this.openAddressesToolStripMenuItem.Click += new System.EventHandler(this.openAddressesToolStripMenuItem_Click);
            // 
            // saveAddressesToolStripMenuItem
            // 
            this.saveAddressesToolStripMenuItem.Name = "saveAddressesToolStripMenuItem";
            this.saveAddressesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAddressesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveAddressesToolStripMenuItem.Text = "Save Addresses";
            this.saveAddressesToolStripMenuItem.Click += new System.EventHandler(this.saveAddressesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(213, 6);
            // 
            // openSearchToolStripMenuItem
            // 
            this.openSearchToolStripMenuItem.Name = "openSearchToolStripMenuItem";
            this.openSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.openSearchToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openSearchToolStripMenuItem.Text = "Open Search";
            this.openSearchToolStripMenuItem.Click += new System.EventHandler(this.openSearchToolStripMenuItem_Click);
            // 
            // saveSearchToolStripMenuItem
            // 
            this.saveSearchToolStripMenuItem.Name = "saveSearchToolStripMenuItem";
            this.saveSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveSearchToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveSearchToolStripMenuItem.Text = "Save Search";
            this.saveSearchToolStripMenuItem.Click += new System.EventHandler(this.saveSearchToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(213, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // searchControl1
            // 
            this.searchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl1.Location = new System.Drawing.Point(0, 50);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Size = new System.Drawing.Size(421, 657);
            this.searchControl1.TabIndex = 16;
            this.searchControl1.OnAddressSelected += new System.EventHandler<MemoryManager.AddressFoundEventArgs>(this.searchControl1_OnAddressSelected);
            // 
            // memoryPatchControl1
            // 
            this.memoryPatchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryPatchControl1.Location = new System.Drawing.Point(427, 50);
            this.memoryPatchControl1.Name = "memoryPatchControl1";
            this.memoryPatchControl1.Size = new System.Drawing.Size(332, 657);
            this.memoryPatchControl1.TabIndex = 15;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 698);
            this.Controls.Add(this.searchControl1);
            this.Controls.Add(this.memoryPatchControl1);
            this.Controls.Add(this.lbActiveProcess);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSelectProcess);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Searcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectProcess;
        private System.Windows.Forms.Label lbActiveProcess;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRun;
        private MemoryPatch.Editing_Memory.MemoryPatchControl memoryPatchControl1;
        private SearchControl searchControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAddressesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAddressesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}

