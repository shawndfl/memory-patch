using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using MemoryManager.Util;

namespace MemoryManager
{
    public class MemoryAccess
    {
        #region Win32
        // REQUIRED CONSTS
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;

        // REQUIRED METHODS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        #endregion 

        #region Fields
        private IProcessManager _processManager;

        private static object _freezeLock = new object();
        private Dictionary<int, AddressLock> _frezonAddresses =
            new Dictionary<int, AddressLock>();

        private Thread _frezzeThread;

        private static MemoryAccess _instance;
        private IntPtr _processHandle;
        #endregion               

        /// <summary>
        /// Gets the process handle
        /// </summary>
        public IntPtr ProcessHandle
        {
            get
            {
                return _processHandle;
            }
        }

        /// <summary>
        /// The name of the procss
        /// </summary>
        public string ProcessName
        {
            get { return _processManager.ProcessName; }
        }

        /// <summary>
        /// The module of the process we are accessing
        /// </summary>
        public string ModuleName
        {
            get { return _processManager.ModuleName; }
        }

        /// <summary>
        /// Gets the base pointer to the module
        /// </summary>
        public IntPtr BaseAddress
        {
            get
            {
                return _processManager.BaseAddress;
            }
        }

        /// <summary>
        /// Gets the size of the module in bytes
        /// </summary>
        public int ModuleMemorySize
        {
            get
            {
                return _processManager.ModuleMemorySize;
            }
        }

        /// <summary>
        /// Gets an instance to memory access. Make sure Create is called first.
        /// </summary>
        public static MemoryAccess Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Create(null, null);
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates an instance of memory access.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static MemoryAccess Create(Process process, ProcessModule module)
        {
            if (_instance == null)
            {
                _instance = new MemoryAccess(new ProcessManagerImp(process, module));
            }
            else
            {
                _instance.SetProcessManager(new ProcessManagerImp(process, module));
            }

            return _instance;
        }

        public static MemoryAccess Create(Process process)
        {
            return Create(process, process.MainModule);
        }

        /// <summary>
        /// Constructor
        /// </summary>        
        private MemoryAccess(IProcessManager manager)
        {
            SetProcessManager(manager);
            StartFreezeThread();
        }               

        private void SetProcessManager(IProcessManager manager)
        {
            _processManager = manager;
            // opening the process with desired access level
            _processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, _processManager.ProcessId);
        }

        /// <summary>
        /// A thread that manages continuously changes a value in memory every 500 ms
        /// </summary>
        private void StartFreezeThread()
        {
            _frezzeThread = new Thread(new ThreadStart(delegate()
                {
                    while (true)
                    {
                        lock (_freezeLock)
                        {
                            foreach (AddressLock addressLock in _frezonAddresses.Values)
                            {
                                WriteValue(addressLock.Address, addressLock.Data);
                            }
                        }
                        Thread.Sleep(500);
                    }
                }));
            _frezzeThread.IsBackground = true;
            _frezzeThread.Start();
        }

        /// <summary>
        /// Kills the process
        /// </summary>
        public void KillProcess()
        {
            _processManager.KillProcess();
        }        

        /// <summary>
        /// Reads memory and stories the result in a string
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dataType"></param>
        /// <param name="lenInBytes"></param>
        /// <returns></returns>
        public string ReadMemoryAsString(int address, DataType dataType, int lenInBytes)
        {
            byte[] buffer = ReadMemoryAsBytes(address, lenInBytes);
            if(dataType == DataType.StringByte || dataType == DataType.StringChar)
                return Parser.ParseBytesOfStrings(buffer, 0, lenInBytes, dataType);
            else
                return Parser.ParseBytes(buffer, 0, dataType);           
        }     
       
        /// <summary>
        /// Reads memory as a raw byte array
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadMemoryAsBytes(int address, int length)
        {
            return _processManager.ReadMemoryAsBytes(address, length);
        }

        /// <summary>
        /// Adds an address to the frezon list
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public void FreezeMemory(int address, byte[] value)
        {
            lock (_freezeLock)
            {
                if (!_frezonAddresses.ContainsKey(address))
                    _frezonAddresses.Add(address, new AddressLock(address, value));
                else
                {
                    if(_frezonAddresses[address].Data != value)
                        _frezonAddresses[address] = new AddressLock(address, value);
                }
            }
        }

        /// <summary>
        /// Removes an address from the frezon list
        /// </summary>
        /// <param name="address"></param>
        public void UnfreezeMemory(int address)
        {
            lock (_freezeLock)
            {
                _frezonAddresses.Remove(address);                    
            }
        }

        /// <summary>
        /// Removes all from the frezon list
        /// </summary>
        public void UnfrezzeAll()
        {
            lock (_freezeLock)
            {
                _frezonAddresses.Clear();
            }
        }

        /// <summary>
        /// Writes a value to memory.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int WriteValue(int address, byte[] value)
        {
            return _processManager.WriteValue(address, value);
        }

        /// <summary>
        /// Is the process alive
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return _processManager.IsAlive();
        }
    }   
}
