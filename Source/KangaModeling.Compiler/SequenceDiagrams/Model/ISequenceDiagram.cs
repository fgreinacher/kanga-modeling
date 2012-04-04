using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ISequenceDiagram
    {
        string Title { get; }

        IEnumerable<IParticipant> Participants { get; }

		RootCombinedFragment Content { get; }
    }
}