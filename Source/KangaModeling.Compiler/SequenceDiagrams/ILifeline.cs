using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ILifeline
    {
        string Name { get; }
        string Id { get; }
        int Index { get; }
        IEnumerable<IPin> Pins { get; }
        int EndRowIndex { get; }
    }
}