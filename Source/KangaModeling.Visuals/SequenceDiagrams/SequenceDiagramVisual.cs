using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;
using System.Linq;

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

			var allSignals = m_SequenceDiagram.AllSignals().ToArray();
			if (allSignals.Length > 0)
			{
				AddChild(new SignalsLayer(allSignals, m_GridLayout));
				AddChild(new RootFragmentVisual(m_SequenceDiagram.Root, m_GridLayout));
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