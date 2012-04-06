namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal interface IActivity
    {
        IPin Start { get; }
        IPin End { get; }
        Orientation Orientation { get; }
        int Level { get; }
    }
}