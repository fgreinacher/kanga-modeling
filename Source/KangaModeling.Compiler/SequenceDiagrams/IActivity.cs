using System.Diagnostics.CodeAnalysis;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface IActivity
    {
        IPin Start { get; }
        int StartRowIndex { get; }

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End")]
        IPin End { get; }

        int EndRowIndex { get; }

        ILifeline Lifeline { get; }
        
        Orientation Orientation { get; }
        int Level { get; }
    }
}