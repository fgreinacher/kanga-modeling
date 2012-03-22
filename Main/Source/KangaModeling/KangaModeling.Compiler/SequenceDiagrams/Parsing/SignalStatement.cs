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
            Participant sourceParticipant = builder.FindParticipant(m_Source.Value);
            if (sourceParticipant == null)
            {
                builder.AddError(m_Source, "no such participant");
                return;
            }
            Participant targetParticipant = builder.FindParticipant(m_Target.Value);
            if (targetParticipant == null)
            {
                builder.AddError(m_Target, "no such participant");
                return;
            }

            builder.AddSignal(new SignalElement(sourceParticipant, targetParticipant, SignalElement.Type.Signal));

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