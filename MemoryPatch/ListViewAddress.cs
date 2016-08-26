using MemoryManager;
using System;
using System.Windows.Forms;

namespace MemoryPatch
{
    /// <summary>
    /// A list view with columns that are used for address view
    /// </summary>
    public partial class ListViewAddress : ListView
    {
        /// <summary>
        /// The columns for the view
        /// </summary>
        public enum ColumnsNames
        {
            Frozen,
            Name,
            Address,
            DataType,
            Value,
            PokeValue
        }

        private AddressSorter _sorter = new AddressSorter();

        public ListViewAddress()
        {            
            InitializeComponent();

            Columns.Add(ColumnsNames.Frozen.ToString());
            Columns.Add(ColumnsNames.Name.ToString());
            Columns.Add(ColumnsNames.Address.ToString());
            Columns.Add(ColumnsNames.DataType.ToString());
            Columns.Add(ColumnsNames.Value.ToString());
            Columns.Add(ColumnsNames.PokeValue.ToString());

            this.View = View.Details;

            this.ListViewItemSorter = _sorter;            
        }

        /// <summary>
        /// Adds an address to the list view
        /// </summary>
        /// <param name="address"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public void AddAddress(SavedAddress address, String groupName)
        {
            ListViewItem newItem = new ListViewItem();

            //try and find the group
            ListViewGroup selectedGroup = null;
            foreach (ListViewGroup group in this.Groups)
            {
                if (group.Header == groupName)
                {
                    selectedGroup = group;
                    break;
                }
            }

            //create a group
            if (selectedGroup == null)
            {
                selectedGroup = new ListViewGroup(groupName);
                this.Groups.Add(selectedGroup);
            }

            //create item for address            
            newItem.Group = selectedGroup;

            newItem.Checked = address.Locked;
            newItem.SubItems.Add(address.Name);
            newItem.SubItems.Add(address.StringAddress);
            newItem.SubItems.Add(address.DataType.ToString());
            newItem.SubItems.Add("0");
            newItem.SubItems.Add(address.StringValue);

            this.Items.Add(newItem);
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (_sorter.Column == e.Column)
            {
                _sorter.CycleState();
            }
            else
            {
                _sorter.State = AddressSorter.SorterState.Assend;
                _sorter.Column = e.Column;
            }
            this.Sort();

            base.OnColumnClick(e);

        }
    }
}
