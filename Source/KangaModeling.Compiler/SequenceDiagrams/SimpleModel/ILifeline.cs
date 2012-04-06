namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal interface ILifeline
    {
        string Name { get; }
        string Id { get; }
        int Index { get; }
    }
}