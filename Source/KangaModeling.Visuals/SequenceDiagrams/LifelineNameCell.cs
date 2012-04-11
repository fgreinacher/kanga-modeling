using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelineNameCell : Cell
    {
        #region Construction / Destruction / Initialisation

        public LifelineNameCell(Grid grid, int row, int column)
            : base(grid, row, column)
        {
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public bool IsTop { get; set; }

        public bool IsBottom { get; set; }

        public bool IsLeft { get; set; }

        public bool IsRight { get; set; }

        #endregion

        #region Overrides / Overrideables

        protected override void LayoutOutersCore(IGraphicContext graphicContext)
        {
            const float outerMargin = 5;
            
            if (!IsLeft)
            {
                LeftOuterWidth = outerMargin;
            }
        }

        protected override void LayoutBodyCore(IGraphicContext graphicContext)
        {
            Size nameSize = graphicContext.MeasureText(Name).Plus(20, 0);

            BodyWidth = nameSize.Width;
            BodyHeight = nameSize.Height;
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            Point location = new Point(
                LeftOuterWidth,
                TopOuterHeight);
            graphicContext.DrawRectangle(location, new Size(BodyWidth, BodyHeight));
            graphicContext.DrawText(Name, HorizontalAlignment.Center, VerticalAlignment.Middle, location, new Size(BodyWidth, BodyHeight));
        }

        #endregion
    }
}