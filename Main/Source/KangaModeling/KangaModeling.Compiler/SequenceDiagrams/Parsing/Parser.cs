using System.Collections.Generic;
using System.Linq;
using System;

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
                m_Scanner.SkipWhiteSpaces();
                if (m_Scanner.Eol) {continue;}
                string keyWordCandidate = m_Scanner.GetKeyWord();
                StatementParser statementParser = m_StatementParserFactory.GetStatementParser(keyWordCandidate);
                yield return statementParser;
            }
        }

        public IEnumerable<Statement> Parse()
        {
            return Parsers().SelectMany(statementParser => statementParser.Parse(m_Scanner));
        }

        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static SequenceDiagram ParseString(string text)
        {
            var sequenceDiagram = new SequenceDiagram();
            if (String.IsNullOrEmpty(text))
                return sequenceDiagram;

            var astBuilder = new ModelBuilder(sequenceDiagram);

            var scanner = new Scanner(text);
            var parser = new Parser(scanner, new StatementParserFactory());

            foreach (var statement in parser.Parse())
            {
                try
                {
                    statement.Build(astBuilder);
                }
                catch (NotImplementedException)
                {

                }
            }

            return sequenceDiagram;
        }
    }
}
