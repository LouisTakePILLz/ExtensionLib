using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionLibTests
{
    [TestClass]
    public class ArrayEnumeratorTestUnit
    {
        [TestMethod]
        public void TestLinearity()
        {
            Int32[,] array = new Int32[8, 8];

            array[3, 1] = 1;
            array[3, 6] = 1;

            Int32 firstIndex = 3 * 8 + 1;
            Int32 secondIndex = 3 * 8 + 6;

            var enumerator = new ArrayEnumerator<Int32>(array);

            while (enumerator.MoveNext())
            {
                if (enumerator.Index == firstIndex)
                    Assert.IsTrue(enumerator.Positions[0] == 3 && enumerator.Positions[1] == 1);
                else if (enumerator.Index == secondIndex)
                    Assert.IsTrue(enumerator.Positions[0] == 3 && enumerator.Positions[1] == 6);
            }
        }
    }
}
