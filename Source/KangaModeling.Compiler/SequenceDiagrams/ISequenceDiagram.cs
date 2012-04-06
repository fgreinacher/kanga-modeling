using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ISequenceDiagram
    {
        IFragment Root { get; }
        IEnumerable<ILifeline> Lifelines { get; }
        IEnumerable<IActivity> Activities { get; }
        IEnumerable<ISignal> Signals { get; }
    }

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