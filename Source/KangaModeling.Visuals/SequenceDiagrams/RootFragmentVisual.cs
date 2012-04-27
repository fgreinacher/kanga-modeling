using System.Collections.Generic;
using System.Linq;
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

        protected override Visual CreateOperandVisual(IOperand operand)
        {
            return new InvisibleOperandVisual(operand, GridLayout);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            IList<Column> columns = GridLayout.Columns;
            float widthSum = columns.Select(column => column.Width).Sum();
            if (widthSum<this.Width)
            {
                float widthPerCoulumn = this.Width/columns.Count;
                foreach (Column column in columns)
                {
                    column.Allocate(widthPerCoulumn);
                }
            }

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