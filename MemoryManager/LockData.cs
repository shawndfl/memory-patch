using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// Data used to keep an address at a value
    /// </summary>
    public struct LockData
    {
        #region Fields
        private int _address;        
        private byte[] _data;
        #endregion 

        #region Properties
        public int Address
        {
            get { return _address; }
        }
        public byte[] Data
        {
            get { return _data; }
        }
        #endregion

        #region Constructor
        public LockData(int address, byte[] data)
        {
            _address = address;
            _data = data;
        }
        #endregion

    }
}
