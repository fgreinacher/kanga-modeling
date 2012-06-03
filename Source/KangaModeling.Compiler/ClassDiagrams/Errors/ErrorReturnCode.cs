namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    internal enum ErrorReturnCode : byte
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