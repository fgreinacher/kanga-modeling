using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class XCross : SDVisualBase
    {
        private readonly Column m_Column;
        private readonly Row m_Row;

        public XCross(IStyle style, Column column, Row row)
            : base(style)
        {
            m_Column = column;
            m_Row = row;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_Column.Allocate(Width);
            m_Row.Body.Allocate(Height);
            m_Row.TopGap.Allocate(Style.Lifeline.XCrossSize.Height);

            Size = Style.Lifeline.XCrossSize;
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float x = m_Column.Body.Middle;
            float y = m_Row.Body.Middle;

            float hWidth = Width / 2;
            float hHeight = Height / 2;

            var leftTop = new Point(x - hWidth, y - hHeight);
            var rightTop = new Point(x + hWidth, y - hHeight);

            var leftBottom = new Point(x - hWidth, y + hHeight);
            var rightBottom = new Point(x + hWidth, y + hHeight);

            graphicContext.DrawLine(leftTop, rightBottom, Style.Lifeline.XCrossLineWidth, Style.Lifeline.XCrossColor, Style.Common.LineStyle);
            graphicContext.DrawLine(rightTop, leftBottom, Style.Lifeline.XCrossLineWidth, Style.Lifeline.XCrossColor, Style.Common.LineStyle);

            base.DrawCore(graphicContext);
        }
    }
}