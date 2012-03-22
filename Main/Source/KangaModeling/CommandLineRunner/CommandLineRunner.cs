using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams;
using CommandLine;
using System.Drawing.Imaging;
using System.Drawing;
using KangaModeling.Graphics.GdiPlus;
using KangaModeling.Graphics.Theming;
using KangaModeling.Layouter.SequenceDiagrams;
using KangaModeling.Graphics.Renderables;
 

namespace CommandLineRunner
{

    /// <summary>
    /// Main class for running KangaModeling from cmd line.
    /// </summary>
    public sealed class CommandLineRunner
    {

        public static int Main(String[] args)
        {
            Options opts = new Options();
            if (!new CommandLineParser().ParseArguments(args, opts))
            {
                System.Console.Error.WriteLine(opts.GetHelp());
                return -1;
            }
            new CommandLineRunner().run(opts);
            return 0;
        }

        internal void run(Options opts)
        {
            SequenceDiagram sd = Parser.ParseString(opts.Model);
            Bitmap bm = genBitmap(sd);

            ImageFormat format = ImageFormat.Png;
            switch (opts.Format.ToLowerInvariant())
            {
                case "png": format = ImageFormat.Png; break;
                case "bmp": format = ImageFormat.Bmp; break;
                case "jpeg": format = ImageFormat.Jpeg; break;
                default:
                    throw new ArgumentException("unknown format: " + opts.Format);
            }

            bm.Save(opts.FileName, format);
            bm.Dispose();
        }

        /// <summary>
        /// intermediate; needs to be hidden!
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        private Bitmap genBitmap(SequenceDiagram sd)
        {
            ITheme theme = new SimpleTheme();

            using(var measureBitmap = new Bitmap(1, 1))
            using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
            {
                var measurerFactory = new GdiPlusMeasurerFactory(measureGraphics);
                var measurer = measurerFactory.CreateMeasurer(theme);

                var layoutEngine = new LayoutEngine(measurer);
                var layoutResult = layoutEngine.PerformLayout(sd);

                var renderBitmap = new Bitmap(
                    (int)Math.Ceiling(layoutResult.Size.Width + 1),
                    (int)Math.Ceiling(layoutResult.Size.Height + 1));

                using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);

                    var rendererFactory = new GdiPlusRendererFactory(renderGraphics);
                    var renderer = rendererFactory.CreateRenderer(theme);

                    foreach (var renderable in layoutResult.Renderables)
                    {
                        var renderableText = renderable as RenderableText;
                        if (renderableText != null)
                        {
                            renderer.RenderText(renderableText);
                        }
                    }
                }

                return renderBitmap;
            }
        }

    }
}
