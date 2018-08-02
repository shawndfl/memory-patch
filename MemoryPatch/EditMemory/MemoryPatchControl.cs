using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using MemoryManager;

namespace MemoryPatch.Editing_Memory
{
    public partial class MemoryPatchControl : UserControl
    {
        #region Fields
        private AddressManager _addressManager;
        private IMemoryAccess _access;
        private TreeNode _root;        
        private bool _updating = false;

        private bool _selectFirstAddress = true;
        private long _firstLastAddress = 0;
        private long _secondLastAddress = 0;
        private FrmAddNew _frmAddNew;

        #endregion

        #region Properties
        private SavedAddress ActiveAddress
        {
            get
            {
                if (tv.SelectedNode == null)
                    return null;
                else
                    return tv.SelectedNode.Tag as SavedAddress;
            }
        }

        private GroupAddress ActiveGroup
        {
            get
            {
                if (tv.SelectedNode == null)
                    return null;
                else
                    return tv.SelectedNode.Tag as GroupAddress;
            }
        }       
        
        #endregion                

        public MemoryPatchControl()
        {
            InitializeComponent();
            _root = tv.Nodes.Add("Addresses");
            _addressManager = new AddressManager();

            string[] datatypes = Enum.GetNames(typeof(DataType));
            foreach (string type in datatypes)
            {
                cboDataType.Items.Add(type);
            }

            cboAssign.Items.Add("Equal");
            cboAssign.Items.Add("Or In");
            cboAssign.Items.Add("Or Out");
            cboAssign.SelectedIndex = 0;

            MenuItem[] items = new MenuItem[]
            {
                new MenuItem("New Group", new EventHandler(OnNewGroup)),
                new MenuItem("Add New...", new EventHandler(OnNewDialog))
            };

            ContextMenu = new ContextMenu(items);            
        }
        
        private void OnNewDialog(object sender, EventArgs e)
        {
            if (_frmAddNew == null || _frmAddNew.IsDisposed)
            {
                _frmAddNew = new FrmAddNew();
                _frmAddNew.Show();
            }
        }

        private void OnNewGroup(object sender, EventArgs e)
        {
            tv.SelectedNode = CreateNewGroup();
        }

        /// <summary>
        /// Enables the memory access.
        /// </summary>
        /// <param name="access">The access.</param>
        public void EnableMemoryAccess(IMemoryAccess access)
        {
            if (access == null)
                throw new ArgumentNullException("access");

            _access = access;

            SetupPluginLoader();

            tabControl1.Enabled = true;
            tv.Enabled = true;
        }

        private void SetupPluginLoader()
        {
            //setup plugin loader
            PluginManager plugin = new PluginManager(_addressManager, _access);
            pluginLoader1.Init(plugin, _addressManager);
        }       

        #region Save and Open
        public void Open(string file)
        {            
            _addressManager = new AddressManager(file);

            SetupPluginLoader();

            RefeshTree();
        }
      
        public void Save(string file)
        {
            _addressManager.Save(file);        
        }
        #endregion

        #region Add and Remove Addresses
        public TreeNode AddAddress(SavedAddress address)
        {
            TreeNode defaultGroup = GetDefaultGroupNode();

            return AddAddress(defaultGroup, address);
        }      

