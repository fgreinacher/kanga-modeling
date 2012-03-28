using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Graphics
{
	[Flags]
	public enum LineOptions
	{
		None = 0,
		Dashed = 1 << 1,
		ArrowEnd = 1 << 2,
	}
	
    public interface IGraphicContext
    {
        void DrawRectangle(Point location, Size size);

        void DrawText(string text, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Point location, Size size);

		void DrawLine(Point from, Point to, float width, LineOptions options = LineOptions.None);
		        
        Size MeasureText(string text);

        IDisposable ApplyOffset(float dx, float dy);
    }
}
