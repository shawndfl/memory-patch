using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.ObjectModel;

namespace MemoryManager
{
    public class MemoryCollection: IEnumerable, IEnumerator<IntPtr>
    {
        #region Consts
        // REQUIRED CONSTS

        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;        
        const int PROCESS_WM_WRITE = 0x0020;
        #endregion

        #region Dll Imports      
       

        // REQUIRED METHODS

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);


        // REQUIRED STRUCTS

        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }
        #endregion
               
        #region Fields
        private long _max, _min;
        private IntPtr _current;
        private byte[] _buffer;
        #endregion

        /// <summary>
        /// the process we are accessing
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// the module in the process to access
        /// </summary>
        public ProcessModule Module { get; private set; }

        /// <summary>
        /// Gets the name of the process
        /// </summary>
        public string ProcessName
        {
            get { return Process.ProcessName; }
        }        

        /// <summary>
        /// Gets the buffer for this memory collection. It may be null of the enumeration has not started yet.
        /// </summary>
        public byte[]  Buffer
        {
            get
            {
                return _buffer;
            }
        }
        /// <summary>
        /// Gets the current value
        /// </summary>
        public IntPtr Current
        {
            get
            {
                return _current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Needed so we can use foreach on this class
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_min >= _max)
                return false;

            // this will store any information we get from VirtualQueryEx()
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

            // number of bytes read with ReadProcessMemory
            int bytesRead = 0;

            //process handle
            IntPtr handle = Process.Handle;

            // 28 = sizeof(MEMORY_BASIC_INFORMATION)
            VirtualQueryEx(handle, _current, out mem_basic_info, 28);

            // if this memory chunk is accessible
            if (mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
            {
                // Allocate for the buffer
                if (_buffer == null || _buffer.Length != mem_basic_info.RegionSize)
                {
                    _buffer = new byte[mem_basic_info.RegionSize];
                }

                // read everything in the buffer above
                ReadProcessMemory(handle,
                mem_basic_info.BaseAddress, _buffer, mem_basic_info.RegionSize, out bytesRead);               
            }
            else
            {
                Console.WriteLine("Skipping " + mem_basic_info.RegionSize + " bytes");
            }

            // move to the next memory chunk
            _min += mem_basic_info.RegionSize;
            _current = new IntPtr(_min);
            return true;
        }

        public void Reset()
        {
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            _min = (long)proc_min_address;
            _max = (long)proc_max_address;

            _current = new IntPtr(_min);
        }

        /// <summary>
        /// Creates an instance.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="module"></param>
        public MemoryCollection(Process process, ProcessModule module)
        {
            Process = process;
            Module = module;          
        }        

        /// <summary>
        /// Creates an instance using the main module of the process.
        /// </summary>
        /// <param name="process"></param>
        public MemoryCollection(Process process)
        {
            Process = process;
            Module = Process.MainModule;

            Reset();
        }                 
    }   
}
