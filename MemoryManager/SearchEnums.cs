using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{

    public enum DataType
    {
        UByte,
        UInt16,
        UInt32,
        Byte,
        Int16,
        Int32,
        StringByte,
        StringChar,
        Float,
        Double,
    }

    public enum SearchType
    {
        Excat,
        UnKnown,
        HasIncreased,
        HasDecreased,
        HasNotChanged,
        HasChanged,
        HasDecreasedBy,
        HasIncreasedBy,
        StoreSnapShot1,
        StoreSnapShot2,
        CompareToSnapShot1,
        CompareToSnapShot2,
    }

    public enum CompareType
    {
        LessThen,
        EqualTo,
        GreaterThen,
    }

    public enum SnapShot
    {
        Current,
        First,
        Second,
    }
}
