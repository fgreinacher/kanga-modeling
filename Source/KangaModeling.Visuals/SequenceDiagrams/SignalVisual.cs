using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SignalVisual : SignalVisualBase
    {
        private readonly Column m_EndColumn;
        private readonly ColumnSection m_EndColumnNeighbor;
        private readonly Column m_StartColumn;

        public SignalVisual(IStyle style, ISignal signal, Column startColumn, Column endColumn, Row row, ColumnSection endColumnNeighbor)
            : base(style, signal, row)
        {
            m_StartColumn = startColumn;
            m_EndColumn = endColumn;
            m_EndColumnNeighbor = endColumnNeighbor;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            ColumnSection columnSection = m_Signal.End.Orientation == Orientation.Left ? m_EndColumn.LeftGap : m_EndColumn.RightGap;
            float width = m_TextSize.Width + Style.Signal.TextPadding * 2;

            if (m_EndColumnNeighbor == null)
            {
                columnSection.Allocate(width);
            }
            else
            {
                columnSection.Allocate(width / 2);
                m_EndColumnNeighbor.Allocate(width / 2);
            }
            m_Row.Body.Allocate(m_TextSize.Height + Style.Signal.TextPadding * 2);
        }

        protected override void DrawText(IGraphicContext graphicContext)
        {
            float xText = m_Signal.End.Orientation == Orientation.Right
                              ? m_EndColumn.Body.Right
                              : m_EndColumn.Body.Left - m_TextSize.Width;

            float yText = m_Row.Body.Bottom - Style.Signal.TextPadding - m_TextSize.Height;
            graphicContext.DrawText(
                new Point(xText, yText),
                m_TextSize,
                m_Signal.Name,
                Style.Common.Font,
                Style.Signal.FontSize,
                Style.Signal.TextColor,
                HorizontalAlignment.Center,
                VerticalAlignment.Middle);
        }

        protected override void DrawArrow(IGraphicContext graphicContext)
        {
            float xStart = m_StartColumn.Body.Middle + GetXFromCenterRelative(m_Signal.Start);
            float xEnd = m_EndColumn.Body.Middle + GetXFromCenterRelative(m_Signal.End);

            float y = m_Row.Body.Bottom;

            var start = new Point(xStart, y);
            var end = new Point(xEnd, y);

            switch (m_Signal.SignalType)
            {
                case SignalType.Call:
                    graphicContext.DrawArrow(
                        start,
                        end,
                        Style.Signal.Width,
                        Style.Signal.ArrowCapSize,
                        Style.Signal.ArrowCapSize,
                        Style.Signal.LineColor,
                        Style.Common.LineStyle);
                    break;

                case SignalType.Return:
                    graphicContext.DrawDashedArrow(
                        start,
                        end,
                        Style.Signal.Width,
                        Style.Signal.ArrowCapSize,
                        Style.Signal.ArrowCapSize,
                        Style.Signal.LineColor,
                        Style.Common.LineStyle);
                    break;
            }
        }
    }
}