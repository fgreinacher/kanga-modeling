using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ParticipantStatement : Statement
    {
        public Token Name
        {
            get { return Arguments[0]; }
        }

        public Token Description
        {
            get { return Arguments[1]; }
        }

        public ParticipantStatement(Token keyword, Token name, Token description) 
            : base(keyword, name, description)
        {
        }

        public override void Build(AstBuilder builder)
        {
            builder.CreateParticipant(Name, Description);
        }
    }
}