        public TreeNode AddAddress(TreeNode groupNode, SavedAddress address)
        {
            try
            {
                //add the address            
                _addressManager.AddAddress(groupNode.Text, address);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            //create the address node
            TreeNode addressNode = new TreeNode(address.TreeNodeText(GetValue(address)));
            addressNode.Tag = address;

            //find index for add
            int index = 0;
            foreach (TreeNode childAddress in groupNode.Nodes)
            {
                SavedAddress addressChild = childAddress.Tag as SavedAddress;
                if (address.Address < addressChild.Address)
                {
                    break;
                }
                index++;
            }

            //add the address node to the group
            if(groupNode.Nodes.Count == 0)
                groupNode.Nodes.Add(addressNode);
            else
                groupNode.Nodes.Insert(index, addressNode);

            groupNode.Expand();

            return addressNode;
        }

        /// <summary>
        /// Removes the whole group
        /// </summary>
        /// <param name="addressNode"></param>
        public void RemoveGroup(TreeNode groupNode)
        {
            GroupAddress group = groupNode.Tag as GroupAddress;
            if (groupNode != null)
            {
                for (int i = 0; i < groupNode.Nodes.Count; i++)
                {
                    TreeNode  node = groupNode.Nodes[i];
                    RemoveAddress(node);
                    i--;
                }                
            }
            
            _addressManager.RemoveGroup(group.Name);
            groupNode.Remove();
        }

        /// <summary>
        /// Removes an address
        /// </summary>
        /// <param name="addressNode"></param>
        public void RemoveAddress(TreeNode addressNode)
        {
            SavedAddress address = addressNode.Tag as SavedAddress;
            if (addressNode != null)
            {
                TreeNode parent = addressNode.Parent;
                GroupAddress group = parent.Tag as GroupAddress;

                //remove from freeze list
                _access.UnfreezeMemory(new IntPtr(address.Address));

                group.RemoveAddress(address);
                parent.Nodes.Remove(addressNode);
            }
        }

        #endregion                

        #region Tree node events
        public void LockAddress(SavedAddress address)
        {
            address.Locked = true;
        }

        private void tv_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode node = tv.GetNodeAt(p);
            if (node != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _updating = true;
                    tv.SelectedNode = node;
                    _updating = false;
                    DoDragDrop(node, DragDropEffects.Move);
                    SelectNode(node);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    _updating = true;
                    tv.SelectedNode = node;
                    _updating = false;

                    ContextMenu.Show(tv, p);

                    //if (ActiveAddress != null)
                    //{
                    //    ContextMenu.Show(tv, p);

                        //MemoryViewer viewer = new MemoryViewer();
                        //viewer.StartAddress(ActiveAddress.Address, _access);
                        //viewer.Show();
                        //MessageBox.Show("Memory viewer not implemented.");
                    //}
                }
            }

        }       

