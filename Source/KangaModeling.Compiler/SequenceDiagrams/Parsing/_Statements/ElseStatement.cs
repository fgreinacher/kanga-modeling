namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ElseStatement : Statement
    {
        public ElseStatement(Token keyword, Token guardExpression)
            : base(keyword, guardExpression)
        {
        }

        public Token GuardExpression
        {
            get { return Arguments[0]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.StartElse(Keyword, GuardExpression);
        }
    }
}