using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Parsing;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Helper
{
    internal static class ParserExtensions
    {
        public static Statement Parse(this StatementParser parser, string input)
        {
            using (Scanner scanner = new Scanner(input))
            {
                scanner.MoveNext();
                return parser.Parse(scanner);
            }
        }
    }
}
