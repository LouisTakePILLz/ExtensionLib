//-----------------------------------------------------------------------
// <copyright file="StringMethods.cs" company="LouisTakePILLz">
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
using System.Globalization;
using System.Linq;

namespace ExtensionLib
{
    /// <content>
    /// Provides various static extension methods.
    /// </content>
    public static partial class ExtensionMethods
    {
        #region IndexesOf
        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified Unicode character in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The Unicode character to seek.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, Char value)
        {
            return source.IndexesOf(value, 0, source.Length);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified Unicode character in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, Char value, Int32 startIndex)
        {
            return source.IndexesOf(value, startIndex, source.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified Unicode character in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, Char value, Int32 startIndex, Int32 count)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException("count");

            for (int i = startIndex - 1; (i = source.IndexOf(value, i + 1, count)) != -1; )
                yield return i;
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value, Int32 startIndex, StringComparison comparisonType)
        {
            return source.IndexesOf(value, startIndex, source.Length - startIndex, comparisonType);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value, Int32 startIndex, Int32 count)
        {
            return source.IndexesOf(value, startIndex, count, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value, Int32 startIndex)
        {
            return source.IndexesOf(value, startIndex, source.Length - startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value, StringComparison comparisonType)
        {
            return source.IndexesOf(value, 0, source.Length, comparisonType);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value)
        {
            return source.IndexesOf(value, 0, source.Length, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Reports the zero-based indexes of all the occurrences of the specified string in the current <see cref="T:System.String"/> object.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The index positions of the <paramref name="value"/> parameter.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for <paramref name="source"/>.</exception>
        public static IEnumerable<Int32> IndexesOf(this String source, String value, Int32 startIndex, Int32 count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (value == null)
                throw new ArgumentNullException("value");
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException("count");

            for (int i = startIndex - value.Length; (i = source.IndexOf(value, i + value.Length, count - i - value.Length, comparisonType)) != -1; )
                yield return i;
        }
        #endregion

        #region CountCharacters
        /// <summary>
        /// Returns a number that represents the number of occurrences of the specified characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="characters">The sequence of characters to count.</param>
        /// <returns>The number of occurrences of the specified characters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 CountCharacters(this String source, params Char[] characters)
        {
            return source.CountCharacters(characters, CultureInfo.InvariantCulture, CompareOptions.None);
        }

        /// <summary>
        /// Returns a number that represents the number of occurrences of the specified characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="characters">The sequence of characters to count.</param>
        /// <returns>The number of occurrences of the specified characters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 CountCharacters(this String source, IEnumerable<Char> characters)
        {
            return source.Count(x => characters.Any(c => String.CompareOrdinal(x.ToString(), c.ToString()) == 0));
        }

        /// <summary>
        /// Returns a number that represents the number of occurrences of the specified characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="characters">The sequence of characters to count.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The number of occurrences of the specified characters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 CountCharacters(this String source, IEnumerable<Char> characters, StringComparison comparisonType)
        {
            return source.Count(x => characters.Any(c => String.Compare(x.ToString(), c.ToString(), comparisonType) == 0));
        }

        /// <summary>
        /// Returns a number that represents the number of occurrences of the specified characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <param name="characters">The sequence of characters to count.</param>
        /// <param name="culture">The culture that supplies culture-specific comparison information.</param>
        /// <param name="options">Options to use when performing the character comparison (such as ignoring case or symbols).</param>
        /// <returns>The number of occurrences of the specified characters.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Int32 CountCharacters(this String source, IEnumerable<Char> characters, CultureInfo culture, CompareOptions options)
        {
            return source.Count(x => characters.Any(c => String.Compare(x.ToString(), c.ToString(), culture, options) == 0));
        }
        #endregion
    }
}
