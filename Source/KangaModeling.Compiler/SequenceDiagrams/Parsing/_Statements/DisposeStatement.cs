namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class DisposeStatement : StateStatement
    {
        public DisposeStatement(Token keyword, Token argument)
            : base(keyword, argument)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.Dispose(Target);
        }
    }
}