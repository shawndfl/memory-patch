using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// Interface for managing a process' memory
    /// </summary>
    interface IProcessManager
    {       
        /// <summary>
        /// The name of the procss
        /// </summary>
        string ProcessName { get; }

        /// <summary>
        /// The module of the process we are accessing
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// Gets the process ID
        /// </summary>
        int ProcessId { get; }

        /// <summary>
        /// Reads memory as a raw byte array
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] ReadMemoryAsBytes(int address, int length);

        /// <summary>
        /// Writes a value to memory.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int WriteValue(int address, byte[] value);

        /// <summary>
        /// Kills the process
        /// </summary>
        void KillProcess();

        /// <summary>
        /// Is the process alive?
        /// </summary>
        /// <returns></returns>
        bool IsAlive();

        /// <summary>
        /// Gets the base pointer to the module
        /// </summary>
        IntPtr BaseAddress { get; }

        /// <summary>
        /// Gets the size of the module in bytes
        /// </summary>
        int ModuleMemorySize { get; }

        /// <summary>
        /// Gets the process handle
        /// </summary>
        IntPtr ProcessHandle { get; }
    }
}
