using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// This is used to give an update to the main thread.
    /// </summary>
    public class SearchUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// The precentage of addresses searched
        /// </summary>
        public int PrecentDone { get; private set; }

        /// <summary>
        /// How many addresses found
        /// </summary>
        public int AddressFoundCount { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="startAddress">first address</param>
        /// <param name="endAddress">last address</param>
        /// <param name="currentAddress">the current address being processed</param>
        /// <param name="addressFoundCount">how many are found</param>
        public SearchUpdateEventArgs(long startAddress, long endAddress, long currentAddress, int addressFoundCount)
        {
            float dx = endAddress - startAddress;
            float dy = currentAddress - startAddress;
            PrecentDone = (int)((float)(dy / dx) * 100.0f);
            AddressFoundCount = addressFoundCount;
        }
    }
}
