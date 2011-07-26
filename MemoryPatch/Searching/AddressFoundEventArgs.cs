﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MemoryPatch
{

    public class AddressFoundEventArgs : EventArgs
    {
        public AddressFound AddressFound { get; private set; }
        public AddressFoundEventArgs(AddressFound addressFound)
        {
            AddressFound = addressFound;
        }
    }

    [Serializable]
    public struct AddressFound
    {
        #region Fields
        private int _address;        
        private byte[] _currentValue;
        private DataType _dataType;
        #endregion

        #region Properties
        [XmlAttribute("address")]
        public int Address
        {
            get { return _address; }
            set { _address = value; }
        }

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
        
        public byte[] CurrentValue
        {
            get { return _currentValue; }           
        }

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
                    case DataType.String:
                        throw new NotImplementedException("String");
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

        public AddressFound(int address, byte[] currentValue, DataType data)
        {
            _currentValue = (byte[])currentValue.Clone();                  
            _address = address;
            _dataType = data;
        }

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
                case DataType.String:
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    return encoding.GetString(_currentValue);
                case DataType.Float:
                    return BitConverter.ToSingle(_currentValue, 0).ToString();
                case DataType.Double:
                    return BitConverter.ToDouble(_currentValue, 0).ToString();
                default:
                    throw new Exception("Unknown DataType");
            }
        }

        public override string ToString()
        {
            return string.Format("{0:X00000000} = ", _address) + GetStringValue();
        }
    }

    public class UpdateCountEventArgs : EventArgs
    {
        public int Count { get; private set; }

        public UpdateCountEventArgs(int count)
        {
            Count = count;
        }
    }
}
