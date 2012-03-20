using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
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