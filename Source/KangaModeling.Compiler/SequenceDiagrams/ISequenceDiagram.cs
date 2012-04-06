using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ISequenceDiagram
    {
        IFragment Root { get; }
        IEnumerable<ILifeline> Lifelines { get; }
    }
}