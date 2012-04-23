using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class ColumnSection
    {
        public float Width { get; private set; }
        public float Left { get; set; }

        public float Middle
        {
            get { return Left + Width/2; }
        }

        public float Right
        {
            get { return Left + Width; }
        }

        public void Allocate(float width)
        {
            Width = Math.Max(Width, width);
        }
    }
}