namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ParticipantStatement : Statement
    {
        public ParticipantStatement(Token keyword, Token id, Token name)
            : base(keyword, id, name)
        {
        }

        public Token Id
        {
            get { return Arguments[0]; }
        }

        public Token Name
        {
            get { return Arguments[1]; }
        }

        public override void Build(IModelBuilder builder)
        {
            builder.CreateParticipant(Id, Name, true);
        }
    }
}