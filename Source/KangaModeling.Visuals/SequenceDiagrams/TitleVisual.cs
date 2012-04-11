using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public class TitleVisual : Visual
    {
        #region Fields
        
        private readonly string m_Title;

        #endregion

        #region Construction / Destruction / Initialisation
        
        public TitleVisual(string title)
        {
            m_Title = title;
        }

        #endregion

        #region Overrides / Overrideables
        
        protected override void DrawCore(IGraphicContext graphicContext)
        {
            graphicContext.DrawText(m_Title, HorizontalAlignment.Left, VerticalAlignment.Middle, new Point(0, 0), Size);
        }

        #endregion
    }
}
