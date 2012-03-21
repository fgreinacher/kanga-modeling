using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Renderer.Primitives
{
	/// <summary>
	/// Represents a width and height in two-dimensional space.
	/// </summary>
	public sealed class Size
	{
		private readonly float m_Width;
		private readonly float m_Height;
		
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
	}
}
