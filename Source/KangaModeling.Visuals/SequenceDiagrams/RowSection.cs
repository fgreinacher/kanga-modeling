using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class RowSection
    {
        public float Height { get; private set; }
        public float Top { get; set; }

        public float Middle
        {
            get { return Top + Height/2; }
        }

        public float Bottom
        {
            get { return Top + Height; }
        }

        public void Allocate(float height)
        {
            Height = Math.Max(Height, height);
        }
    }
}