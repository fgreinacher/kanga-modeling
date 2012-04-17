namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal static class ElementFactory
    {
        public static Call CreateCall(Token name)
        {
            return new Call(name.Value);
        }

        public static Return CreateReturn(Token name)
        {
            return new Return(name.Value);
        }

        public static Activity CreateActivity(int level)
        {
            return new Activity(level);
        }
    }
}