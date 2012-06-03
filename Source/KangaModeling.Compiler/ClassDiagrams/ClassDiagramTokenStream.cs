using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams.Errors;

namespace KangaModeling.Compiler.ClassDiagrams
{
    class ClassDiagramTokenStream : GenericTokenStream
    {
        public new ClassDiagramToken this[int i]
        {
            get { return (ClassDiagramToken) base[i]; }
        }

        public TextRegion CurrentLocation { get; private set; }

        /// <summary>
        /// Consume tokens of a specific type.
        /// If a token type does not match, nothing is consumed.
        /// </summary>
        /// <param name="tokenTypes">Token types to consume.</param>
        /// <returns><c>true</c> if tokens were consumed, otherwise <c>false</c> (wrong type, too few tokens in stream)</returns>
        public bool TryConsume(params TokenType[] tokenTypes)
        {
            if (Count < tokenTypes.Length) return false;
            if (tokenTypes.Where((t, i) => this[i].TokenType != t).Any())
                return false;

            SetCurrentLocation(this[tokenTypes.Length - 1]);
            RemoveRange(0, tokenTypes.Length);

            return true;
        }

        // make resharper happy by making a more specialized version for single-token consumes.
        public bool TryConsume(TokenType tokenType)
        {
            if(Count == 0 || this[0].TokenType != tokenType)
                return false;

            SetCurrentLocation(this[0]);
            RemoveRange(0, 1);

            return true;
        }

        /// <summary>
        /// Consume token of a specific type.
        /// Does nothing if token type does not match.
        /// </summary>
        /// <param name="type">Token type to consume</param>
        /// <param name="token">The consumed token (if types match)</param>
        /// <returns><c>true</c> if tokens were consumed, otherwise <c>false</c> (wrong type, too few tokens in stream)</returns>
        public bool TryConsume(TokenType type, out ClassDiagramToken token)
        {
            token = null;

            if (Count == 0) return false;
            token = this[0];

            if (token.TokenType == type)
            {
                SetCurrentLocation(this[0]);
                RemoveAt(0);
                return true;
            }

            return false;
        }

        private void SetCurrentLocation(GenericToken token)
        {
            CurrentLocation = new TextRegion(token.Line, token.Start, token.Length);
        }

    }
}