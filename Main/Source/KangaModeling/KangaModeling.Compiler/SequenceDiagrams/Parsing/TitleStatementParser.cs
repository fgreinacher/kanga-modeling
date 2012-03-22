using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class TitleStatementParser : StatementParser
    {
        public const string Keyword = "title";

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token argument = scanner.ReadToEnd();
            yield return
                argument.Length != 0
                    ? (Statement) new TitleStatement(keyword, argument)
                    : new MissingArgumentStatement(keyword, argument);
        }
    }
}