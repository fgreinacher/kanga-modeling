namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class Call : Signal
    {
        public Call(string name)
            : base(name)
        {
        }

        public override SignalType SignalType
        {
            get { return SignalType.Call; }
        }
    }
}