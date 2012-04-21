namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class DisposeStatementParser : OneArgumentStatementParser
    {
        public const string DestroyKeyword = "destroy";
        public const string DisposeKeyword = "dispose";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new DisposeStatement(keyword, argument);
        }
    }
}