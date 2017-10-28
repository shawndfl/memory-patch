using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace MemoryManager
{

    public struct SearchContext
    {
        /// <summary>
        /// Is this the first search
        /// </summary>
        public bool FirstSearch { get; set; }

        /// <summary>
        /// The type of search we are using
        /// </summary>
        public SearchType SearchType { get; set; }

        /// <summary>
        /// Value in a byte array. In this form we can change it to
        /// any other data type by using BitConverter
        /// </summary>
        [XmlIgnore]
        public byte[] Value { get; private set; }

        /// <summary>
        /// Used when searching with decreased by and increased by
        /// </summary>
        public float Difference { get; set; }

        /// <summary>
        /// Data type used in this search
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// The byte length of the data type
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// The value stored as a long
        /// </summary>
        [XmlAttribute("value")]
        public long LongValue
        {
            get
            {
                if (Value != null)
                {
                    byte[] data = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    for (int i = 0; i < Value.Length; i++)
                    {
                        data[i] = Value[i];
                    }
                    return BitConverter.ToInt64(data, 0);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Value = BitConverter.GetBytes((long)value);
            }
        }
       
        /// <summary>
        /// Used to create a new or next search
        /// </summary>        
        /// <param name="searchType"> type of search</param>
        /// <param name="dataType"> data type to search for</param>
        /// <param name="value1">the value in the form of a string</param>        
        /// <returns></returns>
        public static SearchContext CreateSearchData(SearchType searchType,
            DataType dataType, string value1)
        {
            SearchContext data = new SearchContext();            
            data.FirstSearch = true;
            data.SearchType = searchType;
            data.DataType = dataType;

            data.SetValue(value1);
            return data;
        }

        /// <summary>
        /// Sets the value based on a string. This value will be used in comparing memory values.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            bool oneVal = (SearchType == SearchType.Excat || SearchType == SearchType.FirstExcat);
            if (SearchType == SearchType.HasIncreasedBy ||
                SearchType == SearchType.HasDecreasedBy)
            {
                Difference = float.Parse(value);
            }

            switch (DataType)
            {
                case DataType.Int32:
                    if (oneVal)
                        Value = BitConverter.GetBytes(Int32.Parse(value));

                    DataLength = sizeof(int);
                    break;
                case DataType.Int16:
                    if (oneVal)
                        Value = BitConverter.GetBytes(Int16.Parse(value));

                    DataLength = sizeof(short);
                    break;
                case DataType.Byte:
                    if (oneVal)
                        Value = BitConverter.GetBytes(SByte.Parse(value));

                    DataLength = sizeof(byte);
                    break;
                case DataType.UInt32:
                    if (oneVal)
                        Value = BitConverter.GetBytes(UInt32.Parse(value));

                    DataLength = sizeof(int);
                    break;
                case DataType.UInt16:
                    if (oneVal)
                        Value = BitConverter.GetBytes(UInt32.Parse(value));

                    DataLength = sizeof(short);
                    break;
                case DataType.UByte:
                    if (oneVal)
                    {
                        byte num;
                        Byte.TryParse(value, out num);
                        Value = BitConverter.GetBytes(num);
                    }

                    DataLength = sizeof(byte);
                    break;
                case DataType.StringChar:
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();

                        if (oneVal)
                        {
                            Value = new byte[value.Length * sizeof(char)];
                            int x = 0;
                            for (int i = 0; i < value.Length; i++)
                            {
                                byte[] bytes = BitConverter.GetBytes(value[i]);
                                Value[x++] = bytes[0];
                                Value[x++] = bytes[1];
                            }
                            DataLength = Value.Length;
                        }
                        else
                            throw new Exception("Can't search for an unkown string");
                    }
                    break;
                case DataType.StringByte:
                    {
                        if (oneVal)
                        {
                            Value = new byte[value.Length];
                            for (int i = 0; i < value.Length; i++)
                            {
                                Value[i] = (byte)value[i];
                            }
                            DataLength = value.Length;
                        }
                        else
                            throw new Exception("Can't search for an unkown string");
                    }
                    break;
                case DataType.Float:
                    if (oneVal)
                        Value = BitConverter.GetBytes(float.Parse(value));
                    DataLength = sizeof(float);
                    break;
                case DataType.Double:
                    if (oneVal)
                        Value = BitConverter.GetBytes(double.Parse(value));
                    DataLength = sizeof(double);
                    break;
            }
        }      

    }

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
        FirstExcat,
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
