using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace MemoryManager
{
    /// <summary>
    /// A byte converter class
    /// </summary>
    public static class ByteConverter
    {

        /// <summary>
        /// If the byte is little endian
        /// </summary>
        public static bool IsLittleEndian = true;
        
        /// <summary>
        /// Doubles to int64 bits.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe long DoubleToInt64Bits(double value)
        {
            return *(((long*)&value));
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static byte[] GetBytes(bool value)
        {
            return new byte[] { (value ? ((byte)1) : ((byte)0)) };
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytes(char value)
        {
            return GetBytes((short)value);
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe byte[] GetBytes(double value)
        {
            return GetBytes(*((long*)&value));
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe byte[] GetBytes(short value)
        {
            byte[] buffer = new byte[2];
            fixed (byte* numRef = buffer)
            {
                *((short*)numRef) = value;
            }
            return buffer;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe byte[] GetBytes(int value)
        {
            byte[] buffer = new byte[4];
            fixed (byte* numRef = buffer)
            {
                *((int*)numRef) = value;
            }
            return buffer;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe byte[] GetBytes(long value)
        {
            byte[] buffer = new byte[8];
            fixed (byte* numRef = buffer)
            {
                *((long*)numRef) = value;
            }
            return buffer;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe byte[] GetBytes(float value)
        {
            return GetBytes(*((int*)&value));
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytes(ushort value)
        {
            return GetBytes((short)value);
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytes(uint value)
        {
            return GetBytes((int)value);
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytes(ulong value)
        {
            return GetBytes((long)value);
        }

        /// <summary>
        /// Gets the hex value.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        private static char GetHexValue(int i)
        {
            if (i < 10)
            {
                return (char)(i + 0x30);
            }
            return (char)((i - 10) + 0x41);
        }

        /// <summary>
        /// Int64s the bits to double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe double Int64BitsToDouble(long value)
        {
            return *(((double*)&value));
        }

        /// <summary>
        /// Toes the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static bool ToBoolean(byte[] value, int startIndex)
        {
           
            return (value[startIndex] != 0);
        }

        /// <summary>
        /// Toes the char.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static char ToChar(byte[] value, int startIndex)
        {
           
            return (char)((ushort)ToInt16(value, startIndex));
        }

        /// <summary>
        /// Toes the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe double ToDouble(byte[] value, int startIndex)
        {           
            long lg = ToInt64(value, startIndex);
            return *(((double*)&lg));
        }

        /// <summary>
        /// Toes the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe short ToInt16(byte[] value, int startIndex)
        {
           
            fixed (byte* numRef = &(value[startIndex]))
            {                
                if (IsLittleEndian)
                {
                    return (short)(numRef[0] | (numRef[1] << 8));
                }
                return (short)((numRef[0] << 8) | numRef[1]);
            }
        }

        /// <summary>
        /// Toes the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe int ToInt32(byte[] value, int startIndex)
        {
           
            fixed (byte* numRef = &(value[startIndex]))
            {               
                if (IsLittleEndian)
                {
                    return (((numRef[0] | (numRef[1] << 8)) | (numRef[2] << 0x10)) | (numRef[3] << 0x18));
                }
                return ((((numRef[0] << 0x18) | (numRef[1] << 0x10)) | (numRef[2] << 8)) | numRef[3]);
            }
        }

        /// <summary>
        /// Toes the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe long ToInt64(byte[] value, int startIndex)
        {
           
            fixed (byte* numRef = &(value[startIndex]))
            {                
                if (IsLittleEndian)
                {
                    int num = ((numRef[0] | (numRef[1] << 8)) | (numRef[2] << 0x10)) | (numRef[3] << 0x18);
                    int num2 = ((numRef[4] | (numRef[5] << 8)) | (numRef[6] << 0x10)) | (numRef[7] << 0x18);
                    return (((long)((ulong)num)) | (num2 << 0x20));
                }
                int num3 = (((numRef[0] << 0x18) | (numRef[1] << 0x10)) | (numRef[2] << 8)) | numRef[3];
                int num4 = (((numRef[4] << 0x18) | (numRef[5] << 0x10)) | (numRef[6] << 8)) | numRef[7];

                return (long)((num4 << 0x20) | num3);
            }
        }

        /// <summary>
        /// Toes the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static unsafe float ToSingle(byte[] value, int startIndex)
        {
            long lg = ToInt32(value, startIndex);
            return *(((float*)&lg));
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static string ToString(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return ToString(value, 0, value.Length);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static string ToString(byte[] value, int startIndex)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return ToString(value, startIndex, value.Length - startIndex);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static string ToString(byte[] value, int startIndex, int length)
        {
           
            if (length == 0)
            {
                return string.Empty;
            }
            if (length > 0x2aaaaaaa)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            int num = length * 3;
            char[] chArray = new char[num];
            int index = 0;
            int num3 = startIndex;
            for (index = 0; index < num; index += 3)
            {
                byte num4 = value[num3++];
                chArray[index] = GetHexValue(num4 / 0x10);
                chArray[index + 1] = GetHexValue(num4 % 0x10);
                chArray[index + 2] = '-';
            }
            return new string(chArray, 0, chArray.Length - 1);
        }

        /// <summary>
        /// Toes the U int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static ushort ToUInt16(byte[] value, int startIndex)
        {           
            return (ushort)ToInt16(value, startIndex);
        }

        /// <summary>
        /// Toes the U int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static uint ToUInt32(byte[] value, int startIndex)
        {           
            return (uint)ToInt32(value, startIndex);
        }

        /// <summary>
        /// Toes the U int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            return (ulong)ToInt64(value, startIndex);
        }
    } 

}
