namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class OptStatementParser : OneArgumentStatementParser
    {
        public const string OptKeyword = "opt";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new OptStatement(keyword, argument);
        }
    }
}