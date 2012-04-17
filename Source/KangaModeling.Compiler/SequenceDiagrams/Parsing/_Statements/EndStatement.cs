namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class EndStatement : Statement
    {
        public EndStatement(Token keyword)
            : base(keyword)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.End(Keyword);
        }
    }
}