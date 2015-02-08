//-----------------------------------------------------------------------
// <copyright file="StringArrayConverterTestUnit.cs" company="LouisTakePILLz">
// Copyright © 2015 LouisTakePILLz
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
using System.Globalization;
using ExtensionLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionLibTests
{
    [TestClass]
    public class StringArrayConverterTestUnit
    {
        [TestMethod]
        public void TestCultureAwareness()
        {
            var converter = new StringArrayConverter();
            var culture = CultureInfo.GetCultureInfoByIetfLanguageTag("sv-SE");
            String[] firstList = (String[]) converter.ConvertFrom(null, culture, "1,2,3,4");
            String[] secondList = (String[]) converter.ConvertFrom(null, culture, "1;2;3;4");

            Assert.IsTrue(firstList.Length == 1);
            Assert.IsTrue(secondList.Length == 4);

            firstList = (String[]) converter.ConvertFrom(null, CultureInfo.InvariantCulture, "1,2,3,4");
            Assert.IsTrue(firstList.Length == secondList.Length);
        }

        [TestMethod]
        public void TestInvariance()
        {
            const String input = " 1;2;3; 4";
            var converter = new StringArrayConverter();
            var culture = CultureInfo.GetCultureInfoByIetfLanguageTag("sv-SE");
            String[] firstList = (string[]) converter.ConvertFrom(null, culture, input);
            String[] secondList = (string[]) converter.ConvertFrom(converter.ConvertTo(firstList, typeof (String)));

            Assert.IsTrue(firstList.Length == secondList.Length);
            Assert.AreNotEqual(
                input,
                converter.ConvertTo(firstList, typeof (String)));
        }

        [TestMethod]
        public void TestEscaping()
        {
            var converter = new StringArrayConverter();
            var firstCulture = CultureInfo.GetCultureInfoByIetfLanguageTag("sv-SE");
            var secondCulture = CultureInfo.InvariantCulture;

            String[] firstList = (String[]) converter.ConvertFrom(null, firstCulture, @"1\;2;3\;4");
            String[] secondList = (String[]) converter.ConvertFrom(null, secondCulture, @"1\,2,3\,4");

            Assert.AreEqual(firstList.Length, secondList.Length);
        }
    }
}
