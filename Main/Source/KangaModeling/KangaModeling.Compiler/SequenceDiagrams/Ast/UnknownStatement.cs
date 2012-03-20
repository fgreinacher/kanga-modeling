using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class UnknownStatement : Statement
    {
        public UnknownStatement(Token invalidToken) 
            : base(invalidToken)
        {
        }

        public override void Build(AstBuilder builder)
        {
            builder.AddError(Keyword, "Unrecognized statement.");
        }
    }
}