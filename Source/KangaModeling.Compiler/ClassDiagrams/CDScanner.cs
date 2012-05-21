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
    class TokenStream : List<CDToken>
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
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Bracket_Open));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith("..")) // TODO "..." ?
                {
                    tokens.Add(new CDToken(_lineIndex, _charIndex+=2, CDTokenType.DotDot));
                    source = source.Remove(0, 2);
                }
                else if (source[0] == '*')
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Star));
                    source = source.Remove(0, 1);
                }
                else if (source[0] == ':')
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Colon));
                    source = source.Remove(0, 1);
                }
                else if (source[0] == '|')
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Pipe));
                    source = source.Remove(0, 1);
                }
                else if (source[0] == ',')
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Comma));
                    source = source.Remove(0, 1);
                }
                else if (source[0] == ']')
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Bracket_Close));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith("-"))
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Dash));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith("<"))
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Angle_Open));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith(">"))
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Angle_Close));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith("+"))
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Plus));
                    source = source.Remove(0, 1);
                }
                else if (source.StartsWith("#"))
                {
                    tokens.Add(new CDToken(_lineIndex, ++_charIndex, CDTokenType.Hash));
                    source = source.Remove(0, 1);
                }
                else
                {
                    // maybe a number?
                    // note: "0adsf" must be invalid! (\b matches word boundary)
                    var match = Regex.Match(source, @"^([0-9]+)\b"); // TODO "00"
                    if (match.Captures.Count > 0)
                    {
                        // found a number
                        var numberString = match.Captures[0].Value;
                        _charIndex += numberString.Length;
                        source = source.Remove(0, numberString.Length);
                        tokens.Add(new CDToken(_lineIndex, _charIndex, CDTokenType.Number, numberString));
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
                            int i = source.IndexOfAny(new char[] { ' ', '\n', '\t' }); // TODO win/lin?
                            if (i >= 0)
                            {
                                source = source.Remove(0, i);
                                _charIndex += i;
                            }
                            else
                            {
                                // no newline. stop.
                                tokens.Add(new CDToken(_lineIndex, _charIndex + source.Length, CDTokenType.Unknown, source));
                                break;
                            }
                        }
                        else
                        {
                            var id = match.Captures[0].Value;
                            _charIndex += id.Length;
                            source = source.Remove(0, id.Length);
                            tokens.Add(new CDToken(_lineIndex, _charIndex, CDTokenType.Identifier, id));
                        }
                    }
                }

                TrimStart(ref source);
            }


            return tokens;
        }

        private void TrimStart(ref string source)
        {
            var match = Regex.Match(source, @"^(\s+)");
            if (match.Captures.Count > 0)
            {
                var wsString = match.Captures[0].Value;

                // correct line count
                var nlCount = wsString.Where(c => c == '\n').Count(); // TODO win/lin correct? Env.NL is a String...
                _lineIndex += nlCount;
                if (nlCount > 0)
                    _charIndex = 0;

                // correct char count in one line
                int lioNewLine = wsString.LastIndexOf(Environment.NewLine);
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

            //var lenOrig = source.Length;
            //source = source.TrimStart();
            //var trimmedCount = lenOrig - source.Length;
            //_charIndex += trimmedCount;
        }

        private int _charIndex;
        private int _lineIndex;

    }
}
