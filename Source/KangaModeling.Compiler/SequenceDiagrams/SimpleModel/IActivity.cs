namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    public interface IActivity
    {
        IPin Start { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End")]
        IPin End { get; }
        Orientation Orientation { get; }
        int Level { get; }
    }
}