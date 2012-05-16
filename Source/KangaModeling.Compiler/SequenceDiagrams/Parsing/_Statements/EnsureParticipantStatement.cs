namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
    /// Statement that makes sure that a given participant is present,
    /// creating one if the participant does not exist.
    /// </summary>
    internal class EnsureParticipantStatement : SimpleParticipantStatement
    {
        public EnsureParticipantStatement(Token name)
            : base(name, name)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.CreateParticipant(Id, Name, false);
        }
    }
}