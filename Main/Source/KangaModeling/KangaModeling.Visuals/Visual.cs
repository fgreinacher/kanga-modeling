using System;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals
{
    public abstract class Visual
    {
        #region Construction / Destruction / Initialisation

        protected Visual()
        {
            Children = new VisualCollection(this);
            Location = new Point(0, 0);
            Size = new Size(0, 0);
        }

        #endregion

        #region Properties

        public VisualCollection Children
        {
            get;
            private set;
        }

        public Point Location
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            set;
        }

        public float Width
        {
            get { return Size.Width; }
        }

        public float Height
        {
            get { return Size.Height; }
        }

        public float X
        {
            get { return Location.X; }
        }

        public float Y
        {
            get { return Location.Y; }
        }

        public float Padding
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public void Layout(IGraphicContext graphicContext)
        {
            foreach (var child in Children)
            {
                child.Layout(graphicContext);
            }

            Arrange(graphicContext);
            Size = Measure(graphicContext);
        }

        public void Draw(IGraphicContext graphicContext)
        {
            using (graphicContext.ApplyOffset(Padding, Padding))
            {
                DrawCore(graphicContext);

                foreach (var child in Children)
                {
                    using (graphicContext.ApplyOffset(child.X, child.Y))
                    {
                        child.Draw(graphicContext);
                    }
                }
            }
        }

        #endregion

        #region Overrides / Overrideables

        protected virtual void ArrangeCore(IGraphicContext graphicContext)
        {
        }

        protected virtual Size MeasureCore(IGraphicContext graphicContext)
        {
            float maximumX = 0;
            float maximumY = 0;

            foreach (var child in Children)
            {
                maximumX = Math.Max(child.X + child.Width, maximumX);
                maximumY = Math.Max(child.Y + child.Height, maximumY);
            }

            return new Size(maximumX, maximumY);
        }

        protected virtual void DrawCore(IGraphicContext graphicContext)
        {
        }
        #endregion

        #region Private Methods

        private void Arrange(IGraphicContext graphicContext)
        {
            ArrangeCore(graphicContext);
        }

        private Size Measure(IGraphicContext graphicContext)
        {
            return MeasureCore(graphicContext).Plus(Padding * 2, Padding * 2);
        }

        #endregion
    }
}
