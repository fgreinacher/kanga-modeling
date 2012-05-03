using System;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FragmentVisual : FragmentVisualBase
    {
        public FragmentVisual(ICombinedFragment fragment, GridLayout gridLayout)
            : base(fragment, gridLayout)
        {
            TopRow = gridLayout.Rows[Area.Top];
            BottomRow = gridLayout.Rows[Area.Bottom];

            LeftColumn = gridLayout.Columns[Area.Left];
            RightColumn = gridLayout.Columns[Area.Right];
            Initialize();
        }

        protected override Visual CreateOperandVisual(IOperand operand, bool isFirst, Column leftColumn, Column rightColumn)
        {
            return new OperandVisual(operand, this.GridLayout, isFirst, leftColumn, rightColumn);
        }
    }
}