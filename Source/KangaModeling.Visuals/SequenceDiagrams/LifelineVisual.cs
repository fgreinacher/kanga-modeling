using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelineVisual : SDVisualBase
    {
        private readonly Column m_Column;
        private readonly Row m_EndRow;
        private readonly GridLayout m_GridLayout;
        private readonly ILifeline m_Lifeline;
        private readonly Row m_StartRow;

        public LifelineVisual(IStyle style, ILifeline lifeline, Column column, Row startRow, Row endRow, GridLayout gridLayout)
            : base(style)
        {
            m_Lifeline = lifeline;
            m_GridLayout = gridLayout;
            m_Column = column;
            m_StartRow = startRow;
            m_EndRow = endRow;
            Initialize();
        }

        private void Initialize()
        {
            AddChild(new FramedText(Style, m_Lifeline.Name, m_Column, m_StartRow));
            AddChild(new XCross(Style, m_Column, m_EndRow));
            foreach (IActivity activity in m_Lifeline.Activities())
            {
                Row activityStartRow = m_GridLayout.Rows[activity.StartRowIndex];
                Row activityEndRow = m_GridLayout.Rows[activity.EndRowIndex];
                AddChild(new ActivityVisual(Style, activity, m_Column, activityStartRow, activityEndRow));
            }
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float x = m_Column.Body.Middle;
            float yStart = m_StartRow.Bottom;
            float yEnd = m_EndRow.Body.Middle;
            graphicContext.DrawLine(new Point(x, yStart), new Point(x, yEnd), Style.Lifeline.Width, Style.Lifeline.Color, Style.Common.LineStyle);
            base.DrawCore(graphicContext);
        }
    }
}