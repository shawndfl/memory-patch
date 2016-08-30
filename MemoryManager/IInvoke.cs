using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// Interface into the main thead while searching
    /// </summary>
    public interface IInvoke
    {
        /// <summary>
        /// Used to call a delegate
        /// </summary>
        /// <param name="method"></param>
        void InvokeMethod(Delegate method);

        /// <summary>
        /// Show a message to the user
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);

        /// <summary>
        /// Called when a value is found
        /// </summary>        
        /// <param name="e"></param>
        void OnValueFound(AddressFoundEventArgs e);

        /// <summary>
        /// Updates progress.
        /// </summary>
        /// <param name="e"></param>
        void OnProgressChange(SearchUpdateEventArgs e);
    }
}
