using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KangaModeling.Compiler.SequenceDiagrams.Reading
{
    internal class Scanner : IEnumerator<string>
    {
        private readonly IEnumerator<string> m_Lines;
        private readonly Stack<int> m_PositionStack;

        public int Line { get; private set; }
        public int Column { get; private set; }


        public Scanner(string text) :
            this(text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
        {
            
        }

        public Scanner(IEnumerable<string> linesOfText)
        {
            m_PositionStack =new Stack<int>();
            m_Lines = linesOfText.GetEnumerator();
        }

        public string GetKeyWord()
        {
            m_PositionStack.Push(Column);
            string result = ReadWord().Value;
            Column = m_PositionStack.Peek();
            return result;
        }

        public Token ReadToEnd()
        {
            return ReadWhile(ch => true);
        }

        public Token ReadWord()
        {
            SkipWhile(char.IsWhiteSpace);
            return ReadWhile(char.IsLetterOrDigit);
        }

        public Token ReadWhile(Func<char, bool> condition)
        {
            StringBuilder buffer = new StringBuilder();
            while (Eol || condition(CurrentChar))
            {
                buffer.Append(LineText[Column]);
                Column++;
            }
            return new Token(Column, buffer.ToString());
        }

        private char CurrentChar
        {
            get { return LineText[Column]; }
        }

        public void SkipWhile(Func<char, bool> condition)
        {
            while (Eol || condition(CurrentChar))
            {
                Column++;
            }
        }

        protected string LineText
        {
            get
            {
                return Current ?? string.Empty;
            }
        }

        protected bool Eol
        {
            get { return Current==null ? true : Column == Current.Length; }
        }

        public void Dispose()
        {
            m_Lines.Dispose();
        }

        public bool MoveNext()
        {
            Line++;
            Column = 0;
            return m_Lines.MoveNext();
        }

        public void Reset()
        {
            Line = 0;
            Column = 0;
        }

        public string Current
        {
            get { return m_Lines.Current; } 
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}