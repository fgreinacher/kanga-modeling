using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.ClassDiagrams
{
    class ClassDiagramTokenStream : GenericTokenStream
    {
        public new ClassDiagramToken this[int i]
        {
            get { return (ClassDiagramToken) base[i]; }
        }

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

            RemoveRange(0, tokenTypes.Length);

            return true;
        }

        // make resharper happy by making a more specialized version for single-token consumes.
        public bool TryConsume(TokenType tokenType)
        {
            if(Count == 0 || this[0].TokenType != tokenType)
                return false;

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
                RemoveAt(0);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Consume token of a specific type.
        /// Does nothing if token type does not match.
        /// </summary>
        /// <param name="tokens">The consumed tokens (if types match)</param>
        /// <param name="tokenTypes">Token types to consume</param>
        /// <returns><c>true</c> if tokens were consumed, otherwise <c>false</c> (wrong type, too few tokens in stream)</returns>
        public bool TryConsume(out List<GenericToken> tokens, params TokenType[] tokenTypes)
        {
            tokens = null;
            if (Count < tokenTypes.Length)
                return false;

            tokens = new List<GenericToken>(2);

            for (int i = 0; i < tokenTypes.Length; i++)
            {
                if (this[i].TokenType != tokenTypes[i])
                {
                    tokens = null;
                    return false;
                }
                tokens.Add(this[i]);
            }

            return true;
        }

    }
}