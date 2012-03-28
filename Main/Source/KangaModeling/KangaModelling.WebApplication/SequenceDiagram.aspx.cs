using System;
using System.Drawing;
using System.Drawing.Imaging;
using KangaModeling.Graphics.GdiPlus;
using KangaModeling.Graphics.Theming;
using KangaModeling.Visuals.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModelling.WebApplication
{
    public class SequenceDiagram : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string codeText = Request["code"] ?? string.Empty;

            ISequenceDiagram sd = DiagramCreator.CreateFrom(codeText);
            using(Bitmap bitmap = GenerateBitmap(sd))
            {

                Response.ContentType = "image/jpeg";
                bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
            }
        }

        private static Bitmap GenerateBitmap(ISequenceDiagram sd)
        {
            ITheme theme = new SimpleTheme();
            var sequenceDiagramVisual = new SequenceDiagramVisual(sd);

            using (var measureBitmap = new Bitmap(1, 1))
            using (var measureGraphics = Graphics.FromImage(measureBitmap))
            {
                var graphicContextFactory = new GdiPlusGraphicContextFactory(measureGraphics);
                var graphicContext = graphicContextFactory.CreateGraphicContext(theme);

                sequenceDiagramVisual.Layout(graphicContext);

                var renderBitmap = new Bitmap(
                    (int) Math.Ceiling(sequenceDiagramVisual.Width + 1),
                    (int) Math.Ceiling(sequenceDiagramVisual.Height + 1));

                using (var renderGraphics = Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);

                    graphicContextFactory = new GdiPlusGraphicContextFactory(renderGraphics);
                    graphicContext = graphicContextFactory.CreateGraphicContext(theme);

                    sequenceDiagramVisual.Draw(graphicContext);

                }

                return renderBitmap;
            }
        }
    }
}
