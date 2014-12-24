//-----------------------------------------------------------------------
// <copyright file="IndexesOf.cs" company="LouisTakePILLz">
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
        /// Reports the zero-based indexes of all the occurrences of the specified object in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to test.</param>
        /// <param name="value">The object to seek.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEnumerable<Int32> IndexesOf<T>(this IEnumerable<T> source, T value)
        {
            var list = source as T[] ?? source.ToArray();
            return list.IndexesOf(value, 0, list.Length);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified object in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to test.</param>
        /// <param name="value">The object to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf<T>(this IEnumerable<T> source, T value, Int32 startIndex)
        {
            var list = source as T[] ?? source.ToArray();
            return list.IndexesOf(value, startIndex, list.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified object in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to test.</param>
        /// <param name="value">The object to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of positions to examine.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.-or-<paramref name="count"/> is less than zero.-or-<paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf<T>(this IEnumerable<T> source, T value, Int32 startIndex, Int32 count)
        {
            var list = source as T[] ?? source.ToArray();
            if (source == null)
                throw new ArgumentNullException("source");
            if (startIndex < 0 || startIndex > list.Length)
                throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex > list.Length - count)
                throw new ArgumentOutOfRangeException("count");

            for (int i = startIndex - 1; (i = Array.IndexOf(list, value, i + 1, count)) != -1; )
                yield return i;
        }
    }
}
