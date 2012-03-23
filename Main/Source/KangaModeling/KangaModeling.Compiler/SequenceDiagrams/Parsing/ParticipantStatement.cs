using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ParticipantStatement : Statement
    {
        private readonly Token m_Name;
        private readonly Token m_Description;

        public Token Name
        {
            get { return m_Name; }
        }

        public Token Description
        {
            get { return m_Description; }
        }

        public ParticipantStatement(Token keyword, Token name, Token description) 
            : base(keyword)
        {
            m_Name = name;
            m_Description = description;
        }

        public override void Build(AstBuilder builder)
        {
            builder.CreateParticipant(Name, Description);
        }

        public override IEnumerable<Token> Tokens()
        {
            return base.Tokens()
                .Append(Name)
                .Append(Description);
        }
    }
}
