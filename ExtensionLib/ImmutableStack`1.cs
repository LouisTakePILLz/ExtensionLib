//-----------------------------------------------------------------------
// <copyright file="ImmutableStack`1.cs" company="LouisTakePILLz">
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

// The idea behind this class came from Eric Lippert's blog: http://blogs.msdn.com/b/ericlippert/archive/2007/12/04/immutability-in-c-part-two-a-simple-immutable-stack.aspx

using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtensionLib
{
    /// <summary>
    /// Represents a generic immutable stack.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    public class ImmutableStack<T> : IStack<T>
    {
        private sealed class EmptyStack : IStack<T>
        {
            public EmptyStack() { }

            public T Peek() { throw new InvalidOperationException("An empty stack cannot be peeked on."); }

            public IStack<T> Push(T value) { return new ImmutableStack<T>(value, this); }
            
            public IStack<T> Pop() { throw new InvalidOperationException("An empty stack cannot be popped."); }
            
            public IEnumerator<T> GetEnumerator() { yield break; }
            
            IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
            
            public Boolean IsEmpty { get { return true; } }
        }
        
        private static readonly EmptyStack emptyStack = new EmptyStack();
        private readonly T head;
        private readonly IStack<T> tail;

        /// <summary>
        /// Represents an empty <see cref="T:ExtensionLib.ImmutableStack`1"/>.
        /// </summary>
        public static IStack<T> Empty { get { return emptyStack; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.ImmutableStack`1"/> class.
        /// </summary>
        /// <param name="head">The object at the top of the stack.</param>
        /// <param name="tail">The child stack.</param>
        public ImmutableStack(T head, IStack<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExtensionLib.ImmutableStack`1"/> class.
        /// </summary>
        /// <param name="element">The object at the top of the stack.</param>
        public ImmutableStack(T element)
        {
            this.head = element;
            this.tail = Empty;
        }

        /// <summary>
        /// Inserts an object at the top of the <see cref="T:ExtensionLib.ImmutableStack`1"/> and returns the newly formed stack.
        /// </summary>
        /// <param name="value">The element to add to the stack.</param>
        /// <returns>The newly formed stack.</returns>
        public IStack<T> Push(T value)
        {
            return new ImmutableStack<T>(value, this);
        }
        
        /// <summary>
        /// Removes the object at the top of the <see cref="T:ExtensionLib.ImmutableStack`1"/> and return the newly created stack.
        /// </summary>
        /// <returns>The newly formed stack.</returns>
        public IStack<T> Pop()
        {
            return this.tail;
        }

        /// <summary>
        /// Returns the object at the top of the <see cref="T:ExtensionLib.ImmutableStack`1"/>.
        /// </summary>
        /// <returns>The object at the top of the stack.</returns>
        public T Peek()
        {
            return this.head;
        }

        /// <summary>
        /// Gets a boolean value indicating whether the <see cref="T:ExtensionLib.ImmutableStack`1"/> is empty.
        /// </summary>
        public Boolean IsEmpty { get { return false; } }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:ExtensionLib.ImmutableStack`1"/>.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1"/> object that can be used to iterate through the stack.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (IStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
                yield return stack.Peek();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a <see cref="T:ExtensionLib.ImmutableStack`1"/>.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the stack.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
