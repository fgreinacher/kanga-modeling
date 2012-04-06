namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class TitleStatement : Statement
    {
        public TitleStatement(Token keyword, Token titleText)
            : base(keyword, titleText)
        {
        }

        public Token Title
        {
            get { return Arguments[0]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.SetTitle(Title);
        }
    }
}