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
            Participant existingParticipant = builder.FindParticipant(Name.Value);
            if (existingParticipant == null)
            {
                base.Build(builder);
            }
        }
    }
}