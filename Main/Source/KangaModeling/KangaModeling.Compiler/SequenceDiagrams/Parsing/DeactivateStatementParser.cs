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
                argument.IsEmpty()
                    ? (Statement) new MissingArgumentStatement(keyword, argument)
                    : (Statement) new DeactivateStatement(keyword, argument);
        }
    }
}
