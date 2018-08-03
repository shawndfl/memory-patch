using MemoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryPatch.EditMemory
{
    interface IMemoryControl
    {
        void Import(string file);

        void Save(String file);

        void Open(String file);

        void EnableMemoryAccess(IMemoryAccess address);

        void AddAddress(SavedAddress savedAddress);


    }
}
