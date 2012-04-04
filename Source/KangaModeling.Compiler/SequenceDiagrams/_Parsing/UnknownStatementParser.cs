using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Parsing
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