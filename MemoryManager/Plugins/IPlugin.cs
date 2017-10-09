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
        /// <summary>
        /// Gets the address manager. This is needed for plugin loader and the plugins themselves.
        /// </summary>
        private AddressManager _addressManager; 

        private IMemoryAccess _access;

        public bool PokeValue(string groupName, string addressName, string value)
        {
            SavedAddress address = _addressManager.FindAddress(groupName, addressName);
            if (address != null)
            {
                byte[] byteValue = SavedAddress.GetByteValue(value, address.DataType);
                _access.WriteValue(new IntPtr(address.Address), byteValue);
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
                return _access.ReadMemoryAsString(new IntPtr(address.Address), address.DataType, address.DataLengthInBytes);
            }
            
            return string.Empty;
        }

        public void LockValue()
        {

        }

        public PluginManager(AddressManager addressManager, IMemoryAccess access)
        {
            _addressManager = addressManager;
            _access = access;
        }
    }
}