        private void tv_DragOver(object sender, DragEventArgs e)
        {
            Point abs = new Point(e.X, e.Y);
            Point p = tv.PointToClient(abs);
            TreeNode destNode = tv.GetNodeAt(p);
            TreeNode selectedNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (selectedNode != null)
            {
                SavedAddress address = selectedNode.Tag as SavedAddress;
                if (address != null)
                {
                    _updating = true;
                    if (destNode != null)
                        tv.SelectedNode = destNode;
                    _updating = false;

                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void tv_DragDrop(object sender, DragEventArgs e)
        {
            Point abs = new Point(e.X, e.Y);
            Point p = tv.PointToClient(abs);
            TreeNode destNode = tv.GetNodeAt(p);
            TreeNode srcNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

            if (srcNode != null && destNode != null)
            {
                SavedAddress address = srcNode.Tag as SavedAddress;
                if (address != null)
                {
                    if (destNode.Tag is GroupAddress)
                    {
                        //add to group
                        RemoveAddress(srcNode);

                        GroupAddress group = destNode.Tag as GroupAddress;
                        AddAddress(destNode, address);

                    }                    
                                        
                }
            }            
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_updating)
                return;

            SelectNode(e.Node);        
        }
        #endregion

        #region Refesh GUI
        private void SelectNode(TreeNode node)
        {
            _updating = true;
            tv.SelectedNode = node;
            _updating = false;
            UpdateGUI(); 
        }

        private void UpdateGUI()
        {
            if (ActiveAddress == null && ActiveGroup != null)
            {
                groupAddressEdit.Visible = false;
                groupEdit.Visible = true;
                
                txtNotes.Text = ActiveGroup.Notes;
                numStructSize.Value = Math.Abs(_secondLastAddress - _firstLastAddress);
            }
            else if (ActiveAddress != null && ActiveGroup == null)
            {
                SavedAddress address = ActiveAddress;
                groupAddressEdit.Visible = true;
                groupEdit.Visible = false;

                if (address.StringValue == null)
                    address.StringValue = GetValue(address);

                txtDescription.Text = address.Name;
                txtAddress.Text = address.StringAddress;
                txtValue.Text = address.StringValue;
                cboDataType.Text = address.DataType.ToString();
                chkLocked.Checked = address.Locked;
                txtCurrentValue.Text = GetValue(address);
                cboValue.SelectedItem = CheckForOptions(address, txtCurrentValue.Text);
                cboAddressOptionGroup.Text = address.OptionList;

                //this is used to make creating nodes easier
                txtInitPokeValue.Text = address.StringValue;
                cboListOption.Text = address.OptionList;
                txtBaseName.Text = address.Name;
                txtAddAddress.Text = address.StringAddress;

                //this is used to calculate the difference between addresses
                if (_selectFirstAddress)
                    _firstLastAddress = address.Address;
                else
                    _secondLastAddress = address.Address;
                _selectFirstAddress = !_selectFirstAddress;
            }
            else
            {
                groupAddressEdit.Visible = false;
                groupEdit.Visible = false;
            }
        }

        private string GetValue(SavedAddress address)
        {           
            return _access.ReadMemoryAsString(new IntPtr(address.Address),
                          address.DataType, address.DataLengthInBytes);
        }
        #endregion

        #region Edit Saved Address
        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ActiveAddress.Name = txtDescription.Text;                              
            }                        
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && ActiveAddress != null)
            {

                try
                {
                    ActiveAddress.Address = int.Parse(txtAddress.Text, NumberStyles.AllowHexSpecifier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Not a valid address " + txtAddress.Text);
                    txtAddress.SelectAll();
                    return;
                }
                ActiveAddress.StringValue = txtValue.Text;                
            }
        }

