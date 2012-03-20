using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Parsing;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

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
