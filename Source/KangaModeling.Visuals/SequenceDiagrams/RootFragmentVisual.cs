using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class RootFragmentVisual : FragmentVisual
    {
        public RootFragmentVisual(IStyle style, ICombinedFragment fragment, GridLayout gridLayout)
            : base(style, fragment, gridLayout)
        {
            TopRow = gridLayout.HeaderRow;
            BottomRow = gridLayout.FooterRow;
            LeftColumn = gridLayout.Columns[0];
            RightColumn = gridLayout.Columns[gridLayout.Columns.Count - 1];
            Initialize();
        }

        protected override Visual CreateOperandVisual(IOperand operand, bool isFirst, Column leftColumn, Column rightColumn)
        {
            return new InvisibleOperandVisual(Style, operand, GridLayout);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);
            
            GridLayout.AllocateBetween(LeftColumn, RightColumn, Width + 3 * Style.Fragment.FramePadding);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yStart = TopRow.Top;
            float yEnd = BottomRow.Bottom;

            float xStart = LeftColumn.Left;
            float xEnd = RightColumn.Right;

            DrawInternal(xEnd, xStart, yEnd, yStart, graphicContext);
        }
    }
}