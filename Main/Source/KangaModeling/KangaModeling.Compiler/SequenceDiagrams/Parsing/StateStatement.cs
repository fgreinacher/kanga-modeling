using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StateStatement : Statement
    {
        private readonly Token m_Argument;

        protected StateStatement(Token keyword, Token argument) 
            : base(keyword)
        {
            m_Argument = argument;
        }

        public override void Build(AstBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Token> Tokens()
        {
            return base.Tokens().Append(m_Argument);
        }
    }
}