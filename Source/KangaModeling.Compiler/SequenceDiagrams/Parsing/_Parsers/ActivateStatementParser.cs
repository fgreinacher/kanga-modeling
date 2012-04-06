namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ActivateStatementParser : OneArgumentStatementParser
    {
        public const string ActivateKeyword = "activate";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new ActivateStatement(keyword, argument);
        }
    }
}