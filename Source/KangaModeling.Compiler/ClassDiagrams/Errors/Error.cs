using System;

namespace KangaModeling.Compiler.ClassDiagrams.Errors
{
    /// <summary>
    /// Encapsulates the data for a single error.
    /// </summary>
    public sealed class Error
    {
        public Error(string errorMessage, TextRegion location, string objectedText, ErrorCategory category = ErrorCategory.Syntactical)
        {
            if (errorMessage == null) throw new ArgumentNullException("errorMessage");
            if (objectedText == null) throw new ArgumentNullException("objectedText");

            ErrorMessage = errorMessage;
            Location = location;
            ObjectedText = objectedText;
            Category = category;
        }

        public static Error Create(SyntaxErrorType syntaxErrorType, TokenType expectedType, ClassDiagramToken actualToken)
        {
            // TODO var region = new TextRegion(actualToken.Line, actualToken.Start, actualToken.Length);
            var region = new TextRegion(0, 0, 0);
            switch(syntaxErrorType)
            {
                case SyntaxErrorType.Unexpected:
                    return new Error("syntax error: expected token " + expectedType.ToDisplayString(), region, String.Empty);
                case SyntaxErrorType.Missing:
                    return new Error("unexpected token", region, actualToken.Value);

            }
            throw new ArgumentException("don't know how to handle error type: " + syntaxErrorType.ToString());
        }

        public string ErrorMessage { get; private set; }
        public TextRegion Location { get; private set; }
        public string ObjectedText { get; private set; }
        public ErrorCategory Category { get; private set; }
    }
}