using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class SignalStatement : Statement
    {
        private readonly Token m_Source;
        private readonly Token m_Target;
        private readonly Token m_Name;

        protected SignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword)
        {
            m_Source = source;
            m_Target = target;
            m_Name = name;
        }

        public override void Build(AstBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Token> Tokens()
        {
            return base.Tokens().Concat(SignalTokens());
        }

        private IEnumerable<Token> SignalTokens()
        {
            yield return m_Name;
            yield return m_Source;
            yield return m_Target;
        }
    }
}