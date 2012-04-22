using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class Column
    {
        public float Width { get; private set; }
        public float Left { get; private set; }

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

        public void AdjustLocation(Column prevColumn)
        {
            if (prevColumn == null)
            {
                Left = 0;
                return;
            }

            Left = prevColumn.Right;
        }
    }
}