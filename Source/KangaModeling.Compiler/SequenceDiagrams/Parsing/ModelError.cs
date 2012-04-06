namespace KangaModeling.Compiler.SequenceDiagrams
{
    public class ModelError
    {
        public ModelError(Token token, string message)
        {
            Token = token;
            Message = message;
        }

        public Token Token { get; private set; }
        public string Message { get; private set; }

        public override string ToString()
        {
            return string.Format("Error: {0} at {1}", Message, Token);
        }
    }
}