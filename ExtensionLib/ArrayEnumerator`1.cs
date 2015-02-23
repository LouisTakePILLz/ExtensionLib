//-----------------------------------------------------------------------
// <copyright file="ArrayEnumerator`1.cs" company="LouisTakePILLz">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionLib
{
    /// <summary>
    /// Supports a simple iteration over a generic collection while providing information relative to the position of the enumerator.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    public class ArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly Int32[] positions;
        private readonly Array array;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.ArrayEnumerator`1"/> class.
        /// </summary>
        protected ArrayEnumerator() { this.Index = -1; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.ArrayEnumerator`1"/> class.
        /// </summary>
        public ArrayEnumerator(Array array)
            : this()
        {
            if (array == null)
                throw new ArgumentNullException("array");

            this.enumerator = array.Cast<T>().GetEnumerator();
            this.array = array;
            this.positions = new Int32[array.Rank];
            this.positions[this.positions.Length - 1] = -1;
        }

        /// <summary>
        /// Gets the current position of the enumerator.
        /// </summary>
        public Int64 Index { get; private set; }

        /// <summary>
        /// Gets the current positions within the multiple dimensions of the enumerator.
        /// </summary>
        public IReadOnlyList<Int32> Positions { get { return this.positions; } }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>A boolean value indicating whether the enumerator successfully advanced to the next element (true) or if the enumerator has passed the end of the collection (false).</returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public Boolean MoveNext()
        {
            if (!this.enumerator.MoveNext())
            {
                this.Index = -1;
                return false;
            }

            this.Index++;
            for (int i = this.positions.Length - 1; i >= 0; i--)
            {
                var parentIndex = i - 1;

                if (this.positions[i] < 0 || parentIndex < 0)
                    this.positions[i]++;
                else if (++this.positions[i] % this.array.GetLength(parentIndex) == 0)
                {
                    this.positions[i] = 0;
                    continue;
                }

                break;
            }

            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public void Reset()
        {
            this.enumerator.Reset();
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>The element in the collection at the current position of the enumerator.</returns>
        public T Current { get { return this.enumerator.Current; } }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        Object IEnumerator.Current
        {
            get { return this.Current; }
        }
    }

    public partial class ExtensionMethods
    {
        /// <summary>
        /// Returns an <see cref="T:ExtensionLib.ArrayEnumerator`1"/> for a supplied array.
        /// </summary>
        /// <param name="array">The array to use to create the enumerator.</param>
        /// <typeparam name="T">The type of the objects to enumerate.</typeparam>
        /// <returns>The created <see cref="T:ExtensionLib.ArrayEnumerator`1"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        public static ArrayEnumerator<T> GetArrayEnumerator<T>(this Array array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            return new ArrayEnumerator<T>(array);
        }
    }
}
