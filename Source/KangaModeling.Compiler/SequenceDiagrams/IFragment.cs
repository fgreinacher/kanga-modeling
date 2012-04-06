using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface IFragment
    {
        string Title { get; }
        IFragment Parent { get; }
        IEnumerable<IFragment> Children { get; }
        FragmentType FragmentType { get; }
        ILifeline Left { get; }
        ILifeline Right { get; }
        int Top { get; }
        int Bottom { get; }
    }
}