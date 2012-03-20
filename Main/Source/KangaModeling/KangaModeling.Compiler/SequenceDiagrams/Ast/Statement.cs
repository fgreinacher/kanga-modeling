using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal abstract class Statement
    {
        public abstract void Build(AstBuilder builder);
        public abstract IEnumerable<Token> Tokens();
    }
}