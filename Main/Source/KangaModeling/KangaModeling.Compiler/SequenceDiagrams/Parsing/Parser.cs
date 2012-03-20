using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
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

        public IEnumerable<StatementParser> Parse()
        {
            while (m_Scanner.MoveNext())
            {
                string keyWordCandidate = m_Scanner.GetKeyWord();
                StatementParser statementParser = m_StatementParserFactory.GetStatementParser(keyWordCandidate);
                statementParser.Parse(m_Scanner);
                yield return statementParser;
            }
        }
    }
}
