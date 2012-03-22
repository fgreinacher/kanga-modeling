using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Renderables;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusRenderer : IRenderer
	{
		private readonly System.Drawing.Graphics m_Graphics;
		private readonly ITheme m_Theme;

		public GdiPlusRenderer(System.Drawing.Graphics graphics, ITheme theme)
		{
			m_Graphics = graphics;
			m_Theme = theme;
		}

		public void RenderText(RenderableText renderableText)
		{
			using (var font = new System.Drawing.Font(m_Theme.Font, m_Theme.FontSize))
			{
				var rectangle = new System.Drawing.RectangleF
				{
					X = renderableText.Location.X,
					Y = renderableText.Location.Y,
					Width = renderableText.Size.Width,
					Height = renderableText.Size.Height,
				};

				m_Graphics.DrawString(renderableText.Text, font, System.Drawing.Brushes.Black, rectangle);
			}
		}

		public void RenderRectangle(RenderableRectangle renderableRectangle)
		{
			var rectangle = new System.Drawing.RectangleF
			{
				X = renderableRectangle.Location.X,
				Y = renderableRectangle.Location.Y,
				Width = renderableRectangle.Size.Width,
				Height = renderableRectangle.Size.Height,
			};

			m_Graphics.FillRectangle(System.Drawing.Brushes.LightBlue, rectangle);
		}
	}
}
