namespace MemoryPatch
{
    partial class frmBatchCreate
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
            this.groupAddAddresses = new System.Windows.Forms.GroupBox();
            this.cboListOption = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            this.txtInitPokeValue = new System.Windows.Forms.TextBox();
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.numCreateCount = new System.Windows.Forms.NumericUpDown();
            this.numPadding = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupAddAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).BeginInit();
            this.SuspendLayout();
            // 
            // groupAddAddresses
            // 
            this.groupAddAddresses.Controls.Add(this.cboListOption);
            this.groupAddAddresses.Controls.Add(this.btnCreate);
            this.groupAddAddresses.Controls.Add(this.txtBaseName);
            this.groupAddAddresses.Controls.Add(this.txtInitPokeValue);
            this.groupAddAddresses.Controls.Add(this.txtStartAddress);
            this.groupAddAddresses.Controls.Add(this.numCreateCount);
            this.groupAddAddresses.Controls.Add(this.numPadding);
            this.groupAddAddresses.Controls.Add(this.label17);
            this.groupAddAddresses.Controls.Add(this.label19);
            this.groupAddAddresses.Controls.Add(this.label20);
            this.groupAddAddresses.Controls.Add(this.label16);
            this.groupAddAddresses.Controls.Add(this.label13);
            this.groupAddAddresses.Controls.Add(this.label12);
            this.groupAddAddresses.Location = new System.Drawing.Point(12, 12);
            this.groupAddAddresses.Name = "groupAddAddresses";
            this.groupAddAddresses.Size = new System.Drawing.Size(239, 223);
            this.groupAddAddresses.TabIndex = 15;
            this.groupAddAddresses.TabStop = false;
            this.groupAddAddresses.Text = "Add Addresses";
            // 
            // cboListOption
            // 
            this.cboListOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboListOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboListOption.FormattingEnabled = true;
            this.cboListOption.Location = new System.Drawing.Point(77, 78);
            this.cboListOption.Name = "cboListOption";
            this.cboListOption.Size = new System.Drawing.Size(144, 21);
            this.cboListOption.TabIndex = 7;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(149, 194);
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
            this.txtInitPokeValue.Location = new System.Drawing.Point(79, 105);
            this.txtInitPokeValue.Name = "txtInitPokeValue";
            this.txtInitPokeValue.Size = new System.Drawing.Size(142, 20);
            this.txtInitPokeValue.TabIndex = 0;
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(79, 49);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(142, 20);
            this.txtStartAddress.TabIndex = 0;
            // 
            // numCreateCount
            // 
            this.numCreateCount.Location = new System.Drawing.Point(146, 162);
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
            this.numCreateCount.Size = new System.Drawing.Size(75, 20);
            this.numCreateCount.TabIndex = 1;
            this.numCreateCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPadding
            // 
            this.numPadding.Location = new System.Drawing.Point(146, 139);
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
            this.numPadding.Size = new System.Drawing.Size(75, 20);
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
            this.label17.Location = new System.Drawing.Point(5, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "BaseName";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 81);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "List Option";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 108);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Poked Value";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Start Address";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 169);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Number To Create";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Bytes Between Addresses";
            // 
            // frmBatchCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 245);
            this.Controls.Add(this.groupAddAddresses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmBatchCreate";
            this.Text = "Batch Create";
            this.Load += new System.EventHandler(this.frmBatchCreate_Load);
            this.groupAddAddresses.ResumeLayout(false);
            this.groupAddAddresses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupAddAddresses;
        private System.Windows.Forms.ComboBox cboListOption;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.TextBox txtInitPokeValue;
        private System.Windows.Forms.TextBox txtStartAddress;
        private System.Windows.Forms.NumericUpDown numCreateCount;
        private System.Windows.Forms.NumericUpDown numPadding;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}