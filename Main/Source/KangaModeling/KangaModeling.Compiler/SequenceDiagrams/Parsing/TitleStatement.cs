using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
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