using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Graphics
{	
    public interface IGraphicContext
    {
        void DrawRectangle(Point location, Size size);

        void DrawText(string text, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Point location, Size size);

		void DrawLine(Point from, Point to, float width, LineOptions options = LineOptions.None);
		        
        Size MeasureText(string text);

        IDisposable ApplyOffset(float dx, float dy);
    }
}
