using KangaModeling.Graphics.Primitives;
using KangaModeling.Graphics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal sealed class ParticipantLifelineVisual : Visual
    {
        #region Overrides / Overrideables

        protected override Size MeasureCore(IGraphicContext graphicContext)
        {
            return new Size(10, 100);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            graphicContext.DrawLine(new Point(0, 0), new Point(0, 100));
        }

        #endregion
    }
}
