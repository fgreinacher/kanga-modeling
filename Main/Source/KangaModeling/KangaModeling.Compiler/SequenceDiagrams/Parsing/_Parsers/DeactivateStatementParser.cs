namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class DeactivateStatementParser : OneArgumentStatementParser
    {
        public const string DeactivateKeyword = "deactivate";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new DeactivateStatement(keyword, argument);
        }
    }
}
