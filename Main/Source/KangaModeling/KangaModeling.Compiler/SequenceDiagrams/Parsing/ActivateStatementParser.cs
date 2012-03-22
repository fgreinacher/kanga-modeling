using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ActivateStatementParser : StatementParser
    {
        public const string ActivateKeyword = "activate";

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token keyword = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token argument = scanner.ReadToEnd();
            yield return
                argument.Length == 0
                    ? (Statement)new MissingArgumentStatement(keyword, argument)
                    : (Statement)new ActivateStatement(keyword, argument);
        }
    }
}
