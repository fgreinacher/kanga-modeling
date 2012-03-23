using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics.GdiPlus
{
    public sealed class GdiPlusGraphicContextFactory : IGraphicContextFactory
    {
        private readonly System.Drawing.Graphics m_Graphics;

        public GdiPlusGraphicContextFactory(System.Drawing.Graphics graphics)
        {
            m_Graphics = graphics;
        }

        #region IGraphicContextFactory Members

        public IGraphicContext CreateGraphicContext(ITheme theme)
        {
            return new GdiPlusGraphicContext(m_Graphics, theme);
        }

        #endregion
    }
}