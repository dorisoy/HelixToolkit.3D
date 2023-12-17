﻿/*
The MIT License (MIT)
Copyright (c) 2022 Helix Toolkit contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Original code from:
SharpDX project. https://github.com/sharpdx/SharpDX
SlimMath project. http://code.google.com/p/slimmath/

Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
The MIT License (MIT)
Copyright (c) 2007-2011 SlimDX Group
The MIT License (MIT)
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Numerics;
using Matrix = System.Numerics.Matrix4x4;
namespace HelixToolkit.Maths
{
    /// <summary>
    /// Defines a three component vector, using half precision floating point coordinates.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Half3 : IEquatable<Half3>
    {
        /// <summary>
        /// Gets or sets the X component of the vector.
        /// </summary>
        /// <value>The X component of the vector.</value>
        public Half X;
        /// <summary>
        /// Gets or sets the Y component of the vector.
        /// </summary>
        /// <value>The Y component of the vector.</value>
        public Half Y;
        /// <summary>
        /// Gets or sets the Z component of the vector.
        /// </summary>
        /// <value>The Z component of the vector.</value>
        public Half Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Half3" /> structure.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        public Half3(Half x, Half y, Half z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Half3" /> structure.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        public Half3(float x, float y, float z)
        {
            this.X = new Half(x);
            this.Y = new Half(y);
            this.Z = new Half(z);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Half3" /> structure.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        public Half3(ushort x, ushort y, ushort z)
        {
            this.X = new Half(x);
            this.Y = new Half(y);
            this.Z = new Half(z);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Half3" /> structure.
        /// </summary>
        /// <param name="value">The value to set for the X, Y, and Z components.</param>
        public Half3(Half value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector3"/> to <see cref="Half3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Half3(Vector3 value)
        {
            return new Half3(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Half3"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector3(Half3 value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector2"/> to <see cref="Half3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Half3(Vector2 value)
        {
            return new Half3(value.X, value.Y, 0.0f);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Half3"/> to <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector2(Half3 value)
        {
            return new Vector2(value.X, value.Y);
        }

        /// <summary>
        /// Tests for equality between two objects.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> has the same value as <paramref name="right" />; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // MethodImplOptions.AggressiveInlining
        public static bool operator ==(Half3 left, Half3 right)
        {
            return Equals(ref left, ref right);
        }

        /// <summary>
        /// Tests for inequality between two objects.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> has a different value than <paramref name="right" />; otherwise, <c>false</c>.</returns>
        [return: MarshalAs(UnmanagedType.U1)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // MethodImplOptions.AggressiveInlining
        public static bool operator !=(Half3 left, Half3 right)
        {
            return !Equals(ref left, ref right);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal. 
        /// </summary>
        /// <param name="value1" />
        /// <param name="value2" />
        /// <returns>
        /// <c>true</c> if <paramref name="value1" /> is the same instance as <paramref name="value2" /> or 
        /// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // MethodImplOptions.AggressiveInlining
        public static bool Equals(ref Half3 value1, ref Half3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to the specified object. 
        /// </summary>
        /// <param name="other">Object to make the comparison with.</param>
        /// <returns>
        /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        public bool Equals(Half3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to a specified object. 
        /// </summary>
        /// <param name="obj">Object to make the comparison with.</param>
        /// <returns>
        /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Half3 half && this.Equals(half);
        }
    }
}