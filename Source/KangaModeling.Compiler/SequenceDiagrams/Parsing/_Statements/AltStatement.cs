namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class AltStatement : Statement
    {
        public AltStatement(Token keyword, Token guardExpression)
            : base(keyword, guardExpression)
        {
        }

        public Token GuardExpression
        {
            get { return Arguments[0]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.StartAlt(Keyword, GuardExpression);
        }
    }
}