using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class Row
    {
        public const float MinHeight = 20;
        public const float RowPadding = 4;

        private float m_MinHeight;

        public Row()
        {
            m_MinHeight = MinHeight;
            TopGap = new RowSection();
            Body = new RowSection();
            BottomGap = new RowSection();
            Body.Allocate(MinHeight);
        }

        public RowSection TopGap { get; private set; }
        public RowSection Body { get; private set; }
        public RowSection BottomGap { get; private set; }

        public float Height
        {
            get { return TopGap.Height + Body.Height + BottomGap.Height + 2 * RowPadding; }
        }

        public float Top
        {
            get { return TopGap.Top - RowPadding; }
        }

        public float Bottom
        {
            get { return BottomGap.Bottom +  RowPadding; }
        }

        public void Allocate(float height)
        {
            m_MinHeight = Math.Max(m_MinHeight, Height);
        }

        public void AdjustLocation(Row prevRow)
        {
            ExtendGaps();
            AdjustSectionLocations(prevRow);
        }

        private void AdjustSectionLocations(Row prevRow)
        {
            TopGap.Top = prevRow == null ? 0 : prevRow.Bottom;
            TopGap.Top += RowPadding;
            Body.Top = TopGap.Bottom;
            BottomGap.Top = Body.Bottom;
        }

        private void ExtendGaps()
        {
            if (m_MinHeight > Height)
            {
                float delta = (m_MinHeight - Body.Height)/2;
                TopGap.Allocate(delta);
                BottomGap.Allocate(delta);
            }
        }
    }
}