using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ReturnSignalStatement : SignalStatement
    {
        public ReturnSignalStatement(Token keyword, Token source, Token target, Token name)
            : base(keyword, source, target, name)
        {
        }

        public override void Build(IModelBuilder builder)
        {
            builder.AddReturnSignal(Source, Target, Name);
        }
    }
}
