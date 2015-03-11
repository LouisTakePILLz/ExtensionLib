//-----------------------------------------------------------------------
// <copyright file="IStack`1.cs" company="LouisTakePILLz">
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

namespace ExtensionLib
{
    /// <summary>
    /// Represents a last-in-first-out (LIFO) generic collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the stack.</typeparam>
    public interface IStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// Inserts an object at the top of the <see cref="T:ExtensionLib.IStack`1"/> and returns the newly formed stack.
        /// </summary>
        /// <param name="value">The element to add to the stack.</param>
        /// <returns>The newly formed stack.</returns>
        IStack<T> Push(T value);

        /// <summary>
        /// Removes the object at the top of the <see cref="T:ExtensionLib.IStack`1"/> and return the newly created stack.
        /// </summary>
        /// <returns>The newly formed stack.</returns>
        IStack<T> Pop();

        /// <summary>
        /// Returns the object at the top of the <see cref="T:ExtensionLib.IStack`1"/>.
        /// </summary>
        /// <returns>The object at the top of the stack.</returns>
        T Peek();
        
        /// <summary>
        /// Gets a boolean value indicating whether the <see cref="T:ExtensionLib.IStack`1"/> is empty.
        /// </summary>
        Boolean IsEmpty { get; }
    }
}
