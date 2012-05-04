using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SelfSignalVisual : SignalVisualBase
    {
        private readonly Column m_Column;
        private readonly Row m_EndRow;

        public SelfSignalVisual(IStyle style, ISignal signal, Column column, Row row, Row endRow)
            : base(style, signal, row)
        {
            m_Column = column;
            m_EndRow = endRow;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_Column.RightGap.Allocate(m_TextSize.Width + ArrowCapHeight + TextPadding * 2);
            m_EndRow.Body.Allocate(m_TextSize.Height + TextPadding * 2);
        }

        protected override void DrawText(IGraphicContext graphicContext)
        {
            float xText = m_Column.Body.Right + ArrowCapHeight + TextPadding;
            float yText = m_EndRow.Body.Bottom - TextPadding - m_TextSize.Height;
            graphicContext.DrawText(
                new Point(xText, yText), 
                m_TextSize + new Padding(TextPadding),
                m_Signal.Name, 
                Style.Signal.Font, 
                Style.Signal.FontSize, 
                Style.Signal.TextColor,
                HorizontalAlignment.Left, 
                VerticalAlignment.Middle);
        }

        protected override void DrawArrow(IGraphicContext graphicContext)
        {
            Action<Point, Point, float, Color, LineStyle> drawLine;
            Action<Point, Point, float, float, float, Color, LineStyle> drawArrow;

            switch (m_Signal.SignalType)
            {
                case Compiler.SequenceDiagrams.SignalType.Call:
                    drawLine = graphicContext.DrawLine;
                    drawArrow = graphicContext.DrawArrow;
                    break;

                case Compiler.SequenceDiagrams.SignalType.Return:
                    drawLine = graphicContext.DrawLine;
                    drawArrow = graphicContext.DrawArrow;
                    break;

                default:
                    drawLine = (a, b, c, d, e) => { };
                    drawArrow = (a, b, c, d, e, f, g) => { };
                    break;
            }

            float xStart = m_Column.Body.Middle + GetXFromCenterRelative(m_Signal.Start);
            float xEnd = m_Column.Body.Middle + GetXFromCenterRelative(m_Signal.End);
            float xRight = m_Column.Body.Right + ArrowCapHeight;

            float yTop = m_Row.Body.Bottom;
            float yBottom = m_EndRow.Body.Bottom;

            drawLine(
                new Point(xStart, yTop),
                new Point(xRight, yTop),
                Style.Signal.Width,
                Style.Signal.LineColor,
                Style.LineStyle);
            drawLine(
                new Point(xRight, yTop),
                new Point(xRight, yBottom),
                Style.Signal.Width,
                Style.Signal.LineColor,
                Style.LineStyle);
            drawArrow(
                new Point(xRight, yBottom),
                new Point(xEnd, yBottom),
                Style.Signal.Width,
                ArrowCapHeight,
                ArrowCapHeight,
                Style.Signal.LineColor,
                Style.LineStyle);
        }
    }
}