using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Renderer.Primitives
{
	/// <summary>
	/// Represents an x- and y-coordinate pair in two-dimensional space.
	/// </summary>
	public sealed class Point
	{
		private readonly float m_X;
		private readonly float m_Y;
		
		/// <summary>
		/// Creates a new point instance from the specifed x- and y-coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public Point(float x, float y)
		{
			m_X = x;
			m_Y = y;
		}

		/// <summary>
		/// Gets the x-coordinate of this point.
		/// </summary>
		public float X
		{
			get { return m_X; }
		} 

		/// <summary>
		/// Gets the y-coordinate of this point.
		/// </summary>
		public float Y
		{
			get { return m_Y; }
		} 
	}
}
