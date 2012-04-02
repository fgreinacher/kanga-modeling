using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class UnknownStatementParser : StatementParser
    {
        private Token m_InvalidToken;

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            m_InvalidToken = scanner.ReadToEnd();
            yield return new UnknownStatement(m_InvalidToken);
        }
    }
}