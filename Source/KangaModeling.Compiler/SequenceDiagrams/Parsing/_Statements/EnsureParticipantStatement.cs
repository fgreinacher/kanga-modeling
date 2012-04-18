namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class EnsureParticipantStatement : SimpleParticipantStatement
    {
        public EnsureParticipantStatement(Token name)
            : base(name, name)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.EnsureParticipant(Id);
        }
    }
}