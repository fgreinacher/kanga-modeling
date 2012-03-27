namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
    /// The interaction operator of a combined fragment.
    /// Think of this as the "type" of the combined fragment.
    /// </summary>
    public enum InteractionOperator {
        /// <summary>
        /// The root combined fragment type. 
        /// Only used for the invisible first combined fragment.
        /// </summary>
        Root,
        /// <summary>Alternative Combined Fragment</summary>
        Alternative,
        /// <summary>Loop Combined Fragment</summary>
        Loop,
        /// <summary>Parallel Combined Fragment</summary>
        Parallel,
    }
}