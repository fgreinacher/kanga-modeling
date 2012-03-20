using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
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