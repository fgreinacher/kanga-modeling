namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
    /// The type of signal.
    /// Allows the user to distinguish between different interactions between participants.
    /// </summary>
    public enum SignalType {
        /// <summary>A asynchronous signal</summary>
        Signal,
        /// <summary>A synchronous call</summary>
        Call,
        /// <summary>A return from a synchronous call</summary>
        CallReturn,
    }
}