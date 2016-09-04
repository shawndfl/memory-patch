using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager.Search
{
    /// <summary>
    /// Type of search
    /// </summary>
    public enum SearchType
    {
        Excat,
        UnKnown,
        HasIncreased,
        HasDecreased,
        HasNotChanged,
        HasChanged,
        HasDecreasedBy,
        HasIncreasedBy       
    }
}
