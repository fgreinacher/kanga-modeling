using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics
{
    public interface IGraphicContextFactory
    {
        IGraphicContext CreateGraphicContext(ITheme theme);
    }
}
