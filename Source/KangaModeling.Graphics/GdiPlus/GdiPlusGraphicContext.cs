using System;
using System.Drawing;
using KangaModeling.Graphics.Primitives;
using Point = KangaModeling.Graphics.Primitives.Point;
using Size = KangaModeling.Graphics.Primitives.Size;
using Color = KangaModeling.Graphics.Primitives.Color;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using KangaModeling.Graphics.GdiPlus.Resources;
using System.Collections.Generic;

namespace KangaModeling.Graphics.GdiPlus
{
    public sealed class GdiPlusGraphicContext : IGraphicContext, IDisposable
    {
        private readonly System.Drawing.Graphics m_Graphics;
        private readonly PrivateFontCollection m_FontCollection = new PrivateFontCollection();
        private readonly Random m_Random = new Random();

        public GdiPlusGraphicContext(System.Drawing.Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");

            m_Graphics = graphics;

            FillFontCollection();
        }

        #region IGraphicContext Members

        public void DrawRectangle(Point location, Size size, Color color)
        {
            var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

            using (var pen = new Pen(color.ToColor()))
            {
                Point topLeft = location;
                Point topRight = location.Offset(size.Width, 0);
                Point bottomLeft = location.Offset(0, size.Height);
                Point bottomRight = location.Offset(size.Width, size.Height);

                DrawLineCore(topLeft, topRight, pen.Width, color);
                DrawLineCore(topRight, bottomRight, pen.Width, color);
                DrawLineCore(bottomRight, bottomLeft, pen.Width, color);
                DrawLineCore(bottomLeft, topLeft, pen.Width, color);
            }
        }

        public void FillPolygon(IEnumerable<Point> points, Color color)
        {
            var pointsF = points.Select(p => p.ToPointF()).ToArray(); 

            using (var brush = new SolidBrush(color.ToColor()))
            {
                m_Graphics.FillPolygon(brush, pointsF);
            }
        }

        public void FillRectangle(Point location, Size size, Color color)
        {
            var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

            using (var brush = new SolidBrush(color.ToColor()))
            {
                m_Graphics.FillRectangle(brush, rectangle);
            }
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

                    string stringToDraw = ReplaceLineBreaks(text);
                    m_Graphics.DrawString(stringToDraw, font, Brushes.Black, rectangle, stringFormat);
                }
            }
        }

        private static string ReplaceLineBreaks(string text)
        {
            return text.Replace("\\n", Environment.NewLine);
        }

        public void DrawLine(Point from, Point to, float width)
        {
            DrawLineCore(from, to, width, Color.Black);
        }

        public void DrawDashedLine(Point from, Point to, float width)
        {
            DrawLineCore(from, to, width, Color.Black, pen =>
            {
                pen.DashStyle = DashStyle.Dash;
            });
        }

        public void DrawArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight)
        {
            DrawLineCore(from, to, width, Color.Black, pen =>
            {
                pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
            });
        }

        public void DrawDashedArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight)
        {
            DrawLineCore(from, to, width, Color.Black, pen =>
            {
                pen.DashStyle = DashStyle.Dash;
                pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
            });
        }

        public Size MeasureText(string text)
        {
            using (var font = CreateFont())
            {
                string stringToDraw = ReplaceLineBreaks(text);
                SizeF size = m_Graphics.MeasureString(stringToDraw, font);
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

        private void DrawLineCore(Point from, Point to, float width, Color color)
        {
            DrawLineCore(from, to, width, color, p => { });
        }

        private void DrawLineCore(Point from, Point to, float width, Color color, Action<Pen> initializePen)
        {
            using (var brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B)))
            using (var pen = new Pen(brush, width))
            {
                initializePen(pen);

                using (AntiAliasedGraphics())
                {
                    var bezierCurve = new BezierCurve(from, to);
                    m_Graphics.DrawBezier(pen, from.ToPointF(), bezierCurve.FirstControlPoint.ToPointF(), bezierCurve.SecondControlPoint.ToPointF(), to.ToPointF());
                }
            }
        }

        private DoOnDispose AntiAliasedGraphics()
        {
            var oldSmoothingMode = m_Graphics.SmoothingMode;
            m_Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            return new DoOnDispose(() => m_Graphics.SmoothingMode = oldSmoothingMode);
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