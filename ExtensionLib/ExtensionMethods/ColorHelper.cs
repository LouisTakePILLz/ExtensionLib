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
using System.Drawing;

namespace ExtensionLib
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Interpolates two colors the R, G and B channels (while preserving the source alpha level) using a supplied scale.
        /// </summary>
        /// <param name="source">The source color to interpolate.</param>
        /// <param name="target">The target color to interpolate.</param>
        /// <param name="scale">The intensity of the target color.</param>
        /// <returns>The newly blended color.</returns>
        public static Color InterpolateRGB(this Color source, Color target, Double scale)
        {
            var r = (byte) (source.R + (target.R - source.R) * scale);
            var g = (byte) (source.G + (target.G - source.G) * scale);
            var b = (byte) (source.B + (target.B - source.B) * scale);

            return Color.FromArgb(source.A, r, g, b);
        }

        /// <summary>
        /// Interpolates linearly two colors the R, G, B and alpha channels using a supplied scale.
        /// </summary>
        /// <param name="source">The source color to interpolate.</param>
        /// <param name="target">The target color to interpolate.</param>
        /// <param name="scale">The intensity of the target color.</param>
        /// <returns>The newly blended color.</returns>
        public static Color InterpolateARGB(this Color source, Color target, Double scale)
        {
            var a = (byte) (source.A + (target.A - source.A) * scale);
            var r = (byte) (source.R + (target.R - source.R) * scale);
            var g = (byte) (source.G + (target.G - source.G) * scale);
            var b = (byte) (source.B + (target.B - source.B) * scale);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
