using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ISequenceDiagram
    {
        ICombinedFragment Root { get; }
        IEnumerable<ILifeline> Lifelines { get; }
        int RowCount { get; }
        int LifelineCount { get; }
    }
}