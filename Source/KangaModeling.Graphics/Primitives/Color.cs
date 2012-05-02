using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Graphics.Primitives
{
	public sealed class Color
	{
		public static readonly Color Red = new Color(255, 255, 0, 0);
		public static readonly Color Green = new Color(255, 0, 255, 0);
		public static readonly Color Blue = new Color(255, 0, 0, 255);
		public static readonly Color Black = new Color(255, 0, 0, 0);
		public static readonly Color White = new Color(255, 255, 255, 255);
		public static readonly Color Transparent = new Color(0, 0, 0, 0);
        public static readonly Color SemiTransparent = new Color(170, 255, 255, 255);
        public static readonly Color Gray = new Color(255, 50, 50, 50);

		private readonly byte[] m_ARGB = new byte[4];

		public Color(byte a, byte r, byte g, byte b)
		{
			m_ARGB = new[] { a, r, g, b };
		}

		public Color(byte r, byte g, byte b)
			: this(255, r, g, b)
		{
		}

		public Color(byte a, Color baseColor)
			: this(a, baseColor.R, baseColor.G, baseColor.B)
		{
		}

		public byte A { get { return m_ARGB[0]; } }
		public byte R { get { return m_ARGB[1]; } }
		public byte G { get { return m_ARGB[2]; } }
		public byte B { get { return m_ARGB[3]; } }
	}
}
