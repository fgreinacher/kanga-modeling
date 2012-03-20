using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal abstract class Statement
    {
        protected Token Keyword { get; private set; }

        protected Statement()
        {
            
        }

        protected Statement(Token keyword)
        {
            Keyword = keyword;
        }

        public abstract void Build(AstBuilder builder);
        public virtual IEnumerable<Token> Tokens()
        {
            yield return Keyword;
        }
    }
}