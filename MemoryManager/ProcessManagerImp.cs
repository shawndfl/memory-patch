using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryManager
{
    /// <summary>
    /// The main implementation of of process manager
    /// </summary>
    public class ProcessManagerImp: IProcessManager
    {
        #region Dll Imports
        /// <summary>
        /// Used to read memory from a process
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        /// <summary>
        /// Writes memory to a process
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, int lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        /// <summary>
        /// Opens a process
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dbProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dbProcessId);
        #endregion

        #region Consts
        /// <summary>
        /// Read const
        /// </summary>
        public const int PROCESS_VM_READ = 0x0010;
        /// <summary>
        /// Write const
        /// </summary>
        public const int PROCESS_VM_WRITE = 0x0020;
        #endregion              

        private Process _process;
        private ProcessModule _module;

        /// <summary>
        /// The name of the procss
        /// </summary>
        public string ProcessName
        {
            get {
                if (_process != null)
                    return _process.ProcessName;
                else
                    return "No Process";
            }
        }

        /// <summary>
        /// The module of the process we are accessing
        /// </summary>
        public string ModuleName
        {
            get
            {
                if (_module != null)
                    return _module.ModuleName;
                else
                    return "No Module";
            }
        }
              
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="process"></param>
        /// <param name="module"></param>
        public ProcessManagerImp(Process process, ProcessModule module)
        {
            _process = process;
            _module = module;            
        }                       

        /// <summary>
        /// Kills the process
        /// </summary>
        public void KillProcess()
        {
            if (_process != null)
                _process.Kill();
        }                
       
        /// <summary>
        /// Reads memory as a raw byte array
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadMemoryAsBytes(int address, int length)
        {
            byte[] buffer = new byte[length];
            try
            {
                int lengthRead;
                int baseAddress = _module.BaseAddress.ToInt32();
                int len = _module.ModuleMemorySize;

                IntPtr handle = _process.Handle;

                if (address >= baseAddress && address + length <= baseAddress + len)
                {
                    ReadProcessMemory(handle, address, buffer, buffer.Length, out lengthRead);
                }
            }
            catch (Exception ex)
            {
                buffer = null;
            }
            return buffer;
        }
               
        /// <summary>
        /// Writes a value to memory.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int WriteValue(int address, byte[] value)
        {
            int lengthWrite = 0;
            try
            {
                IntPtr handle = _process.Handle;
                WriteProcessMemory(handle, address, value, value.Length, out lengthWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lengthWrite;
        }

        /// <summary>
        /// Is the process alive
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            try
            {
                if (_process != null)
                    Process.GetProcessById(_process.Id);
                else
                    return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the base pointer to the module
        /// </summary>
        public IntPtr BaseAddress
        {
            get
            {
                return (_module != null) ? _module.BaseAddress : IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets the size of the module in bytes
        /// </summary>
        public int ModuleMemorySize
        {
            get
            {
                return (_module != null) ? _module.ModuleMemorySize : 0;
            }
        }
    }   
}
