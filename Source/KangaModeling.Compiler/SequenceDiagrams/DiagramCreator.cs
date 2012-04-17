using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

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
            return CreateFrom(text, new ModelErrors());
        }

        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="errors"></param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static ISequenceDiagram CreateFrom(string text,  ModelErrors errors)
        {
            var root = new RootFragment();
            var matrix = new Matrix(root);
            var builder = new MatrixBuilder(matrix, errors);

            using (var scanner = new Scanner(text))
            {
                var parser = new Parser(scanner, new StatementParserFactory());
                foreach (Statement statement in parser.Parse())
                {
                    try
                    {
                        statement.Build(builder);
                    }
                    catch (NotImplementedException)
                    {
                    }
                }
                builder.Flush();
            }
            return matrix;
        }
    }
}