using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SelfSignalVisual : SignalVisualBase
    {
        private readonly Column m_Column;
        private readonly Row m_EndRow;

        public SelfSignalVisual(ISignal signal, Column column, Row row, Row endRow)
            : base(signal, row)
        {
            m_Column = column;
            m_EndRow = endRow;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_Column.RightGap.Allocate(m_TextSize.Width + ArrowCapHeight + TextPadding * 2);
            m_Row.Body.Allocate(m_TextSize.Height + TextPadding * 2);
        }

        protected override void DrawText(IGraphicContext graphicContext)
        {
            float xText = m_Column.Body.Right + ArrowCapHeight + TextPadding;
            float yText = m_Row.Body.Bottom - TextPadding - m_TextSize.Height;
            graphicContext.DrawText(m_Signal.Name, HorizontalAlignment.Center, VerticalAlignment.Middle,
                                    new Point(xText, yText), m_TextSize + new Padding(TextPadding));
        }

        protected override void DrawArrow(IGraphicContext graphicContext)
        {
            Action<Point, Point, float> drawLine;
            Action<Point, Point, float, float, float> drawArrow;

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
                    drawLine = (a, b, c) => { };
                    drawArrow = (a, b, c, d, e) => { };
                    break;
            }

            float xStart = m_Column.Body.Middle + GetXFromCenterRelative(m_Signal.Start);
            float xEnd = m_Column.Body.Middle + GetXFromCenterRelative(m_Signal.End);
            float xRight = m_Column.Body.Right + ArrowCapHeight + (TextPadding * 2);

            float yTop = m_Row.Body.Bottom;
            float yBottom = m_EndRow.Body.Bottom;

            drawLine(
                new Point(xStart, yTop),
                new Point(xRight, yTop),
                2);
            drawLine(
                new Point(xRight, yTop),
                new Point(xRight, yBottom),
                2);
            drawArrow(
                new Point(xRight, yBottom),
                new Point(xEnd, yBottom),
                2,
                ArrowCapHeight,
                ArrowCapHeight);
        }
    }
}