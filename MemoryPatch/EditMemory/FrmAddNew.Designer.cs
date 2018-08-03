namespace MemoryPatch.EditMemory
{
    partial class FrmAddNew
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
            this.components = new System.ComponentModel.Container();
            this.groupAddAddresses = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboListOption = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            this.txtInitPokeValue = new System.Windows.Forms.TextBox();
            this.txtAddAddress = new System.Windows.Forms.TextBox();
            this.numCreateCount = new System.Windows.Forms.NumericUpDown();
            this.numPadding = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstPreview = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPoke = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupAddAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupAddAddresses
            // 
            this.groupAddAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupAddAddresses.Controls.Add(this.cboDataType);
            this.groupAddAddresses.Controls.Add(this.label10);
            this.groupAddAddresses.Controls.Add(this.btnCancel);
            this.groupAddAddresses.Controls.Add(this.cboListOption);
            this.groupAddAddresses.Controls.Add(this.btnCreate);
            this.groupAddAddresses.Controls.Add(this.txtBaseName);
            this.groupAddAddresses.Controls.Add(this.txtInitPokeValue);
            this.groupAddAddresses.Controls.Add(this.txtAddAddress);
            this.groupAddAddresses.Controls.Add(this.label17);
            this.groupAddAddresses.Controls.Add(this.label19);
            this.groupAddAddresses.Controls.Add(this.label20);
            this.groupAddAddresses.Controls.Add(this.label16);
            this.groupAddAddresses.Controls.Add(this.groupBox2);
            this.groupAddAddresses.Location = new System.Drawing.Point(12, 12);
            this.groupAddAddresses.Name = "groupAddAddresses";
            this.groupAddAddresses.Size = new System.Drawing.Size(354, 399);
            this.groupAddAddresses.TabIndex = 8;
            this.groupAddAddresses.TabStop = false;
            this.groupAddAddresses.Text = "Add Addresses";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(142, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(98, 20);
            this.textBox1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "2nd Address";
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Location = new System.Drawing.Point(79, 49);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(142, 21);
            this.cboDataType.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Data Type";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(6, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cboListOption
            // 
            this.cboListOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboListOption.FormattingEnabled = true;
            this.cboListOption.Location = new System.Drawing.Point(79, 102);
            this.cboListOption.Name = "cboListOption";
            this.cboListOption.Size = new System.Drawing.Size(142, 21);
            this.cboListOption.TabIndex = 7;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(276, 370);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(72, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // txtBaseName
            // 
            this.txtBaseName.Location = new System.Drawing.Point(79, 23);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.Size = new System.Drawing.Size(142, 20);
            this.txtBaseName.TabIndex = 0;
            // 
            // txtInitPokeValue
            // 
            this.txtInitPokeValue.Location = new System.Drawing.Point(79, 129);
            this.txtInitPokeValue.Name = "txtInitPokeValue";
            this.txtInitPokeValue.Size = new System.Drawing.Size(142, 20);
            this.txtInitPokeValue.TabIndex = 0;
            // 
            // txtAddAddress
            // 
            this.txtAddAddress.Location = new System.Drawing.Point(79, 76);
            this.txtAddAddress.Name = "txtAddAddress";
            this.txtAddAddress.Size = new System.Drawing.Size(142, 20);
            this.txtAddAddress.TabIndex = 0;
            // 
            // numCreateCount
            // 
            this.numCreateCount.Location = new System.Drawing.Point(142, 58);
            this.numCreateCount.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numCreateCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCreateCount.Name = "numCreateCount";
            this.numCreateCount.Size = new System.Drawing.Size(98, 20);
            this.numCreateCount.TabIndex = 1;
            this.numCreateCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPadding
            // 
            this.numPadding.Location = new System.Drawing.Point(142, 25);
            this.numPadding.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numPadding.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPadding.Name = "numPadding";
            this.numPadding.Size = new System.Drawing.Size(98, 20);
            this.numPadding.TabIndex = 0;
            this.numPadding.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Prefix Name";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "List Option";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 129);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Poked Value";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Address";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Number To Create";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Bytes Between Addresses";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lstPreview);
            this.groupBox1.Location = new System.Drawing.Point(372, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 399);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // lstPreview
            // 
            this.lstPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPreview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAddress,
            this.colCurrent,
            this.colPoke,
            this.colDataType});
            this.lstPreview.Location = new System.Drawing.Point(6, 19);
            this.lstPreview.Name = "lstPreview";
            this.lstPreview.Size = new System.Drawing.Size(428, 374);
            this.lstPreview.TabIndex = 0;
            this.lstPreview.UseCompatibleStateImageBehavior = false;
            this.lstPreview.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 79;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 79;
            // 
            // colCurrent
            // 
            this.colCurrent.Text = "Current Value";
            this.colCurrent.Width = 101;
            // 
            // colPoke
            // 
            this.colPoke.Text = "Poked Value";
            this.colPoke.Width = 73;
            // 
            // colDataType
            // 
            this.colDataType.Text = "Data Type";
            this.colDataType.Width = 86;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numPadding);
            this.groupBox2.Controls.Add(this.numCreateCount);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(9, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 143);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show Range";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "OR";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // FrmAddNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 423);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupAddAddresses);
            this.Name = "FrmAddNew";
            this.Text = "Add New Addresses";
            this.groupAddAddresses.ResumeLayout(false);
            this.groupAddAddresses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupAddAddresses;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboListOption;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.TextBox txtInitPokeValue;
        private System.Windows.Forms.TextBox txtAddAddress;
        private System.Windows.Forms.NumericUpDown numCreateCount;
        private System.Windows.Forms.NumericUpDown numPadding;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstPreview;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colCurrent;
        private System.Windows.Forms.ColumnHeader colPoke;
        private System.Windows.Forms.ColumnHeader colDataType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
    }
}