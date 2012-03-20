namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal abstract class Statement
    {
        public abstract void Build(AstBuilder builder);
    }
}