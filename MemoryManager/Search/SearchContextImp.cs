using MemoryManager.Util;
using System.Collections.Generic;

namespace MemoryManager.Search
{
    /// <summary>
    /// The search context for a search
    /// </summary>
    class SearchContextImp : ISearchContext
    {
        private byte[] _targetBytes;
        private SearchType _searchType;
        private List<string> _parameters;
        private int _dataLength;
        private DataType _dataType;
        private List<long> _foundAddresses;

        /// <summary>
        /// Size of the data in bytes
        /// </summary>
        public int DataLength
        {
            get
            {
                return _dataLength;
            }
        }

        /// <summary>
        /// Type of data
        /// </summary>
        public DataType DataType
        {
            get
            {
                return _dataType;
            }
        }

        /// <summary>
        /// String parametes use in the search
        /// </summary>
        public List<string> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        /// <summary>
        /// Type of search
        /// </summary>
        public SearchType SearchType
        {
            get
            {
                return _searchType;
            }
        }

        /// <summary>
        /// Target Bytes used for comparing memory
        /// </summary>
        public byte[] TargetBytes
        {
            get
            {
                return _targetBytes;
            }
        }
        
        /// <summary>
        /// List of address to compare with
        /// </summary>
        public List<long> FoundAddresses
        {
            get
            {
                return _foundAddresses;
            }
        }           

        public SearchContextImp()
        {
            _foundAddresses = new List<long>();
        }

        /// <summary>
        /// Used to setup next search
        /// </summary>
        /// <param name="targetBytes"></param>
        /// <param name="searchType"></param>
        /// <param name="dataType"></param>
        /// <param name="parameters"></param>
        public void ConfigureNextSearch(byte[] targetBytes, SearchType searchType, DataType dataType, List<string> parameters)
        {
            _targetBytes = targetBytes;
            _searchType = searchType;
            _dataType = dataType;
            _parameters = parameters;
            _dataLength = Parser.GetByteLength(dataType);
        }

        /// <summary>
        /// Reset addresses
        /// </summary>
        public void ResetFoundAddresses()
        {
            _foundAddresses = new List<long>();
        }
    }
}
