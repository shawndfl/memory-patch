using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager.Util
{
    /// <summary>
    /// Parses strings and data types
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parses a string value into a given data type
        /// </summary>        
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Byte[] ToBytes(string value, DataType type) 
        {
            try
            {
                switch (type)
                {
                    case DataType.Byte:
                        return new byte[] { (byte)SByte.Parse(value) };

                    case DataType.UByte:                                                
                        return new byte[] { Byte.Parse(value) };

                    case DataType.Int16:
                        return BitConverter.GetBytes(Int16.Parse(value));

                    case DataType.UInt16:
                        return BitConverter.GetBytes(UInt16.Parse(value));

                    case DataType.Int32:
                        return BitConverter.GetBytes(Int32.Parse(value));

                    case DataType.UInt32:
                        return BitConverter.GetBytes(UInt32.Parse(value));                                       

                    case DataType.StringChar:
                        {
                            byte[] bytes = new byte[value.Length * sizeof(char)];
                            int x = 0;
                            for (int i = 0; i < value.Length; i++)
                            {
                                byte[] tmp = BitConverter.GetBytes(value[i]);
                                bytes[x++] = tmp[0];
                                bytes[x++] = tmp[1];
                            }
                            return bytes;
                        }
                    case DataType.StringByte:
                        {
                            byte[] bytes = new byte[value.Length];
                            for (int i = 0; i < value.Length; i++)
                            {
                                bytes[i] = (byte)value[i];
                            }
                            return bytes;
                        }
                    case DataType.Float:
                        return BitConverter.GetBytes(float.Parse(value));

                    case DataType.Double:
                        return BitConverter.GetBytes(double.Parse(value));                                
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Overflow: " + value + " Type: " + type.ToString());
            }

            return new byte[] { 0 };
        }

        /// <summary>
        /// Used to parse byte buffers that are strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ParseBytesOfStrings(byte[] buffer, int startIndex, int length, DataType type)
        {
            try
            {
                if (type == DataType.StringChar)
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    return encoding.GetString(buffer, startIndex, length);
                }
                else if(type == DataType.StringByte)
                {
                    char[] chars = new char[length];
                    int x = 0;
                    for (int i = startIndex; i < length; i++)
                    {
                        if (buffer[i] == '\0')
                            chars[x] = ' ';
                        else
                            chars[x] = (char)buffer[i];
                        x++;
                    }
                    string temp = new string(chars);
                    return temp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
            }
            return "";
        }

        /// <summary>
        /// Parses a byte array into a string. If the data type is somekind of string use ParseBytesOfStrings()
        /// </summary>
        /// <param name="buffer"></param>      
        /// <param name="startIndex"></param>
        /// <param name="type">Any data type but a string</param>
        /// <returns></returns>
        public static string ParseBytes(byte[] buffer, int startIndex, DataType type)
        {
            try
            {
                switch (type)
                {
                    case DataType.Int32:
                        return BitConverter.ToInt32(buffer, startIndex).ToString();
                    case DataType.Int16:
                        return BitConverter.ToInt16(buffer, startIndex).ToString();
                    case DataType.Byte:
                        return ((sbyte)buffer[startIndex]).ToString();
                    case DataType.UInt32:
                        return BitConverter.ToUInt32(buffer, startIndex).ToString();
                    case DataType.UInt16:
                        return BitConverter.ToUInt16(buffer, startIndex).ToString();
                    case DataType.UByte:
                        return ((byte)buffer[startIndex]).ToString();                    
                    case DataType.Float:
                        return BitConverter.ToSingle(buffer, startIndex).ToString();
                    case DataType.Double:
                        return BitConverter.ToDouble(buffer, startIndex).ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
            }
            return "";
        }

        /// <summary>
        /// Getst the length for data that is not a string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetByteLength(DataType type)
        {
            return GetByteLength("", type);
        }

        /// <summary>
        /// Gets how many bytes a value takes up
        /// </summary>
        /// <param name="value">This is only used if the type is some kind of a string</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetByteLength(string value, DataType type)
        {
            switch (type)
            {
                case DataType.Byte:
                case DataType.UByte:
                    return sizeof(byte);

                case DataType.Int16:
                case DataType.UInt16:
                    return sizeof(short);

                case DataType.Int32:
                case DataType.UInt32:
                    return sizeof(int);

                case DataType.StringChar:
                    return value.Length * sizeof(char);

                case DataType.StringByte:
                    return value.Length;

                case DataType.Float:
                    return sizeof(float);

                case DataType.Double:
                    return sizeof(double);
            }
            return 0;
        }

        /// <summary>
        /// Converts to a byte
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static byte ToByte(byte[] buffer, int startIndex)
        {
            try
            {
                return buffer[startIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Converts to sbyte
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static sbyte ToSByte(byte[] buffer, int startIndex)
        {
            try
            {
                return (sbyte)buffer[startIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Int16 ToInt16(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToInt16(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToUInt16(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int ToInt32(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToInt32(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static uint ToUInt32(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToUInt32(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }                      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static float ToFloat(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToSingle(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static double ToDouble(byte[] buffer, int startIndex)
        {
            try
            {
                return BitConverter.ToDouble(buffer, startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                return 0;
            }
        }
    }
}
