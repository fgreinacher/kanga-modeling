namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal interface IPin
    {
        ILifeline Lifeline { get; }
        PinType PinType { get; }
        ISignal Signal { get; }
        IActivity Activity { get; }
        Orientation Orientation { get; }
        int Level { get; }
        int LifelineIndex { get; }
        int RowIndex { get; }
    }
}