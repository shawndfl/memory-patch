using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    internal struct AddressLock
    {
        public int Address;
        public byte[] Data;

        public AddressLock(int address, byte[] data)
        {
            Address = address;
            Data = data;
        }
    }
}
