using System;

namespace KangaModeling.Compiler.ClassDiagrams
{

    public enum TokenType
    {
        Unknown,
        Identifier,
        Number,
        BracketOpen,
        BracketClose,
        AngleOpen,
        AngleClose,
        Dash,
        Plus,
        Hash,
        Comma,
        Star,
        DotDot,
        Colon,
        Pipe,
        Tilde,
        ParenthesisOpen,
        ParenthesisClose,
    }

    public static class TokenTypeExtensions
    {
        public static string ToDisplayString(this TokenType ttype)
        {
            switch (ttype)
            {
                case TokenType.AngleClose: return ">";
                case TokenType.AngleOpen: return "<";
                case TokenType.BracketClose: return "]";
                case TokenType.BracketOpen: return "[";
                case TokenType.Comma: return ",";
                case TokenType.Dash: return "-";
                case TokenType.Hash: return "#";
                case TokenType.Plus: return "+";
                case TokenType.Star: return "*";
                case TokenType.DotDot: return "..";
                case TokenType.Colon: return ":";
                case TokenType.Pipe: return "|";
                case TokenType.Tilde: return "~";
                case TokenType.ParenthesisOpen: return "(";
                case TokenType.ParenthesisClose: return ")";

                case TokenType.Unknown: throw new ArgumentException("must provide value for UNKNOWN tokentype");
                case TokenType.Identifier: throw new ArgumentException("must provide value for IDENTIFIER tokentype");
            }

            throw new ArgumentException("Unknown CD token type: " + ttype.ToString());
        }

        public static TokenType FromDisplayString(this string displayString)
        {
            int dummy;
            if (int.TryParse(displayString, out dummy))
                return TokenType.Number;

            switch (displayString)
            {
                case ">": return TokenType.AngleClose;
                case "<": return TokenType.AngleOpen;
                case "]": return TokenType.BracketClose;
                case "[": return TokenType.BracketOpen;
                case ")": return TokenType.ParenthesisClose;
                case "(": return TokenType.ParenthesisOpen;

                case ",": return TokenType.Comma;
                case "-": return TokenType.Dash;
                case "#": return TokenType.Hash;
                case "+": return TokenType.Plus;
                case "*": return TokenType.Star;
                case "..": return TokenType.DotDot;
                case ":": return TokenType.Colon;
                case "|": return TokenType.Pipe;
                case "~": return TokenType.Tilde;

                default: return TokenType.Identifier;
            }
        }
    }

    public sealed class ClassDiagramToken : GenericToken
    {
        public ClassDiagramToken(int line, int end, TokenType tokenType, string value = null)
            : base(line, end, value ?? tokenType.ToDisplayString())
        {
            TokenType = tokenType;
        }

        public TokenType TokenType { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(ClassDiagramToken)) return false;
            var other = (ClassDiagramToken) obj;

            if (other.TokenType != TokenType) return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Start;
                result = (result * 397) ^ Line;
                result = (result * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                return result;
            }
        }

    }
}