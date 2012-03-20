namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class TitleStatementParser : StatementParser
    {
        public const string Keyword = "title";

        public override Statement Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token argument = scanner.ReadToEnd();
            return new TitleStatement(keyword, argument);
        }
    }
}