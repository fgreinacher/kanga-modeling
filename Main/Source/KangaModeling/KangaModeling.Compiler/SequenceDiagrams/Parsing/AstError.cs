namespace KangaModeling.Compiler.SequenceDiagrams
{
    public class AstError
    {
        public Token Token { get; private set; }
        public string Message { get; private set; }

        public AstError(Token token, string message)
        {
            Token = token;
            Message = message;
        }

        public override string ToString()
        {
            return string.Format("Error: {0} at {1}", Message, Token);
        }
    }
}