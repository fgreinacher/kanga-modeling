using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelinesLayer : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IEnumerable<ILifeline> m_Lifelines;

        public LifelinesLayer(IEnumerable<ILifeline> lifelines, GridLayout gridLayout)
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
                //TODO Must be modified after Dispose was introduces
                Row endRow = m_GridLayout.FooterRow;
                AddChild(new LifelineVisual(lifeline, column, startRow, endRow, m_GridLayout));
            }
        }
    }
}