// <copyright file="Cords.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using Windows.Foundation.Metadata;
using Point = Windows.Foundation.Point;
using Size = Windows.Foundation.Size;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Contains element coordinates.
    /// </summary>
    public sealed class Cords
    {
        internal AdvancedSharpAdbClient.Cords cords;

        /// <summary>
        /// Creates a new instance of the <see cref='Cords'/> class with member data left uninitialized.
        /// </summary>
        public static Cords Empty => new(AdvancedSharpAdbClient.Cords.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="Cords"/> class.
        /// </summary>
        /// <param name="cx">The horizontal "X" coordinate.</param>
        /// <param name="cy">The vertical "Y" coordinate.</param>
        public Cords(int cx, int cy) => cords = new(cx, cy);

        /// <summary>
        /// Initializes a new instance of the <see cref="Cords"/> class using coordinates specified by an integer _value.
        /// </summary>
        public Cords(int dw) => cords = new(dw);

        internal Cords(AdvancedSharpAdbClient.Cords cords) => this.cords = cords;

        internal static Cords GetCords(AdvancedSharpAdbClient.Cords cords) => new(cords);

        /// <summary>
        /// Gets a _value indicating whether this <see cref='Cords'/> is empty.
        /// </summary>
        public bool IsEmpty => cords.IsEmpty;

        /// <summary>
        /// Gets or sets the horizontal "X" coordinate.
        /// </summary>
        public int X
        {
            get => cords.X;
            set => cords.X = value;
        }

        /// <summary>
        /// Gets or sets the vertical "Y" coordinate.
        /// </summary>
        public int Y
        {
            get => cords.Y;
            set => cords.Y = value;
        }

        /// <summary>
        /// Translates a <see cref='Cords'/> by a given <see cref='Size'/>.
        /// </summary>
        /// <param name="pt">The <see cref='Cords'/> to add.</param>
        /// <param name="sz">The <see cref='Size'/> to add.</param>
        /// <returns>The <see cref='Cords'/> that is the result of the addition operation.</returns>
        public static Cords Add(Cords pt, Size sz) => GetCords(AdvancedSharpAdbClient.Cords.Add(pt.cords, sz));

        /// <summary>
        /// Translates a <see cref='Cords'/> by the negative of a given <see cref='Size'/>.
        /// </summary>
        /// <param name="pt">The <see cref='Cords'/> to be subtracted from.</param>
        /// <param name="sz">The <see cref='Size'/> to subtract from the Point.</param>
        /// <returns>The <see cref='Cords'/> that is the result of the subtraction operation.</returns>
        public static Cords Subtract(Cords pt, Size sz) => GetCords(AdvancedSharpAdbClient.Cords.Subtract(pt.cords, sz));

        /// <summary>
        /// Converts a <see cref='Point'/> to a <see cref='Cords'/> by performing a ceiling operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref='Point'/> to convert.</param>
        /// <returns>The <see cref='Cords'/> this method converts to.</returns>
        public static Cords Ceiling(Point _value) => GetCords(AdvancedSharpAdbClient.Cords.Ceiling(_value));

        /// <summary>
        /// Converts a <see cref='Point'/> to a <see cref='Cords'/> by performing a truncate operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref='Point'/> to convert.</param>
        /// <returns>The <see cref='Cords'/> this method converts to.</returns>
        public static Cords Truncate(Point _value) => GetCords(AdvancedSharpAdbClient.Cords.Truncate(_value));

        /// <summary>
        /// Converts a <see cref='Point'/> to a <see cref='Cords'/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref='Point'/> to convert.</param>
        /// <returns>The <see cref='Cords'/> this method converts to.</returns>
        public static Cords Round(Point _value) => GetCords(AdvancedSharpAdbClient.Cords.Round(_value));

        /// <summary>
        /// Specifies whether this <see cref='Cords'/> contains the same coordinates as the specified
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref='Cords'/> and has the same coordinates as this point instance.</returns>
        public override bool Equals(object obj) => obj is Cords cords && this.cords.Equals(cords.cords);

        /// <summary>
        /// Specifies whether this <see cref='Cords'/> contains the same coordinates as the specified
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="other">The point to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="other"/> has the same coordinates as this point instance.</returns>
        [DefaultOverload]
        public bool Equals(Cords other) => cords.Equals(other.cords);

        /// <summary>
        /// Returns a hash code for this <see cref="Cords"/>.
        /// </summary>
        /// <returns>An integer _value that specifies a hash _value for this <see cref="Cords"/>.</returns>
        public override int GetHashCode() => cords.GetHashCode();

        /// <summary>
        /// Translates this <see cref='Cords'/> by the specified amount.
        /// </summary>
        /// <param name="dx">The amount to offset the x-coordinate.</param>
        /// <param name="dy">The amount to offset the y-coordinate.</param>
        public void Offset(int dx, int dy) => cords.Offset(dx, dy);

        /// <summary>
        /// Translates this <see cref='Cords'/> by the specified amount.
        /// </summary>
        /// <param name="p">The <see cref='Cords'/> used offset this <see cref='Cords'/>.</param>
        public void Offset(Cords p) => cords.Offset(p.cords);

        /// <summary>
        /// Converts this <see cref='Cords'/> to a human readable string.
        /// </summary>
        /// <returns>A string that represents this <see cref='Cords'/>.</returns>
        public override string ToString() => cords.ToString();

        /// <summary>
        /// Deconstruct the <see cref="Cords"/> class.
        /// </summary>
        /// <param name="cx">The horizontal "X" coordinate.</param>
        /// <param name="cy">The vertical "Y" coordinate.</param>
        public void Deconstruct(out int cx, out int cy) => cords.Deconstruct(out cx, out cy);
    }
}
