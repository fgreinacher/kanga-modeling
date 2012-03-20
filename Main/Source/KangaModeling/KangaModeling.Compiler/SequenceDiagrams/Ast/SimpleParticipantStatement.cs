using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class SimpleParticipantStatement : ParticipantStatement
    {
        public SimpleParticipantStatement(Token keyword, Token name) 
            : base(keyword, name)
        {
        }
    }
}
