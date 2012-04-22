using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SiganlVisual : Visual
    {
        public const float TextPadding = 6;

        private readonly Column m_EndColumn;
        private readonly Row m_Row;
        private readonly ISignal m_Signal;
        private readonly Column m_StartColumn;
        private Size m_TextSize;

        public SiganlVisual(ISignal signal, Column startColumn, Column endColumn, Row row)
        {
            m_Signal = signal;
            m_StartColumn = startColumn;
            m_EndColumn = endColumn;
            m_Row = row;
        }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            m_TextSize = graphicContext.MeasureText(m_Signal.Name);

            m_EndColumn.Allocate(m_TextSize.Width + TextPadding*2);
            m_Row.Allocate(m_TextSize.Height);
            base.LayoutCore(graphicContext);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            //TODO Remove hardcoded sizes etc.
            DrawArrow(graphicContext);
            DrawText(graphicContext);

            base.DrawCore(graphicContext);
        }

        private void DrawText(IGraphicContext graphicContext)
        {
            float xText = m_Signal.End.Orientation == Orientation.Right
                              ? m_EndColumn.Middle + 2
                              : m_EndColumn.Middle - 2 - m_TextSize.Width;

            float yText = m_Row.Bottom - 2 - m_TextSize.Height;
            graphicContext.DrawText(m_Signal.Name, HorizontalAlignment.Center, VerticalAlignment.Middle,
                                    new Point(xText, yText), m_TextSize + new Padding(2));
        }

        private void DrawArrow(IGraphicContext graphicContext)
        {
            float xStart = m_StartColumn.Middle + GetXFromCenterRelative(m_Signal.Start, m_Signal.Start.Level);
            float xEnd = m_EndColumn.Middle + GetXFromCenterRelative(m_Signal.End);

            float y = m_Row.Bottom;

            var start = new Point(xStart, y);
            var end = new Point(xEnd, y);

            switch (m_Signal.SignalType)
            {
                case Compiler.SequenceDiagrams.SignalType.Call:
                    graphicContext.DrawArrow(start, end, 1, 8, 8);
                    break;

                case Compiler.SequenceDiagrams.SignalType.Return:
                    graphicContext.DrawDashedArrow(start, end, 1, 8, 8);
                    break;

                default:
                    break;
            }
        }

        private float GetXFromCenterRelative(IPin pin)
        {
            int levels = pin.Level;
            if (m_Signal.End.Activity!=null)
            {
                levels++;
            }
            return GetXFromCenterRelative(pin, levels);
        }

        private float GetXFromCenterRelative(IPin pin, int levels)
        {
            float xFromCenterAbsoulute = ActivityVisual.ActivityWidth / 2 * levels;

            return pin.Orientation == Orientation.Left
                       ? -xFromCenterAbsoulute
                       : xFromCenterAbsoulute;
        }
    }
}