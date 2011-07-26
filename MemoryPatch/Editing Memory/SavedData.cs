using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;

namespace MemoryPatch
{
    [XmlRoot("Groups")]
    public class AddressData
    {
        [XmlAttribute("process")]
        public string Process;
        [XmlAttribute("module")]
        public string Module;

        public List<SaveGroupData> Groups = new List<SaveGroupData>();
        public List<SaveGroupOptions> Options = new List<SaveGroupOptions>();
    }

    public class SaveGroupOptions
    {
        [XmlAttribute("name")]
        public string Name;
        public List<Option> Options = 
            new List<Option>();

        public override string ToString()
        {
            return Name;
        }
    }

    public class Option
    {
        [XmlAttribute("item")]
        public string Item;
        [XmlAttribute("value")]
        public string Value;

        public override string ToString()
        {
            return Item + " = " + Value;
        }
    }

    public class SaveGroupData
    {
        [XmlAttribute("name")]
        public string Name;
        public string Notes = string.Empty;
        public List<SavedAddress> Addresses = new List<SavedAddress>();

        public override string ToString()
        {
            return Name;
        }

    }

    [Serializable]
    public class SavedAddress
    {
        #region Fields
        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("locked")]
        public bool Locked;       
        [XmlAttribute("dataType")]
        public DataType DataType;        
        [XmlAttribute("optionList")]
        public string OptionList;

        [XmlIgnore]
        public int Address;
        [XmlIgnore]
        public byte[] Value;
       
        #endregion

        #region Properties
        [XmlAttribute("address")] 
        public string StringAddress
        {
            get
            {
                string str = string.Format("{0:X00000000}", Address);
                return str;
            }
            set
            {
                Address = Int32.Parse(value, NumberStyles.AllowHexSpecifier, null);
            }
        }
        

