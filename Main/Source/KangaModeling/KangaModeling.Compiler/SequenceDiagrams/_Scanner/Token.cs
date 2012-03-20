using System;

namespace KangaModeling.Compiler.SequenceDiagrams._Scanner
{
    internal struct Token
    {
        private readonly int m_Start;
        private readonly string m_Value;

        public Token(int end, string value)
        {
            if (value == null) {throw new ArgumentNullException("value");}
            if (value.Length>end) {throw new ArgumentOutOfRangeException("end", end, "End is less then length.");}

            m_Start = end - value.Length;
            m_Value = value;
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
    }
}