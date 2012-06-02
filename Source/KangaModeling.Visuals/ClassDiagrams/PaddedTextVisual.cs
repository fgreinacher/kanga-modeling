using System;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public sealed class PaddedTextVisual : Visual
    {
        public PaddedTextVisual(IDisplayable text, Padding padding = null)
        {
            if (text == null) throw new ArgumentNullException("text");
            _text = text;
            _padding = padding ?? new Padding(0f);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            // TODO debug flag graphicContext.FillRectangle(Point.Empty, Size, Color.Blue);

            graphicContext.DrawText(
                Point.Empty,
                PaddedSize,
                _text.DisplayText,
                Font.Handwritten,
                12,
                Color.Black,
                HorizontalAlignment.Left,
                VerticalAlignment.Middle
                );
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            Size = graphicContext.MeasureText(_text.DisplayText, Font.Handwritten, 12);
        }


        //public Size Size { get { return _textSize; } }
        private Size PaddedSize
        {
            get { return Size.Add(_padding.Left + _padding.Right, _padding.Top + _padding.Bottom); }
        }

        //public Point Location { get { return _location; } }
        private Point PaddedLocation
        {
            get { return new Point(Location.X + _padding.Left, Location.Y + _padding.Top); }
        }

        private readonly Padding _padding;
        private readonly IDisplayable _text;
    }
}