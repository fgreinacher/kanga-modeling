using System.Collections.Generic;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Graphics
{	
    public interface IGraphicContext
    {
        void DrawRectangle(Point location, Size size, Color color);

		void FillRectangle(Point location, Size size, Color color);

        void FillPolygon(IEnumerable<Point> points, Color color);

        void DrawText(string text, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Point location, Size size);

		void DrawLine(Point from, Point to, float width);

		void DrawDashedLine(Point from, Point to, float width);

		void DrawArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight);

		void DrawDashedArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight);
		        
        Size MeasureText(string text);

        IDisposable ApplyOffset(float dx, float dy);
    }
}
