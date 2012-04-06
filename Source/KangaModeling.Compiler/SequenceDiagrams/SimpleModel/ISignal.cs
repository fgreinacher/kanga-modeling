namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal interface ISignal
    {
        IPin End { get; }
        string Name { get; set; }
        IPin Start { get; }
        bool IsReturn { get; }
    }
}