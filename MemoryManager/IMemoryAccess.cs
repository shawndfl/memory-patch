using System.Diagnostics;

namespace MemoryManager
{
    public interface IMemoryAccess
    {
        ProcessModule Module { get; }
        string ModuleName { get; }
        Process Process { get; }
        string ProcessName { get; }

        void FreezeMemory(int address, byte[] value);
        void KillProcess();
        byte[] ReadMemoryAsBytes(int address, int length);
        string ReadMemoryAsString(int address, DataType dataType, int lenInBytes);
        void UnfreezeMemory(int address);
        void UnfrezzeAll();
        int WriteValue(int address, byte[] value);
    }
}