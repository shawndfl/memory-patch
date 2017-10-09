using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    internal struct AddressLock
    {
        public IntPtr Address;
        public byte[] Data;

        public AddressLock(IntPtr address, byte[] data)
        {
            Address = address;
            Data = data;
        }
    }
}
