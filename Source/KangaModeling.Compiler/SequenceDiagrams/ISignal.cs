namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ISignal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End")]
        IPin End { get; }
        string Name { get; set; }
        IPin Start { get; }
        SignalType SignalType { get; }
        int RowIndex { get; }
    }
}