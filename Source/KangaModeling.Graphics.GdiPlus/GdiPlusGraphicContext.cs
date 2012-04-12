using System;
using System.Drawing;
using KangaModeling.Graphics.GdiPlus.Utilities;
using KangaModeling.Graphics.Primitives;
using Point = KangaModeling.Graphics.Primitives.Point;
using Size = KangaModeling.Graphics.Primitives.Size;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using KangaModeling.Graphics.GdiPlus.Resources;

namespace KangaModeling.Graphics.GdiPlus
{
	public sealed class GdiPlusGraphicContext : IGraphicContext, IDisposable
	{
		private readonly System.Drawing.Graphics m_Graphics;
		private readonly PrivateFontCollection m_FontCollection = new PrivateFontCollection();

		public GdiPlusGraphicContext(System.Drawing.Graphics graphics)
		{
			if (graphics == null) throw new ArgumentNullException("graphics");

			m_Graphics = graphics;

			FillFontCollection();
		}

		#region IGraphicContext Members

		public void DrawRectangle(Point location, Size size)
		{
			var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

			m_Graphics.DrawRectangle(Pens.Black, location.X, location.Y, size.Width, size.Height);
		}

		public void FillRectangle(Point location, Size size)
		{
			var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

			m_Graphics.FillRectangle(Brushes.Orange, rectangle);
		}

		public void DrawText(string text, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Point location, Size size)
		{
			using (var font = CreateFont())
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

		public void DrawLine(Point from, Point to, float width)
		{
			DrawLineCore(from, to, width, pen => { });
		}

		public void DrawDashedLine(Point from, Point to, float width)
		{
			DrawLineCore(from, to, width, pen =>
			{
				pen.DashStyle = DashStyle.Dash;
			});
		}

		public void DrawArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight)
		{
			DrawLineCore(from, to, width, pen =>
			{
				pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
			});
		}

		public void DrawDashedArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight)
		{
			DrawLineCore(from, to, width, pen =>
			{
				pen.DashStyle = DashStyle.Dash;
				pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
			});
		}

		public Size MeasureText(string text)
		{
			using (var font = CreateFont())
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

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			m_FontCollection.Dispose();
		}

		#endregion

		#region Private Methods

		private void DrawLineCore(Point from, Point to, float width, Action<Pen> initializePen)
		{
			using (var pen = new Pen(Brushes.Black, width))
			{
				initializePen(pen);

				pen.Alignment = PenAlignment.Left;

				m_Graphics.DrawLine(pen, from.ToPointF(), to.ToPointF());
			}
		}

		private void FillFontCollection()
		{
			byte[] fontData = Fonts.BuxtonSketch;
			IntPtr fontMemory = Marshal.AllocCoTaskMem(fontData.Length);
			try
			{
				Marshal.Copy(fontData, 0, fontMemory, fontData.Length);
				m_FontCollection.AddMemoryFont(fontMemory, fontData.Length);
			}
			finally
			{
				Marshal.FreeCoTaskMem(fontMemory);
			}
		}

		private Font CreateFont()
		{
			return new Font(m_FontCollection.Families[0], 15);
		}

		#endregion
	}
}