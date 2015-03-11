//-----------------------------------------------------------------------
// <copyright file="Range.cs" company="LouisTakePILLz">
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
using System.ComponentModel;

namespace ExtensionLib
{
    /// <summary>
    /// Represents a generic range of <see cref="T:System.IComparable`1"/> objects.
    /// </summary>
    /// <typeparam name="T">The type of the range's units.</typeparam>
    [Serializable]
    [Browsable(false)]
    ////[Editor(typeof (RangeEditor<>), typeof (UITypeEditor))]
    ////[Editor(typeof (RangeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof (RangeConverter<>))]
    public struct Range<T> where T : IComparable
    {
        /// <summary>
        /// Represents a <see cref="T:ExtensionLib.Range`1"/> that holds default <see cref="P:ExtensionLib.Range`1.Minimum"/> and <see cref="P:ExtensionLib.Range`1.Maximum"/> values.
        /// </summary>
        public static readonly Range<T> Empty = new Range<T>(); 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.Range`1"/> struct with the specified extrema.
        /// </summary>
        /// <param name="minimum">The lower-bound value of the range.</param>
        /// <param name="maximum">The upper-bound value of the range.</param>
        public Range(T minimum, T maximum) : this()
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.Range`1"/> struct with the specified extrema.
        /// </summary>
        /// <param name="value">The value held by both the lower- and the upper-bound.</param>
        public Range(T value) : this()
        {
            this.Minimum = value;
            this.Maximum = value;
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
        public override String ToString()
        {
            return String.Format("[{0}, {1}]", this.Minimum, this.Maximum);
        }

        /// <summary>
        /// Determines if the range is valid.
        /// </summary>
        /// <returns>A boolean value indicating whether the range is valid (i.e. if <see cref="P:ExtensionLib.Range.Minimum"/> is lesser or equal to <see cref="P:ExtensionLib.Range.Maximum"/>.</returns>
        public Boolean IsValid()
        {
            return this.IsValid(Comparer<T>.Default);
        }

        /// <summary>
        /// Determines if the range is valid.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A boolean value indicating whether the range is valid (i.e. if <see cref="P:ExtensionLib.Range.Minimum"/> is lesser or equal to <see cref="P:ExtensionLib.Range.Maximum"/>.</returns>
        public Boolean IsValid(IComparer<T> comparer)
        {
            return comparer.Compare(this.Minimum, this.Maximum) <= 0;
        }

        /// <summary>
        /// Determines whether the provided value is inside the range.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns>A boolean value indicating whether the provided value is inside the range.</returns>
        public Boolean ContainsValue(T value)
        {
            return this.ContainsValue(value, Comparer<T>.Default);
        }

        /// <summary>
        /// Determines whether the provided value is inside the range.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A boolean value indicating whether the provided value is inside the range.</returns>
        public Boolean ContainsValue(T value, IComparer<T> comparer)
        {
            return (comparer.Compare(this.Minimum, value) <= 0) && (comparer.Compare(value, this.Maximum) <= 0);
        }

        /// <summary>
        /// Determines whether the bounds of this range are situated within those of the supplied one.
        /// </summary>
        /// <param name="range">The parent range to test against.</param>
        /// <returns>A boolean value indicating whether the extrema of this range are inclusive.</returns>
        public Boolean IsInsideRange(Range<T> range)
        {
            return this.IsInsideRange(range, Comparer<T>.Default);
        }

        /// <summary>
        /// Determines whether the bounds of this range are situated within those of the supplied one.
        /// </summary>
        /// <param name="range">The parent range to test against.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A boolean value indicating whether the extrema of this range are inclusive.</returns>
        public Boolean IsInsideRange(Range<T> range, IComparer<T> comparer)
        {
            return this.IsValid(comparer) && range.IsValid(comparer) && range.ContainsValue(this.Minimum, comparer) && range.ContainsValue(this.Maximum, comparer);
        }

        /// <summary>
        /// Determines whether the supplied range is contained within the bounds of the current one.
        /// </summary>
        /// <param name="range">The child range to test.</param>
        /// <returns>A boolean value indicating whether the provided range is situated inside the current one.</returns>
        public Boolean ContainsRange(Range<T> range)
        {
            return this.ContainsRange(range, Comparer<T>.Default);
        }

        /// <summary>
        /// Determines whether the supplied range is contained within the bounds of the current one.
        /// </summary>
        /// <param name="range">The child range to test.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A boolean value indicating whether the provided range is situated inside the current one.</returns>
        public Boolean ContainsRange(Range<T> range, IComparer<T> comparer)
        {
            return this.IsValid(comparer) && range.IsValid(comparer) && this.ContainsValue(range.Minimum, comparer) && this.ContainsValue(range.Maximum, comparer);
        }

        /// <summary>
        /// Produces the union of two contiguous <see cref="T:ExtensionLib.Range`1"/>.
        /// </summary>
        /// <param name="other">The range to join.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A new range combining the furthest extrema.</returns>
        public Range<T> Union(Range<T> other, IComparer<T> comparer)
        {
            if (!this.IsValid(comparer) || !other.IsValid(comparer))
                throw new InvalidOperationException("Non-linear ranges can not be manipulated.");

            if (this.IsInsideRange(other, comparer))
                return other;

            if (this.ContainsRange(other, comparer))
                return this;

            if (comparer.Compare(this.Minimum, other.Minimum) == 0
                || comparer.Compare(this.Minimum, other.Maximum) == 0
                || comparer.Compare(this.Maximum, other.Maximum) == 0
                || comparer.Compare(this.Maximum, other.Minimum) == 0)
                return new Range<T>(
                    minimum: comparer.Compare(this.Minimum, other.Minimum) < 0 ? this.Minimum : other.Minimum,
                    maximum: comparer.Compare(this.Maximum, other.Maximum) > 0 ? this.Maximum : other.Maximum);

            throw new InvalidOperationException("Non-contiguous ranges can not be joined.");
        }

        /// <summary>
        /// Produces the union of two contiguous <see cref="T:ExtensionLib.Range`1"/>.
        /// </summary>
        /// <param name="other">The range to join.</param>
        /// <returns>A new range combining the furthest extrema.</returns>
        public Range<T> Union(Range<T> other)
        {
            return this.Union(other, Comparer<T>.Default);
        }

        /// <summary>
        /// Produces the intersection of two overlapping <see cref="T:ExtensionLib.Range`1"/>.
        /// </summary>
        /// <param name="other">The range to intersect.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to use to compare the entries.</param>
        /// <returns>A new range constituted of the furthest shared values.</returns>
        public Range<T> Intersect(Range<T> other, IComparer<T> comparer)
        {
            if (!this.IsValid(comparer) || !other.IsValid(comparer))
                throw new InvalidOperationException("Non-linear ranges can not be manipulated.");

            var thisContainsMinimum = this.ContainsValue(other.Minimum, comparer);
            var thisContainsMaximum = this.ContainsValue(other.Maximum, comparer);
            var otherContainsMinimum = other.ContainsValue(this.Minimum, comparer);
            var otherContainsMaximum = other.ContainsValue(this.Maximum, comparer);

            if (thisContainsMinimum || thisContainsMaximum || otherContainsMinimum || otherContainsMaximum)
                return new Range<T>(
                    minimum: thisContainsMinimum
                        ? other.Minimum
                        : (otherContainsMinimum ? this.Minimum : other.Minimum),
                    maximum: thisContainsMaximum
                        ? other.Maximum
                        : (otherContainsMaximum ? this.Maximum : other.Maximum));

            throw new InvalidOperationException("Non-overlapping ranges can not be intersected.");
        }

        /// <summary>
        /// Produces the intersection of two overlapping <see cref="T:ExtensionLib.Range`1"/>.
        /// </summary>
        /// <param name="other">The range to intersect.</param>
        /// <returns>A new range constituted of the furthest shared values.</returns>
        public Range<T> Intersect(Range<T> other)
        {
            return this.Intersect(other, Comparer<T>.Default);
        }
    }
}
