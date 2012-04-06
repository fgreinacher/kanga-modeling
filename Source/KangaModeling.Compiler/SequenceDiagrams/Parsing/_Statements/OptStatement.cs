namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class OptStatement : Statement
    {
        public OptStatement(Token keyword, Token guardExpression)
            : base(keyword, guardExpression)
        {
        }

        public Token GuardExpression
        {
            get { return Arguments[0]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.StartOpt(GuardExpression);
        }
    }
}