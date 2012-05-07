using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryManager
{
    /// <summary>
    /// The interface a plugin will use
    /// </summary>
    public interface IPlugin
    {
        Control Control { get; }

        void Init(PluginManager manager);

    }

    public class PluginManager
    {
        private AddressManager _addressManager;
        private MemoryAccess _access;

        public bool PokeValue(string groupName, string addressName, string value)
        {
            SavedAddress address = _addressManager.FindAddress(groupName, addressName);
            if (address != null)
            {
                _access.WriteValue(address.Address, address.Value);
                return true;
            }
            else 
                return false;
        }

        /// <summary>
        /// Reads the value.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="addressName">Name of the address.</param>
        public string ReadValue(string groupName, string addressName)
        {
            SavedAddress address = _addressManager.FindAddress(groupName, addressName);
            if (address != null)
            {
                return address.StringValue;
            }
            
            return string.Empty;
        }

        public void LockValue()
        {

        }

        public PluginManager(AddressManager addressManager, MemoryAccess access)
        {
            _addressManager = addressManager;
            _access = access;
        }
    }
}

