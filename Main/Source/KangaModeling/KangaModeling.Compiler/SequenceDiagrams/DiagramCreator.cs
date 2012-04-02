using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Model;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public static class DiagramCreator
    {
        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static ISequenceDiagram CreateFrom(string text)
        {
            IEnumerable<ModelError> errors;
            return CreateFrom(text, out errors);
        }

        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="errors"></param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static ISequenceDiagram CreateFrom(string text, out IEnumerable<ModelError> errors)
        {
            var elementFactory = new DiagramElementFactory();
            var sequenceDiagram = new SequenceDiagram();
            var astBuilder = new ModelBuilder(sequenceDiagram, elementFactory);

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
            errors = astBuilder.Errors;
            return sequenceDiagram;
        }
    }
}
