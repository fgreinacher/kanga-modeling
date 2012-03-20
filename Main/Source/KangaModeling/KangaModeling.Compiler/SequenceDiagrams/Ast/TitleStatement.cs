using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Reading;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class TitleStatement : Statement
    {
        private readonly Token m_Argument;

        public TitleStatement(Token keyword, Token argument) 
            : base(keyword)
        {
            m_Argument = argument;
        }

        public override void Build(AstBuilder builder)
        {
            builder.SetTitle(m_Argument.Value);
        }

        public override IEnumerable<Token> Tokens()
        {
            return 
                base.Tokens()
                .Append(m_Argument);
        }
    }
}