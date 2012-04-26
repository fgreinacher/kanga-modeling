using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SignalVisual : Visual
    {
        public const float TextPadding = 6;
        public const int ArrowCapHeight = 8;


        private readonly Column m_EndColumn;
        private readonly Row m_Row;
        private readonly ISignal m_Signal;
        private readonly Column m_StartColumn;
        private Size m_TextSize;

        public SignalVisual(ISignal signal, Column startColumn, Column endColumn, Row row)
        {
            m_Signal = signal;
            m_StartColumn = startColumn;
            m_EndColumn = endColumn;
            m_Row = row;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
		{
			base.LayoutCore(graphicContext);
            m_TextSize = graphicContext.MeasureText(m_Signal.Name);

            ColumnSection columnSection = m_Signal.End.Orientation == Orientation.Left ? m_EndColumn.LeftGap : m_EndColumn.RightGap;
            columnSection.Allocate(m_TextSize.Width + TextPadding * 2);
            m_Row.Body.Allocate(m_TextSize.Height + TextPadding * 2);
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
                              ? m_EndColumn.Body.Right + TextPadding 
                              : m_EndColumn.Body.Left - TextPadding  - m_TextSize.Width;

            float yText = m_Row.Body.Bottom - TextPadding - m_TextSize.Height;
            graphicContext.DrawText(m_Signal.Name, HorizontalAlignment.Center, VerticalAlignment.Middle,
                                    new Point(xText, yText), m_TextSize + new Padding(TextPadding));
        }

        private void DrawArrow(IGraphicContext graphicContext)
        {
            float xStart = m_StartColumn.Body.Middle + GetXFromCenterRelative(m_Signal.Start);
            float xEnd = m_EndColumn.Body.Middle + GetXFromCenterRelative(m_Signal.End);

            float y = m_Row.Body.Bottom;

            var start = new Point(xStart, y);
            var end = new Point(xEnd, y);

            switch (m_Signal.SignalType)
            {
                case Compiler.SequenceDiagrams.SignalType.Call:
                    graphicContext.DrawArrow(start, end, 2, ArrowCapHeight, ArrowCapHeight);
                    break;

                case Compiler.SequenceDiagrams.SignalType.Return:
                    graphicContext.DrawDashedArrow(start, end, 2, ArrowCapHeight, ArrowCapHeight);
                    break;

                default:
                    break;
            }
        }

        private float GetXFromCenterRelative(IPin pin)
        {
            float xFromCenterAbsoulute = ActivityVisual.ActivityWidth/2*pin.Level;

            return pin.Orientation == Orientation.Left
                       ? -xFromCenterAbsoulute
                       : xFromCenterAbsoulute;
        }
    }
}