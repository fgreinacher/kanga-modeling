using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StateStatement : Statement
    {
        public Token Target
        {
            get { return Arguments[0]; }
        }

        protected StateStatement(Token keyword, Token target)
            : base(keyword, target)
        {
           
        }

        public override void Build(AstBuilder builder)
        {
            Participant targetParticipant = builder.FindParticipant(Target.Value);
            if (targetParticipant==null)
            {
                builder.AddError(Target, "Unknown participant. Missing 'participant' declaration or use in signal statement.");
                return;
            }
            //TOD Implement state change
        }
    }
}