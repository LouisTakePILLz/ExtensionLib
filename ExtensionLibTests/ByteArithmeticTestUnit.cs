//-----------------------------------------------------------------------
// <copyright file="ByteArithmeticTestUnit.cs" company="LouisTakePILLz">
// Copyright © 2014 LouisTakePILLz
// <author>LouisTakePILLz</author>
// </copyright>
//-----------------------------------------------------------------------

/*
 * This program is free software: you can redistribute it and/or modify it under the terms of
 * the GNU General Public License as published by the Free Software Foundation, either
 * version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */


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
