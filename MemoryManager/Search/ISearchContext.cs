using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager.Search
{
    public interface ISearchContext
    {
        /// <summary>
        /// The type of search we are using
        /// </summary>
        SearchType SearchType { get; }        

        /// <summary>
        /// The Bytes used to compare memory
        /// </summary>
        byte[] TargetBytes { get; }

        /// <summary>
        /// The byte length of the data type
        /// </summary>
        int DataLength { get; }

        /// <summary>
        /// Data type used in this search
        /// </summary>
        DataType DataType { get; }

        /// <summary>
        /// Custom parameters for searching
        /// </summary>
        List<String> Parameters { get; }

        /// <summary>
        /// Addresses that are to be searched next
        /// </summary>
        List<long> FoundAddresses { get; }

        /// <summary>
        ///  Gets the context ready for the next search
        /// </summary>
        /// <param name="TargetBytes"></param>
        /// <param name="searchType"></param>
        /// <param name="dataType"></param>
        /// <param name="parameters"></param>
        void ConfigureNextSearch(byte[] TargetBytes, SearchType searchType, DataType dataType, List<String> parameters);

        /// <summary>
        /// Resets found addresses
        /// </summary>
        void ResetFoundAddresses();    
        
            
    }
}
