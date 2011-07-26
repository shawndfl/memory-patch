namespace MemoryPatch
{
    partial class SearchControl
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
            this.components = new System.ComponentModel.Container();
            this.groupFound = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbFoundCount = new System.Windows.Forms.Label();
            this.dataResults = new System.Windows.Forms.DataGridView();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupNextSearch = new System.Windows.Forms.GroupBox();
            this.cboNextSearch = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbDataType = new System.Windows.Forms.Label();
            this.btnNextSearch = new System.Windows.Forms.Button();
            this.txtNextVal1 = new System.Windows.Forms.TextBox();
            this.groupFirstSearch = new System.Windows.Forms.GroupBox();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.btnFirstSearch = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.groupSearching = new System.Windows.Forms.GroupBox();
            this.pgSearching = new System.Windows.Forms.ProgressBar();
            this.btnCancle = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataResults)).BeginInit();
            this.groupNextSearch.SuspendLayout();
            this.groupFirstSearch.SuspendLayout();
            this.groupSearching.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFound
            // 
            this.groupFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupFound.Controls.Add(this.label7);
            this.groupFound.Controls.Add(this.lbFoundCount);
            this.groupFound.Controls.Add(this.dataResults);
            this.groupFound.Enabled = false;
            this.groupFound.Location = new System.Drawing.Point(1, 209);
            this.groupFound.Name = "groupFound";
            this.groupFound.Size = new System.Drawing.Size(420, 417);
            this.groupFound.TabIndex = 12;
            this.groupFound.TabStop = false;
            this.groupFound.Text = "Results";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Address Found";
            // 
            // lbFoundCount
            // 
            this.lbFoundCount.AutoSize = true;
            this.lbFoundCount.Location = new System.Drawing.Point(92, 16);
            this.lbFoundCount.Name = "lbFoundCount";
            this.lbFoundCount.Size = new System.Drawing.Size(33, 13);
            this.lbFoundCount.TabIndex = 10;
            this.lbFoundCount.Text = "None";
            // 
            // dataResults
            // 
            this.dataResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAddress,
            this.colValue,
            this.colType});
            this.dataResults.Location = new System.Drawing.Point(6, 40);
            this.dataResults.Name = "dataResults";
            this.dataResults.Size = new System.Drawing.Size(406, 371);
            this.dataResults.TabIndex = 9;
            this.dataResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataResults_CellClick);
            // 
            // colAddress
            // 
            this.colAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Width = 70;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Width = 59;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colType.HeaderText = "DataType";
            this.colType.Name = "colType";
            this.colType.Width = 79;
            // 
            // groupNextSearch
            // 
            this.groupNextSearch.Controls.Add(this.cboNextSearch);
            this.groupNextSearch.Controls.Add(this.label4);
            this.groupNextSearch.Controls.Add(this.label5);
            this.groupNextSearch.Controls.Add(this.lbDataType);
            this.groupNextSearch.Controls.Add(this.btnNextSearch);
            this.groupNextSearch.Controls.Add(this.txtNextVal1);
            this.groupNextSearch.Enabled = false;
            this.groupNextSearch.Location = new System.Drawing.Point(1, 141);
            this.groupNextSearch.Name = "groupNextSearch";
            this.groupNextSearch.Size = new System.Drawing.Size(420, 62);
            this.groupNextSearch.TabIndex = 11;
            this.groupNextSearch.TabStop = false;
            this.groupNextSearch.Text = "Next Search Options";
            // 
            // cboNextSearch
            // 
            this.cboNextSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNextSearch.FormattingEnabled = true;
            this.cboNextSearch.Location = new System.Drawing.Point(9, 32);
            this.cboNextSearch.Name = "cboNextSearch";
            this.cboNextSearch.Size = new System.Drawing.Size(116, 21);
            this.cboNextSearch.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Search";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Value";
            // 
            // lbDataType
            // 
            this.lbDataType.AutoSize = true;
            this.lbDataType.Location = new System.Drawing.Point(251, 32);
            this.lbDataType.Name = "lbDataType";
            this.lbDataType.Size = new System.Drawing.Size(57, 13);
            this.lbDataType.TabIndex = 5;
            this.lbDataType.Text = "Data Type";
            // 
            // btnNextSearch
            // 
            this.btnNextSearch.Location = new System.Drawing.Point(342, 30);
            this.btnNextSearch.Name = "btnNextSearch";
            this.btnNextSearch.Size = new System.Drawing.Size(71, 23);
            this.btnNextSearch.TabIndex = 2;
            this.btnNextSearch.Text = "Next Search";
            this.btnNextSearch.UseVisualStyleBackColor = true;
            this.btnNextSearch.Click += new System.EventHandler(this.btnNextSearch_Click);
            // 
            // txtNextVal1
            // 
            this.txtNextVal1.Location = new System.Drawing.Point(131, 32);
            this.txtNextVal1.Name = "txtNextVal1";
            this.txtNextVal1.Size = new System.Drawing.Size(114, 20);
            this.txtNextVal1.TabIndex = 4;
            this.txtNextVal1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNextVal1_KeyDown);
            // 
            // groupFirstSearch
            // 
            this.groupFirstSearch.Controls.Add(this.cboSearch);
            this.groupFirstSearch.Controls.Add(this.label3);
            this.groupFirstSearch.Controls.Add(this.label2);
            this.groupFirstSearch.Controls.Add(this.label1);
            this.groupFirstSearch.Controls.Add(this.cboDataType);
            this.groupFirstSearch.Controls.Add(this.btnFirstSearch);
            this.groupFirstSearch.Controls.Add(this.txtValue);
            this.groupFirstSearch.Enabled = false;
            this.groupFirstSearch.Location = new System.Drawing.Point(0, 62);
            this.groupFirstSearch.Name = "groupFirstSearch";
            this.groupFirstSearch.Size = new System.Drawing.Size(421, 73);
            this.groupFirstSearch.TabIndex = 10;
            this.groupFirstSearch.TabStop = false;
            this.groupFirstSearch.Text = "Search Options";
            // 
            // cboSearch
            // 
            this.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Location = new System.Drawing.Point(9, 37);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(116, 21);
            this.cboSearch.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Data Type";
            // 
            // cboDataType
            // 
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Location = new System.Drawing.Point(253, 36);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(81, 21);
            this.cboDataType.TabIndex = 3;
            // 
            // btnFirstSearch
            // 
            this.btnFirstSearch.Location = new System.Drawing.Point(344, 37);
            this.btnFirstSearch.Name = "btnFirstSearch";
            this.btnFirstSearch.Size = new System.Drawing.Size(71, 23);
            this.btnFirstSearch.TabIndex = 2;
            this.btnFirstSearch.Text = "Search";
            this.btnFirstSearch.UseVisualStyleBackColor = true;
            this.btnFirstSearch.Click += new System.EventHandler(this.btnFirstSearch_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(131, 37);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(116, 20);
            this.txtValue.TabIndex = 4;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // groupSearching
            // 
            this.groupSearching.Controls.Add(this.pgSearching);
            this.groupSearching.Controls.Add(this.btnCancle);
            this.groupSearching.Enabled = false;
            this.groupSearching.Location = new System.Drawing.Point(1, 3);
            this.groupSearching.Name = "groupSearching";
            this.groupSearching.Size = new System.Drawing.Size(420, 53);
            this.groupSearching.TabIndex = 9;
            this.groupSearching.TabStop = false;
            this.groupSearching.Text = "Searching";
            // 
            // pgSearching
            // 
            this.pgSearching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgSearching.Location = new System.Drawing.Point(6, 19);
            this.pgSearching.Name = "pgSearching";
            this.pgSearching.Size = new System.Drawing.Size(325, 23);
            this.pgSearching.TabIndex = 5;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.Location = new System.Drawing.Point(341, 17);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(71, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "Cancel";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupFound);
            this.Controls.Add(this.groupNextSearch);
            this.Controls.Add(this.groupFirstSearch);
            this.Controls.Add(this.groupSearching);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(421, 626);
            this.groupFound.ResumeLayout(false);
            this.groupFound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataResults)).EndInit();
            this.groupNextSearch.ResumeLayout(false);
            this.groupNextSearch.PerformLayout();
            this.groupFirstSearch.ResumeLayout(false);
            this.groupFirstSearch.PerformLayout();
            this.groupSearching.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFound;
        private System.Windows.Forms.Label lbFoundCount;
        private System.Windows.Forms.DataGridView dataResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.GroupBox groupNextSearch;
        private System.Windows.Forms.ComboBox cboNextSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbDataType;
        private System.Windows.Forms.Button btnNextSearch;
        private System.Windows.Forms.TextBox txtNextVal1;
        private System.Windows.Forms.GroupBox groupFirstSearch;
        private System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.Button btnFirstSearch;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.GroupBox groupSearching;
        private System.Windows.Forms.ProgressBar pgSearching;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer1;
    }
}
