using KangaModeling.Graphics.Primitives;
using KangaModeling.Graphics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal sealed class ParticipantLifelineVisual : Visual
    {
		public ParticipantLifelineVisual()
		{
			AutoSize = true;
		}

        #region Overrides / Overrideables

        protected override Size MeasureCore(IGraphicContext graphicContext)
        {
            return new Size(10, 200);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            graphicContext.DrawLine(new Point(0, 0), new Point(0, Height), 1);
        }

        #endregion
    }
}