        [XmlAttribute("pokedValue")]
        public string StringValue
        {
            get
            {
                return GetStringValue(Value, false);
            }
            set
            {
                Value = GetByteValue(value);
            }
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

        #region Constructors
        private SavedAddress() { }   
        public SavedAddress(bool locked, string name, int address, DataType dataType)
        {
            DataType = dataType;
            Address = address;
            Value = null;
            Name = name;
            Locked = locked;
            OptionList = null;
            StringValue = "0";
        }

        public SavedAddress(bool locked, string name, int address, string value, DataType dataType)
        {
            DataType = dataType;
            Address = address;
            Value = null;
            Name = name;
            Locked = locked;
            OptionList = null;
            StringValue = value;
        }

        public SavedAddress(bool locked, string name, string address, string value, DataType dataType)
        {
            DataType = dataType;
            Address = 0;
            Value = null;
            Name = name;
            Locked = locked;
            OptionList = null;
            StringValue = value;
            StringAddress = address;
        }

        public SavedAddress(bool locked, string name, int address, byte[] value, DataType dataType)
        {
            DataType = dataType;
            Value = value;
            Address = address;
            Name = name;
            Locked = locked;
            OptionList = null;
        }
        #endregion

        public SavedAddress CloneAddress()
        {
            SavedAddress newAddress = new SavedAddress(Locked, Name, Address, DataType);
            newAddress.OptionList = OptionList;
            newAddress.StringValue = StringValue;
            return newAddress;
        }
        public string TreeNodeText(string value)
        {
            //return "(" + Locked + ") " + Name + " " + StringAddress + " = " + value + " {" + DataType + "}";
            return BuildString(value);
        }

        public override string ToString()
        {
            //return "(" + Locked + ") " + Name + " " + StringAddress + " = " + StringValue + " {" + DataType + "}";
            return BuildString(StringValue);
        }


        /// <summary>
        /// Gets a string version of the byte value using 
        /// the current data type for this saved address.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="inHex">Do you want it in hex form</param>        
        /// <returns></returns>
        public string GetStringValue(byte[] value, bool inHex)
        {
            switch (DataType)
            {
                case DataType.Int32:
                    if (value.Length != 4)
                        Array.Resize<Byte>(ref value, 4);
                    if (inHex)
                        return string.Format("{0:X000000}", BitConverter.ToInt32(value, 0));
                    else
                        return BitConverter.ToInt32(value, 0).ToString();
                case DataType.Int16:
                    if (value.Length != 2)
                        Array.Resize<Byte>(ref value, 2);

                    return BitConverter.ToInt16(value, 0).ToString();
                case DataType.Byte:
                    if (value.Length != 1)
                        Array.Resize<Byte>(ref value, 1);

                    if (inHex)
                        return string.Format("{0:X000000}", (sbyte)value[0]);
                    else
                        return ((sbyte)value[0]).ToString();

                case DataType.UInt32:
                    if (value.Length != 4)
                        Array.Resize<Byte>(ref value, 4);

                    if (inHex)
                        return string.Format("{0:X000000}", BitConverter.ToUInt32(value, 0));
                    else
                        return BitConverter.ToUInt32(value, 0).ToString();
                case DataType.UInt16:
                    if (value.Length != 2)
                        Array.Resize<Byte>(ref value, 2);

                    if (inHex)
                        return string.Format("{0:X000000}", BitConverter.ToUInt16(value, 0));
                    else
                        return BitConverter.ToUInt16(value, 0).ToString();
                case DataType.UByte:
                    if (value.Length != 1)
                        Array.Resize<Byte>(ref value, 1);

                    if (inHex)
                        return string.Format("{0:X000000}", (byte)value[0]);
                    else
                        return ((byte)value[0]).ToString();
                case DataType.String:
                    throw new NotImplementedException();
                case DataType.Float:
                    if (value.Length != 4)
                        Array.Resize<Byte>(ref value, 4);

                    if (inHex)
                        return string.Format("{0:X000000}", BitConverter.ToSingle(value, 0));
                    else
                        return BitConverter.ToSingle(value, 0).ToString();
                case DataType.Double:
                    if (value.Length != 8)
                        Array.Resize<Byte>(ref value, 8);

                    if (inHex)
                        return string.Format("{0:X000000}", BitConverter.ToDouble(value, 0));
                    else
                        return BitConverter.ToDouble(value, 0).ToString();
                default:
                    throw new Exception("Unknown DataType");
            }
        }

        public byte[] GetByteValue(string value)
        {
            byte[] byteValue = null;
            bool isHex = value.ToLower().StartsWith("0x");
            if (string.IsNullOrEmpty(value))
                value = "0";

            switch (DataType)
            {
                case DataType.Int32:
                    try
                    {
                        byteValue = BitConverter.GetBytes(Int32.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    break;
                case DataType.Int16:
                    try
                    {
                        byteValue = BitConverter.GetBytes(Int16.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0 };
                    }
                    break;
                case DataType.Byte:
                    try
                    {
                        byteValue = new byte[] { (byte)SByte.Parse(value.ToLower(), isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None) };
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0 };
                    }
                    break;
                case DataType.UInt32:
                    try
                    {
                        byteValue = BitConverter.GetBytes(UInt32.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    break;
                case DataType.UInt16:
                    try
                    {
                        byteValue = BitConverter.GetBytes(UInt16.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0 };
                    }
                    break;
                case DataType.UByte:
                    try
                    {
                        byteValue = new byte[] { Byte.Parse(value.ToLower(), isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None) };
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0 };
                    }
                    break;
                case DataType.String:
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byteValue = encoding.GetBytes(value);
                    break;
                case DataType.Float:
                    try
                    {
                        byteValue = BitConverter.GetBytes(float.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0 };
                    }
                    break;
                case DataType.Double:
                    try
                    {
                        byteValue = BitConverter.GetBytes(double.Parse(value, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None));
                    }
                    catch (FormatException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    }
                    catch (OverflowException)
                    {
                        byteValue = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    }
                    break;
            }
            return byteValue;
        }

        private StringBuilder _builder = new StringBuilder();
        private string BuildString(string value)
        {
            _builder.Remove(0, _builder.Length);
            _builder.Append(Locked ? "(X)" : "   ");
            _builder.Append(Name.PadRight(30));
            _builder.Append(' ');
            _builder.Append(StringAddress);
            _builder.Append(" = ");
            _builder.Append(value.PadRight(15));
            _builder.Append(DataType.ToString());
            return _builder.ToString();
        }
    }   
}
