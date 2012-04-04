namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class UnexpectedArgumentStatement : Statement
    {
        public UnexpectedArgumentStatement(Token keyword, Token argument)
            : base(keyword, argument)
        {
        }

        public Token Source
        {
            get { return Arguments[0]; }
        }

        public override void Build(ModelBuilder builder)
        {
            builder.AddError(Source, string.Format("Unexpected argument in statement '{0}'", Keyword.Value));
        }
   } 
}