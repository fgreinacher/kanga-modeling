using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class EndStatement : Statement
    {
        public EndStatement(Token keyword) 
            : base(keyword)
        {
        }

        public override void Build(ModelBuilder builder)
        {
            builder.End();
        }
    }
}