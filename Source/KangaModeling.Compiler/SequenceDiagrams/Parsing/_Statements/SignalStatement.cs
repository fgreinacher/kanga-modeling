namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class SignalStatement : Statement
    {
        protected SignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

        public Token Source
        {
            get { return Arguments[0]; }
        }

        public Token Target
        {
            get { return Arguments[1]; }
        }

        public Token Name
        {
            get { return Arguments[2]; }
        }
    }
}