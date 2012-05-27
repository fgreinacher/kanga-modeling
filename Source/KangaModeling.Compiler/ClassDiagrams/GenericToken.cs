using System;
using System.Diagnostics;

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

                // TODO numbers?
                default: return TokenType.Identifier;
            }
        }
    }

    public sealed class CDToken : GenericToken
    {
        public CDToken(int line, int end, TokenType tokenType, string value = null)
            : base(line, end, value ?? tokenType.ToDisplayString())
        {
            TokenType = tokenType;
        }

        public TokenType TokenType { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(CDToken)) return false;
            var other = (CDToken) obj;

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

    [DebuggerDisplay("'{Value}' at Col {Start}")]
    public abstract class GenericToken
    {
        private readonly int m_Line;
        private readonly int m_Start;
        private readonly string m_Value;

        protected GenericToken(int line, int end, string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Length > end) throw new ArgumentOutOfRangeException("end", end, "End is less then length.");

            m_Start = end - value.Length;
            m_Line = line;
            m_Value = value;
        }

        public int Line
        {
            get { return m_Line; }
        }

        public string Value
        {
            get { return m_Value; }
        }

        public int Start
        {
            get { return m_Start; }
        }

        public int End
        {
            get { return m_Start + Length; }
        }

        public int Length
        {
            get { return m_Value.Length; }
        }

        public bool IsEmpty()
        {
            return Length == 0;
        }

        public override string ToString()
        {
            return string.Format("[Ln {0} Col {1}] '{2}' ", Line, Start, Value);
        }

        public bool Equals(GenericToken other)
        {
            return
                other.m_Start == m_Start &&
                other.m_Line == m_Line &&
                Equals(other.m_Value, m_Value);
        }

    }

}