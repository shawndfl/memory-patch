using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public GroupAddress(string name)
        {
            Name = name;
        }
        #endregion

        internal ReadOnlyCollection<SavedAddress> GetListOfAddresses()
        {           
            return _addresses.AsReadOnly();
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
        /// Removes an address from a group
        /// </summary>
        /// <param name="address"></param>
        public void RemoveAddress(SavedAddress address)
        {
            _addresses.Remove(address);
        }

        /// <summary>
        /// Removes all addresses from this group
        /// </summary>
        public void ClearAddress()
        {
            _addresses.Clear();
        }

        public override string ToString()
        {
            return Name + "(" + _addresses.Count + ")";
        }
    }
}
