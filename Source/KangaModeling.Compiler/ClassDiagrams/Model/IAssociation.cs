using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    public enum AssociationKind
    {
        Undirected,
        Directed,
        Aggregation,
        Composition,
        // Realization
    }
    
    /// <summary>
    /// Associates two IClass instances.
    /// Also used for undirected associations, then of course, Source/Target does not make that much sense.
    /// </summary>
    interface IAssociation
    {
        AssociationKind Kind { get; }
        IClass Source { get; }
        IClass Target { get; }
    }

}
