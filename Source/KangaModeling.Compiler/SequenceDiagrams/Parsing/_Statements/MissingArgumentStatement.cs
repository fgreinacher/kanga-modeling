namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class MissingArgumentStatement : Statement
    {
        public MissingArgumentStatement(Token keyword, Token source)
            : base(keyword, source)
        {
        }

        public Token Source
        {
            get { return Arguments[0]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.AddError(Source, string.Format("Argument expected in statement '{0}'", Keyword.Value));
        }
    }
}