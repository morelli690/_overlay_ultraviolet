﻿using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Ultraviolet.Core;

namespace Ultraviolet
{
    /// <summary>
    /// Represents a two-dimensional area with single-precision floating point components.
    /// </summary>
    [Serializable]
    [DebuggerDisplay(@"\{Width:{Width} Height:{Height}\}")]
    public struct Size2F : IEquatable<Size2F>, IInterpolatable<Size2F>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Size2F"/> structure.
        /// </summary>
        /// <param name="width">The area's width.</param>
        /// <param name="height">The area's height.</param>
        [Preserve]
        [JsonConstructor]
        public Size2F(Single width, Single height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Compares two sizes for equality.
        /// </summary>
        /// <param name="s1">The first <see cref="Size2F"/> to compare.</param>
        /// <param name="s2">The second <see cref="Size2F"/> to compare.</param>
        /// <returns><see langword="true"/> if the specified sizes are equal; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public static Boolean operator ==(Size2F s1, Size2F s2)
        {
            return s1.Equals(s2);
        }

        /// <summary>
        /// Compares two sizes for inequality.
        /// </summary>
        /// <param name="s1">The first <see cref="Size2F"/> to compare.</param>
        /// <param name="s2">The second <see cref="Size2F"/> to compare.</param>
        /// <returns><see langword="true"/> if the specified sizes are unequal; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public static Boolean operator !=(Size2F s1, Size2F s2)
        {
            return !s1.Equals(s2);
        }

        /// <summary>
        /// Adds a <see cref="Size2F"/> to another <see cref="Size2F"/>.
        /// </summary>
        /// <param name="s1">The <see cref="Size2F"/> on the left side of the operator.</param>
        /// <param name="s2">The <see cref="Size2F"/> on the right side of the operator.</param>
        /// <returns>The result of adding the two instances.</returns>
        [Preserve]
        public static Size2F operator +(Size2F s1, Size2F s2)
        {
            Size2F result;

            result.Width = s1.Width + s2.Width;
            result.Height = s1.Height + s2.Height;

            return result;
        }

        /// <summary>
        /// Subtracts a <see cref="Size2F"/> from another <see cref="Size2F"/>.
        /// </summary>
        /// <param name="s1">The <see cref="Size2F"/> on the left side of the operator.</param>
        /// <param name="s2">The <see cref="Size2F"/> on the right side of the operator.</param>
        /// <returns>The result of subtracting the two instances.</returns>
        [Preserve]
        public static Size2F operator -(Size2F s1, Size2F s2)
        {
            Size2F result;

            result.Width = s1.Width - s2.Width;
            result.Height = s1.Height - s2.Height;

            return result;
        }

        /// <summary>
        /// Multiplies a <see cref="Size2F"/> by a scalar multiplier.
        /// </summary>
        /// <param name="size">The size to multiply.</param>
        /// <param name="multiplier">The multiplier to apply to the size.</param>
        /// <returns>A <see cref="Size2F"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2F operator *(Size2F size, Int32 multiplier)
        {
            Size2F result;

            result.Width = size.Width * multiplier;
            result.Height = size.Height * multiplier;

            return result;
        }

        /// <summary>
        /// Multiplies a <see cref="Size2F"/> by a scalar multiplier.
        /// </summary>
        /// <param name="size">The size to multiply.</param>
        /// <param name="multiplier">The multiplier to apply to the size.</param>
        /// <returns>A <see cref="Size2F"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2F operator *(Size2F size, Single multiplier)
        {
            Size2F result;

            result.Width = size.Width * multiplier;
            result.Height = size.Height * multiplier;

            return result;
        }

        /// <summary>
        /// Multiplies a <see cref="Size2F"/> by a scalar multiplier.
        /// </summary>
        /// <param name="size">The size to multiply.</param>
        /// <param name="multiplier">The multiplier to apply to the size.</param>
        /// <returns>A <see cref="Size2D"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2D operator *(Size2F size, Double multiplier)
        {
            Size2D result;

            result.Width = size.Width * multiplier;
            result.Height = size.Height * multiplier;

            return result;
        }

        /// <summary>
        /// Divides a <see cref="Size2F"/> by a scalar divisor.
        /// </summary>
        /// <param name="size">The size to divide.</param>
        /// <param name="divisor">The divisor to apply to the size.</param>
        /// <returns>A <see cref="Size2F"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2F operator /(Size2F size, Int32 divisor)
        {
            Size2F result;

            result.Width = size.Width / divisor;
            result.Height = size.Height / divisor;

            return result;
        }

        /// <summary>
        /// Divides a <see cref="Size2F"/> by a scalar divisor.
        /// </summary>
        /// <param name="size">The size to divide.</param>
        /// <param name="divisor">The divisor to apply to the size.</param>
        /// <returns>A <see cref="Size2F"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2F operator /(Size2F size, Single divisor)
        {
            Size2F result;

            result.Width = size.Width / divisor;
            result.Height = size.Height / divisor;

            return result;
        }

        /// <summary>
        /// Divides a <see cref="Size2F"/> by a scalar divisor.
        /// </summary>
        /// <param name="size">The size to divide.</param>
        /// <param name="divisor">The divisor to apply to the size.</param>
        /// <returns>A <see cref="Size2D"/> which is the result of the muliplication.</returns>
        [Preserve]
        public static Size2D operator /(Size2F size, Double divisor)
        {
            Size2D result;

            result.Width = size.Width / divisor;
            result.Height = size.Height / divisor;

            return result;
        }

        /// <summary>
        /// Explicitly converts a <see cref="Size2F"/> structure to a <see cref="Vector2"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static explicit operator Vector2(Size2F size)
        {
            Vector2 result;

            result.X = size.Width;
            result.Y = size.Height;

            return result;
        }

        /// <summary>
        /// Explicitly converts a <see cref="Size2F"/> structure to a <see cref="Point2"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static explicit operator Point2(Size2F size)
        {
            Point2 result;

            result.X = (Int32)size.Width;
            result.Y = (Int32)size.Height;

            return result;
        }

        /// <summary>
        /// Explicitly converts a <see cref="Size2F"/> structure to a <see cref="Point2F"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static explicit operator Point2F(Size2F size)
        {
            Point2F result;

            result.X = size.Width;
            result.Y = size.Height;

            return result;
        }

        /// <summary>
        /// Explicitly converts a <see cref="Size2F"/> structure to a <see cref="Point2D"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static explicit operator Point2D(Size2F size)
        {
            Point2D result;

            result.X = size.Width;
            result.Y = size.Height;

            return result;
        }

        /// <summary>
        /// Explicitly converts a <see cref="Size2F"/> structure to a <see cref="Size2"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static explicit operator Size2(Size2F size)
        {
            Size2 result;

            result.Width = (Int32)size.Width;
            result.Height = (Int32)size.Height;

            return result;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Size2F"/> structure to a <see cref="Size2D"/> structure.
        /// </summary>
        /// <param name="size">The structure to convert.</param>
        /// <returns>The converted structure.</returns>
        [Preserve]
        public static implicit operator Size2D(Size2F size)
        {
            Size2D result;

            result.Width = size.Width;
            result.Height = size.Height;

            return result;
        }

        /// <summary>
        /// Converts the string representation of an area into an instance of the <see cref="Size2F"/> structure.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a size to convert.</param>
        /// <param name="size">A variable to populate with the converted value.</param>
        /// <returns><see langword="true"/> if <paramref name="s"/> was converted successfully; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public static Boolean TryParse(String s, out Size2F size)
        {
            return TryParse(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out size);
        }

        /// <summary>
        /// Converts the string representation of an area into an instance of the <see cref="Size2F"/> structure.
        /// </summary>
        /// <param name="s">A string containing an area to convert.</param>
        /// <returns>A instance of the <see cref="Size2F"/> structure equivalent to the area contained in <paramref name="s"/>.</returns>
        [Preserve]
        public static Size2F Parse(String s)
        {
            return Parse(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the string representation of an area into an instance of the <see cref="Size2F"/> structure.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a size to convert.</param>
        /// <param name="style">A set of <see cref="NumberStyles"/> values indicating which elements are present in <paramref name="s"/>.</param>
        /// <param name="provider">A format provider that provides culture-specific formatting information.</param>
        /// <param name="size">A variable to populate with the converted value.</param>
        /// <returns><see langword="true"/> if <paramref name="s"/> was converted successfully; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public static Boolean TryParse(String s, NumberStyles style, IFormatProvider provider, out Size2F size)
        {
            size = default(Size2F);

            if (String.IsNullOrEmpty(s))
                return false;

            var components = s.Split((Char[])null, StringSplitOptions.RemoveEmptyEntries);
            if (components.Length != 2)
                return false;

            Single width, height;
            if (!Single.TryParse(components[0], style, provider, out width))
                return false;
            if (!Single.TryParse(components[1], style, provider, out height))
                return false;

            size = new Size2F(width, height);
            return true;
        }

        /// <summary>
        /// Converts the string representation of an area into an instance of the <see cref="Size2F"/> structure.
        /// </summary>
        /// <param name="s">A string containing an area to convert.</param>
        /// <param name="style">A set of <see cref="NumberStyles"/> values indicating which elements are present in <paramref name="s"/>.</param>
        /// <param name="provider">A format provider that provides culture-specific formatting information.</param>
        /// <returns>A instance of the <see cref="Size2F"/> structure equivalent to the area contained in <paramref name="s"/>.</returns>
        [Preserve]
        public static Size2F Parse(String s, NumberStyles style, IFormatProvider provider)
        {
            Size2F area;
            if (!TryParse(s, style, provider, out area))
            {
                throw new FormatException();
            }
            return area;
        }

        /// <summary>
        /// Gets the object's hash code.
        /// </summary>
        /// <returns>The object's hash code.</returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Converts the object to a human-readable string.
        /// </summary>
        /// <returns>A human-readable string that represents the object.</returns>
        public override String ToString()
        {
            return ToString(null);
        }

        /// <summary>
        /// Converts the object to a human-readable string using the specified culture information.
        /// </summary>
        /// <param name="provider">A format provider that provides culture-specific formatting information.</param>
        /// <returns>A human-readable string that represents the object.</returns>
        public String ToString(IFormatProvider provider)
        {
            return String.Format(provider, "{0} {1}", Width, Height);
        }

        /// <summary>
        /// Determines whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if this instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public override Boolean Equals(Object obj)
        {
            if (!(obj is Size2F))
                return false;
            return Equals((Size2F)obj);
        }

        /// <summary>
        /// Determines whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if this instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        [Preserve]
        public Boolean Equals(Size2F other)
        {
            return Width == other.Width && Height == other.Height;
        }

        /// <summary>
        /// Interpolates between this value and the specified value.
        /// </summary>
        /// <param name="target">The target value.</param>
        /// <param name="t">A value between 0.0 and 1.0 representing the interpolation factor.</param>
        /// <returns>The interpolated value.</returns>
        [Preserve]
        public Size2F Interpolate(Size2F target, Single t)
        {
            Size2F result;

            result.Width = Tweening.Lerp(this.Width, target.Width, t);
            result.Height = Tweening.Lerp(this.Height, target.Height, t);

            return result;
        }

        /// <summary>
        /// Gets an area with zero width and height.
        /// </summary>
        public static Size2F Zero
        {
            get { return new Size2F(0, 0); }
        }

        /// <summary>
        /// Gets the size's total area (width times height).
        /// </summary>
        [JsonIgnore]
        public Single Area
        {
            get { return Width * Height; }
        }

        /// <summary>
        /// The area's width.
        /// </summary>
        [Preserve]
        [JsonProperty(PropertyName = "width", Required = Required.Always)]
        public Single Width;

        /// <summary>
        /// The area's height.
        /// </summary>
        [Preserve]
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public Single Height;
    }
}
