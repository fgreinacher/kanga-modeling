namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class CreateSignalStatement : SignalStatement
    {
        public CreateSignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.AddCreateSignal(Source, Target, Name);
        }
    }
}