        private void cboDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveAddress != null)
                ActiveAddress.DataType = (DataType)Enum.Parse(typeof(DataType), cboDataType.Text);         
        }

        private void chkLocked_CheckedChanged(object sender, EventArgs e)
        {
            if(ActiveAddress != null)
                ActiveAddress.Locked = chkLocked.Checked;            
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (_updating)
                return;

            if (e.KeyCode == Keys.Return && ActiveAddress != null)
            {
                AcceptNewValue();
            }
            else if (e.KeyCode == Keys.Up)
            {
                double val = 0;
                try
                {
                    val = double.Parse(txtValue.Text);
                }
                catch (Exception ex)
                {
                    val = 0;
                }

                val+=1.0;
                txtValue.Text = val.ToString();

                AcceptNewValue();

            }
            else if (e.KeyCode == Keys.Down)
            {
                long val = 0;
                try
                {
                    val = long.Parse(txtValue.Text);
                }
                catch (Exception ex)
                {
                    val = 0;
                }

                val--;
                txtValue.Text = val.ToString();

                AcceptNewValue();

            }
        }

        private void AcceptNewValue()
        {
            ActiveAddress.StringValue = txtValue.Text;            
            _access.WriteValue(new IntPtr(ActiveAddress.Address), ActiveAddress.Value);

            //in case there was an error in the text get the latest value
            _updating = true;
            txtValue.Text = ActiveAddress.StringValue;

            //set the selected option
            foreach (Option option in cboValue.Items)
            {
                if (option.Value == txtValue.Text)
                {
                    cboValue.SelectedItem = option;
                    break;
                }
            }
            _updating = false;
        }

        private void cboValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cboValue.SelectedIndex;
            if (_updating || i ==-1)
                return;

            _updating = true;       
     
            txtValue.Text = ((Option)cboValue.Items[i]).Value;
            if (cboAssign.Text == "Equal")
            {
                ActiveAddress.StringValue = txtValue.Text;
            }            
                //ActiveAddress.Value |= txtValue.Text;

            _access.WriteValue(new IntPtr(ActiveAddress.Address), ActiveAddress.Value);
            _updating = false;
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveAddress != null)
            {
                ActiveAddress.OptionList = cboAddressOptionGroup.Text;
                RefeshAddressOptionList();
            }
        }

        private void txtNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveGroup != null)
            {
                ActiveGroup.Notes = txtAddress.Text;
            }
        }
        #endregion

        #region Refesh Tree

        private void RefeshTree()
        {
            cboAddressOptionGroup.Items.Clear();
            cboValue.Items.Clear();
            cboOptionGroup.Items.Clear();
            lstOptions.Items.Clear();

            _addressManager.FillInTree(_root);

            RefeshOptionGroups();            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_access == null)
                return;

            foreach (TreeNode groupNode in _root.Nodes)
            {
                RefeshAddressNode(groupNode);
            }
        }

        private void RefeshAddressNode(TreeNode groupNode)
        {
            foreach (TreeNode addressNode in groupNode.Nodes)
            {
                SavedAddress address = (SavedAddress)addressNode.Tag;

                if (address.Locked)
                    _access.FreezeMemory(new IntPtr(address.Address), address.Value);
                else
                    _access.UnfreezeMemory(new IntPtr(address.Address));

                if (groupNode.IsExpanded)
                {
                    string text = address.TreeNodeText(CheckForOptionsValue(address, GetValue(address)));
                    if (addressNode.Text != text)
                        addressNode.Text = text;
                }
            }
        }

        private Option CheckForOptions(SavedAddress address, string value)
        {
            Option option = _addressManager.GetOptionByValue(address.OptionList, value);
            if (option != null)
                return option;
            else
                return null;

        }

        private string CheckForOptionsValue(SavedAddress address, string value)
        {
            Option option = _addressManager.GetOptionByValue(address.OptionList, value);
            if (option != null)
                return option.Item;
            else
                return value;

        }
        #endregion       

        #region Option Functions and Events

        private void cboOptionGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                _addressManager.AddOptionGroup(cboOptionGroup.Text);
                RefeshOptionGroups();
                txtOption.Focus();
            }
        }

        private void cboOptionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefeshOptionList();
        }

        private void RefeshOptionGroups()
        {
            cboOptionGroup.Items.Clear();
            cboOptionGroup.Items.Add("");
            cboOptionGroup.Items.AddRange(_addressManager.GetOptionGroups());
            cboAddressOptionGroup.Items.Clear();
            cboAddressOptionGroup.Items.Add("");
            cboAddressOptionGroup.Items.AddRange(_addressManager.GetOptionGroups());
            cboListOption.Items.Clear();
            cboListOption.Items.Add("");
            cboListOption.Items.AddRange(_addressManager.GetOptionGroups());
            
        }

        private void RefeshOptionList()
        {
            List<Option> options = _addressManager.GetOptions(cboOptionGroup.Text);

            options.Sort();
            lstOptions.Items.Clear();           
            foreach (Option option in options)
            {
                lstOptions.Items.Add(option);                
            }                        
        }

        private void RefeshAddressOptionList()
        {
            List<Option> options = _addressManager.GetOptions(cboAddressOptionGroup.Text);

            options.Sort();
            cboValue.Items.Clear();                        
            foreach (Option option in options)
            {
                cboValue.Items.Add(option);
            }
        }       

        public void AddOption(string groupName, string optionName, string value)
        {
            try
            {
                Option option;
                if (_addressManager.AddOption(groupName, optionName, value, out option))
                {
                    lstOptions.Items.Add(option);
                }
                else
                {
                    int index = lstOptions.Items.IndexOf(option);
                    lstOptions.Items.RemoveAt(index);
                    lstOptions.Items.Insert(index, option);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtOption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                NextValue();
            }
        }

        private void btnIncValue_Click(object sender, EventArgs e)
        {
            NextValue();
        }

        private void NextValue()
        {
            long value;
            try
            {
                if (txtOptValue.Text.ToLower().StartsWith("0x"))
                    value = Int64.Parse(txtOptValue.Text.Substring(2), NumberStyles.AllowHexSpecifier);
                else
                    value = Int64.Parse(txtOptValue.Text);
            }
            catch (Exception)
            {
                value = 0;
            }           

            string strValue = value.ToString();            

            //add option
            AddOption(cboOptionGroup.Text, txtOption.Text, strValue);

            //increase value
            value++;            
            txtOptValue.Text = value.ToString();
            
            try
            {
                if (txtOptValue.Text.ToLower().StartsWith("0x"))
                    value = Int64.Parse(txtOptValue.Text.Substring(2), NumberStyles.AllowHexSpecifier);
                else
                    value = Int64.Parse(txtOptValue.Text);
            }
            catch (Exception)
            {
                value = 0;
            }

            strValue = value.ToString();    

            if (ActiveAddress != null)
                _access.WriteValue(new IntPtr(ActiveAddress.Address),
                    ActiveAddress.GetByteValue(strValue));

            txtOption.SelectAll();
            txtOption.Focus();
           
        }

        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstOptions.SelectedIndex;
            if (i > -1)
            {
                Option option = lstOptions.Items[i] as Option;
                txtOption.Text = option.Item;
                txtOptValue.Text = option.Value;
            }
        }
        #endregion                     

        #region Address Group 
        private void btnCreateNewGroup_Click(object sender, EventArgs e)
        {
            tv.SelectedNode = CreateNewGroup();
        }        

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            if (ActiveGroup != null)
                ActiveGroup.Notes = txtNotes.Text;
        }          

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (ActiveGroup != null)
            {
                if (txtAddAddress.Text == "")
                {
                    MessageBox.Show("Enter in an address.");
                    return;
                }
                int address = Int32.Parse(txtAddAddress.Text, NumberStyles.AllowHexSpecifier);

                for (int i = 0; i < numCreateCount.Value; i++)
                {
                    TreeNode groupNode = tv.SelectedNode; 
                    TreeNode addressNode = CreateNewAddress(groupNode, txtBaseName.Text, address);
                    if ( addressNode == null)
                        break;

                    //set the option list
                    SavedAddress newAddress = addressNode.Tag as SavedAddress;
                    newAddress.OptionList = cboListOption.Text;

                    //set the initial poke value
                    newAddress.StringValue = txtInitPokeValue.Text;

                    address += (int)numPadding.Value;
                }
            }
        }

        private TreeNode GetGroupNode(string groupName)
        {
            foreach (TreeNode group in _root.Nodes)
            {
                if (group.Text == groupName)
                    return group;
            }
            return null;
        }

        private TreeNode GetDefaultGroupNode()
        {
            string group = "Default";            

            //update the tree
            TreeNode groupNode = GetGroupNode(group);
            if (groupNode == null)
            {
                groupNode = new TreeNode(group);
                GroupAddress groupAddress = _addressManager.GetGroup(group);
                if (groupAddress == null)
                    groupAddress = _addressManager.AddNewGroup(group);

                groupNode.Tag = groupAddress;
                _root.Nodes.Add(groupNode);
                _root.Expand();
            }
            return groupNode;
        }

        private TreeNode CreateNewGroup()
        {
            string baseName = "New Group";
            string fullName = baseName;
            bool found = true;
            int i = 1;
            while (found)
            {
                found = false;
                foreach (TreeNode node in _root.Nodes)
                {
                    if (node.Text == fullName)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    fullName = baseName + "(" + i + ")";
                    i++;
                }
            }
            GroupAddress group = _addressManager.AddNewGroup(fullName);
            
            TreeNode newNode= _root.Nodes.Add(fullName);
            newNode.Tag = group;

            return newNode;
        }

        private TreeNode CreateNewAddress(TreeNode groupNode, string baseName, int address)
        {            
            string fullName = baseName;
            bool found = true;
            int i = 2;
            SavedAddress savedAddress;

            //get a new name for the saved address
            while (found)
            {
                found = false;
                foreach (TreeNode node in groupNode.Nodes)
                {
                    savedAddress = node.Tag as SavedAddress;

                    //check for the same address
                    //if (savedAddress.Address == address)
                    //{
                    //    MessageBox.Show("Address already added to group " + groupNode.Text);
                    //    return null;
                    //}

                    //check for the same name
                    if (savedAddress.Name == fullName)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    fullName = baseName + "(" + i + ")";
                    i++;
                }
            }

            savedAddress = new SavedAddress(false, fullName, address, DataType.UByte);

            return AddAddress(groupNode, savedAddress);
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            int offset =(int)numStructSize.Value;
            if (ActiveGroup != null)
            {
                TreeNode newGroupNode = CreateNewGroup();
                foreach (TreeNode addressNode in tv.SelectedNode.Nodes)
                {
                    SavedAddress address = addressNode.Tag as SavedAddress;
                    SavedAddress newAddress = address.CloneAddress();
                    newAddress.Address += offset;

                    AddAddress(newGroupNode, newAddress);
                }
            }
        }

        private void btnPokeAll_Click(object sender, EventArgs e)
        {
            if (ActiveGroup != null)
            {              
                foreach (TreeNode addressNode in tv.SelectedNode.Nodes)
                {
                    SavedAddress address = addressNode.Tag as SavedAddress;
                    _access.WriteValue(new IntPtr(address.Address), address.Value);                    
                }
            }
        }  

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (ActiveGroup != null && tv.SelectedNode.FirstNode != null)
            {
                SavedAddress address = tv.SelectedNode.FirstNode.Tag as SavedAddress;

            }
        }
        
        #endregion                     

        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Import(string file)
        {

            StreamReader r = null;
            string line;
            //try
            //{
            r = new StreamReader(file);
            line = r.ReadLine();

            while (line != null)
            {
                string[] lineSplit = line.Split(new char[] { ';' });

                if (!line.StartsWith("-"))
                {
                    SavedAddress savedAddress = new SavedAddress(
                                          bool.Parse(lineSplit[0]),
                                          lineSplit[1],
                                          lineSplit[2],
                                          lineSplit[3],
                                          (DataType)Enum.Parse(typeof(DataType), lineSplit[4]));
                    AddAddress(savedAddress);
                }               

                line = r.ReadLine();
            }
        }   

        private void txtValue_Enter(object sender, EventArgs e)
        {
            txtValue.SelectAll();
        }

        private void txtOption_MouseUp(object sender, MouseEventArgs e)
        {
            txtOption.SelectAll();
        }

        private void MemoryPatchControl_Load(object sender, EventArgs e)
        {

        }

        private void btnPoke_Click(object sender, EventArgs e)
        {
            if (ActiveAddress != null)
            {
                AcceptNewValue();
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the tv control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void tv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                tv.LabelEdit = true;
                if(tv.SelectedNode != null)
                    tv.SelectedNode.BeginEdit();
            }
        }

        private void tv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (ActiveAddress != null)
                {
                    RemoveAddress(tv.SelectedNode);
                }
                else if (ActiveGroup != null)
                {
                    RemoveGroup(tv.SelectedNode);
                }
            }
        }

        private void tv_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string newText = e.Label;
            e.Node.Text = newText;
            tv.LabelEdit = false;           
                 
            if (ActiveGroup != null)
            {               
                _addressManager.RenameGroup(ActiveGroup.Name, newText);                
            }
            else
            {
                e.CancelEdit = true;
            }
        }            
    }
}
