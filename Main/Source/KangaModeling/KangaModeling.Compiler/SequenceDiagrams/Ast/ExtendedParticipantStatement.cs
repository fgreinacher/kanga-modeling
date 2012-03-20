using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
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