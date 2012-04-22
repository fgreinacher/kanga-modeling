namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal sealed class ColumnDimension
    {
        public float BodyWidth { get; set; }

        public float LeftOuterWidth { get; set; }

        public float RightOuterWidth { get; set; }

        public float Width()
        {
            return BodyWidth + LeftOuterWidth + RightOuterWidth;
        }
    }
}