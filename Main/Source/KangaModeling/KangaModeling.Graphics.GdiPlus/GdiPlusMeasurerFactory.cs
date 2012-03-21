using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusMeasurerFactory : IMeasurerFactory
	{
		private readonly System.Drawing.Graphics m_Graphics;

		public GdiPlusMeasurerFactory(System.Drawing.Graphics graphics)
		{
			m_Graphics = graphics;
		}

		public IMeasurer CreateMeasurer(ITheme theme)
		{
			return new GdiPlusMeasurer(m_Graphics, theme);
		}
	}
}
