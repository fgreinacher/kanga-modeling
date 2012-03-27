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
                argument.IsEmpty()
                    ? new MissingArgumentStatement(keyword, argument)
                    : (Statement) new TitleStatement(keyword, argument);
        }
    }
}