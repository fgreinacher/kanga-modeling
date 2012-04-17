namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class AltStatementParser : OneArgumentStatementParser
    {
        public const string AltKeyword = "alt";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new AltStatement(keyword, argument);
        }
    }
}