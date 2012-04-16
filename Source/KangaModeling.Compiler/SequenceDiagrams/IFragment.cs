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
        IEnumerable<IActivity> Activities { get; }
        IEnumerable<ISignal> Signals  { get; }
    }
}