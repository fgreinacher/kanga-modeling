using System.Collections.Generic;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Graphics
{	
    public interface IGraphicContext
    {
        void FillRectangle(Point location, Size size, Color color);

        void FillPolygon(IEnumerable<Point> points, Color color);

        void DrawRectangle(Point location, Size size, Color color, LineStyle lineStyle);
        
        void DrawText(Point location, Size size, string text, Font font, float fontSize, Color color, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment);

		void DrawLine(Point from, Point to, float width, Color color, LineStyle lineStyle);

        void DrawDashedLine(Point from, Point to, float width, Color color, LineStyle lineStyle);

        void DrawArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight, Color color, LineStyle lineStyle);

        void DrawDashedArrow(Point from, Point to, float width, float arrowCapWidth, float arrowCapHeight, Color color, LineStyle lineStyle);

        Size MeasureText(string text, Font font, float fontSize);

        IDisposable ApplyOffset(float dx, float dy);
    }
}
