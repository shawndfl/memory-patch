using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// This factory will create the IMemoryAccess object
    /// </summary>
    public class MemoryAccessFactory
    {
        /// <summary>
        /// Creates memory access object
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IMemoryAccess CreateMemoryAccess(Process p)
        {
            return new MemoryAccess(p);
        }
    }
}
