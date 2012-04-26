using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class SignalsLayer : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IEnumerable<ISignal> m_Signals;

        public SignalsLayer(IEnumerable<ISignal> signals, GridLayout gridLayout)
        {
            m_Signals = signals;
            m_GridLayout = gridLayout;
            Initialize();
        }

        private void Initialize()
        {
            foreach (ISignal signal in m_Signals)
            {
                Row row = m_GridLayout.Rows[signal.RowIndex];
                Column startColumn = m_GridLayout.Columns[signal.Start.LifelineIndex];
                Column endColumn = m_GridLayout.Columns[signal.End.LifelineIndex];

                AddChild(new SignalVisual(signal, startColumn, endColumn, row));
            }
        }
    }
}