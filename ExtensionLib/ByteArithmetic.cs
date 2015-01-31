//-----------------------------------------------------------------------
// <copyright file="ByteArithmetic.cs" company="LouisTakePILLz">
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
using System.Collections.Generic;
using System.Linq;

namespace ExtensionLib
{
    /// <summary>
    /// Provides various static methods pertaining to <see cref="T:System.Byte"/> operations.
    /// </summary>
    public static class ByteArithmetic
    {
        /// <summary>
        /// Returns an object of the specified type from a range of bytes within a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the returned elements.</typeparam>
        /// <param name="source">The sequence to extract the bytes from.</param>
        /// <param name="position">The starting position of the bytes to extract.</param>
        /// <param name="length">The amount of bytes to extract.</param>
        /// <param name="endianness">The order in which bytes are read.</param>
        /// <returns>The equivalent object of the bytes from the parsed region in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in <paramref name="source"/> exceeds the maximum value of the <typeparamref name="T"/> type.</exception>
        public static T GetBytes<T>(this IEnumerable<Byte> source, Int32 position, Int32 length, ByteOrder endianness)
        {
            return GetBytes<T>(source.TakeRange(position, length), endianness);
        }

        /// <summary>
        /// Returns an object of the specified type from a range of bytes within a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the returned elements.</typeparam>
        /// <param name="source">The sequence to extract the bytes from.</param>
        /// <param name="position">The starting position of the bytes to extract.</param>
        /// <param name="length">The amount of bytes to extract.</param>
        /// <returns>The equivalent object of the bytes from the parsed region in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in <paramref name="source"/> exceeds the maximum value of the <typeparamref name="T"/> type.</exception>
        public static T GetBytes<T>(this IEnumerable<Byte> source, Int32 position, Int32 length)
        {
            return GetBytes<T>(source.TakeRange(position, length));
        }

        /// <summary>
        /// Returns an object of the specified type equivalent to a sequence of bytes.
        /// </summary>
        /// <typeparam name="T">The type of the returned elements.</typeparam>
        /// <param name="source">The sequence of bytes to compute.</param>
        /// <returns>The equivalent object of the bytes from the parsed region in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in <paramref name="source"/> exceeds the maximum value of the <typeparamref name="T"/> type.</exception>
        public static T GetBytes<T>(this IEnumerable<Byte> source)
        {
            return GetBytes<T>(source, BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian);
        }

        /// <summary>
        /// Returns an object of the specified type equivalent to a sequence of bytes.
        /// </summary>
        /// <typeparam name="T">The type of the returned elements.</typeparam>
        /// <param name="source">The sequence of bytes to compute.</param>
        /// <param name="endianness">The order in which bytes are read.</param>
        /// <returns>The equivalent object of the bytes from the parsed region in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="source"/> contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in <paramref name="source"/> exceeds the maximum value of the <typeparamref name="T"/> type.</exception>
        public static T GetBytes<T>(this IEnumerable<Byte> source, ByteOrder endianness)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IEnumerable<Byte> items = source as Byte[] ?? source.ToArray();
            if (!items.Any())
                throw new InvalidOperationException();

            var bytes = items.Select(x => (T) Convert.ChangeType(x, typeof (T)));
            if (endianness == ByteOrder.LittleEndian)
                bytes = bytes.Reverse();

            return bytes
                .Aggregate((x, i) => (T) Convert.ChangeType((((UInt64) Convert.ChangeType(x, typeof (UInt64))) << 8)
                    + (UInt64) Convert.ChangeType(i, typeof (UInt64)), typeof (T)));
        }
    }
}
