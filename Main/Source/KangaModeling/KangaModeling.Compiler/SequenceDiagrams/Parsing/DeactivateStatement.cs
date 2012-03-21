namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class DeactivateStatement : StateStatement
    {
        public DeactivateStatement(Token keyword, Token argument) 
            : base(keyword, argument)
        {
        }
    }
}