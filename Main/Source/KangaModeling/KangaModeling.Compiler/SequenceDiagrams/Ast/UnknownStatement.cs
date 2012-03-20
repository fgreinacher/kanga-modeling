using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class UnknownStatement : Statement
    {
        private readonly Token m_InvalidToken;

        public UnknownStatement(Token invalidToken)
        {
            m_InvalidToken = invalidToken;
        }

        public override void Build(AstBuilder builder)
        {
            builder.AddError(m_InvalidToken, "Unrecognized statement.");
        }
    }
}