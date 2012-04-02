using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class EndStatementParser : StatementParser
    {
        public const string EndKeyword = "end";

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token argument = scanner.ReadToEnd();
            yield return
                !argument.IsEmpty()
                    ? (Statement) new UnexpectedArgumentStatement(keyword, argument)
                    : new EndStatement(keyword);
        }
    }
}
