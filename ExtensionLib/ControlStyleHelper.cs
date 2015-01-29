//-----------------------------------------------------------------------
// <copyright file="ControlStyleHelper.cs" company="LouisTakePILLz">
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
using System.Reflection;
using System.Windows.Forms;

namespace ExtensionLib
{
    /// <summary>
    /// Provides static methods to alter controls' style attributes.
    /// </summary>
    public static class ControlStyleHelper
    {
        /// <summary>
        /// Sets one or more <see cref="T:System.Windows.Forms.ControlStyles"/> attributes to an array of controls.
        /// </summary>
        /// <param name="flag">One or more <see cref="T:System.Windows.Forms.ControlStyles"/> attributes to redefine.</param>
        /// <param name="value">The new state of the supplied <paramref name="flag"/> attributes.</param>
        /// <param name="controls">The controls to affect the style attribute(s) to.</param>
        public static void SetControlStyle(ControlStyles flag, Boolean value, params Control[] controls)
        {
            foreach (Control control in controls)
                SetControlStyle(control, flag, value);
        }

        /// <summary>
        /// Sets one or more <see cref="T:System.Windows.Forms.ControlStyles"/> attributes to a single control.
        /// </summary>
        /// <param name="control">The control to affect the style attribute(s) to.</param>
        /// <param name="flag">One or more <see cref="T:System.Windows.Forms.ControlStyles"/> attributes to redefine.</param>
        /// <param name="value">The new state of the supplied <paramref name="flag"/> attributes.</param>
        public static void SetControlStyle(this Control control, ControlStyles flag, Boolean value)
        {
            MethodInfo setStyleMethod = control.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            if (setStyleMethod == null)
                return;

            setStyleMethod.Invoke(control, new Object[] { flag, value });
        }
    }
}
