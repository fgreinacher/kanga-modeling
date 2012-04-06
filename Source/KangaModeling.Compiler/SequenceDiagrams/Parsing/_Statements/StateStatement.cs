namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StateStatement : Statement
    {
        protected StateStatement(Token keyword, Token target)
            : base(keyword, target)
        {
        }

        public Token Target
        {
            get { return Arguments[0]; }
        }
    }
}