using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// These are groups of addresses that have something in common
    /// </summary>
    public class GroupAddress
    {
        /// <summary>
        /// The name of the group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Notes for the group
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// List of addresses
        /// </summary>
        private List<SavedAddress> _addresses =
            new List<SavedAddress>();

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saved"></param>
        public GroupAddress(SaveGroupData saved)
        {
            Name = saved.Name;
            Notes = saved.Notes;
            foreach (SavedAddress address in saved.Addresses)
            {
                AddAddress(address);
            }
        }
        #endregion

        /// <summary>
        /// Gets a copy of addresses
        /// </summary>
        /// <returns></returns>
        public List<SavedAddress> GetListOfAddresses()
        {
            List<SavedAddress> addresses = new List<SavedAddress>(_addresses);
            return addresses;
        }

        /// <summary>
        /// Adds an address to a group
        /// </summary>
        /// <param name="address"></param>
        internal void AddAddress(SavedAddress address)
        {           
            _addresses.Add(address);
        }

        /// <summary>
        /// Removed and address. This needs to be the same object that is in the list.
        /// </summary>
        /// <param name="address"></param>
        public void RemoveAddress(SavedAddress address)
        {
            _addresses.Remove(address);
        }

        /// <summary>
        /// Clears the addresses
        /// </summary>
        public void ClearAddress()
        {
            _addresses.Clear();
        }

        /// <summary>
        /// Gets the group name
        /// </summary>
        /// <param name="name"></param>
        public GroupAddress(string name)
        {
            Name = name;
        }
    }
}
