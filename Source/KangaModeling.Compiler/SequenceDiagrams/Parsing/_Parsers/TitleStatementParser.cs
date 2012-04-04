namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class TitleStatementParser : OneArgumentStatementParser
    {
        public const string Keyword = "title";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new TitleStatement(keyword, argument);
        }
    }
}