namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class SimpleParticipantStatement : ParticipantStatement
    {
        public SimpleParticipantStatement(Token keyword, Token name) 
            : base(keyword, name, name)
        {
        }
    }
}
