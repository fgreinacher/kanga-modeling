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
        }
    }
}