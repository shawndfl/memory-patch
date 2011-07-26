using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryPatch
{
    public class SearchUpdateEventArgs : EventArgs
    {
        public int PrecentDone { get; private set; }
        public int AddressFoundCount { get; private set; }

        public SearchUpdateEventArgs(int startAddress, int endAddress, int currentAddress, int addressFoundCount)
        {
            float dx = endAddress - startAddress;
            float dy = currentAddress - startAddress;
            PrecentDone = (int)((float)(dy / dx) * 100.0f);
            AddressFoundCount = addressFoundCount;
        }
    }
}
