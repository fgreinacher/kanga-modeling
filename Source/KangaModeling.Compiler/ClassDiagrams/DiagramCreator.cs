using System;
using System.Collections.Generic;
using KangaModeling.Compiler.ClassDiagrams.Errors;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.ClassDiagrams
{

    public static class DiagramCreator
    {
        public class DiagramCreationResult
        {
            public DiagramCreationResult(IClassDiagram classDiagram, IEnumerable<Error> errors)
            {
                //if (classDiagram == null) throw new ArgumentNullException("classDiagram");
                if (errors == null) throw new ArgumentNullException("errors");
                ClassDiagram = classDiagram;
                Errors = errors;
            }

            public IClassDiagram ClassDiagram { get; private set; }
            public IEnumerable<Error> Errors { get; private set; }
        }

        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static DiagramCreationResult CreateFrom(string text)
        {
            var errors = new List<Error>(2);
            
            ClassDiagramParser.ErrorCallback errorCallback = (syntaxErrorType, expected, actual) => {
                                                                                                        errors.Add(Error.Create(syntaxErrorType, expected, actual));
                                                                                                        return ErrorReturnCode.StopParsing;
            };
            var cd = new ClassDiagramParser(new ClassDiagramScanner().Parse(text), errorCallback).ParseClassDiagram();

            return new DiagramCreationResult(cd, errors);
        }

    }

}