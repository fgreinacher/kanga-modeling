namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    /// <summary>
    /// A text region denotes a substring inside a text (=user source).
    /// </summary>
    public struct TextRegion
    {
        public TextRegion(int line, int charStart, int length)
        {
            Line = line;
            PositionInLine = charStart;
            Length = length;
        }

        public readonly int Line;
        public readonly int PositionInLine;
        public readonly int Length;
    }
}