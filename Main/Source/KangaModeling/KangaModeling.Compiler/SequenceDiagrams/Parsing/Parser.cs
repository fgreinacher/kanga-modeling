using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
{
    internal class Parser
    {
        private readonly Scanner m_Scanner;
        private readonly StatementFactory m_StatementFactory;

        public Parser(Scanner scanner, StatementFactory statementFactory)
        {
            m_Scanner = scanner;
            m_StatementFactory = statementFactory;
        }

        public IEnumerable<StatementParser> Parse()
        {
            while (m_Scanner.MoveNext())
            {
                string keyWordCandidate = m_Scanner.GetKeyWord();
                StatementParser statementParser = m_StatementFactory.GetStatementParser(keyWordCandidate);
                statementParser.Parse(m_Scanner);
                yield return statementParser;
            }
        }
    }
}
