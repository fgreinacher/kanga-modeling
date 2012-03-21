using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusMeasurer : IMeasurer
	{
		private readonly System.Drawing.Graphics m_Graphics;
		private readonly ITheme m_Theme;

		public GdiPlusMeasurer(System.Drawing.Graphics graphics, ITheme theme)
		{
			m_Graphics = graphics;
			m_Theme = theme;
		}

		public Size MeasureText(string text)
		{
			using (var font = new System.Drawing.Font(m_Theme.Font, m_Theme.FontSize))
			{
				System.Drawing.SizeF size = m_Graphics.MeasureString(text, font);
				return new Size(size.Width, size.Height);
			}
		}
	}
}
