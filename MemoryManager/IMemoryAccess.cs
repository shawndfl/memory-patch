using System;
using System.Diagnostics;

namespace MemoryManager
{
    /// <summary>
    /// The main interface to the outside world.
    /// This interface will allow access to memory of a 
    /// given process. 
    /// </summary>
    public interface IMemoryAccess
    {
        /// <summary>
        /// The process who's memory we are trying to access        
        /// </summary>
        Process Process { get; }

        /// <summary>
        /// The human readable name of the process
        /// </summary>
        string ProcessName { get; }

        /// <summary>
        /// Min memory address
        /// </summary>
        IntPtr MinAddress { get; }
        /// <summary>
        /// Last memory address
        /// </summary>
        IntPtr MaxAddress { get; }

        /// <summary>
        /// Used in searching for values in memory
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        MEMORY_BASIC_INFORMATION VirtualQuery(IntPtr address);

        /// <summary>
        /// Frezzes a memory value.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        void FreezeMemory(IntPtr address, byte[] value);

        /// <summary>
        /// Kills the process
        /// </summary>
        void KillProcess();

        /// <summary>
        /// Reads memory as bytes
        /// </summary>
        /// <param name="address"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        int ReadMemoryAsBytes(IntPtr address, ref byte[] buffer);

        /// <summary>
        /// Reads memory as bytes and returns an array
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        byte[] ReadMemoryAsBytes(IntPtr address, int dataLength);

        /// <summary>
        /// Reads memory as a string. This is used in forms to display memory values.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        string ReadMemoryAsString(IntPtr address, DataType dataType, int lenInBytes);

        /// <summary>
        /// Removes memory from the frezze queue
        /// </summary>
        /// <param name="address"></param>
        void UnfreezeMemory(IntPtr address);

        /// <summary>
        /// Removes all addresses from the frezze queue
        /// </summary>
        void UnfrezzeAll();

        /// <summary>
        /// Writes a value to memory
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int WriteValue(IntPtr address, byte[] value);
    }

    public struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public AllocationProtectEnum AllocationProtect;
        public IntPtr RegionSize;
        public StateEnum State;
        public AllocationProtectEnum Protect;
        public TypeEnum lType;
    }
    public enum AllocationProtectEnum : uint
    {
        PAGE_EXECUTE = 0x00000010,
        PAGE_EXECUTE_READ = 0x00000020,
        PAGE_EXECUTE_READWRITE = 0x00000040,
        PAGE_EXECUTE_WRITECOPY = 0x00000080,
        PAGE_NOACCESS = 0x00000001,
        PAGE_READONLY = 0x00000002,
        PAGE_READWRITE = 0x00000004,
        PAGE_WRITECOPY = 0x00000008,
        PAGE_GUARD = 0x00000100,
        PAGE_NOCACHE = 0x00000200,
        PAGE_WRITECOMBINE = 0x00000400
    }

    public enum StateEnum : uint
    {
        MEM_COMMIT = 0x1000,
        MEM_FREE = 0x10000,
        MEM_RESERVE = 0x2000
    }

    public enum TypeEnum : uint
    {
        MEM_IMAGE = 0x1000000,
        MEM_MAPPED = 0x40000,
        MEM_PRIVATE = 0x20000
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

   
}