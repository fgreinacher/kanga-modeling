using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelineVisual : Visual
    {
        private readonly Column m_Column;
        private readonly Row m_EndRow;
        private readonly GridLayout m_GridLayout;
        private readonly ILifeline m_Lifeline;
        private readonly Row m_StartRow;


        public LifelineVisual(ILifeline lifeline, Column column, Row startRow, Row endRow, GridLayout gridLayout)
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
            AddChild(new FramedText(m_Lifeline.Name, m_Column, m_StartRow));
            AddChild(new XCross(m_Column, m_EndRow));
            foreach (IActivity activity in m_Lifeline.Activities())
            {
                Row activityStartRow = m_GridLayout.Rows[activity.StartRowIndex];
                Row activityEndRow = m_GridLayout.Rows[activity.EndRowIndex];
                AddChild(new ActivityVisual(activity, m_Column, activityStartRow, activityEndRow));
            }
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float x = m_Column.Middle;
            float yStart = m_StartRow.Bottom;
            float yEnd = m_EndRow.Middle;
            graphicContext.DrawLine(new Point(x, yStart), new Point(x, yEnd), 1);
            base.DrawCore(graphicContext);
        }
    }
}