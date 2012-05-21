using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    public class Multiplicity
    {
        public enum Kind
        {
            None,
            SingleNumber,
            Star
        }

        public Multiplicity(Kind fromKind, string from, Kind toKind, string to)
        {
            FromKind = fromKind;
            From = from;
            ToKind = toKind;
            To = to;
        }

        public readonly Kind FromKind;
        public readonly string From;
        public readonly Kind ToKind;
        public readonly string To;
    }

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
        string SourceRole { get; }
        Multiplicity SourceMultiplicity { get; }
        
        string TargetRole { get; }
        Multiplicity TargetMultiplicity { get; }

        AssociationKind Kind { get; }
        IClass Source { get; }
        IClass Target { get; }
    }

}
