using System;
using System.Diagnostics;

namespace KangaModeling.Graphics.Primitives
{
    /// <summary>
    /// Represents a width and height in two-dimensional space.
    /// </summary>
	[DebuggerDisplay("Size (Width={Width} Height={Height})")]
	public sealed class Size : IEquatable<Size>
    {
        #region Fields

        private readonly float m_Width;
        private readonly float m_Height;
        private static readonly Size s_Empty = new Size(0, 0);

        #endregion

        #region Construction / Destruction / Initialisation

        /// <summary>
        /// Initializes a new instance from the specified component.
        /// </summary>
        /// <param name="width">The horizontal component.</param>
        /// <param name="height">The vertical component.</param>
        public Size(float width, float height)
        {
            m_Width = width;
            m_Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents an size with a width and height of zero.
        /// </summary>
        public static Size Empty
        {
            get { return s_Empty; }
        }

        /// <summary>
        /// Gets the horizontal component of this size.
        /// </summary>
        public float Width
        {
            get { return m_Width; }
        }

        /// <summary>
        /// Gets the vertical component of this size.
        /// </summary>
        public float Height
        {
            get { return m_Height; }
        }

        #endregion

        #region Public Methods

        public Size Plus(float width, float height)
        {
            return new Size(m_Width + width, m_Height + height);
        }

        #endregion

        #region Overrides / Overrideables

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return m_Width.GetHashCode() ^ m_Height.GetHashCode();
        }

        #endregion

        #region IEquatable<Size> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Size other)
        {
            return other != null && other.m_Width == m_Width && other.m_Height == m_Height;
        }

        #endregion
    }
}
