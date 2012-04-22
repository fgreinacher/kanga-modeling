using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class Row
    {
        public const float MinHeight = 20;

        public Row()
        {
            Heigth = MinHeight;
        }

        public float Heigth { get; private set; }

        public float Bottom
        {
            get { return Top + Heigth; }
        }

        public float Top { get; private set; }

        public float Middle
        {
            get { return Top + Heigth/2; }
        }

        public void Allocate(float height)
        {
            Heigth = Math.Max(Heigth, height);
        }

        public void AdjustLocation(Row prevRow)
        {
            if (prevRow == null)
            {
                Top = 0;
                return;
            }

            Top = prevRow.Bottom;
        }
    }
}