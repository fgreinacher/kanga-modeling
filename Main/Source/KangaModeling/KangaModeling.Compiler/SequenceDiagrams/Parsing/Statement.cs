using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class Statement
    {
        private readonly Token[] m_Arguments;

        protected Token[] Arguments
        {
            get { return m_Arguments; }
        }

        protected Token Keyword { get; private set; }

        protected Statement()
        {
            
        }

        protected Statement(Token keyword, params Token[] arguments)
        {
            m_Arguments = arguments;
            Keyword = keyword;
        }

        public abstract void Build(ModelBuilder builder);
        public IEnumerable<Token> Tokens()
        {
            return Enumerable.Repeat(Keyword, 1).Concat(Arguments);
        }
    }
}