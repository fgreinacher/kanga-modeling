using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public static class DiagramCreator
    {
        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        internal static SequenceDiagram CreateFrom(string text)
        {
            var sequenceDiagram = new SequenceDiagram();
            var astBuilder = new ModelBuilder(sequenceDiagram);

            using (var scanner = new Scanner(text))
            {
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
            }

            return sequenceDiagram;
        }
    }
}
