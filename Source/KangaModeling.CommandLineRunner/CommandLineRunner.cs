using System;
using KangaModeling.Compiler.SequenceDiagrams;
using CommandLine;
using System.Drawing.Imaging;
using System.Drawing;
using KangaModeling.Graphics.GdiPlus;
using KangaModeling.Visuals.SequenceDiagrams;

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
            ISequenceDiagram sd = DiagramCreator.CreateFrom(opts.Model);
            Bitmap bm = genBitmap(sd);

            ImageFormat format = ImageFormat.Png;
            switch (opts.Format.ToLowerInvariant())
            {
                case "png":
                    format = ImageFormat.Png;
                    break;
                case "bmp":
                    format = ImageFormat.Bmp;
                    break;
                case "jpeg":
                    format = ImageFormat.Jpeg;
                    break;
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
        private Bitmap genBitmap(ISequenceDiagram sd)
        {
            var sequenceDiagramVisual = new SequenceDiagramVisual(sd);

            using (var measureBitmap = new Bitmap(1, 1))
            using (var measureGraphics = Graphics.FromImage(measureBitmap))
            {
				var graphicContext = new GdiPlusGraphicContext(measureGraphics);

                sequenceDiagramVisual.Layout(graphicContext);

                var renderBitmap = new Bitmap(
                    (int)Math.Ceiling(sequenceDiagramVisual.Width + 1),
                    (int)Math.Ceiling(sequenceDiagramVisual.Height + 1));

                using (var renderGraphics = Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);

					graphicContext =new GdiPlusGraphicContext(renderGraphics);

                    sequenceDiagramVisual.Draw(graphicContext);

                }

                return renderBitmap;
            }
        }
    }
}
