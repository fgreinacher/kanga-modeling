using System;
using System.Drawing;
using KangaModeling.Graphics.GdiPlus.Utilities;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Graphics.Theming;
using Point = KangaModeling.Graphics.Primitives.Point;
using Size = KangaModeling.Graphics.Primitives.Size;
using System.Drawing.Drawing2D;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusGraphicContext : IGraphicContext
	{
		private readonly System.Drawing.Graphics m_Graphics;
		private readonly ITheme m_Theme;

		public GdiPlusGraphicContext(System.Drawing.Graphics graphics, ITheme theme)
		{
			m_Graphics = graphics;
			m_Theme = theme;
		}

		#region IGraphicContext Members

		public void DrawRectangle(Point location, Size size)
		{
			var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

			m_Graphics.FillRectangle(Brushes.LightBlue, rectangle);
		}

		public void DrawText(string text, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Point location, Size size)
		{
			using (var font = new Font(m_Theme.Font, m_Theme.FontSize))
			{
				var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

				using (var stringFormat = new StringFormat())
				{
					stringFormat.Alignment = horizontalAlignment.ToStringAlignment();
					stringFormat.LineAlignment = verticalAlignment.ToStringAlignment();

					m_Graphics.DrawString(text, font, Brushes.Black, rectangle, stringFormat);
				}
			}
		}

		public void DrawLine(Point from, Point to, float width, LineOptions options = LineOptions.None)
		{
			using (var pen = new Pen(Brushes.Black, width))
			{
				if (options.HasFlag(LineOptions.ArrowEnd))
				{
					pen.EndCap = LineCap.ArrowAnchor;
				}
				m_Graphics.DrawLine(pen, from.ToPointF(), to.ToPointF());
			}
		}

		public Size MeasureText(string text)
		{
			using (var font = new Font(m_Theme.Font, m_Theme.FontSize))
			{
				SizeF size = m_Graphics.MeasureString(text, font);
				return new Size(size.Width, size.Height);
			}
		}

		public IDisposable ApplyOffset(float dx, float dy)
		{
			var graphicsContainer = m_Graphics.BeginContainer();

			m_Graphics.TranslateTransform(dx, dy);

			return new DoOnDispose(() => m_Graphics.EndContainer(graphicsContainer));
		}

		private class DoOnDispose : IDisposable
		{
			private readonly Action m_Do;

			public DoOnDispose(Action @do)
			{
				m_Do = @do;
			}

			#region IDisposable Members

			public void Dispose()
			{
				m_Do();
			}

			#endregion
		}

		#endregion
	}
}