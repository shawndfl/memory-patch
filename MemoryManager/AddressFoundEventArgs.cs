using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MemoryManager
{
    /// <summary>
    /// Used to pass address found information back to the client
    /// </summary>
    public class AddressFoundEventArgs : EventArgs
    {
        /// <summary>
        /// The address that was found
        /// </summary>
        public AddressFound AddressFound { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="addressFound">the address found</param>
        public AddressFoundEventArgs(AddressFound addressFound)
        {
            AddressFound = addressFound;
        }
    }

    /// <summary>
    /// Event args used for status updates.
    /// </summary>
    public class UpdateArgs: EventArgs
    {
        /// <summary>
        /// Update Detials.
        /// </summary>
        public string Detials { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="detials"></param>
        public UpdateArgs(string detials)
        {
            Detials = detials;
        }
    }

    /// <summary>
    /// A structor defining a memory address
    /// </summary>
    [Serializable]
    public struct AddressFound
    {
        #region Fields
        private long _address;        
        private byte[] _currentValue;
        private DataType _dataType;        
        #endregion

        #region Properties
        /// <summary>
        /// The address
        /// </summary>
        [XmlAttribute("address")]
        public long Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// The value as a long
        /// </summary>
        [XmlAttribute("value")]
        public long LongValue
        {
            get 
            {
                if (_currentValue != null)
                {
                    byte[] data = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    for (int i = 0; i < _currentValue.Length; i++)
                    {
                        data[i] = _currentValue[i];
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
                _currentValue = BitConverter.GetBytes((long)value);
            }
        }
        
        /// <summary>
        /// The value as a byte array
        /// </summary>
        public byte[] CurrentValue
        {
            get { return _currentValue; }           
        }

        /// <summary>
        /// The data type of this address
        /// </summary>
        [XmlAttribute("dataType")]
        public DataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// Gets the length of the data in bytes
        /// </summary>
        [XmlIgnore]
        public int DataLengthInBytes
        {
            get
            {
                switch (DataType)
                {
                    case DataType.Int32:
                        return sizeof(int);
                    case DataType.Int16:
                        return sizeof(Int16);
                    case DataType.Byte:
                        return sizeof(sbyte);
                    case DataType.UInt32:
                        return sizeof(UInt32);
                    case DataType.UInt16:
                        return sizeof(UInt16);
                    case DataType.UByte:
                        return sizeof(byte);
                    case DataType.StringByte:
                    case DataType.StringChar:
                        return CurrentValue.Length;
                    case DataType.Float:
                        return sizeof(float);
                    case DataType.Double:
                        return sizeof(double);
                    default:
                        throw new NotImplementedException(DataType.ToString());
                }
            }
        }
        #endregion       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="currentValue"></param>
        /// <param name="data"></param>
        public AddressFound(long address, byte[] currentValue, DataType data)
        {
            _currentValue = (byte[])currentValue.Clone();                  
            _address = address;
            _dataType = data;            
        }

        /// <summary>
        /// The string value of this address
        /// </summary>
        /// <returns></returns>
        public string GetStringValue()
        {
            switch (DataType)
            {
                case DataType.Int32:
                    return BitConverter.ToInt32(_currentValue, 0).ToString();                    
                case DataType.Int16:
                    return BitConverter.ToInt16(_currentValue, 0).ToString();                   
                case DataType.Byte:
                    return ((sbyte)_currentValue[0]).ToString();                    
                case DataType.UInt32:
                    return BitConverter.ToUInt32(_currentValue, 0).ToString();                    
                case DataType.UInt16:
                    return BitConverter.ToUInt16(_currentValue, 0).ToString();                    
                case DataType.UByte:
                    return ((byte)_currentValue[0]).ToString();
                case DataType.StringChar:
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        return encoding.GetString(_currentValue);
                    }
                case DataType.StringByte:
                    {
                        char[] chars = new char[_currentValue.Length];
                        for (int i = 0; i < _currentValue.Length; i++)
                        {
                            chars[i] = (char)_currentValue[i];
                        }
                        string temp = new string(chars);
                        return temp;            
                    }
                case DataType.Float:
                    return BitConverter.ToSingle(_currentValue, 0).ToString();
                case DataType.Double:
                    return BitConverter.ToDouble(_currentValue, 0).ToString();
                default:
                    throw new Exception("Unknown DataType");
            }
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0:X00000000} = ", _address) + GetStringValue();
        }
    }    
}
