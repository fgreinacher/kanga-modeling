using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StateStatement : Statement
    {
        public Token Target
        {
            get { return Arguments[0]; }
        }

        protected StateStatement(Token keyword, Token target)
            : base(keyword, target)
        {
           
        }
    }
}