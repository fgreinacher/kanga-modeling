namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ReturnSignalStatement : SignalStatement
    {
        public ReturnSignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

        protected override void AddSignal(ModelBuilder builder, Participant sourceParticipant, Participant targetParticipant)
        {
            builder.AddSignal(new SignalElement(Name.Value, sourceParticipant, targetParticipant, SignalType.CallReturn));
        }
    }
}
