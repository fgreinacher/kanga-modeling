using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.ClassDiagrams
{
    public enum CDTokenType
    {
        Unknown,
        Identifier,
        Number,
        Bracket_Open,
        Bracket_Close,
        Angle_Open,
        Angle_Close,
        Dash,
        Plus,
        Hash,
        Comma,
        Star,
        DotDot,
        Colon,
        Pipe,
        Tilde
    }

    public sealed class CDToken : GenericToken<CDTokenType>
    {
        public CDToken(int line, int end, CDTokenType tokenType, string value = null)
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

        private static string typeToString(CDTokenType ttype)
        {
            switch(ttype) {
                case CDTokenType.Angle_Close: return ">";
                case CDTokenType.Angle_Open: return "<";
                case CDTokenType.Bracket_Close: return "]";
                case CDTokenType.Bracket_Open: return "[";
                case CDTokenType.Comma: return ",";
                case CDTokenType.Dash: return "-";
                case CDTokenType.Hash: return "#";
                case CDTokenType.Plus: return "+";
                case CDTokenType.Star: return "*";
                case CDTokenType.DotDot: return "..";
                case CDTokenType.Colon: return ":";
                case CDTokenType.Pipe: return "|";
                case CDTokenType.Tilde: return "~";

                case CDTokenType.Unknown: throw new ArgumentException("must provide value for UNKNOWN tokentype");
                case CDTokenType.Identifier: throw new ArgumentException("must provide value for IDENTIFIER tokentype");
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