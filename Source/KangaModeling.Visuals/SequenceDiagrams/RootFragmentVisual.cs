using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class RootFragmentVisual : FragmentVisual
    {
        public RootFragmentVisual(ICombinedFragment fragment, GridLayout gridLayout)
            : base(fragment, gridLayout)
        {
            TopRow = gridLayout.HeaderRow;
            BottomRow = gridLayout.FooterRow;
            LeftColumn = gridLayout.Columns[0];
            RightColumn = gridLayout.Columns[gridLayout.Columns.Count - 1];
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yStart = TopRow.Top;
            float yEnd = BottomRow.Bottom;

            float xStart = LeftColumn.Left;
            float xEnd = RightColumn.Right;

            DrawInternal(xEnd, xStart, yEnd, yStart, graphicContext);
            //base.DrawCore(graphicContext);
        }
    }
}