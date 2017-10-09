using System;
using System.Diagnostics;

namespace MemoryManager
{
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

    public interface IMemoryAccess
    {              
        Process Process { get; }
        string ProcessName { get; }

        IntPtr MinAddress { get;  }
        IntPtr MaxAddress { get;  }

        MEMORY_BASIC_INFORMATION VirtualQuery(IntPtr address);

        void FreezeMemory(IntPtr address, byte[] value);
        void KillProcess();
        int ReadMemoryAsBytes(IntPtr address, ref byte[]  buffer);
        byte[] ReadMemoryAsBytes(IntPtr address, int dataLength);
        string ReadMemoryAsString(IntPtr address, DataType dataType, int lenInBytes);
        void UnfreezeMemory(IntPtr address);
        void UnfrezzeAll();
        int WriteValue(IntPtr address, byte[] value);
    }
}