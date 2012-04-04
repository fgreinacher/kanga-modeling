using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Helper
{
    internal static class ParserExtensions
    {
        internal static IEnumerable<Statement> Parse(this StatementParser parser, string input)
        {
            using (Scanner scanner = new Scanner(input))
            {
                scanner.MoveNext();
                foreach(var statement in parser.Parse(scanner))
                {
                    yield return statement;
                }
            }
        }
    }
}
