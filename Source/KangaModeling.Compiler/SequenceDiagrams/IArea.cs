using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface IArea
    {
        int Left { get; }
        int Right { get; }
        int Top { get; }
        int Bottom { get; }
        IEnumerable<IArea> Children { get; }
        bool HasFrame { get; }
    }
}