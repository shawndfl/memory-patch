using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace MemoryManager
{

    internal class MemoryAccess : IMemoryAccess
    {
        #region Dll Imports
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dbProcessId);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
                
        #endregion

        #region Consts
        // REQUIRED CONSTS
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int MEM_COMMIT = 0x00001000;
        public const int PAGE_READWRITE = 0x04;
        public const int PROCESS_WM_READ = 0x0010;
        public const int PROCESS_WM_WRITE = 0x0020;
        #endregion

        #region Fields
        private static object _freezeLock = new object();
        private Dictionary<IntPtr, AddressLock> _frezonAddresses =
            new Dictionary<IntPtr, AddressLock>();

        private Thread _frezzeThread;        
        #endregion

        /// <summary>
        /// the process we are accessing
        /// </summary>
        public Process Process { get; private set; }      

        public string ProcessName
        {
            get { return Process.ProcessName; }
        }
      
        public IntPtr MinAddress { get; private set; }
        public IntPtr MaxAddress { get; private set; }

        public MemoryAccess(Process process)
        {
            Process = process;

            //TODO get length of memory
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            MinAddress = sys_info.minimumApplicationAddress;
            MaxAddress = sys_info.maximumApplicationAddress;

            StartFreezeThread();
        }

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
            if (Process != null)
                Process.Kill();
        }

        /// <summary>
        /// USed to read memory as a string
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dataType"></param>
        /// <param name="lenInBytes"></param>
        /// <returns></returns>
        public string ReadMemoryAsString(IntPtr address, DataType dataType, int lenInBytes)
        {
            byte[] buffer = ReadMemoryAsBytes(address, lenInBytes);

            switch (dataType)
            {
                case DataType.Int32:
                    return BitConverter.ToInt32(buffer, 0).ToString();
                case DataType.Int16:
                    return BitConverter.ToInt16(buffer, 0).ToString();
                case DataType.Byte:
                    return ((sbyte)buffer[0]).ToString();
                case DataType.UInt32:
                    return BitConverter.ToUInt32(buffer, 0).ToString();
                case DataType.UInt16:
                    return BitConverter.ToUInt16(buffer, 0).ToString();
                case DataType.UByte:
                    return ((byte)buffer[0]).ToString();
                case DataType.StringChar:
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        return encoding.GetString(buffer);
                    }
                case DataType.StringByte:
                    {
                        char[] chars = new char[lenInBytes];
                        for (int i = 0; i < lenInBytes; i++)
                        {
                            if (buffer[i] == '\0')
                                chars[i] = ' ';
                            else
                                chars[i] = (char)buffer[i];
                        }
                        string temp = new string(chars);
                        return temp;
                    }
                case DataType.Float:
                    return BitConverter.ToSingle(buffer, 0).ToString();
                case DataType.Double:
                    return BitConverter.ToDouble(buffer, 0).ToString();
                default:
                    throw new Exception("Unknown DataType");
            }

        }

        /// <summary>
        /// Gets the memory info around a given address        
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public MEMORY_BASIC_INFORMATION VirtualQuery(IntPtr address)
        {
            //IntPtr handle = Process.Handle;
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();
            
            VirtualQueryEx(Process.Handle, address, out mem_basic_info, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));
            return mem_basic_info;                    
        }

        /// <summary>
        /// Reads memory of the process. Use this to read memory that is in sequence
        /// </summary>
        /// <param name="address">The address to start reading from</param>
        /// <param name="buffer">The buffer to store memory in</param>
        /// <returns></returns>                       
        public int ReadMemoryAsBytes(IntPtr address, ref byte[] buffer)
        {
            int length = 0;
            try
            {
                if (buffer != null && buffer.Length > 0)
                {
                    IntPtr handle = Process.Handle;
                    ReadProcessMemory(handle, address, buffer, buffer.Length, out length);
                }

            }
            catch (Exception ex)
            {
                length = 0;
            }
            
            return length;
        }

        /// <summary>
        /// Reads memory.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        public byte[] ReadMemoryAsBytes(IntPtr address, int dataLength)
        {
            int length = 0;
            byte[] buffer = new byte[dataLength];
            try
            {                
                IntPtr handle = Process.Handle;
                ReadProcessMemory(handle, address, buffer, buffer.Length, out length);

            }
            catch (Exception ex)
            {
                buffer = null;
            }
            return buffer;
        }

        public void FreezeMemory(IntPtr address, byte[] value)
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

        public void UnfreezeMemory(IntPtr address)
        {
            lock (_freezeLock)
            {
                _frezonAddresses.Remove(address);                    
            }
        }

        public void UnfrezzeAll()
        {
            lock (_freezeLock)
            {
                _frezonAddresses.Clear();
            }
        }

        public int WriteValue(IntPtr address, byte[] value)
        {
            int lengthWrite = 0;
            try
            {
                IntPtr handle = Process.Handle;
                WriteProcessMemory(handle, address, value, value.Length, out lengthWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lengthWrite;
        }        
    }   
}
