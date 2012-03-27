namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class FindOrCreateParticipantStatement : SimpleParticipantStatement
    {
        public FindOrCreateParticipantStatement(Token name) 
            : base(name, name)
        {
        }

        public override void Build(ModelBuilder builder)
        {
            Participant existingParticipant;
            if (!builder.TryGetParticipantByName(Name.Value, out existingParticipant))
            {
                base.Build(builder);
            }
        }
    }
}