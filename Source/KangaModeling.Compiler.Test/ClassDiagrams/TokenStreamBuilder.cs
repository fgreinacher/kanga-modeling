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
            var tt = CDTokenType.Identifier;
            int dummy;
            if (int.TryParse(value, out dummy)) tt = CDTokenType.Number;
            if (value.Equals("*")) tt = CDTokenType.Star;
            if (value.Equals("..")) tt = CDTokenType.DotDot;
            if (value.Equals(":")) tt = CDTokenType.Colon;
            if (value.Equals("+")) tt = CDTokenType.Plus;
            if (value.Equals("-")) tt = CDTokenType.Dash;
            if (value.Equals("#")) tt = CDTokenType.Hash;
            if (value.Equals("~")) tt = CDTokenType.Tilde;

            return new CDToken(0, value.Length, tt, value);
        }

        public static CDToken Token(this CDTokenType tokenType)
        {
            // constant 10 is arbitrary and just to make the CDToken happy.
            return new CDToken(0, 10, tokenType);
        }

        public static TokenStream Class(string className, TokenStream fields = null)
        {
            var f = CombineTokenStreams(new TokenStream { CDTokenType.Pipe.Token() }, fields);

            var ts = CombineTokenStreams(
                new TokenStream { CDTokenType.BracketOpen.Token(), className.Token() },
                fields == null ? null : f,
                new TokenStream { CDTokenType.BracketClose.Token(), }
                );
            return ts;
        }

        public static TokenStream Field(string name, string type = null, string accessModifier = null)
        {
            var stream = new TokenStream { name.Token() };
            if (type != null)
                stream = CombineTokenStreams(stream, new TokenStream { CDTokenType.Colon.Token(), type.Token() });
            if(accessModifier != null)
                stream = CombineTokenStreams(new TokenStream { accessModifier.Token()}, stream);
            return stream;
        }

        public static TokenStream Association(string sourceFrom, string sourceTo, string targetFrom, string targetTo)
        {
            var tokens = new TokenStream();

            tokens.AddRange(Class("a"));

            tokens.AddRange(new[] { sourceFrom.Token()});
            if (sourceTo != null)
                tokens.AddRange(new[] { "..".Token(), sourceTo.Token() });

            tokens.AddRange(new[] { CDTokenType.Dash.Token()});

            tokens.AddRange(new[] { targetFrom.Token()});
            if (targetTo != null)
                tokens.AddRange(new[] { "..".Token(), targetTo.Token() });

            tokens.AddRange(Class("b"));

            return tokens;
        }

        public static TokenStream CombineTokenStreams(params TokenStream[] streams)
        {
            var combinedStream = new TokenStream();
            foreach (var singleStream in streams) if(singleStream != null) combinedStream.AddRange(singleStream);
            return combinedStream;
        }
    }
}