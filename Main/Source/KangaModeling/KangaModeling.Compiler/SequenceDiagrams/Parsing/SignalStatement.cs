namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class SignalStatement : Statement
    {
        public Token Source {get { return Arguments[0]; }}
        public Token Target { get { return Arguments[1]; } }
        public Token Name { get { return Arguments[2]; } }

        protected SignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

        public override void Build(ModelBuilder builder)
        {
            Participant sourceParticipant = builder.FindParticipant(Source.Value);
            if (sourceParticipant == null)
            {
                builder.AddError(Source, "No such participant");
                return;
            }
            Participant targetParticipant = builder.FindParticipant(Target.Value);
            if (targetParticipant == null)
            {
                builder.AddError(Target, "No such participant");
                return;
            }

            if (sourceParticipant.Equals(targetParticipant))
            {
                builder.AddError(Target, "Self signal is not supported currently.");
                return;
            }

            builder.AddSignal(new SignalElement(sourceParticipant, targetParticipant, SignalElement.Type.Signal));
        }
    }
}