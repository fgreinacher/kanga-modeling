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

    public sealed class CDToken : GenericToken<TokenType>
    {
        public CDToken(int line, int end, TokenType tokenType, string value = null)
            : base(line, end, value ?? typeToString(tokenType), tokenType)
        {
        }

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

        private static string typeToString(TokenType ttype)
        {
            switch(ttype) {
                case ClassDiagrams.TokenType.AngleClose: return ">";
                case ClassDiagrams.TokenType.AngleOpen: return "<";
                case ClassDiagrams.TokenType.BracketClose: return "]";
                case ClassDiagrams.TokenType.BracketOpen: return "[";
                case ClassDiagrams.TokenType.Comma: return ",";
                case ClassDiagrams.TokenType.Dash: return "-";
                case ClassDiagrams.TokenType.Hash: return "#";
                case ClassDiagrams.TokenType.Plus: return "+";
                case ClassDiagrams.TokenType.Star: return "*";
                case ClassDiagrams.TokenType.DotDot: return "..";
                case ClassDiagrams.TokenType.Colon: return ":";
                case ClassDiagrams.TokenType.Pipe: return "|";
                case ClassDiagrams.TokenType.Tilde: return "~";
                case ClassDiagrams.TokenType.ParenthesisOpen: return "(";
                case ClassDiagrams.TokenType.ParenthesisClose: return ")";

                case ClassDiagrams.TokenType.Unknown: throw new ArgumentException("must provide value for UNKNOWN tokentype");
                case ClassDiagrams.TokenType.Identifier: throw new ArgumentException("must provide value for IDENTIFIER tokentype");
            }

            throw new ArgumentException("Unknown CD token type: " + ttype.ToString());
        }

    }

    [DebuggerDisplay("'{Value}' at Col {Start}")]
    public abstract class GenericToken<TType>
    {
        private readonly int m_Line;
        private readonly int m_Start;
        private readonly string m_Value;

        public GenericToken(int line, int end, string value, TType type)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Length > end) throw new ArgumentOutOfRangeException("end", end, "End is less then length.");

            m_Start = end - value.Length;
            m_Line = line;
            m_Value = value;
            TokenType = type;
        }

        public TType TokenType
        {
            get;
            private set;
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

        public bool Equals(GenericToken<TType> other)
        {
            return
                other.m_Start == m_Start &&
                other.m_Line == m_Line &&
                Equals(other.m_Value, m_Value);
        }

    }
}