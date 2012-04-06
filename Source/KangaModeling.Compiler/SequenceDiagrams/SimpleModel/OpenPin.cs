namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class OpenPin : Pin
    {
        private readonly Orientation m_Orientation;

        public OpenPin(Lifeline lifeline, Orientation orientation, Token token)
            : base(null, lifeline, PinType.None)
        {
            m_Orientation = orientation;
            Token = token;
        }

        public override Orientation Orientation
        {
            get { return m_Orientation; }
        }

        public Token Token { get; private set; }
    }
}