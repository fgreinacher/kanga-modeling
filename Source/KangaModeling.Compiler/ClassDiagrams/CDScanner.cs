using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Compiler.ClassDiagrams
{

    
    /// <summary>
    /// A stream of tokens; result of the scanner, input to the parser.
    /// </summary>
    /// TODO must not implement List;
    /// TODO must be lazy: cache some Tokens, call into scanner for more ( -> ANTLR )
    class TokenStream : List<Token>
    {
    }

    /// <summary>
    /// Scans user input into a TokenStream.
    /// </summary>
    class CDScanner
    {

        public TokenStream parse(string source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var tokens = new TokenStream();

            // TODO multi-line!
            TrimStart(ref source);

            while (source.Length > 0)
            {
                if (source[0] == '[')
                {
                    tokens.Add(new SequenceDiagrams.Token(0, ++_charIndex, "["));
                    source = source.Remove(0, 1);
                }
                else if (source[0] == ']')
                {
                    tokens.Add(new SequenceDiagrams.Token(0, ++_charIndex, "]"));
                    source = source.Remove(0, 1);
                }
                else
                {
                    // catch-all: identifier
                    // TODO unknown token? (start with number?)
                    var match = Regex.Match(source, "^([A-Za-z][A-Za-z0-9]*)");
                    if (match.Captures.Count == 0)
                    {
                        // no match -> error. Remove all non-ws characters up to the first ws.
                        // TODO cannot flag a token "invalid"
                        int i = source.IndexOf(" "); // TODO tab? newline?
                        source = source.Remove(0, i);
                        _charIndex += i;
                    }
                    else
                    {
                        var id = match.Captures[0].Value;
                        _charIndex += id.Length;
                        source = source.Remove(0, id.Length);
                        tokens.Add(new SequenceDiagrams.Token(0, _charIndex, id));
                    }
                }

                TrimStart(ref source);
            }


            return tokens;
        }

        private void TrimStart(ref string source)
        {
            var lenOrig = source.Length;
            source = source.TrimStart();
            var trimmedCount = lenOrig - source.Length;

            _charIndex += trimmedCount;
        }

        private int _charIndex;

    }
}
