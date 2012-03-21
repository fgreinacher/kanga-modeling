using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusRendererFactory : IRendererFactory
	{
		private readonly System.Drawing.Graphics m_Graphics;

		public GdiPlusRendererFactory(System.Drawing.Graphics graphics)
		{
			m_Graphics = graphics;
		}

		public IRenderer CreateRenderer(ITheme theme)
		{
			return new GdiPlusRenderer(m_Graphics, theme);
		}
	}
}
