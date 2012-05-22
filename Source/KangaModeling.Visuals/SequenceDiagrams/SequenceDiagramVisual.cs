using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;
using System.Linq;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public class SequenceDiagramVisual : SDVisualBase
    {
        private readonly GridLayout m_GridLayout;
        private readonly ISequenceDiagram m_SequenceDiagram;

        public SequenceDiagramVisual(IStyle style, ISequenceDiagram sequenceDiagram)
            : base(style)
        {
            m_SequenceDiagram = sequenceDiagram;
            m_GridLayout = new GridLayout(Style, sequenceDiagram.LifelineCount, sequenceDiagram.RowCount);

            Initialize();
        }

        public void Initialize()
        {
            AddChild(new LifelinesLayer(Style, m_SequenceDiagram.Lifelines, m_GridLayout));

            var allSignals = m_SequenceDiagram.AllSignals().ToArray();
            if (allSignals.Length > 0)
            {
                AddChild(new SignalsLayer(Style, allSignals, m_GridLayout));
                AddChild(new RootFragmentVisual(Style, m_SequenceDiagram.Root, m_GridLayout));
            }
            AddChild(m_GridLayout);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            Size = m_GridLayout.Size;
        }
    }
}