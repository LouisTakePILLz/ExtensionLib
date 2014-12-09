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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Represents a generic range of <see cref="T:System.IComparable`1"/> objects.
    /// </summary>
    /// <typeparam name="T">The type of the range's units.</typeparam>
    [Serializable]
    [Browsable(false)]
    //[Editor(typeof (RangeEditor<>), typeof (UITypeEditor))]
    //[Editor(typeof (RangeEditor), typeof(UITypeEditor))]
    //[TypeConverter(typeof (RangeConverter<>))]
    public struct Range<T> where T : IComparable, IFormattable
    {
        /// <summary>
        /// Represents a <see cref="T:ExtensionLib.Range`1"/> that holds default <see cref="P:ExtensionLib.Range`1.Minimum"/> and <see cref="P:ExtensionLib.Range`1.Maximum"/> values.
        /// </summary>
        public static readonly Range<T> Empty = new Range<T>(); 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.Range`1"/> class with the specified extremums.
        /// </summary>
        /// <param name="minimum">The lower-bound value of the range.</param>
        /// <param name="maximum">The upper-bound value of the range.</param>
        public Range(T minimum, T maximum) : this()
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.Range`1"/> class with the specified extremums.
        /// </summary>
        /// <param name="value">The value held by both the lower- and the upper-bound.</param>
        public Range(T value) : this()
        {
            Minimum = value;
            Maximum = value;
        }

        /// <summary>
        /// Gets or sets the minimum value of the range.
        /// </summary>
        public T Minimum { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the range.
        /// </summary>
        public T Maximum { get; set; }

        /// <summary>
        /// Returns a string that represents this range.
        /// </summary>
        /// <returns>A human-readable representation of this range.</returns>
        public override String ToString() { return String.Format("[{0}, {1}]", this.Minimum, this.Maximum); }
        
        /// <summary>        
        /// Determines if the range is valid.
        /// </summary>
        /// <returns>A boolean value indicating whether the range is valid (i.e. if <see cref="P:ExtensionLib.Range.Minimum"/> is lesser or equal to <see cref="P:ExtensionLib.Range.Maximum"/>.</returns>
        public Boolean IsValid() { return this.Minimum.CompareTo(this.Maximum) <= 0; }

        /// <summary>
        /// Determines whether the provided value is inside the range.
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>A boolean value indicating whether the provided value is inside the range.</returns>
        public Boolean ContainsValue(T value)
        {
            return (this.Minimum.CompareTo(value) <= 0) && (value.CompareTo(this.Maximum) <= 0);
        }

        /// <summary>
        /// Determines whether the bounds of this range are situated within those of the supplied one.
        /// </summary>
        /// <param name="range">The parent range to test against.</param>
        /// <returns>A boolean value indicating whether the extremums of this range is inclusive.</returns>
        public Boolean IsInsideRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && range.ContainsValue(this.Minimum) && range.ContainsValue(this.Maximum);
        }

        /// <summary>
        /// Determines whether another range is contained within the bounds of this one.
        /// </summary>
        /// <param name="range">The child range to test.</param>
        /// <returns>A boolean value indicating whether the provided range is situated inside this one.</returns>
        public Boolean ContainsRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && this.ContainsValue(range.Minimum) && this.ContainsValue(range.Maximum);
        }
    }
}
