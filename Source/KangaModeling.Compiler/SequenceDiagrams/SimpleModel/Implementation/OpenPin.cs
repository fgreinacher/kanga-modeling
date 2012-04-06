namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class OpenPin : Pin
    {
        private readonly Orientation m_Orientation;

        public OpenPin(Lifeline lifeline, Orientation orientation)
            : base(null, lifeline, PinType.None)
        {
            m_Orientation = orientation;
        }

        public override Orientation Orientation
        {
            get { return m_Orientation; }
        }
    }
}