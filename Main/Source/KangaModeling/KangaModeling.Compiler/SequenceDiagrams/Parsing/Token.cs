using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    [DebuggerDisplay("'{Value}' at Col {Start}")]
    public struct Token
    {
        private readonly int m_Start;
        private readonly int m_Line;
        private readonly string m_Value;

        public Token(int line, int end, string value)
        {
            if (value == null) {throw new ArgumentNullException("value");}
            if (value.Length>end) {throw new ArgumentOutOfRangeException("end", end, "End is less then length.");}

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
    }
}