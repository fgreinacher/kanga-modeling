namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ReturnSignalStatement : SignalStatement
    {
        public ReturnSignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

		protected override SignalType GetSignalType()
		{
			return SignalType.CallReturn;
		}
    }
}
