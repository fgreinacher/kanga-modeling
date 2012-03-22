using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Renderables;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Layouter.SequenceDiagrams
{
	/// <summary>
	/// The layout engine is responsible for creating the appropriate renderable objects 
	/// from a specified sequence diagram. It has a dependency on <see cref="IMeasurementEngine"/>
	/// because it might need to measure the dimensions of renderable text.
	/// </summary>
	public sealed class RenderableFactory
	{
		private readonly IMeasurer m_Measurer;
		private readonly ITheme m_Theme;

		public RenderableFactory(IMeasurer measurer, ITheme theme)
		{
			if (measurer == null) throw new ArgumentNullException("measurer");
			if (theme == null) throw new ArgumentNullException("theme");

			m_Measurer = measurer;
			m_Theme = theme;
		}

		public IEnumerable<Renderable> CreateRenderables(ISequenceDiagram sequenceDiagram)
		{
			if (sequenceDiagram == null) throw new ArgumentNullException("sequenceDiagram");

			var renderables = new List<Renderable>();

			float x = m_Theme.Padding;
			float y = m_Theme.Padding;

			var title = sequenceDiagram.Title;
			if (title != null)
			{
				var titleSize = m_Measurer.MeasureText(title);
				var renderableText = new RenderableText(title, new Point(x, y), titleSize);
				renderables.Add(renderableText);

				y += titleSize.Height;
			}

			y += m_Theme.Padding;

			float maximumParticpantNameHeight = 0;

			foreach (var participant in sequenceDiagram.Participants)
			{
				var participantName = participant.Name;
				var participantNameSize = m_Measurer.MeasureText(participantName);

				var renderableRectangle = new RenderableRectangle(new Point(x, y), participantNameSize);
				renderables.Add(renderableRectangle);

				var renderableText = new RenderableText(participantName, new Point(x, y), participantNameSize);
				renderables.Add(renderableText);

				x += participantNameSize.Width + m_Theme.Padding;

				maximumParticpantNameHeight = Math.Max(maximumParticpantNameHeight, participantNameSize.Height);
			}

			return renderables;
		}

		public Size CalculateSize(IEnumerable<Renderable> renderables)
		{
			float maximumRight = 0;
			float maximumBottom = 0;
			foreach (var renderable in renderables)
			{
				maximumRight = Math.Max(maximumRight, renderable.Location.X + renderable.Size.Width);
				maximumBottom = Math.Max(maximumBottom, renderable.Location.Y + renderable.Size.Height);
			}

			maximumRight += m_Theme.Padding;
			maximumBottom += m_Theme.Padding;

			return new Size(maximumRight, maximumBottom);
		}
	}
}
