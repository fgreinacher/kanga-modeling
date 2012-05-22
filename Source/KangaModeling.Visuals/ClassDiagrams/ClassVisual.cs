using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public sealed class ClassVisual : Visual
    {
        private readonly IClass _Class;

        public ClassVisual(IClass cd)
            : base()
        {
            if (cd == null) throw new ArgumentNullException("cd");
            _Class = cd;
            Initialize();
        }

        public void Initialize()
        {
        }

        protected override void DrawCore(Graphics.IGraphicContext graphicContext)
        {
            // the class name
            graphicContext.DrawRectangle(new Point(0, 0), _nameCompartmentSize, Color.Black, LineStyle.Clean);

            graphicContext.DrawText(
                _nameTextPoint,
                _nameTextSize,
                _Class.Name,
                Font.Handwritten,
                12,
                Color.Black,
                HorizontalAlignment.Center,
                VerticalAlignment.Middle
            );
        }

        protected override void LayoutCore(Graphics.IGraphicContext graphicContext)
        {
            Size textSize = graphicContext.MeasureText(_Class.Name, Font.Handwritten, 12);

            // some padding left and right...
            float leftPadding = 10f, rightPadding = 10f;

            _nameTextSize = textSize;
            _nameTextPoint = new Point(leftPadding, 4f);

            _nameCompartmentSize = new Size(
                leftPadding + textSize.Width + rightPadding,
                4f + textSize.Height + 4f
            );

            Size = _nameCompartmentSize;
        }

        private Size _nameCompartmentSize;
        private Size _nameTextSize;
        private Point _nameTextPoint;
    }
}