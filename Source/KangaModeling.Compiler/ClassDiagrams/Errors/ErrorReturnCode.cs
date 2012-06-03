namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    public enum ErrorReturnCode : byte
    {

        /// <summary>
        /// Stops parsing on the first error.
        /// </summary>
        StopParsing,

        ///// <summary>
        ///// Tries to continue parsing by inserting/deleting tokens.
        ///// </summary>
        //TryContinueByModification,
    }
}