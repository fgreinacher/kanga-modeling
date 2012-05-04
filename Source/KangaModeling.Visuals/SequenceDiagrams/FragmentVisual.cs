using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FragmentVisual : FragmentVisualBase
    {
        public FragmentVisual(IStyle style, ICombinedFragment fragment, GridLayout gridLayout)
            : base(style, fragment, gridLayout)
        {
            TopRow = gridLayout.Rows[Area.Top];
            BottomRow = gridLayout.Rows[Area.Bottom];

            LeftColumn = gridLayout.Columns[Area.Left];
            RightColumn = gridLayout.Columns[Area.Right];
            Initialize();
        }

        protected override Visual CreateOperandVisual(IOperand operand, bool isFirst, Column leftColumn, Column rightColumn)
        {
            return new OperandVisual(Style, operand, this.GridLayout, isFirst, leftColumn, rightColumn);
        }
    }
}