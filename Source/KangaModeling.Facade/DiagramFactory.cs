using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Errors;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams;
using KangaModeling.Graphics.GdiPlus;
using KangaModeling.Visuals.SequenceDiagrams.Styles;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Visuals.ClassDiagrams;
using DiagramCreator = KangaModeling.Compiler.SequenceDiagrams.DiagramCreator;

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

                case DiagramType.Class:
                    return CreateClassDiagram(arguments);


                default:
                    throw new ArgumentException("", "arguments");
            }
        }

        private static DiagramResult CreateSequenceDiagram(DiagramArguments arguments)
        {
            ModelErrorsCollection modelErrors = new ModelErrorsCollection();
            ISequenceDiagram sequenceDiagram = DiagramCreator.CreateFrom(arguments.Source, modelErrors);

            var diagramErrors = new List<DiagramError>();
            foreach (ModelError modelError in modelErrors)
            {
                diagramErrors.Add(new DiagramError(modelError.Message, modelError.Token.Line, modelError.Token.Start, modelError.Token.Length, modelError.Token.Value));
            }

            return new DiagramResult(
                arguments,
                GenerateBitmap(arguments, sequenceDiagram),
                diagramErrors.ToArray(),
                sequenceDiagram.Root.Title);
        }

        private static DiagramError ToModelError(Error error)
        {
            return new DiagramError(error.ErrorMessage, error.Location.Line, error.Location.PositionInLine,
                                    error.Location.Length, error.ObjectedText);
        }

        private static DiagramResult CreateClassDiagram(DiagramArguments arguments)
        {
            var result = Compiler.ClassDiagrams.DiagramCreator.CreateFrom(arguments.Source);

            var hasErrors = result.Errors.Count() != 0;
            
            return new DiagramResult(
                arguments,
                hasErrors ? null : GenerateBitmap(arguments, result.ClassDiagram),
                result.Errors.Select(ToModelError),
                "class diagram");
        }

        private static Bitmap GenerateBitmap(DiagramArguments arguments, IClassDiagram cd)
        {
            var cdVisual = new ClassDiagramVisual(cd);

            using (var measureBitmap = new Bitmap(1, 1))
            using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
            {
                var graphicContext = new GdiPlusGraphicContext(measureGraphics);

                cdVisual.Layout(graphicContext);

                var renderBitmap = new Bitmap(
                    (int)Math.Ceiling(cdVisual.Width + 1),
                    (int)Math.Ceiling(cdVisual.Height + 1));

                using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);
                    renderGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    renderGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    renderGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    graphicContext = new GdiPlusGraphicContext(renderGraphics);

                    cdVisual.Draw(graphicContext);
                }

                return renderBitmap;
            }
        }

        private static Bitmap GenerateBitmap(DiagramArguments arguments, ISequenceDiagram sequenceDiagram)
        {
            IStyle style;
            switch (arguments.Style)
            {
                case DiagramStyle.Sketchy:
                    style = new SketchyStyle();
                    break;

                case DiagramStyle.Classic:
                    style = new ClassicStyle();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("style");
            }

            if (arguments.Debug)
            {
                style = new DebugStyle(style);
            }

            var sequenceDiagramVisual = new SequenceDiagramVisual(style, sequenceDiagram);

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
                    renderGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    graphicContext = new GdiPlusGraphicContext(renderGraphics);

                    sequenceDiagramVisual.Draw(graphicContext);

                }

                return renderBitmap;
            }
        }
    }
}
