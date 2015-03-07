//-----------------------------------------------------------------------
// <copyright file="RangeTestUnit.cs" company="LouisTakePILLz">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionLib;

namespace ExtensionLibTests
{
    [TestClass]
    public class RangeTestUnit
    {
        [TestMethod]
        public void TestJoinLowerBound()
        {
            Range<Int32> firstRange = new Range<Int32>(-15, 15);
            Range<Int32> secondRange = new Range<Int32>(-16, 15);
            Range<Int32> joinedRange = firstRange.Union(secondRange);

            Assert.AreEqual(secondRange.Minimum, joinedRange.Minimum);
            Assert.AreEqual(secondRange.Maximum, joinedRange.Maximum);
        }

        [TestMethod]
        public void TestJoinUpperBound()
        {
            Range<Int32> firstRange = new Range<Int32>(-15, 32);
            Range<Int32> secondRange = new Range<Int32>(-15, 64);
            Range<Int32> joinedRange = firstRange.Union(secondRange);

            Assert.AreEqual(secondRange.Minimum, joinedRange.Minimum);
            Assert.AreEqual(secondRange.Maximum, joinedRange.Maximum);
        }

        [TestMethod]
        public void TestJoinInfinity()
        {
            Range<Double> infiniteRange = new Range<Double>(Double.NegativeInfinity, Double.PositiveInfinity);
            Range<Double> secondRange = new Range<Double>(-15, 3);
            Range<Double> joinedRange = infiniteRange.Union(secondRange);

            Assert.AreEqual(infiniteRange.Minimum, joinedRange.Minimum);
            Assert.AreEqual(infiniteRange.Maximum, joinedRange.Maximum);
        }

        [TestMethod]
        public void TestJoinNegativeInfinity()
        {
            Range<Double> firstRange = new Range<Double>(Double.NegativeInfinity, 5);
            Range<Double> secondRange = new Range<Double>(5, 6);
            Range<Double> joinedRange = firstRange.Union(secondRange, MathHelper.DoubleComparer);

            Assert.AreEqual(firstRange.Minimum, joinedRange.Minimum);
            Assert.AreEqual(secondRange.Maximum, joinedRange.Maximum);
        }

        [TestMethod]
        public void TestNestedIntersection()
        {
            Range<Double> firstRange = new Range<Double>(3, 3);
            Range<Double> secondRange = new Range<Double>(-6, 6);
            Range<Double> intersectRange = firstRange.Intersect(secondRange, MathHelper.DoubleComparer);

            Assert.AreEqual(firstRange.Minimum, intersectRange.Minimum);
            Assert.AreEqual(firstRange.Maximum, intersectRange.Maximum);
        }

        [TestMethod]
        public void TestInfinityIntersection()
        {
            Range<Double> firstRange = new Range<Double>(Double.NegativeInfinity, 3);
            Range<Double> secondRange = new Range<Double>(-15, 4);
            Range<Double> intersectRange = firstRange.Intersect(secondRange, MathHelper.DoubleComparer);

            Assert.AreEqual(secondRange.Minimum, intersectRange.Minimum);
            Assert.AreEqual(firstRange.Maximum, intersectRange.Maximum);
        }
    }
}
 