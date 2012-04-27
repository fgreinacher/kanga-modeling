using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class ActivityVisual : Visual
    {
        public const float ActivityWidth = 10;
        private readonly IActivity m_Activity;
        private readonly Column m_Column;
        private readonly Row m_EndRow;
        private readonly Row m_StartRow;

        public ActivityVisual(IActivity activity, Column column, Row startRow, Row endRow)
        {
            m_Activity = activity;
            m_Column = column;
            m_StartRow = startRow;
            m_EndRow = endRow;
            Width = ActivityWidth;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
		{
			base.LayoutCore(graphicContext);

            //Alocation form center symetrically in both directions.
            float widthToAlocate = Width*(m_Activity.Level + 1);
            m_Column.Body.Allocate(widthToAlocate);

        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float xFromCenterAbsoulute = Width / 2 * m_Activity.Level;
            float xFromCenterRelative = m_Activity.Orientation == Orientation.Left
                                            ? -xFromCenterAbsoulute
                                            : xFromCenterAbsoulute;

            float x = m_Column.Body.Middle + xFromCenterRelative - Width/2;

            float yStart = m_StartRow.Body.Bottom;
            float yEnd = m_EndRow.Body.Bottom;

            var location = new Point(x, yStart);
            var size = new Size(Width, yEnd - yStart);

			graphicContext.FillRectangle(location, size, Color.White);
			graphicContext.DrawRectangle(location, size, Color.Black);

            base.DrawCore(graphicContext);
        }
    }
}