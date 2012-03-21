using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class Parser
    {
        private readonly Scanner m_Scanner;
        private readonly StatementParserFactory m_StatementParserFactory;

        public Parser(Scanner scanner, StatementParserFactory statementParserFactory)
        {
            m_Scanner = scanner;
            m_StatementParserFactory = statementParserFactory;
        }

        public IEnumerable<StatementParser> Parsers()
        {
            while (m_Scanner.MoveNext())
            {
                string keyWordCandidate = m_Scanner.GetKeyWord();
                StatementParser statementParser = m_StatementParserFactory.GetStatementParser(keyWordCandidate);
                yield return statementParser;
            }
        }

        public IEnumerable<Statement> Parse()
        {
            return Parsers().SelectMany(statementParser => statementParser.Parse(m_Scanner));
        }
    }
}
