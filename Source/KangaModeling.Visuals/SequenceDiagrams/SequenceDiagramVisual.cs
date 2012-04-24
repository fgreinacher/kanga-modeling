using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;
using System.Linq;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	[Flags]
	public enum SequenceDiagramVisualOptions
	{
		None = 0,
		EnableGridLayer = 1 << 0,
	}

	public class SequenceDiagramVisual : Visual
	{
		private readonly GridLayout m_GridLayout;
		private readonly ISequenceDiagram m_SequenceDiagram;
		private readonly SequenceDiagramVisualOptions m_Options;

		public SequenceDiagramVisual(
			ISequenceDiagram sequenceDiagram,
			SequenceDiagramVisualOptions options = SequenceDiagramVisualOptions.EnableGridLayer)
		{
			m_SequenceDiagram = sequenceDiagram;
			m_Options = options;
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

		protected override void DrawCore(IGraphicContext graphicContext)
		{		
			base.DrawCore(graphicContext);
		}

		private void AdjustSize()
		{
		}
	}
}