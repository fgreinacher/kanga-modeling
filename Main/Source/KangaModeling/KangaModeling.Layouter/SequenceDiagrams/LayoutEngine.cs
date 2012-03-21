using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Renderables;

namespace KangaModeling.Layouter.SequenceDiagrams
{
	public class LayoutResult
	{
		public IEnumerable<Renderable> Renderables { get; set; }
		public Size Size { get; set; }
	}

	/// <summary>
	/// The layout engine is responsible for creating the appropriate renderable objects 
	/// from a specified sequence diagram. It has a dependency on <see cref="IMeasurementEngine"/>
	/// because it might need to measure the dimensions of renderable text.
	/// </summary>
	public sealed class LayoutEngine
	{
		private readonly IMeasurer m_Measurer;

		public LayoutEngine(IMeasurer measurer)
		{
			if (measurer == null) throw new ArgumentNullException("measurer");

			m_Measurer = measurer;
		}

		public LayoutResult PerformLayout(ISequenceDiagram sequenceDiagram)
		{
			if (sequenceDiagram == null) throw new ArgumentNullException("sequenceDiagram");

			var renderables = new List<Renderable>();
			Size size = new Size(0, 0);

			var title = sequenceDiagram.Title;
			if (title != null)
			{
				var titleSize = m_Measurer.MeasureText(title);
				var renderableText = new RenderableText(title, new Point(0, 0), titleSize);
				renderables.Add(renderableText);

				size = titleSize;
			}

			return new LayoutResult
			{
				Renderables = renderables,
				Size = size,
			};
		}
	}
}
