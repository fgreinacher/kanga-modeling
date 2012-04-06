namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Return : Signal
    {
        public Return(string name)
            : base(name)
        {
        }

        public override bool IsReturn
        {
            get { return true; }
        }
    }
}