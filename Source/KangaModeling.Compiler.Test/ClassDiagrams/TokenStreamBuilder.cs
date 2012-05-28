using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.Toolbox;
using KangaModeling.Compiler.ClassDiagrams;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    /// <summary>
    /// Helper methods for testing the parser w/o the scanner.
    /// </summary>
    static class TokenStreamBuilder
    {
        public static ClassDiagramToken Token(this string value)
        {
            return new ClassDiagramToken(0, value.Length, value.FromDisplayString(), value);
        }

        public static ClassDiagramToken Token(this TokenType tokenType)
        {
            // constant 10 is arbitrary and just to make the CDToken happy.
            return new ClassDiagramToken(0, 10, tokenType);
        }

        public static ClassDiagramTokenStream Class(string className, ClassDiagramTokenStream fields = null, ClassDiagramTokenStream methods = null)
        {
            var f = CombineTokenStreams(new ClassDiagramTokenStream { TokenType.Pipe.Token() }, fields);
            var m = CombineTokenStreams(new ClassDiagramTokenStream { TokenType.Pipe.Token() }, methods);

            var ts = CombineTokenStreams(
                new ClassDiagramTokenStream { TokenType.BracketOpen.Token(), className.Token() },
                fields == null ? (methods != null ? new ClassDiagramTokenStream { TokenType.Pipe.Token() } : null) : f,
                methods == null ? null : m,
                new ClassDiagramTokenStream { TokenType.BracketClose.Token(), }
            );
            return ts;
        }

        public static ClassDiagramTokenStream Field(string name, string type = null, string accessModifier = null)
        {
            var stream = new ClassDiagramTokenStream { name.Token() };
            if (type != null)
                stream = CombineTokenStreams(stream, new ClassDiagramTokenStream { TokenType.Colon.Token(), type.Token() });
            if(accessModifier != null)
                stream = CombineTokenStreams(new ClassDiagramTokenStream { accessModifier.Token() }, stream);
            return stream;
        }

        public static ClassDiagramTokenStream Association(string sourceFrom, string sourceTo, string association, string targetFrom, string targetTo)
        {
            var tokens = new ClassDiagramTokenStream();

            tokens.AddRange(new[] { sourceFrom.Token()});
            if (sourceTo != null)
                tokens.AddRange(new[] { "..".Token(), sourceTo.Token() });

            tokens.AddRange(PureAssociation(association));

            tokens.AddRange(new[] { targetFrom.Token()});
            if (targetTo != null)
                tokens.AddRange(new[] { "..".Token(), targetTo.Token() });

            return tokens;
        }

        public static ClassDiagramTokenStream PureAssociation(string association)
        {
            switch(association)
            {
                case "-":
                    return new ClassDiagramTokenStream { TokenType.Dash.Token() };
            }

            throw new ArgumentException("unexpected association: " + association);
        }

        public static ClassDiagramTokenStream Method(string name, string visibilityModifier = "+", ClassDiagramTokenStream parameterStream = null)
        {
            var stream = CombineTokenStreams(
                new ClassDiagramTokenStream { visibilityModifier.Token(), name.Token(), TokenType.ParenthesisOpen.Token() },
                parameterStream,
                new ClassDiagramTokenStream { TokenType.ParenthesisClose.Token() }
            );
            return stream;
        }

        public static ClassDiagramTokenStream CombineTokenStreams(params ClassDiagramTokenStream[] streams)
        {
            var combinedStream = new ClassDiagramTokenStream();
            foreach (var singleStream in streams) if(singleStream != null) combinedStream.AddRange(singleStream);
            return combinedStream;
        }

        public static ClassDiagramTokenStream FromStrings(params string[] tokens)
        {
            var s = new ClassDiagramTokenStream();
            tokens.Where(s1 => s1.Length > 0).ForEach(t => s.Add(t.Token()));
            return s;
        }
    }
}