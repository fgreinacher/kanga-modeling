using KangaModeling.Graphics.Primitives;
using KangaModeling.Graphics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal sealed class ParticipantNameVisual : Visual
    {
        #region Fields

        private readonly string m_Name;

        #endregion

        #region Construction / Destruction / Initialisation

        public ParticipantNameVisual(string name)
        {
            m_Name = name;

			AutoSize = true;
		}

        #endregion
        
        #region Overrides / Overrideables
        
        protected override Size MeasureCore(IGraphicContext graphicContext)
        {
            Size sizeOfName = graphicContext.MeasureText(m_Name);

            return sizeOfName.Plus(10, 10);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            graphicContext.DrawRectangle(new Point(0, 0), Size);
            graphicContext.DrawText(m_Name, HorizontalAlignment.Center, VerticalAlignment.Center, new Point(0, 0), Size);
        }

        #endregion
    }
}
