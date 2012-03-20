namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StatementParser
    {
        public abstract Statement Parse(Scanner scanner);
    }
}