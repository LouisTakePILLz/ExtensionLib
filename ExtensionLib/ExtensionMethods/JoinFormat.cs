// Copyright 2014 LouisTakePILLz

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
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a format to represent them as a string.
        /// </summary>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="objectFormat">A standard or custom object format string.</param>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String objectFormat)
            where T : IFormattable
        {
            return JoinFormat(list, separator, objectFormat, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="args">An object array that contains zero or more objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, params Object[] args)
        {
            return JoinFormat(list, separator, format, CultureInfo.CurrentUICulture, args);
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="objectFormat">A standard or custom object format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String objectFormat, IFormatProvider formatProvider)
            where T : IFormattable
        {
            return String.Join(separator, list.Select(item => item.ToString(objectFormat, formatProvider)));
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="args">An object array that contains zero or more objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, IFormatProvider formatProvider, params Object[] args)
        {
            return String.Join(separator, list.Select(item => String.Format(formatProvider, format, item, args)));
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="predicate">An array that contains zero or more functions that provide objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, Func<T, Object[]> predicate)
        {
            return String.Join(separator, list.Select(item => String.Format(format, predicate(item))));
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="predicate">A function that supplies the objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, params Func<T, Object>[] predicate)
        {
            return JoinFormat(list, separator, format, CultureInfo.CurrentUICulture, predicate);
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="predicate">A function that supplies the objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, IFormatProvider formatProvider, Func<T, Object[]> predicate)
        {
            return String.Join(separator, list.Select(item => String.Format(formatProvider, format, predicate(item))));
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.IFormattable"/>, using the specified separator between each member and a <b>composite</b> format to represent them as a string.
        /// </summary>
        /// <returns>A string that consists of the formatted representation of the members of <paramref name="list"/> delimited by the <paramref name="separator"/> string. If <paramref name="list"/> has no members, the method returns <see cref="F:System.String.Empty"/>.</returns>
        /// <param name="list">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="list"/> has more than one element.</param>
        /// <param name="format">A composite format string (see <see cref="String.Format(String, Object[])"/>).</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="predicate">An array that contains zero or more functions that provide objects to be used within <see cref="String.Format(String, Object[])"/>.</param>
        /// <typeparam name="T">The type of the elements to concatenate.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static String JoinFormat<T>(this IEnumerable<T> list, String separator, String format, IFormatProvider formatProvider, params Func<T, Object>[] predicate)
        {
            return String.Join(separator, list.Select(item => String.Format(formatProvider, format, predicate.Select(x => x(item)).ToArray())));
        }
    }
}
