using System;
using System.Drawing;
using KangaModeling.Graphics.Primitives;
using Point = KangaModeling.Graphics.Primitives.Point;
using Size = KangaModeling.Graphics.Primitives.Size;
using Color = KangaModeling.Graphics.Primitives.Color;
using Font = KangaModeling.Graphics.Primitives.Font;
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

        public void DrawRectangle(Point location, Size size, Color color, LineStyle lineStyle)
        {
            var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

            using (var pen = new Pen(color.ToColor()))
            {
                Point topLeft = location;
                Point topRight = location.Offset(size.Width, 0);
                Point bottomLeft = location.Offset(0, size.Height);
                Point bottomRight = location.Offset(size.Width, size.Height);

                DrawLineCore(topLeft, topRight, pen.Width, color, lineStyle);
                DrawLineCore(topRight, bottomRight, pen.Width, color, lineStyle);
                DrawLineCore(bottomRight, bottomLeft, pen.Width, color, lineStyle);
                DrawLineCore(bottomLeft, topLeft, pen.Width, color, lineStyle);
            }
        }

        public void DrawText(Point location, Size size, string text, Font font, float fontSize, Color color, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            using (var drawFont = CreateFont(font, fontSize))
            {
                var rectangle = new RectangleF(location.ToPointF(), size.ToSizeF());

                using (var stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = horizontalAlignment.ToStringAlignment();
                    stringFormat.LineAlignment = verticalAlignment.ToStringAlignment();

                    using (var brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B)))
                    {
                        string stringToDraw = ReplaceLineBreaks(text);
                        m_Graphics.DrawString(stringToDraw, drawFont, brush, rectangle, stringFormat);
                    }
                }
            }
        }

        public void DrawLine(Point from, Point to, float width, Color color, LineStyle lineStyle)
        {
            DrawLineCore(from, to, width, color, lineStyle);
        }

        public void DrawDashedLine(Point from, Point to, float width, Color color, LineStyle lineStyle)
        {
            DrawLineCore(from, to, width, color, lineStyle, pen =>
            {
                pen.DashStyle = DashStyle.Dash;
            });
        }

        public void DrawArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight, Color color, LineStyle lineStyle)
        {
            DrawLineCore(from, to, width, color, lineStyle, pen =>
            {
                pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
            });
        }

        public void DrawDashedArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight, Color color, LineStyle lineStyle)
        {
            DrawLineCore(from, to, width, color, lineStyle, pen =>
            {
                pen.DashStyle = DashStyle.Dash;
                pen.CustomEndCap = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight, false);
            });
        }

        public Size MeasureText(string text, Font font, float fontSize)
        {
            using (var measureFont = CreateFont(font, fontSize))
            {
                string stringToMeasure = ReplaceLineBreaks(text);
                SizeF size = m_Graphics.MeasureString(stringToMeasure, measureFont);
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

        private static string ReplaceLineBreaks(string text)
        {
            return text.Replace("\\n", Environment.NewLine);
        }

        private void DrawLineCore(Point from, Point to, float width, Color color, LineStyle lineStyle)
        {
            DrawLineCore(from, to, width, color, lineStyle, p => { });
        }

        private void DrawLineCore(Point from, Point to, float width, Color color, LineStyle lineStyle, Action<Pen> initializePen)
        {
            using (var brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B)))
            using (var pen = new Pen(brush, width))
            {
                initializePen(pen);

                switch (lineStyle)
                {
                    case LineStyle.Clean:
                        m_Graphics.DrawLine(pen, from.ToPointF(), to.ToPointF());
                        break;

                    case LineStyle.Sketchy:
                        using (AntiAliasedGraphics())
                        {
                            var bezierCurve = new BezierCurve(from, to);
                            m_Graphics.DrawBezier(pen, from.ToPointF(), bezierCurve.FirstControlPoint.ToPointF(), bezierCurve.SecondControlPoint.ToPointF(), to.ToPointF());
                        }
                        break;

                    default:
                        break;
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

        private System.Drawing.Font CreateFont(Font font, float fontSize)
        {
            FontFamily fontFamily;
            switch (font)
            {
                case Font.SansSerif:
                    fontFamily = FontFamily.GenericSansSerif;
                    break;

                case Font.Handwritten:
                    fontFamily = m_FontCollection.Families[0];
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new System.Drawing.Font(fontFamily, fontSize);
        }

        #endregion
    }
}