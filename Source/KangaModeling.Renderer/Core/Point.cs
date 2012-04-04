using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Renderer.Core
{
	/// <summary>
	/// Represents an x- and y-coordinate pair in two-dimensional space.
	/// </summary>
	public sealed class Point
	{
		private readonly int m_X;
		private readonly int m_Y;
		
		/// <summary>
		/// Creates a new point instance from the specifed x- and y-coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public Point(int x, int y)
		{
			m_X = x;
			m_Y = y;
		}

		/// <summary>
		/// Gets the x-coordinate of this point.
		/// </summary>
		public int X
		{
			get { return m_X; }
		} 

		/// <summary>
		/// Gets the y-coordinate of this point.
		/// </summary>
		public int Y
		{
			get { return m_Y; }
		} 
	}
}
