using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class ActivityVisual : SDVisualBase
    {
        private readonly IActivity m_Activity;
        private readonly Column m_Column;
        private readonly Row m_EndRow;
        private readonly Row m_StartRow;

        public ActivityVisual(IStyle style, IActivity activity, Column column, Row startRow, Row endRow)
            : base(style)
        {
            m_Activity = activity;
            m_Column = column;
            m_StartRow = startRow;
            m_EndRow = endRow;
            Width = Style.Activity.Width;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            //Alocation form center symetrically in both directions.
            float widthToAlocate = Width * (m_Activity.Level + 1);
            m_Column.Body.Allocate(widthToAlocate);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float xFromCenterAbsoulute = Width / 2 * m_Activity.Level;
            float xFromCenterRelative = m_Activity.Orientation == Orientation.Left
                                            ? -xFromCenterAbsoulute
                                            : xFromCenterAbsoulute;

            float x = m_Column.Body.Middle + xFromCenterRelative - Width / 2;

            float yStart = m_StartRow.Body.Bottom;
            float yEnd = m_EndRow.Body.Bottom;

            var location = new Point(x, yStart);
            var size = new Size(Width, yEnd - yStart);

            graphicContext.FillRectangle(location, size, Style.Activity.BackColor);
            graphicContext.DrawRectangle(location, size, Style.Activity.FrameColor, Style.Common.LineStyle);

            base.DrawCore(graphicContext);
        }
    }
}