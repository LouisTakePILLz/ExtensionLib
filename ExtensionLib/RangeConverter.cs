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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ExtensionLib
{
    /// <summary>
    /// Converts a <see cref="T:ExtensionLib.Range`1"/> object from one data type to another.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public class RangeConverter<T> : TypeConverter where T : IComparable, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.RangeConverter"/> class.
        /// </summary>
        public RangeConverter() { }

        /// <summary>
        /// Determines whether this converter can convert a given source type to the native type of the converter.
        /// </summary>
        /// <returns>A boolean value indicating whether this type can be converted to a <see cref="T:ExtensionLib.Range`1"/>.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="sourceType">The source type to test.</param>
        /// <filterpriority>1</filterpriority>
        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (String) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Determines whether this converter can convert the native type of the converter to a given destination type.
        /// </summary>
        /// <returns>A boolean value indicating whether <see cref="T:ExtensionLib.Range`1"/> can be converted to a given destination type.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="destinationType">The destination type to test.</param>
        /// <filterpriority>1</filterpriority>
        public override Boolean CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the specified object to a <see cref="T:ExtensionLib.Range`1"/> object.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="culture">An object that contains culture-specific information, such as the language, calendar, and cultural conventions associated with a specific culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <exception cref="T:System.NotSupportedException">The conversion couldn't be completed.</exception>
        /// <exception cref="T:System.ArgumentException">The object could not be parsed.</exception>
        /// <filterpriority>1</filterpriority>
        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            String str = value as String;
            if (str == null)
                return base.ConvertFrom(context, culture, value);
            str = str.Trim();
            if (str.Length == 0) return null;

            String[] values = str.Split(new[] { culture.TextInfo.ListSeparator }, StringSplitOptions.None);
            if (values.Count() != 2)
                throw new ArgumentException("Values could not be parsed.");
            
            TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
            return new Range<T>(
                (T) converter.ConvertFromString(context, culture, values[0]),
                (T) converter.ConvertFromString(context, culture, values[1]));
        }

        /// <summary>
        /// Converts the specified object to the specified type.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="culture">An object that contains culture-specific information, such as the language, calendar, and cultural conventions associated with a specific culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The type to convert the object to.</param>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
        /// <filterpriority>1</filterpriority>
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");
            if (value is Range<T>)
            {
                if (destinationType == typeof (String))
                {
                    var range = (Range<T>) value;
                    if (culture == null)
                        culture = CultureInfo.CurrentCulture;
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));

                    return String.Join(culture.TextInfo.ListSeparator + " ", new[]
                    {
                        converter.ConvertToString(context, culture, range.Minimum),
                        converter.ConvertToString(context, culture, range.Maximum)
                    });
                }
                if (destinationType == typeof (InstanceDescriptor))
                {
                    var range = (Range<T>) value;
                    ConstructorInfo constructor = typeof (Range<T>).GetConstructor(new[] { typeof (T), typeof (T) });
                    if (constructor != null)
                        return new InstanceDescriptor(constructor, new[] { range.Minimum, range.Maximum });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Creates an instance of this type given a set of property values for the object.
        /// </summary>
        /// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:ExtensionLib.RangeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])"/>.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <filterpriority>1</filterpriority>
        public override Object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            var min = propertyValues["Minimum"];
            var max = propertyValues["Maximum"];
            if (min == null || max == null || (!(min is T) || !(max is T)))
                throw new ArgumentException("Either the Minimum or Maximum property holds invalid values.");
            
            return new Range<T>((T) min, (T) max);
        }

        /// <summary>
        /// Determines whether changing a value on this object should require a call to <see cref="M:ExtensionLib.RangeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)"/> to create a new value.
        /// </summary>
        /// <returns>A boolean value indicating whether the <see cref="M:ExtensionLib.RangeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)"/> method should be called when a change is made to one or more properties of this object.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <filterpriority>1</filterpriority>
        public override Boolean GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Retrieves the set of properties for this type.
        /// </summary>
        /// <returns>The set of properties that are exposed for this data type. If no properties are exposed, this method might return null.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <param name="value">The value of the object to get the properties for.</param>
        /// <param name="attributes">An array of <see cref="T:System.Attribute"/> objects that describe the properties.</param>
        /// <filterpriority>1</filterpriority>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof (Range<T>), attributes).Sort(new[] { "Minimum, Maximum" });
        }

        /// <summary>
        /// Determines if this object supports properties.
        /// </summary>
        /// <returns>A boolean value indicating whether the <see cref="M:ExtensionLib.RangeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])"/> method should be called to find the properties of this object.</returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> object that provides a format context.</param>
        /// <filterpriority>1</filterpriority>
        public override Boolean GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}