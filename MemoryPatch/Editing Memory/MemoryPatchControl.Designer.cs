namespace MemoryPatch.Editing_Memory
{
    partial class MemoryPatchControl
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupAddressEdit = new System.Windows.Forms.GroupBox();
            this.btnPoke = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAddressOptionGroup = new System.Windows.Forms.ComboBox();
            this.chkLocked = new System.Windows.Forms.CheckBox();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCurrentValue = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTestAddress = new System.Windows.Forms.TextBox();
            this.btnIncValue = new System.Windows.Forms.Button();
            this.lstOptions = new System.Windows.Forms.ListBox();
            this.txtOption = new System.Windows.Forms.TextBox();
            this.txtOptValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOptionGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupEdit = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPokeAll = new System.Windows.Forms.Button();
            this.btnCreateNewGroup = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupClone = new System.Windows.Forms.GroupBox();
            this.btnClone = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numStructSize = new System.Windows.Forms.NumericUpDown();
            this.groupTest = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.groupAddAddresses = new System.Windows.Forms.GroupBox();
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
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.addressView1 = new MemoryPatch.AddressView();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupAddressEdit.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupEdit.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupClone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStructSize)).BeginInit();
            this.groupTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.groupAddAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(655, 768);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(647, 742);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Addresses";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(641, 736);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 4;
            // 
            // tv
            // 
            this.tv.AllowDrop = true;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Enabled = false;
            this.tv.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(641, 394);
            this.tv.TabIndex = 0;
            this.tv.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_AfterLabelEdit);
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
            this.tv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv_KeyDown);
            this.tv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tv_KeyUp);
            this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(641, 338);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupAddressEdit);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(633, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Selected Nodes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupAddressEdit
            // 
            this.groupAddressEdit.Controls.Add(this.btnPoke);
            this.groupAddressEdit.Controls.Add(this.txtAddress);
            this.groupAddressEdit.Controls.Add(this.cboDataType);
            this.groupAddressEdit.Controls.Add(this.label1);
            this.groupAddressEdit.Controls.Add(this.cboAddressOptionGroup);
            this.groupAddressEdit.Controls.Add(this.chkLocked);
            this.groupAddressEdit.Controls.Add(this.cboValue);
            this.groupAddressEdit.Controls.Add(this.label15);
            this.groupAddressEdit.Controls.Add(this.label10);
            this.groupAddressEdit.Controls.Add(this.label4);
            this.groupAddressEdit.Controls.Add(this.label7);
            this.groupAddressEdit.Controls.Add(this.txtDescription);
            this.groupAddressEdit.Controls.Add(this.txtValue);
            this.groupAddressEdit.Controls.Add(this.label14);
            this.groupAddressEdit.Controls.Add(this.txtCurrentValue);
            this.groupAddressEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAddressEdit.Location = new System.Drawing.Point(3, 3);
            this.groupAddressEdit.Name = "groupAddressEdit";
            this.groupAddressEdit.Size = new System.Drawing.Size(627, 306);
            this.groupAddressEdit.TabIndex = 7;
            this.groupAddressEdit.TabStop = false;
            this.groupAddressEdit.Text = "Address Edit";
            this.groupAddressEdit.Visible = false;
            // 
            // btnPoke
            // 
            this.btnPoke.Location = new System.Drawing.Point(193, 139);
            this.btnPoke.Name = "btnPoke";
            this.btnPoke.Size = new System.Drawing.Size(53, 23);
            this.btnPoke.TabIndex = 7;
            this.btnPoke.Text = "Poke";
            this.btnPoke.UseVisualStyleBackColor = true;
            this.btnPoke.Click += new System.EventHandler(this.btnPoke_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(93, 48);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(94, 20);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Location = new System.Drawing.Point(93, 81);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(94, 21);
            this.cboDataType.TabIndex = 2;
            this.cboDataType.SelectedIndexChanged += new System.EventHandler(this.cboDataType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // cboAddressOptionGroup
            // 
            this.cboAddressOptionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAddressOptionGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddressOptionGroup.FormattingEnabled = true;
            this.cboAddressOptionGroup.Location = new System.Drawing.Point(93, 176);
            this.cboAddressOptionGroup.Name = "cboAddressOptionGroup";
            this.cboAddressOptionGroup.Size = new System.Drawing.Size(381, 21);
            this.cboAddressOptionGroup.TabIndex = 6;
            this.cboAddressOptionGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
            // 
            // chkLocked
            // 
            this.chkLocked.AutoSize = true;
            this.chkLocked.Location = new System.Drawing.Point(193, 87);
            this.chkLocked.Name = "chkLocked";
            this.chkLocked.Size = new System.Drawing.Size(80, 17);
            this.chkLocked.TabIndex = 3;
            this.chkLocked.Text = "Lock Value";
            this.chkLocked.UseVisualStyleBackColor = true;
            this.chkLocked.CheckedChanged += new System.EventHandler(this.chkLocked_CheckedChanged);
            // 
            // cboValue
            // 
            this.cboValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(252, 139);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(222, 21);
            this.cboValue.TabIndex = 5;
            this.cboValue.SelectedIndexChanged += new System.EventHandler(this.cboValue_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 113);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Current Value";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Data Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Poked Value";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "List Options";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(93, 19);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(94, 20);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(93, 139);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(94, 20);
            this.txtValue.TabIndex = 4;
            this.txtValue.Enter += new System.EventHandler(this.txtValue_Enter);
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Address";
            // 
            // txtCurrentValue
            // 
            this.txtCurrentValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentValue.Location = new System.Drawing.Point(93, 110);
            this.txtCurrentValue.Name = "txtCurrentValue";
            this.txtCurrentValue.ReadOnly = true;
            this.txtCurrentValue.Size = new System.Drawing.Size(80, 20);
            this.txtCurrentValue.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.txtTestAddress);
            this.tabPage3.Controls.Add(this.btnIncValue);
            this.tabPage3.Controls.Add(this.lstOptions);
            this.tabPage3.Controls.Add(this.txtOption);
            this.tabPage3.Controls.Add(this.txtOptValue);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cboOptionGroup);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(633, 312);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 13);
            this.label21.TabIndex = 6;
            this.label21.Text = "Test Address";
            // 
            // txtTestAddress
            // 
            this.txtTestAddress.Location = new System.Drawing.Point(90, 12);
            this.txtTestAddress.Name = "txtTestAddress";
            this.txtTestAddress.Size = new System.Drawing.Size(203, 20);
            this.txtTestAddress.TabIndex = 5;
            // 
            // btnIncValue
            // 
            this.btnIncValue.Location = new System.Drawing.Point(187, 91);
            this.btnIncValue.Name = "btnIncValue";
            this.btnIncValue.Size = new System.Drawing.Size(106, 23);
            this.btnIncValue.TabIndex = 3;
            this.btnIncValue.Text = "Increment Value";
            this.btnIncValue.UseVisualStyleBackColor = true;
            this.btnIncValue.Click += new System.EventHandler(this.btnIncValue_Click);
            // 
            // lstOptions
            // 
            this.lstOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstOptions.FormattingEnabled = true;
            this.lstOptions.Location = new System.Drawing.Point(15, 146);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(278, 160);
            this.lstOptions.TabIndex = 4;
            this.lstOptions.SelectedIndexChanged += new System.EventHandler(this.lstOptions_SelectedIndexChanged);
            // 
            // txtOption
            // 
            this.txtOption.Location = new System.Drawing.Point(55, 65);
            this.txtOption.Name = "txtOption";
            this.txtOption.Size = new System.Drawing.Size(238, 20);
            this.txtOption.TabIndex = 1;
            this.txtOption.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOption_KeyDown);
            this.txtOption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtOption_MouseUp);
            // 
            // txtOptValue
            // 
            this.txtOptValue.Location = new System.Drawing.Point(55, 91);
            this.txtOptValue.Name = "txtOptValue";
            this.txtOptValue.Size = new System.Drawing.Size(126, 20);
            this.txtOptValue.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            // 
            // cboOptionGroup
            // 
            this.cboOptionGroup.FormattingEnabled = true;
            this.cboOptionGroup.Location = new System.Drawing.Point(55, 38);
            this.cboOptionGroup.Name = "cboOptionGroup";
            this.cboOptionGroup.Size = new System.Drawing.Size(238, 21);
            this.cboOptionGroup.TabIndex = 0;
            this.cboOptionGroup.SelectedIndexChanged += new System.EventHandler(this.cboOptionGroup_SelectedIndexChanged);
            this.cboOptionGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboOptionGroup_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Group";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupEdit);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(633, 312);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Batch Create";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupEdit
            // 
            this.groupEdit.Controls.Add(this.tabPage7);
            this.groupEdit.Controls.Add(this.tabPage4);
            this.groupEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEdit.Location = new System.Drawing.Point(3, 3);
            this.groupEdit.Name = "groupEdit";
            this.groupEdit.SelectedIndex = 0;
            this.groupEdit.Size = new System.Drawing.Size(627, 306);
            this.groupEdit.TabIndex = 11;
            this.groupEdit.Visible = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.txtNotes);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(619, 280);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Notes";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(3, 3);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(613, 274);
            this.txtNotes.TabIndex = 7;
            this.txtNotes.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.groupClone);
            this.tabPage4.Controls.Add(this.groupTest);
            this.tabPage4.Controls.Add(this.groupAddAddresses);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(619, 280);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Create";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPokeAll);
            this.groupBox1.Controls.Add(this.btnCreateNewGroup);
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(248, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 80);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group options";
            // 
            // btnPokeAll
            // 
            this.btnPokeAll.Location = new System.Drawing.Point(229, 47);
            this.btnPokeAll.Name = "btnPokeAll";
            this.btnPokeAll.Size = new System.Drawing.Size(72, 23);
            this.btnPokeAll.TabIndex = 2;
            this.btnPokeAll.Text = "Poke All";
            this.btnPokeAll.UseVisualStyleBackColor = true;
            // 
            // btnCreateNewGroup
            // 
            this.btnCreateNewGroup.Location = new System.Drawing.Point(151, 47);
            this.btnCreateNewGroup.Name = "btnCreateNewGroup";
            this.btnCreateNewGroup.Size = new System.Drawing.Size(72, 23);
            this.btnCreateNewGroup.TabIndex = 2;
            this.btnCreateNewGroup.Text = "Create New Group";
            this.btnCreateNewGroup.UseVisualStyleBackColor = true;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(71, 21);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(230, 20);
            this.txtGroupName.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Group Name";
            // 
            // groupClone
            // 
            this.groupClone.Controls.Add(this.btnClone);
            this.groupClone.Controls.Add(this.label6);
            this.groupClone.Controls.Add(this.numStructSize);
            this.groupClone.Location = new System.Drawing.Point(248, 177);
            this.groupClone.Name = "groupClone";
            this.groupClone.Size = new System.Drawing.Size(307, 52);
            this.groupClone.TabIndex = 2;
            this.groupClone.TabStop = false;
            this.groupClone.Text = "Clone Group as Data Structure";
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(229, 19);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(72, 23);
            this.btnClone.TabIndex = 1;
            this.btnClone.Text = "Clone Addresses";
            this.btnClone.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Size of Structure";
            // 
            // numStructSize
            // 
            this.numStructSize.Location = new System.Drawing.Point(125, 23);
            this.numStructSize.Maximum = new decimal(new int[] {
            -1474836480,
            4,
            0,
            0});
            this.numStructSize.Name = "numStructSize";
            this.numStructSize.Size = new System.Drawing.Size(98, 20);
            this.numStructSize.TabIndex = 0;
            // 
            // groupTest
            // 
            this.groupTest.Controls.Add(this.label9);
            this.groupTest.Controls.Add(this.label8);
            this.groupTest.Controls.Add(this.numDelay);
            this.groupTest.Controls.Add(this.btnStartTest);
            this.groupTest.Location = new System.Drawing.Point(248, 92);
            this.groupTest.Name = "groupTest";
            this.groupTest.Size = new System.Drawing.Size(307, 83);
            this.groupTest.TabIndex = 8;
            this.groupTest.TabStop = false;
            this.groupTest.Text = "Test Values";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Next Address in: 10.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Delay Between Test (ms)";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(229, 19);
            this.numDelay.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(72, 20);
            this.numDelay.TabIndex = 0;
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(229, 49);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(72, 23);
            this.btnStartTest.TabIndex = 1;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            // 
            // groupAddAddresses
            // 
            this.groupAddAddresses.Controls.Add(this.cboListOption);
            this.groupAddAddresses.Controls.Add(this.btnCreate);
            this.groupAddAddresses.Controls.Add(this.txtBaseName);
            this.groupAddAddresses.Controls.Add(this.txtInitPokeValue);
            this.groupAddAddresses.Controls.Add(this.txtAddAddress);
            this.groupAddAddresses.Controls.Add(this.numCreateCount);
            this.groupAddAddresses.Controls.Add(this.numPadding);
            this.groupAddAddresses.Controls.Add(this.label17);
            this.groupAddAddresses.Controls.Add(this.label19);
            this.groupAddAddresses.Controls.Add(this.label20);
            this.groupAddAddresses.Controls.Add(this.label16);
            this.groupAddAddresses.Controls.Add(this.label13);
            this.groupAddAddresses.Controls.Add(this.label12);
            this.groupAddAddresses.Location = new System.Drawing.Point(6, 6);
            this.groupAddAddresses.Name = "groupAddAddresses";
            this.groupAddAddresses.Size = new System.Drawing.Size(239, 223);
            this.groupAddAddresses.TabIndex = 7;
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
            // txtAddAddress
            // 
            this.txtAddAddress.Location = new System.Drawing.Point(79, 49);
            this.txtAddAddress.Name = "txtAddAddress";
            this.txtAddAddress.Size = new System.Drawing.Size(142, 20);
            this.txtAddAddress.TabIndex = 0;
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.addressView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(647, 742);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "New View";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // addressView1
            // 
            this.addressView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addressView1.Location = new System.Drawing.Point(3, 3);
            this.addressView1.Name = "addressView1";
            this.addressView1.Size = new System.Drawing.Size(641, 736);
            this.addressView1.TabIndex = 0;
            // 
            // MemoryPatchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Name = "MemoryPatchControl";
            this.Size = new System.Drawing.Size(655, 768);
            this.Load += new System.EventHandler(this.MemoryPatchControl_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupAddressEdit.ResumeLayout(false);
            this.groupAddressEdit.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupEdit.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupClone.ResumeLayout(false);
            this.groupClone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStructSize)).EndInit();
            this.groupTest.ResumeLayout(false);
            this.groupTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.groupAddAddresses.ResumeLayout(false);
            this.groupAddAddresses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPadding)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupAddressEdit;
        private System.Windows.Forms.Button btnPoke;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAddressOptionGroup;
        private System.Windows.Forms.CheckBox chkLocked;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCurrentValue;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnIncValue;
        private System.Windows.Forms.ListBox lstOptions;
        private System.Windows.Forms.TextBox txtOption;
        private System.Windows.Forms.TextBox txtOptValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOptionGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTestAddress;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabControl groupEdit;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPokeAll;
        private System.Windows.Forms.Button btnCreateNewGroup;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupClone;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numStructSize;
        private System.Windows.Forms.GroupBox groupTest;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.GroupBox groupAddAddresses;
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
        private System.Windows.Forms.TabPage tabPage5;
        private AddressView addressView1;
    }
}
