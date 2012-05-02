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

                if (signal.IsSelfSignal)
                {
                    Column column = m_GridLayout.Columns[signal.Start.LifelineIndex];
                    Row endRow = m_GridLayout.Rows[signal.End.RowIndex];
                    AddChild(new SelfSignalVisual(signal, column, row, endRow));
                }
                else
                {
                    Column startColumn = m_GridLayout.Columns[signal.Start.LifelineIndex];
                    Column endColumn = m_GridLayout.Columns[signal.End.LifelineIndex];
                    ColumnSection endColumnNeighborSection = GetEndColumnNeighborSection(signal);

                    AddChild(new SignalVisual(signal, startColumn, endColumn, row, endColumnNeighborSection));
                }
            }
        }

        private ColumnSection GetEndColumnNeighborSection(ISignal signal)
        {
            int neighborIndex = signal.End.Orientation == Orientation.Left
                                    ? signal.End.LifelineIndex - 1
                                    : signal.End.LifelineIndex + 1;

            ColumnSection endColumnNeighborSection = null;
            if (neighborIndex>=0 && neighborIndex<m_GridLayout.Columns.Count)
            {
                Column endColumnNeighbor = m_GridLayout.Columns[neighborIndex];
                endColumnNeighborSection = signal.End.Orientation == Orientation.Left
                                               ? endColumnNeighbor.RightGap
                                               : endColumnNeighbor.LeftGap;
            }
            return endColumnNeighborSection;
        }
    }
}