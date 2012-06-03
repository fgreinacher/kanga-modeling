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

            ErrorMessage = string.Format("syntax error: {0}, at: {1}", errorMessage, location);
            Location = location;
            ObjectedText = objectedText;
            Category = category;
        }

        public static Error Unexpected(TokenType expectedType, ClassDiagramToken actualToken)
        {
            var region = new TextRegion(actualToken.Line, actualToken.Start, actualToken.Length);
            return new Error("unexpected token", region, actualToken.Value);
        }

        public static Error Missing(TokenType expectedType, TextRegion region)
        {
            return new Error("expected token " + expectedType.ToDisplayString(), region, String.Empty);
        }

        public string ErrorMessage { get; private set; }
        public TextRegion Location { get; private set; }
        public string ObjectedText { get; private set; }
        public ErrorCategory Category { get; private set; }
    }
}