using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    public interface ILifeline
    {
        string Name { get; }
        string Id { get; }
        int Index { get; }
        IEnumerable<IPin> Pins { get;}
    }
}