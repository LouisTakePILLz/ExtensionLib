//-----------------------------------------------------------------------
// <copyright file="Shuffle.cs" company="LouisTakePILLz">
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

using System.Collections.Generic;
using System.Linq;

namespace ExtensionLib
{
    /// <content>
    /// Provides various static extension methods.
    /// </content>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Shuffles the order of the elements contained within a list using the Fisher–Yates shuffling algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the elements to shuffle.</typeparam>
        /// <param name="list">A collection that contains the objects to shuffle.</param>
        /// <returns>A scrambled list of the original items.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            T[] elements = list.ToArray();
            int n = elements.Length;
            while (n-- > 1)
            {
                int k = RandomProvider.Next(n + 1);
                T value = elements[k];
                elements[k] = elements[n];
                elements[n] = value;
                yield return value;
            }
        }
    }
}
