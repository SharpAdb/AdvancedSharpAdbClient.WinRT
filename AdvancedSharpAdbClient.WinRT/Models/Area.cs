// <copyright file="Area.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using Windows.Foundation;
using Windows.Foundation.Metadata;
using Rect = Windows.Foundation.Rect;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Stores the location and size of a rectangular region.
    /// </summary>
    public sealed class Area
    {
        internal AdvancedSharpAdbClient.Area area;

        /// <summary>
        /// Creates a new instance of the <see cref='Cords'/> class with member data left uninitialized.
        /// </summary>
        public static Area Empty => new(AdvancedSharpAdbClient.Area.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref='Area'/> class with the specified location
        /// and size.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the area.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        public Area(int x, int y, int width, int height) => area = new(x, y, width, height);

        /// <summary>
        /// Initializes a new instance of the <see cref='Area'/> class with the specified rectangle.
        /// </summary>
        /// <param name="rectangle">A <see cref="Rect"/> that represents the rectangular region.</param>
        public Area(Rect rectangle) => area = new(rectangle);

        /// <summary>
        /// Initializes a new instance of the <see cref='Area'/> class with the specified location and size.
        /// </summary>
        /// <param name="location">A <see cref="Cords"/> that represents the upper-left corner of the rectangular region.</param>
        /// <param name="size">A <see cref="Size"/> that represents the width and height of the rectangular region.</param>
        public Area(Cords location, Size size) => area = new(location.cords, size);

        internal Area(AdvancedSharpAdbClient.Area area) => this.area = area;

        internal static Area GetArea(AdvancedSharpAdbClient.Area area) => new(area);

        /// <summary>
        /// Creates a new <see cref='Area'/> with the specified location and size.
        /// </summary>
        /// <param name="left">The x-coordinate of the upper-left corner of this <see cref='Area'/> structure.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of this <see cref='Area'/> structure.</param>
        /// <param name="right">The x-coordinate of the lower-right corner of this <see cref='Area'/> structure.</param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref='Area'/> structure.</param>
        /// <returns>The new <see cref="Area"/> that this method creates.</returns>
        public static Area FromLTRB(int left, int top, int right, int bottom) => GetArea(AdvancedSharpAdbClient.Area.FromLTRB(left, top, right, bottom));

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the rectangular region represented by this
        /// <see cref='Area'/>.
        /// </summary>
        public Cords Location
        {
            get => Cords.GetCords(area.Location);
            set => area.Location = value.cords;
        }

        /// <summary>
        /// Gets or sets the coordinates of the center of the rectangular region represented by this
        /// <see cref='Area'/>.
        /// </summary>
        public Cords Center => Cords.GetCords(area.Center);

        /// <summary>
        /// Gets or sets the size of this <see cref='Area'/>.
        /// </summary>
        public Size Size
        {
            get => new(Width, Height);
            set
            {
                Width = unchecked((int)value.Width);
                Height = unchecked((int)value.Height);
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        public int X
        {
            get => area.X;
            set => area.X = value;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        public int Y
        {
            get => area.Y;
            set => area.Y = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangular region defined by this <see cref='Area'/>.
        /// </summary>
        public int Width
        {
            get => area.Width;
            set => area.Width = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangular region defined by this <see cref='Area'/>.
        /// </summary>
        public int Height
        {
            get => area.Height;
            set => area.Height = value;
        }

        /// <summary>
        /// Gets the x-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='Area'/> .
        /// </summary>
        public int Left => area.Left;

        /// <summary>
        /// Gets the y-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        public int Top => area.Top;

        /// <summary>
        /// Gets the x-coordinate of the lower-right corner of the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        public int Right => area.Right;

        /// <summary>
        /// Gets the y-coordinate of the lower-right corner of the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        public int Bottom => area.Bottom;

        /// <summary>
        /// Tests whether this <see cref='Area'/> has a <see cref='Area.Width'/>
        /// or a <see cref='Area.Height'/> of 0.
        /// </summary>
        public bool IsEmpty => area.IsEmpty;

        /// <summary>
        /// Tests whether <paramref name="obj"/> is a <see cref='Area'/> with the same location
        /// and size of this Area.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test.</param>
        /// <returns>This method returns <see langword="true"/> if <paramref name="obj"/> is a <see cref="Area"/> structure
        /// and its <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/>, and <see cref="Height"/> properties are equal to
        /// the corresponding properties of this <see cref="Area"/> structure; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) => obj is Area area && this.area.Equals(area);

        /// <inheritdoc/>
        [DefaultOverload]
        public bool Equals(Area other) => area.Equals(other);

        /// <summary>
        /// Converts a <see cref="Rect"/> to a <see cref="Area"/> by performing a ceiling operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref="Rect"/> structure to be converted.</param>
        public static Area Ceiling(Rect _value) => GetArea(AdvancedSharpAdbClient.Area.Ceiling(_value));

        /// <summary>
        /// Converts a <see cref="Rect"/> to a <see cref="Area"/> by performing a truncate operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref="Rect"/> structure to be converted.</param>
        public static Area Truncate(Rect _value) => GetArea(AdvancedSharpAdbClient.Area.Truncate(_value));

        /// <summary>
        /// Converts a <see cref="Rect"/> to a <see cref="Area"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="_value">The <see cref="Rect"/> structure to be converted.</param>
        public static Area Round(Rect _value) => GetArea(AdvancedSharpAdbClient.Area.Round(_value));

        /// <summary>
        /// Determines if the specified point is contained within the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the point to test.</param>
        /// <param name="y">The y-coordinate of the point to test.</param>
        /// <returns>This method returns <see langword="true"/> if the point defined by <paramref name="x"/> and <paramref name="y"/>
        /// is contained within this <see cref="Area"/> structure; otherwise <see langword="false"/>.</returns>
        public bool Contains(int x, int y) => area.Contains(x, y);

        /// <summary>
        /// Determines if the specified point is contained within the rectangular region defined by this
        /// <see cref='Area'/>.
        /// </summary>
        /// <param name="pt">The <see cref="Cords"/> to test.</param>
        /// <returns>This method returns <see langword="true"/> if the point represented by <paramref name="pt"/>
        /// is contained within this <see cref="Area"/> structure; otherwise <see langword="false"/>.</returns>
        [DefaultOverload]
        public bool Contains(Cords pt) => area.Contains(pt.cords);

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the
        /// rectangular region represented by this <see cref='Area'/>.
        /// </summary>
        /// <param name="rect">The <see cref="Area"/> to test.</param>
        /// <returns>This method returns <see langword="true"/> if the rectangular region represented by <paramref name="rect"/>
        /// is entirely contained within this <see cref="Area"/> structure; otherwise <see langword="false"/>.</returns>
        public bool Contains(Area rect) => area.Contains(rect.area);

        /// <summary>
        /// Returns the hash code for this <see cref="Area"/> structure.
        /// </summary>
        /// <returns>An integer that represents the hash code for this rectangle.</returns>
        public override int GetHashCode() => area.GetHashCode();

        /// <summary>
        /// Inflates this <see cref='Area'/> by the specified amount.
        /// </summary>
        /// <param name="width">The amount to inflate this <see cref="Area"/> horizontally.</param>
        /// <param name="height">The amount to inflate this <see cref="Area"/> vertically.</param>
        public void Inflate(int width, int height) => area.Inflate(width, height);

        /// <summary>
        /// Inflates this <see cref='Area'/> by the specified amount.
        /// </summary>
        /// <param name="size">The amount to inflate this rectangle.</param>
        public void Inflate(Size size) => area.Inflate(size);

        /// <summary>
        /// Creates a Area that represents the intersection between this Area and rect.
        /// </summary>
        /// <param name="rect">The <see cref="Area"/> with which to intersect.</param>
        public void Intersect(Area rect) => area.Intersect(rect.area);

        /// <summary>
        /// Determines if this rectangle intersects with rect.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>This method returns <see langword="true"/> if there is any intersection, otherwise <see langword="false"/>.</returns>
        public bool IntersectsWith(Area rect) => area.IntersectsWith(rect.area);

        /// <summary>
        /// Creates a rectangle that represents the union between a and b.
        /// </summary>
        /// <param name="a">A rectangle to union.</param>
        /// <param name="b">A rectangle to union.</param>
        /// <returns>A <see cref="Area"/> structure that bounds the union of the two <see cref="Area"/> structures.</returns>
        public static Area Union(Area a, Area b) => GetArea(AdvancedSharpAdbClient.Area.Union(a.area, b.area));

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="pos">Amount to offset the location.</param>
        public void Offset(Cords pos) => area.Offset(pos.cords);

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="x">The horizontal offset.</param>
        /// <param name="y">The vertical offset.</param>
        public void Offset(int x, int y) => area.Offset(x, y);

        /// <summary>
        /// Converts the attributes of this <see cref='Area'/> to a human readable string.
        /// </summary>
        /// <returns>A string that contains the position, width, and height of this <see cref="Area"/> structure ¾
        /// for example, <c>{X=20, Y=20, Width=100, Height=50}</c>.</returns>
        public override string ToString() => area.ToString();
    }
}
