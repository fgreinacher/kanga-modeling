namespace KangaModeling.Compiler.SequenceDiagrams
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