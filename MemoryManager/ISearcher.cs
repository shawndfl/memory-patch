using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;

namespace MemoryManager
{
    public interface ISearcher
    {
        SearchContext SearchContext { get; }

        void CancelSearch();

        void NewSearch(SearchContext context);

        void NextSearch(SearchType type, string optValue);                 

        void SaveSnapShot(string file);

        void LoadSnapShot(string file);       
    }    
}
