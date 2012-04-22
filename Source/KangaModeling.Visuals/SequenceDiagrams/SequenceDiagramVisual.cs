using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public class SequenceDiagramVisual : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly ISequenceDiagram m_SequenceDiagram;

        public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
        {
            m_SequenceDiagram = sequenceDiagram;
            m_GridLayout = new GridLayout(sequenceDiagram.LifelineCount, sequenceDiagram.RowCount);

            Initialize();
        }

        public void Initialize()
        {
            AddChild(new LifelinesLayer(m_SequenceDiagram.Lifelines, m_GridLayout));
            AddChild(new SignalsLayer(m_SequenceDiagram.AllSignals(), m_GridLayout));
            AddChild(new FragmentVisual(m_SequenceDiagram.Root, m_GridLayout));
        }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);
            m_GridLayout.AdjustLoactions();

            //TODO Why need to know size here.
            AdjustSize();
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            AdjustSize();
            base.DrawCore(graphicContext);
        }

        private void AdjustSize()
        {
            if (m_GridLayout.Columns.Count == 0)
            {
                Size = new Size(0, 0);
                return;
            }
            Column lastColumn = m_GridLayout.Columns[m_GridLayout.Columns.Count - 1];
            Row lastRow = m_GridLayout.FooterRow;

            Size = new Size(lastColumn.Right, lastRow.Bottom);
        }
    }
}