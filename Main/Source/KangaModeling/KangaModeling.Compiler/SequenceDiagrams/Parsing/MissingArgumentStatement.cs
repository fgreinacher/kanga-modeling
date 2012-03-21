using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class MissingArgumentStatement : Statement
    {
        private readonly Token m_Source;

        public MissingArgumentStatement(Token source)
        {
            m_Source = source;
        }

        public override void Build(AstBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}