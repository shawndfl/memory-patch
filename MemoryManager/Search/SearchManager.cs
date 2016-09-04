using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager.Search
{
    /// <summary>
    /// Manages the memory searches
    /// </summary>
    public static class SearchManager
    {        

        /// <summary>
        /// Creates a searcher
        /// </summary>
        /// <returns></returns>
        public static ISearcher CreateSearcher()
        {
            ISearcher searcher = new SearcherImp();
            return searcher;
        }

        /// <summary>
        /// Creates a context
        /// </summary>
        /// <returns></returns>
        public static ISearchContext CreateSearchContext()
        {
            ISearchContext context = new SearchContextImp();
            return context;
        }

    }
}
