using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
{
    internal class TitleStatementParser : StatementParser
    {
        public static string Keyword
        {
            get { return "title"; }
        }

        public override Statement Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhile(char.IsWhiteSpace);
            Token argument = scanner.ReadToEnd();
            return new TitleStatement(keyword, argument);
        }
    }
}