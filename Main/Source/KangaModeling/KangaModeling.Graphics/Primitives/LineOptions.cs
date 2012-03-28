using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Graphics.Primitives
{
	/// <summary>
	/// Represents the options that are available when drawing a line.
	/// </summary>
	[Flags]
	public enum LineOptions
	{
		/// <summary>
		/// No options. This is the default value.
		/// </summary>
		None = 0,

		/// <summary>
		/// An arrow is drawn at the end of the line.
		/// </summary>
		ArrowEnd = 1 << 1,
	}
}
