using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    [DebuggerDisplay("'{Value}' at Col {Start}")]
    public struct Token
    {
        private readonly int m_Line;
        private readonly int m_Start;
        private readonly string m_Value;

        public Token(int line, int end, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Length > end)
            {
                throw new ArgumentOutOfRangeException("end", end, "End is less then length.");
            }

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

        /// <summary>
        /// Returns a new token with its value set to the trimmed value of this token. 
        /// </summary>
        /// <returns></returns>
        public Token Trim()
        {
            string trimmedValue = m_Value.Trim();

            return new Token(m_Line, m_Start + trimmedValue.Length, trimmedValue);
        }

        public override string ToString()
        {
            return string.Format("[Ln {0} Col {1}] '{2}' ", Line, Start, Value);
        }

        public bool Equals(Token other)
        {
            return
                other.m_Start == m_Start &&
                other.m_Line == m_Line &&
                Equals(other.m_Value, m_Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (Token)) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = m_Start;
                result = (result*397) ^ m_Line;
                result = (result*397) ^ (m_Value != null ? m_Value.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Token left, Token right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Token left, Token right)
        {
            return !left.Equals(right);
        }
    }
}