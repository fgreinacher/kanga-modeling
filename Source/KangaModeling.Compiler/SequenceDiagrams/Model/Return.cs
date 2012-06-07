namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class Return : Signal
    {
        public Return(string name)
            : base(name)
        {
        }

        public override SignalType SignalType
        {
            get { return SignalType.Return; }
        }
    }
}