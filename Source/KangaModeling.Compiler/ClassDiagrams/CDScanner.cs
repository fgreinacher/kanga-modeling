using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// A scanner for class diagrams.
    /// </summary>
    class CDScanner : Scanner
    {
        private readonly HashSet<string> _keywords;
        private readonly List<ScannerRule> _rules;

        public CDScanner()
        {
            _keywords = new HashSet<string>();
            _keywords.AddRange(new[] { "[", "..", "*", ":", "|", "(", ")", ",", "[", "]", "<", ">", "-", "~", "+", "#" });
            _rules = new List<ScannerRule> {ScannerRules.MatchNumbers, ScannerRules.MatchIdentifier};
        }

        protected override ISet<string> Keywords
        {
            get { return _keywords; }
        }

        protected override ICollection<ScannerRule> Rules
        {
            get { return _rules; }
        }
    
    }

    /// <summary>
    /// Scans user input into a TokenStream.
    /// </summary>
    abstract class Scanner
    {
        private const int LongestKeywordNotComputed = -1;

        private int _longestKeyword = LongestKeywordNotComputed;
        private readonly ScannerState _scannerState;

        protected Scanner()
        {
            _scannerState = new ScannerState();
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
                for (var keywordLength = 1;
                     keywordLength <= LongestKeyword && source.Length >= keywordLength && !keywordFound;
                     keywordLength++)
                {
                    var sourceSubstring = source.Substring(0, keywordLength);
                    if (Keywords.Contains(sourceSubstring))
                    {
                        var tokenType = source.Substring(0, keywordLength).FromDisplayString();
                        tokens.Add(new CDToken(_scannerState.LineIndex, _scannerState.AdvanceCharIndex(keywordLength),
                                               tokenType));
                        source = source.Remove(0, keywordLength);
                        keywordFound = true;
                    }
                }

                // no keyword found? => check additional rules.
                if (!keywordFound)
                {
                    bool ruleApplied = false;
                    foreach (var rule in Rules)
                    {
                        var matchInfo = rule(ref source, _scannerState);
                        if (matchInfo.IsSuccess)
                        {
                            tokens.Add(matchInfo.Token);
                            ruleApplied = true;
                            break;
                        }
                    }

                    if (!ruleApplied)
                    {
                        // no match -> error. 
                        // Remove all non-ws characters up to the first ws and continue scanning.
                        // when there is no newline, create one token with anything and stop.

                        // TODO cannot flag a token "invalid"
                        int i = source.IndexOfAny(new[] { ' ', '\n', '\t' }); // TODO win/lin?
                        if (i >= 0)
                        {
                            source = source.Remove(0, i);
                            _scannerState.AdvanceCharIndex(i);
                        }
                        else
                        {
                            // no whitespace. stop.
                            tokens.Add(new CDToken(_scannerState.LineIndex, _scannerState.CharIndex + source.Length,
                                                   TokenType.Unknown,
                                                   source));
                            break;
                        }
                    }
                }

                TrimStart(ref source);
            }

            return tokens;
        }

        #region types used for scanner configuration

        // does not work; see http://stackoverflow.com/questions/2462814/func-delegate-with-ref-variable
        // private readonly List<Func<ref string, ScannerState, RuleMatchInfo>> _rules; 
        protected delegate RuleMatchInfo ScannerRule(ref string source, ScannerState scannerState);

        protected struct RuleMatchInfo
        {
#pragma warning disable 649
            public static readonly RuleMatchInfo Fail;
#pragma warning restore 649

            public RuleMatchInfo(CDToken token)
            {
                Token = token;
                IsSuccess = true;
            }

            public readonly bool IsSuccess;
            public readonly CDToken Token;
        }

        #endregion

        #region pre-defined scanner rules

        protected static class ScannerRules
        {

            public static RuleMatchInfo MatchIdentifier(ref string source, ScannerState scannerState)
            {
                var match = Regex.Match(source, "^([A-Za-z][A-Za-z0-9]*)");
                if (match.Captures.Count == 1)
                {
                    var id = match.Captures[0].Value;
                    source = source.Remove(0, id.Length);
                    scannerState.AdvanceCharIndex(id.Length);

                    return new RuleMatchInfo(
                        new CDToken(scannerState.LineIndex, scannerState.CharIndex, TokenType.Identifier, id));
                }

                return RuleMatchInfo.Fail;
            }

            // note: "0adsf" must be invalid! (\b matches word boundary)
            public static RuleMatchInfo MatchNumbers(ref string source, ScannerState scannerState)
            {
                var match = Regex.Match(source, @"^([0-9]+)\b");
                if (match.Captures.Count > 0)
                {
                    // found a number
                    var numberString = match.Captures[0].Value;
                    source = source.Remove(0, numberString.Length);
                    scannerState.AdvanceCharIndex(numberString.Length);

                    return new RuleMatchInfo(
                        new CDToken(scannerState.LineIndex, scannerState.CharIndex, TokenType.Number, numberString));
                }

                return RuleMatchInfo.Fail;
            }

        }

        #endregion

        #region scanner state

        [DebuggerDisplay("ScannerState: CharIndex={CharIndex}, LineIndex={LineIndex}")]
        protected sealed class ScannerState
        {
            public int AdvanceCharIndex(int offset)
            {
                return CharIndex += offset;
            }

            public void AdvanceLineIndex(int offset)
            {
                LineIndex += offset;
            }

            public int CharIndex;
            public int LineIndex;
        }

        #endregion

        #region protected template properties

        protected abstract ISet<string> Keywords { get; }
        protected abstract ICollection<ScannerRule> Rules { get; }

        #endregion

        #region private utility functions

        private int LongestKeyword
        {
            get
            {
                if (_longestKeyword == LongestKeywordNotComputed)
                    _longestKeyword = Keywords.Select(keyword => keyword.Length).Max();
                return _longestKeyword;
            }
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
                _scannerState.AdvanceLineIndex(nlCount);
                if (nlCount > 0)
                    _scannerState.CharIndex = 0;

                // correct char count in one line
                int lioNewLine = wsString.LastIndexOf(Environment.NewLine, StringComparison.Ordinal);
                if (lioNewLine >= 0)
                {
                    var lastWS = wsString.Substring(lioNewLine + Environment.NewLine.Length);
                    var count = lastWS.Length;
                    _scannerState.AdvanceCharIndex(count);
                }
                else
                {
                    _scannerState.AdvanceCharIndex(wsString.Length);
                }

                source = source.Substring(wsString.Length);
            }
        }

        #endregion

    }
}
