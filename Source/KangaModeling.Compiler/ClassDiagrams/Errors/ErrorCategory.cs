namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    /// <summary>
    /// The "nature" of the error.
    /// </summary>
    public enum ErrorCategory
    {
        /// <summary> parse failure. </summary>
        Syntactical,
        /// <summary> parse OK, but does not make sense semantically. </summary>
        Semantical,
    }
}