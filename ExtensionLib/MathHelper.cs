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
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace ExtensionLib
{
    /// <summary>
    /// Provides miscellaneous mathematical functions.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Computes the euclidean distance of two <see cref="T:System.Drawing.PointF"/>.
        /// </summary>
        /// <param name="firstPoint">The first point on the plane.</param>
        /// <param name="secondPoint">The second point on the plane.</param>
        /// <returns>The euclidean distance between two points.</returns>
        public static Double Distance(PointF firstPoint, PointF secondPoint)
        {
            return firstPoint.Equals(secondPoint)
                ? 0
                : Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }

        /// <summary>
        /// Computes the projected euclidean distance of two <see cref="T:System.Drawing.PointF"/>.
        /// </summary>
        /// <param name="scalar">The magnitude factor of the metric.</param>
        /// <param name="firstPoint">The first point on the plane.</param>
        /// <param name="secondPoint">The second point on the plane.</param>
        /// <returns>The euclidean distance of two <see cref="T:System.Drawing.PointF"/> multiplied by a given magnitude.</returns>
        public static Double Distance(Double scalar, PointF firstPoint, PointF secondPoint)
        {
            return firstPoint.Equals(secondPoint)
                ? 0
                : scalar *
                  Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }

        /// <summary>
        /// Computes the projected euclidean distance of two <see cref="T:System.Drawing.PointF"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the <paramref name="vectors"/> parameter.</typeparam>
        /// <param name="scalar">The magnitude factor of the metric.</param>
        /// <param name="vectors">An array of <typeparamref name="T"/> representing vectorial lengths.</param>
        /// <returns>The euclidean distance of the supplied vectorial lengths.</returns>
        public static Double Distance<T>(Double scalar, params T[] vectors)
            where T : IConvertible
        {
            if (!vectors.Any())
                return 0;
            if (vectors.Length == 1)
                return vectors.First().ToDouble(CultureInfo.CurrentUICulture);
            return scalar * Math.Sqrt(vectors.Sum(t => Math.Pow(t.ToDouble(CultureInfo.CurrentUICulture), 2)));
        }

        /// <summary>
        /// Computes the euclidean distance of two series of matching dimension rank.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the series.</typeparam>
        /// <param name="firstSeries">The first series of values.</param>
        /// <param name="secondSeries">The second series of values.</param>
        /// <returns>The euclidean distance of two <see cref="T:System.Drawing.PointF"/>.</returns>
        public static Double Distance<T>(IEnumerable<T> firstSeries, IEnumerable<T> secondSeries)
            where T : IConvertible
        { return Distance<T>(1, firstSeries, secondSeries); }

        /// <summary>
        /// Computes the euclidean distance of two series of matching dimension rank.
        /// </summary>
        /// <typeparam name="T1">The type of the elements in <paramref name="firstSeries"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements in <paramref name="secondSeries"/>.</typeparam>
        /// <param name="firstSeries">The first series of values.</param>
        /// <param name="secondSeries">The second series of values.</param>
        /// <returns>The euclidean distance of two <see cref="T:System.Drawing.PointF"/> multiplied by a given magnitude.</returns>
        public static Double Distance<T1, T2>(IEnumerable<T1> firstSeries, IEnumerable<T2> secondSeries)
            where T1 : IConvertible
            where T2 : IConvertible
        { return Distance(1, firstSeries, secondSeries); }

        /// <summary>
        /// Computes the projected euclidean distance of two series of matching dimension rank.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the series.</typeparam>
        /// <param name="scalar">The magnitude factor of the metric.</param>
        /// <param name="firstSeries">The first series of values.</param>
        /// <param name="secondSeries">The second series of values.</param>
        /// <returns>The euclidean distance of the two supplied series.</returns>
        public static Double Distance<T>(Double scalar, IEnumerable<T> firstSeries, IEnumerable<T> secondSeries)
            where T : IConvertible
        {
            IList<T> firstSeriesList = firstSeries as T[] ?? firstSeries.ToArray();
            IList<T> secondSeriesList = secondSeries as T[] ?? secondSeries.ToArray();
            if (!firstSeriesList.Any() || !secondSeriesList.Any())
                throw new ArgumentException("An empty dimension count can not be computed.");
            if (firstSeriesList.Count() != secondSeriesList.Count())
                throw new ArgumentException("The dimension ranks of the supplied series do not match.");
            return Distance(scalar,
                firstSeriesList.Zip(secondSeriesList,
                    (t1, t2) => (IConvertible) (t1.ToDouble(CultureInfo.CurrentUICulture) - t2.ToDouble(CultureInfo.CurrentUICulture))).ToArray());
        }

        /// <summary>
        /// Computes the projected euclidean distance of two series of matching dimension rank.
        /// </summary>
        /// <typeparam name="T1">The type of the elements in <paramref name="firstSeries"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements in <paramref name="secondSeries"/>.</typeparam>
        /// <param name="scalar">The magnitude factor of the metric.</param>
        /// <param name="firstSeries">The first series of values.</param>
        /// <param name="secondSeries">The second series of values.</param>
        /// <returns>The euclidean distance of the two supplied series.</returns>
        public static Double Distance<T1, T2>(Double scalar, IEnumerable<T1> firstSeries, IEnumerable<T2> secondSeries)
            where T1 : IConvertible
            where T2 : IConvertible
        {
            return Distance<Double>(scalar,
                firstSeries.Select(x => x.ToDouble(CultureInfo.CurrentUICulture)),
                secondSeries.Select(x => x.ToDouble(CultureInfo.CurrentUICulture)));
        }
    }
}
