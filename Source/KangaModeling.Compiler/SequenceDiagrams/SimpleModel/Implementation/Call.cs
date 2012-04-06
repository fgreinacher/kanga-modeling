namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Call : Signal
    {
        public Call(string name)
            : base(name)
        {
        }

        public override bool IsReturn
        {
            get { return false; }
        }
    }
}