using System;
using KangaModeling.Compiler.ClassDiagrams;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    /// <summary>
    /// Helper methods for testing the parser w/o the scanner.
    /// </summary>
    static class TokenStreamBuilder
    {
        public static CDToken Token(this string value)
        {
            var tt = TokenType.Identifier;
            int dummy;
            if (int.TryParse(value, out dummy)) tt = TokenType.Number;
            if (value.Equals("*")) tt = TokenType.Star;
            if (value.Equals("..")) tt = TokenType.DotDot;
            if (value.Equals(":")) tt = TokenType.Colon;
            if (value.Equals("+")) tt = TokenType.Plus;
            if (value.Equals("-")) tt = TokenType.Dash;
            if (value.Equals("#")) tt = TokenType.Hash;
            if (value.Equals("~")) tt = TokenType.Tilde;

            return new CDToken(0, value.Length, tt, value);
        }

        public static CDToken Token(this TokenType tokenType)
        {
            // constant 10 is arbitrary and just to make the CDToken happy.
            return new CDToken(0, 10, tokenType);
        }

        public static TokenStream Class(string className, TokenStream fields = null, TokenStream methods = null)
        {
            var f = CombineTokenStreams(new TokenStream { TokenType.Pipe.Token() }, fields);
            var m = CombineTokenStreams(new TokenStream { TokenType.Pipe.Token() }, methods);

            var ts = CombineTokenStreams(
                new TokenStream { TokenType.BracketOpen.Token(), className.Token() },
                fields == null ? (methods != null? new TokenStream { TokenType.Pipe.Token() } : null) : f,
                methods == null ? null : m,
                new TokenStream { TokenType.BracketClose.Token(), }
            );
            return ts;
        }

        public static TokenStream Field(string name, string type = null, string accessModifier = null)
        {
            var stream = new TokenStream { name.Token() };
            if (type != null)
                stream = CombineTokenStreams(stream, new TokenStream { TokenType.Colon.Token(), type.Token() });
            if(accessModifier != null)
                stream = CombineTokenStreams(new TokenStream { accessModifier.Token()}, stream);
            return stream;
        }

        public static TokenStream Association(string sourceFrom, string sourceTo, string association, string targetFrom, string targetTo)
        {
            var tokens = new TokenStream();

            tokens.AddRange(new[] { sourceFrom.Token()});
            if (sourceTo != null)
                tokens.AddRange(new[] { "..".Token(), sourceTo.Token() });

            tokens.AddRange(PureAssociation(association));

            tokens.AddRange(new[] { targetFrom.Token()});
            if (targetTo != null)
                tokens.AddRange(new[] { "..".Token(), targetTo.Token() });

            return tokens;
        }

        public static TokenStream PureAssociation(string association)
        {
            switch(association)
            {
                case "-":
                    return new TokenStream {TokenType.Dash.Token()};
            }

            throw new ArgumentException("unexpected association: " + association);
        }

        public static TokenStream Method(string name, string visibilityModifier = "+", TokenStream parameterStream = null)
        {
            var stream = CombineTokenStreams(
                new TokenStream {visibilityModifier.Token(), name.Token(), TokenType.ParenthesisOpen.Token()},
                parameterStream,
                new TokenStream {TokenType.ParenthesisClose.Token()}
            );
            return stream;
        }

        public static TokenStream CombineTokenStreams(params TokenStream[] streams)
        {
            var combinedStream = new TokenStream();
            foreach (var singleStream in streams) if(singleStream != null) combinedStream.AddRange(singleStream);
            return combinedStream;
        }

    }
}