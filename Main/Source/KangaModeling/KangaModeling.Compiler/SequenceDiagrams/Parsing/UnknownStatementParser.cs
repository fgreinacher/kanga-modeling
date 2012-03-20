using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
{
    internal class UnknownStatementParser : StatementParser
    {
        private Token m_InvalidToken;

        public override Statement Parse(Scanner scanner)
        {
            m_InvalidToken = scanner.ReadToEnd();
            return new UnknownStatement(m_InvalidToken);
        }
    }
}