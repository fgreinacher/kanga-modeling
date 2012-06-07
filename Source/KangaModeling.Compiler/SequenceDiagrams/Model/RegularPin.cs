namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class RegularPin : Pin
    {
        public RegularPin(Row row, Lifeline lifeline)
            : base(row, lifeline, PinType.None)
        {
        }

        public override Orientation Orientation
        {
            get
            {
                if (PinType == PinType.None)
                {
                    return Orientation.None;
                }

                int otherIndex = OtherEnd().Lifeline.Index;
                int thisIndex = Lifeline.Index;
                return
                    otherIndex < thisIndex
                        ? Orientation.Left
                        : Orientation.Right;
            }
        }

        private IPin OtherEnd()
        {
            return
                Signal.Start.Equals(this)
                    ? Signal.End
                    : Signal.Start;
        }
    }
}