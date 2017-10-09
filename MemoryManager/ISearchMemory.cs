using System;

namespace MemoryManager
{
    public interface ISearchMemory
    {
        SearchContext SearchContext { get; }

        event EventHandler<SearchUpdateEventArgs> OnProgressChange;
        event EventHandler<AddressFoundEventArgs> OnValueFound;

        void CancelSearch();        
        void NewSearch(SearchContext context);
        void NextSearch(SearchType type, string optValue);

        void LoadSnapShot(string file);
        void SaveSnapShot(string file);
    }
}