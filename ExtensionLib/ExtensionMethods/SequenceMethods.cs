//-----------------------------------------------------------------------
// <copyright file="SequenceMethods.cs" company="LouisTakePILLz">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionLib
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Inserts an element a given amount of time at beginning of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <param name="source">The sequence to pad.</param>
        /// <param name="length">The amount of times the <paramref name="paddingElement"/> is inserted.</param>
        /// <param name="paddingElement">The element to insert.</param>
        /// <returns>The newly padded sequence.</returns>
        public static IEnumerable<T> PrependPadding<T>(this IEnumerable<T> source, Int32 length, T paddingElement)
        {
            return Enumerable.Repeat(paddingElement, length).Concat(source);
        }

        /// <summary>
        /// Inserts an element a given amount of time at end of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <param name="source">The sequence to pad.</param>
        /// <param name="length">The amount of times the <paramref name="paddingElement"/> is inserted.</param>
        /// <param name="paddingElement">The element to insert.</param>
        /// <returns>The newly padded sequence.</returns>
        public static IEnumerable<T> AppendPadding<T>(this IEnumerable<T> source, Int32 length, T paddingElement)
        {
            return source.Concat(Enumerable.Repeat(paddingElement, length));
        }

        /// <summary>
        /// Returns unique random <see cref="T:System.Collections.Generic.KeyValuePair`2"/> entries from a sequence.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The sequence to return unique random entries from.</param>
        /// <returns>A sequence of random <see cref="T:System.Collections.Generic.KeyValuePair`2"/> entries.</returns>
        public static IEnumerable<KeyValuePair<TKey, TValue>> UniqueRandom<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>(source);
            while (values.Count > 0)
            {
                TKey key = values.Keys.ElementAt(RandomProvider.Next(0, values.Count));
                TValue value = values[key];
                values.Remove(key);

                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        /// <summary>
        /// Returns the index of the minimum value in a generic sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <returns>The index of the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 IndexOfMin<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IEnumerable<T> items = source as T[] ?? source.ToArray();
            return !items.Any()
                ? -1
                : items.ToIndexedDictionary()
                    .OrderBy(x => x.Value)
                    .First()
                    .Key;
        }

        /// <summary>
        /// Returns the index of the maximum value in a generic sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <returns>The index of the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 IndexOfMax<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IEnumerable<T> items = source as T[] ?? source.ToArray();
            return !items.Any()
                ? -1
                : items.ToIndexedDictionary()
                    .OrderByDescending(x => x.Value)
                    .First()
                    .Key;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.Generic.IDictionary`2"/> collection of elements from a sequence paired with their index.
        /// </summary>
        /// <param name="source">The sequence of elements to index.</param>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <returns>A dictionary of elements paired with their respective index as key.</returns>
        public static IDictionary<Int32, T> ToIndexedDictionary<T>(this IEnumerable<T> source)
        {
            IEnumerable<T> items = source as T[] ?? source.ToArray();
            return items
                .Select((value, index) => new { Value = value, Index = index })
                .ToDictionary(x => x.Index, x => x.Value);
        }

        /// <summary>
        /// Returns the number of occurrences of the specified items in a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to test.</param>
        /// <param name="items">The items to count.</param>
        /// <returns>The number of occurrences of the specified items.</returns>
        public static Int32 CountMany<T>(this IEnumerable<T> source, IEnumerable<T> items) where T : IComparable
        {
            return source.Count(x => items.Any(i => i.CompareTo(x) == 0));
        }

        /// <summary>
        /// Returns the number of occurrences of the specified items in a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to test.</param>
        /// <param name="items">The items to count.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.IComparer"/> object used to compare the objects in the two sequences.</param>
        /// <returns>The number of occurrences of the specified items.</returns>
        public static Int32 CountMany<T>(this IEnumerable<T> source, IEnumerable<T> items, IComparer comparer) where T : IComparable
        {
            return source.Count(x => items.Any(i => comparer.Compare(i, x) == 0));
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from a given position in a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to take from.</param>
        /// <param name="position">The amount of positions to skip before starting to take.</param>
        /// <param name="length">The amount of elements to take.</param>
        /// <returns>A part of the supplied sequence sequence from the specified <paramref name="position"/> and <paramref name="length"/>.</returns>
        /// <remarks>This method is actually just a shorthand for the Enumerable.Skip and Enumerable.Take methods.</remarks>
        public static IEnumerable<T> TakeRange<T>(this IEnumerable<T> source, Int32 position, Int32 length)
        {
            return source.Skip(position).Take(length);
        }
    }
}
