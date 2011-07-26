using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryPatch
{
    public struct LockData
    {
        private int _address;        
        private byte[] _data;

        public int Address
        {
            get { return _address; }
        }
        public byte[] Data
        {
            get { return _data; }            
        }
        

        public LockData(int address, byte[] data)
        {
            _address = address;
            _data = data;            
        }
    }
}
