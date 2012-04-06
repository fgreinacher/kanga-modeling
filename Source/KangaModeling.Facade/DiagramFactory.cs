using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams;
using KangaModeling.Graphics.GdiPlus;

namespace KangaModeling.Facade
{
	/// <summary>
	/// Represents a factory that creates a diagram from a specified set of arguments.
	/// </summary>
	public class DiagramFactory
	{
		public static DiagramResult Create(DiagramArguments arguments)
		{
			switch (arguments.Type)
			{
				case DiagramType.Sequence:
					return CreateSequenceDiagram(arguments);

				default:
					throw new ArgumentException("", "arguments");
			}
		}

		private static DiagramResult CreateSequenceDiagram(DiagramArguments arguments)
		{
			IEnumerable<ModelError> modelErrors;
			ISequenceDiagram sequenceDiagram = DiagramCreator.CreateFrom(arguments.Text, out modelErrors);

			var diagramErrors = new List<DiagramError>();
			foreach (ModelError modelError in modelErrors)
			{
				diagramErrors.Add(new DiagramError(modelError.Message, modelError.Token.Line, modelError.Token.Start, modelError.Token.Length));
			}

			return new DiagramResult(
				arguments,
				GenerateBitmap(sequenceDiagram),
				diagramErrors.ToArray());
		}

		private static Bitmap GenerateBitmap(ISequenceDiagram sd)
		{
			var sequenceDiagramVisual = new SequenceDiagramVisual(sd);

			using (var measureBitmap = new Bitmap(1, 1))
			using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
			{
				var graphicContext = new GdiPlusGraphicContext(measureGraphics);

				sequenceDiagramVisual.Layout(graphicContext);

				var renderBitmap = new Bitmap(
					(int)Math.Ceiling(sequenceDiagramVisual.Width + 1),
					(int)Math.Ceiling(sequenceDiagramVisual.Height + 1));

				using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
				{
					renderGraphics.Clear(Color.White);
					renderGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
					renderGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

					graphicContext = new GdiPlusGraphicContext(renderGraphics);

					sequenceDiagramVisual.Draw(graphicContext);

				}

				return renderBitmap;
			}
		}
	}
}
