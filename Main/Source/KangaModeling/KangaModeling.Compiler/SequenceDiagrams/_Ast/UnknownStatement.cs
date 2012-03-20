using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
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