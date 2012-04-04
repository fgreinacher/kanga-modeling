using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
{
    internal class SimpleParticipantStatement : ParticipantStatement
    {
        public SimpleParticipantStatement(Token keyword, Token name) 
            : base(keyword, name)
        {
        }
    }
}
