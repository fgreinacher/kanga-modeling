namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    /// <summary>
    /// More concrete description of syntactical errors.
    /// </summary>
    public enum SyntaxErrorType : byte
    {
        /// <summary> Something the parser didn't expect. </summary>
        Unexpected,
        /// <summary> Something the parser expected but wasn't there </summary>
        Missing,
    }
}