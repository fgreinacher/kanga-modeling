using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelinesLayer : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IEnumerable<ILifeline> m_Lifelines;

        public LifelinesLayer(IStyle style, IEnumerable<ILifeline> lifelines, GridLayout gridLayout)
            : base(style)
        {
            m_Lifelines = lifelines;
            m_GridLayout = gridLayout;
            Initialize();
        }

        private void Initialize()
        {
            foreach (ILifeline lifeline in m_Lifelines)
            {
                Column column = m_GridLayout.Columns[lifeline.Index];
                Row startRow = m_GridLayout.HeaderRow;
                Row endRow = lifeline.EndRowIndex >= m_GridLayout.Rows.Count ? m_GridLayout.FooterRow : m_GridLayout.Rows[lifeline.EndRowIndex];
                AddChild(new LifelineVisual(Style, lifeline, column, startRow, endRow, m_GridLayout));
            }
        }
    }
}