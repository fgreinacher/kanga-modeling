using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer;
using KangaModeling.Renderer.Core;

namespace KangaModeling.Layouter.SequenceDiagrams
{
	public interface ISequenceDiagram
	{
		bool HasTitle { get; }
		string Title { get; }
	}

	/// <summary>
	/// The layout engine is responsible for creating the appropriate renderable objects 
	/// from a specified sequence diagram. It has a dependency on <see cref="IMeasurementEngine"/>
	/// because it might need to measure the dimensions of renderable text.
	/// </summary>
	public sealed class LayoutEngine
	{
		private readonly IMeasurementEngine m_MeasurementEngine;

		public LayoutEngine(IMeasurementEngine measurementEngine)
		{
			if (measurementEngine == null) throw new ArgumentNullException("measurementEngine");

			m_MeasurementEngine = measurementEngine;
		}

		public IEnumerable<RenderableObject> PerformLayout(ISequenceDiagram sequenceDiagram)
		{
			if (sequenceDiagram == null) throw new ArgumentNullException("sequenceDiagram");

			var renderableObjects = new List<RenderableObject>();

			if (sequenceDiagram.HasTitle)
			{
				var title = sequenceDiagram.Title; 
				var renderableText = new RenderableText(title, new Point(0, 0), new Size(100, 100));
				renderableObjects.Add(renderableText);
			}
			
			// TODO dummy usage of m_MeasurementEngine
			m_MeasurementEngine.MeasureText("dummy");
			
			return renderableObjects;
		}
	}
}
