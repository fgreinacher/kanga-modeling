namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class FindOrCreateParticipantStatement : SimpleParticipantStatement
    {
        public FindOrCreateParticipantStatement(Token name) 
            : base(name, name)
        {
        }

        public override void Build(AstBuilder builder)
        {
            Participant p = builder.FindParticipant(Keyword.Value);
            if (p == null)
            {
                builder.CreateParticipant(Keyword.Value);
            }
        }
    }
}