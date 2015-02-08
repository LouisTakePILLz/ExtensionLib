//-----------------------------------------------------------------------
// <copyright file="StringArrayConverter.cs" company="LouisTakePILLz">
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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtensionLib
{
    /// <summary>
    /// Provides a type converter to convert a string of character-separated values to and from an array of strings.
    /// </summary>
    public class StringArrayConverter : TypeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.StringArrayConverter"/> class.
        /// </summary>
        public StringArrayConverter() { }

        /// <summary>
        /// Determines whether this converter can convert a given source type to the native type of the converter.
        /// </summary>
        /// <returns>A boolean value indicating whether this type can be converted to a <see cref="T:System.String"/> array.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="sourceType">The source <see cref="T:System.Type"/> to test.</param>
        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (String);
        }

        /// <summary>
        /// Converts the specified object to a <see cref="T:System.String"/> array, using the specified context and culture information.
        /// </summary>
        /// <returns>An <see cref="T:System.Object"/> that represents the converted value.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            String source = value as String;
            if (source == null)
                throw this.GetConvertFromException(value);

            if (source.Length == 0)
                return new String[0];

            String separator = Regex.Escape((culture ?? CultureInfo.InvariantCulture).TextInfo.ListSeparator);
            String[] names = Regex.Matches(
                source,
                @"(?<=" + separator + @"|^)((?:.*?(?:\\.)?)*)(?=" + separator + @"|$)",
                RegexOptions.Singleline | RegexOptions.CultureInvariant)
                .OfType<Match>()
                .Select(x => Regex.Replace(x.Value, @"\\(.?)", "$1", RegexOptions.CultureInvariant))
                .ToArray();
            
            return names;
        }

        /// <summary>
        /// Converts the specified object to the specified <see cref="T:System.Type"/>, using the specified context and culture information.
        /// </summary>
        /// <returns>An <see cref="T:System.Object"/> that represents the converted value.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type"/> to convert the object to.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType"/> parameter is null.</exception>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (destinationType != typeof (String))
                throw this.GetConvertToException(value, destinationType);

            String separator = (culture ?? CultureInfo.InvariantCulture).TextInfo.ListSeparator;
            
            return value == null
                ? String.Empty
                : String.Join(separator, ((String[]) value).Select(x => x.Replace(separator, @"\" + separator)));
        }
    }
}
