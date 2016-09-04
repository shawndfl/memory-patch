using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryManager;

namespace MemoryPatchTests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TestByteParser()
        {
            //byte
            byte[] result = MemoryManager.Util.Parser.ToBytes("123", DataType.Byte);
            byte[] expected = new byte[] { 123 };
            
            Assert.AreEqual(expected.Length, result.Length);
            for(int i=0; i < expected.Length; i++)            
                Assert.AreEqual(expected[i], result[i]);

            //byte overflow
            result = MemoryManager.Util.Parser.ToBytes("-1", DataType.UByte);
            expected = new byte[] { 0 };

            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

            //negitive
            result = MemoryManager.Util.Parser.ToBytes("-1", DataType.Byte);
            expected = new byte[] { 255 };

            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

            //overflow
            result = MemoryManager.Util.Parser.ToBytes("256", DataType.Byte);
            expected = new byte[] { 0 };
            
            Assert.AreEqual(expected.Length, result.Length);            
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

            //unsigned overflow
            result = MemoryManager.Util.Parser.ToBytes("128", DataType.Byte);
            expected = new byte[] { 0 };
            
            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void TestShortParser()
        {
            byte[] result = MemoryManager.Util.Parser.ToBytes("65535", DataType.UInt16);
            byte[] expected = new byte[] { 255, 255 };

            //check length
            Assert.AreEqual(expected.Length, result.Length);

            //check values
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

            result = MemoryManager.Util.Parser.ToBytes("123", DataType.Int16);
            expected = new byte[] { 123, 0 };

            //check length
            Assert.AreEqual(expected.Length, result.Length);

            //check values
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void TestIntParser()
        {
            byte[] result = MemoryManager.Util.Parser.ToBytes("65535", DataType.Int32);
            byte[] expected = new byte[] { 255, 255, 0, 0 };

            //check length
            Assert.AreEqual(expected.Length, result.Length);

            //check values
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

            result = MemoryManager.Util.Parser.ToBytes("123", DataType.UInt32);
            expected = new byte[] { 123, 0, 0, 0 };

            //check length
            Assert.AreEqual(expected.Length, result.Length);

            //check values
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void TestShortOverflowParser()
        {
            byte[] result = MemoryManager.Util.Parser.ToBytes("65536", DataType.Int16);
            byte[] expected = new byte[] { 0 };

            //check length
            Assert.AreEqual(expected.Length, result.Length);

            //check values
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);
        }

        [TestMethod]
        public void TestBytesToString()
        {
            //short
            byte[] buffer = new byte[] { 255, 255 };
            String result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Int16);
            String expected = "-1";            
            Assert.AreEqual(expected, result);

            //int
            buffer = new byte[] { 255, 255, 0, 0 };
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Int32);
            expected = "65535";
            Assert.AreEqual(expected, result);

            //byte offset
            buffer = new byte[] { 7, 128, 0, 0 };
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 1, DataType.Byte);
            expected = "-128";
            Assert.AreEqual(expected, result);

            buffer = new byte[] { 128, 123, 0, 5 };
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 3, DataType.UByte);
            expected = "5";
            Assert.AreEqual(expected, result);

            //float test
            buffer = MemoryManager.Util.Parser.ToBytes("-1.34", DataType.Float);
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Float);
            expected = "-1.34";
            Assert.AreEqual(expected, result);

            //exponent
            buffer = MemoryManager.Util.Parser.ToBytes("-1.34e-2", DataType.Float);
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Float);
            expected = "-0.0134";
            Assert.AreEqual(expected, result);

            //double
            buffer = MemoryManager.Util.Parser.ToBytes("-1.34e-10", DataType.Double);
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Double);
            expected = "-1.34E-10";
            Assert.AreEqual(expected, result);

            //double
            buffer = MemoryManager.Util.Parser.ToBytes("32432431.34343", DataType.Double);
            result = MemoryManager.Util.Parser.ParseBytes(buffer, 0, DataType.Double);
            expected = "32432431.34343";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestByteToDataParser()
        {
            byte[] buffer = new byte[] { 255, 255 };
            byte result = MemoryManager.Util.Parser.ToByte(buffer, 0);
            byte expected = 255;
            
            Assert.AreEqual(expected, result);
        }

        [TestMethod]        
        public void TestShortToDataParser()
        {
            unchecked
            {
                byte[] buffer = new byte[] { 255, 255 };
                Int16 result = MemoryManager.Util.Parser.ToInt16(buffer, 0);
                Int16 expected = (short)65535;
                Assert.AreEqual(expected, result);
            }

            unchecked
            {
                byte[] buffer = new byte[] { 255, 255 };
                UInt16 result = MemoryManager.Util.Parser.ToUInt16(buffer, 0);
                UInt16 expected = (ushort)65535;
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestIntToDataParser()
        {
            unchecked
            {
                byte[] buffer = new byte[] { 255, 255, 0, 0 };
                int result = MemoryManager.Util.Parser.ToInt32(buffer, 0);
                int expected = 65535;
                Assert.AreEqual(expected, result);
            }

            unchecked
            {
                byte[] buffer = new byte[] { 255, 255,0,0 };
                UInt32 result = MemoryManager.Util.Parser.ToUInt32(buffer, 0);
                UInt32 expected = 65535;
                Assert.AreEqual(expected, result);
            }
        }
    }
}
