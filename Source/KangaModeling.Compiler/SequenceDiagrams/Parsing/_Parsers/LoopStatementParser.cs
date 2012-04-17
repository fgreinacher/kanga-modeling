namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class LoopStatementParser : OneArgumentStatementParser
    {
        public const string LoopKeyword = "loop";

        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new LoopStatement(keyword, argument);
        }
    }
}