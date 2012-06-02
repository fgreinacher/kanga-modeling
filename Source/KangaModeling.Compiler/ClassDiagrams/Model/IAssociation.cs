namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// The kind of an association.
    /// </summary>
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
    public interface IAssociation
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
