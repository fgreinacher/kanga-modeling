using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class DeactivateStatementParser : StatementParser
    {
        public const string DeactivateKeyword = "deactivate";

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token argument = scanner.ReadToEnd();
            yield return
                argument.Length == 0
                    ? (Statement) new MissingArgumentStatement(argument)
                    : (Statement) new DeactivateStatement(keyword, argument);
        }
    }
}
