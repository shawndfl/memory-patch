using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace MemoryManager
{
    public class MemoryAccess : IMemoryAccess
    {
        #region Dll Imports
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, int lpBaseAddress,
            byte[] buffer, int size, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dbProcessId);
        #endregion

        #region Consts
        public const int PROCESS_VM_READ = 0x0010;
        public const int PROCESS_VM_WRITE = 0x0020;
        #endregion

        #region Fields
        private static object _freezeLock = new object();
        private Dictionary<int, AddressLock> _frezonAddresses =
            new Dictionary<int, AddressLock>();

        private Thread _frezzeThread;
        #endregion

        /// <summary>
        /// the process we are accessing
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// the module in the process to access
        /// </summary>
        public ProcessModule Module { get; private set; }

        public string ProcessName
        {
            get { return Process.ProcessName; }
        }

        public string ModuleName
        {
            get { return Module.ModuleName; }
        }

        public MemoryAccess(Process process, ProcessModule module)
        {
            Process = process;
            Module = module;
            StartFreezeThread();
        }        

        public MemoryAccess(Process process)
        {
            Process = process;
            Module = Process.MainModule;
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

        public void KillProcess()
        {
            if (Process != null)
                Process.Kill();
        }        

        public string ReadMemoryAsString(int address, DataType dataType, int lenInBytes)
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
       
        public byte[] ReadMemoryAsBytes(int address, int length)
        {
            byte[] buffer = new byte[length];
            try
            {
                int lengthRead;
                int baseAddress = Module.BaseAddress.ToInt32();
                int len = Module.ModuleMemorySize;

                IntPtr handle = Process.Handle;

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

        public void UnfreezeMemory(int address)
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

        public int WriteValue(int address, byte[] value)
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
