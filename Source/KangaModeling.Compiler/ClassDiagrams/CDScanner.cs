using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KangaModeling.Compiler.ClassDiagrams
{

    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> @this, params T[] items)
        {
            if (@this == null) throw new NullReferenceException("@this");
            items.ForEach(item => @this.Add(item));
        }
    }

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
                action(item);
        }
    }

    /// <summary>
    /// Scans user input into a TokenStream.
    /// </summary>
    class CDScanner
    {
        private readonly HashSet<String> _keywords;
        private readonly int _longestKeyword;

        public CDScanner()
        {
            _keywords = new HashSet<string>();
         
            // TODO "..." input?
            _keywords.AddRange(new[] {"[", "..", "*", ":", "|", "(", ")", ",", "[", "]", "<", ">", "-", "~", "+", "#"});
            _longestKeyword = _keywords.Select(kw => kw.Length).Max();
        }

        public TokenStream Parse(string source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var tokens = new TokenStream();

            TrimStart(ref source);
            while (source.Length >= 1)
            {
                // check for keywords.
                var keywordFound = false;
                for (var keywordLength = 1; keywordLength <= _longestKeyword && source.Length >= keywordLength; keywordLength++ )
                {
                    var sourceSubstring = source.Substring(0, keywordLength);
                    if(_keywords.Contains(sourceSubstring))
                    {
                        var tokenType = source.Substring(0, keywordLength).FromDisplayString();
                        tokens.Add(new CDToken(_lineIndex, _charIndex += keywordLength, tokenType));
                        source = source.Remove(0, keywordLength);
                        keywordFound = true;
                    }
                }

                // no keyword found? => check additional rules.
                if(!keywordFound)
                {
                    // maybe a number?
                    // note: "0adsf" must be invalid! (\b matches word boundary)
                    var match = Regex.Match(source, @"^([0-9]+)\b");
                    if (match.Captures.Count > 0)
                    {
                        // found a number
                        var numberString = match.Captures[0].Value;
                        _charIndex += numberString.Length;
                        source = source.Remove(0, numberString.Length);
                        tokens.Add(new CDToken(_lineIndex, _charIndex, TokenType.Number, numberString));
                    }
                    else
                    {
                        // catch-all: identifier
                        // TODO unknown token? (start with number?)
                        match = Regex.Match(source, "^([A-Za-z][A-Za-z0-9]*)");
                        if (match.Captures.Count == 0)
                        {
                            // no match -> error. 
                            // Remove all non-ws characters up to the first ws and continue scanning.
                            // when there is no newline, create one token with anything and stop.

                            // TODO cannot flag a token "invalid"
                            int i = source.IndexOfAny(new[] { ' ', '\n', '\t' }); // TODO win/lin?
                            if (i >= 0)
                            {
                                source = source.Remove(0, i);
                                _charIndex += i;
                            }
                            else
                            {
                                // no whitespace. stop.
                                tokens.Add(new CDToken(_lineIndex, _charIndex + source.Length, TokenType.Unknown, source));
                                break;
                            }
                        }
                        else
                        {
                            var id = match.Captures[0].Value;
                            _charIndex += id.Length;
                            source = source.Remove(0, id.Length);
                            tokens.Add(new CDToken(_lineIndex, _charIndex, TokenType.Identifier, id));
                        }
                    }
                }

                TrimStart(ref source);
            }

            return tokens;
        }

        /// <summary>
        /// Trims the start of the given string, but makes sure the _charIndex and _lineIndex
        /// members are adapted suitably.
        /// </summary>
        /// <param name="source">String to trim. Must not be null.</param>
        private void TrimStart(ref string source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var match = Regex.Match(source, @"^(\s+)");
            if (match.Captures.Count > 0)
            {
                var wsString = match.Captures[0].Value;

                // correct line count
                var nlCount = wsString.Count(c => c == '\n'); // TODO win/lin correct? Env.NL is a String...
                _lineIndex += nlCount;
                if (nlCount > 0)
                    _charIndex = 0;

                // correct char count in one line
                int lioNewLine = wsString.LastIndexOf(Environment.NewLine, StringComparison.Ordinal);
                if (lioNewLine >= 0)
                {
                    var lastWS = wsString.Substring(lioNewLine + Environment.NewLine.Length);
                    var count = lastWS.Length;
                    _charIndex += count;
                }
                else
                {
                    _charIndex += wsString.Length;
                }

                source = source.Substring(wsString.Length);
            }
        }

        private int _charIndex;
        private int _lineIndex;

    }
}
