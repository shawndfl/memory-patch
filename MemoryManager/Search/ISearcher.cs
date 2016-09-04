using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;

namespace MemoryManager.Search
{
    /// <summary>
    /// Used for searching memory
    /// </summary>
    public interface ISearcher
    {
        /// <summary>
        /// Gets the context being used
        /// </summary>
        ISearchContext SearchContext { get; }

        /// <summary>
        /// Sets a listener for search events
        /// </summary>
        /// <param name="listener"></param>
        void SetSearchListener(ISearchListener listener);

        /// <summary>
        /// Cancels the search
        /// </summary>
        void CancelSearch();

        /// <summary>
        /// Does the search
        /// </summary>
        /// <param name="context"></param>
        void Search(ISearchContext context);        

        /// <summary>
        /// Saves the search context to a file
        /// </summary>
        /// <param name="file"></param>
        void SaveContext(string file);

        /// <summary>
        /// Loads a search context
        /// </summary>
        /// <param name="file"></param>
        void LoadContext(string file);       
    }    
}
