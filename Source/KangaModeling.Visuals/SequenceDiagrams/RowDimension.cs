namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal sealed class RowDimension
    {
        public float BodyHeight { get; set; }

        public float TopOuterHeight { get; set; }

        public float BottomOuterHeight { get; set; }

        public float Height()
        {
            return BodyHeight + TopOuterHeight + BottomOuterHeight;
        }
    }
}