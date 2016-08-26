using MemoryManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MemoryPatch
{
    public partial class AddressView : UserControl
    {
        private ListViewAddress _listView;
        private AddressSorter _sorter;

        public AddressView()
        {
            InitializeComponent();
            _listView = lstViewAddress1;
            _sorter = new AddressSorter();
            //_listView.ListViewItemSorter = _sorter;
        }

        /// <summary>
        /// Loads the addresses for the list. It will also create the groups if needed.
        /// </summary>
        /// <param name="groups"></param>
        public void LoadAddresses(Dictionary<string, GroupAddress> groups)
        {
            _listView.Items.Clear();

            foreach (GroupAddress group in groups.Values)
            {               
                foreach (SavedAddress address in group.GetListOfAddresses())
                {
                    _listView.AddAddress(address, group.Name);
                }                
            }
        }               
    }
}
