namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class UnknownStatement : Statement
    {
        public UnknownStatement(Token invalidToken) 
            : base(invalidToken)
        {
        }

        public override void Build(ModelBuilder builder)
        {
            builder.AddError(Keyword, "Unrecognized statement.");
        }
    }
}