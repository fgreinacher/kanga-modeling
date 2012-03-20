using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class TitleStatement : Statement
    {
        private readonly Token m_Keyword;
        private readonly Token m_Argument;

        public TitleStatement(Token keyword, Token argument)
        {
            m_Keyword = keyword;
            m_Argument = argument;
        }

        public override void Build(AstBuilder builder)
        {
            builder.SetTitle(m_Argument.Value);
        }

        public override IEnumerable<Token> Tokens()
        {
            yield return m_Keyword;
            yield return m_Argument;
        }
    }
}