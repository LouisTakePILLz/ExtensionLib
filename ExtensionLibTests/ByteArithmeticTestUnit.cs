using System;
using ExtensionLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionLibTests
{
    [TestClass]
    public class ByteArithmeticTestUnit
    {
        [TestMethod]
        public void TestByteEndianness()
        {
            const UInt32 value = 0x90AB12CD;
            byte[] bytes = BitConverter.GetBytes(value);
            Assert.AreEqual(value, bytes.GetBytes<UInt32>());

            byte[] lengthBytes = { 0x68, 0x2a, 0x08, 0x00 };
            Assert.AreEqual((UInt32) 0x00082a68, lengthBytes.GetBytes<UInt32>());

            lengthBytes = new byte[] { 0x00, 0x68, 0x2a, 0x08 };
            Assert.AreEqual((UInt32) 0x082a6800, lengthBytes.GetBytes<UInt32>());
        }
    }
}
