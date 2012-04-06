namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    public interface ISignal
    {
        IPin End { get; }
        string Name { get; set; }
        IPin Start { get; }
        SignalType SignalType { get; }
    }
}