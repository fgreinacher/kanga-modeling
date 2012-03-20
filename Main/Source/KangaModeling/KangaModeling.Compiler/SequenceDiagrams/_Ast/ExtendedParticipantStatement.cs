using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
{
    internal class ExtendedParticipantStatement : ParticipantStatement
    {
        private readonly Token m_Description;

        public ExtendedParticipantStatement(Token keyword, Token name, Token description) 
            : base(keyword, name)
        {
            m_Description = description;
        }

        public override IEnumerable<Token> Tokens()
        {
           return base.Tokens()
                .Append(m_Description);
        }
    }
}