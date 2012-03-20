using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Parsing
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