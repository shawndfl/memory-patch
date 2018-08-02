using System;

namespace MemoryManager
{
    /// <summary>
    /// Used in searching
    /// </summary>
    public interface ISearchMemory: IDisposable
    {
        /// <summary>
        /// Search context. This lests the search know what we are searching for
        /// </summary>
        SearchContext SearchContext { get; }

        /// <summary>
        /// When the progress of the search changes.
        /// </summary>
        event EventHandler<SearchUpdateEventArgs> OnProgressChange;
        /// <summary>
        /// When a value is found
        /// </summary>
        event EventHandler<AddressFoundEventArgs> OnValueFound;

        /// <summary>
        /// This can be use to check if the search is done.
        /// TODO change this to done.
        /// </summary>
        event EventHandler<UpdateArgs> OnUpdate;

        /// <summary>
        /// Cancel the search
        /// </summary>
        void CancelSearch();        

        /// <summary>
        /// Start a new search
        /// </summary>
        /// <param name="context"></param>
        void NewSearch(SearchContext context);

        /// <summary>
        /// Run the next search
        /// </summary>
        /// <param name="type"></param>
        /// <param name="optValue"></param>
        void NextSearch(SearchType type, string optValue);

        /// <summary>
        /// Loads a snapshot
        /// </summary>
        /// <param name="file"></param>
        void LoadSnapShot(string file);

        /// <summary>
        /// Save a snapshot of the search
        /// </summary>
        /// <param name="file"></param>
        void SaveSnapShot(string file);
    }
}