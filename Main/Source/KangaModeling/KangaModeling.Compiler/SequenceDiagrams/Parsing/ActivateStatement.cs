namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ActivateStatement : StateStatement
    {
        public ActivateStatement(Token keyword, Token argument)
            : base(keyword, argument)
        {
        }
    }
}