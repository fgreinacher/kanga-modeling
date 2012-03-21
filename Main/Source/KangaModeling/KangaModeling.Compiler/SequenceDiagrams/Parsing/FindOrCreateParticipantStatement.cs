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
            bool found = false;
            if (!found)
            {
                base.Build(builder);
            }
        }
    }
}