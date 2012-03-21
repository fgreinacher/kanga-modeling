using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KangaModeling.Compiler.SequenceDiagrams
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
            string signal = GetSignal();
            return string.IsNullOrEmpty(signal) ? GetWord() : signal;
        }

        public void SkipWhiteSpaces()
        {
            SkipWhile(char.IsWhiteSpace);
        }

        public Token ReadToEnd()
        {
            return ReadWhile(() => true);
        }

        public Token ReadTo(string text)
        {
            return ReadWhile(() => !GetToEnd().TrimStart().StartsWith(text));
        }

        public Token ReadToWord(string text)
        {
            return ReadWhile(() => !GetWord().Equals(text));
        }

        public Token ReadWord()
        {
            SkipWhile(ch => !IsWordChar(ch));
            return ReadWhile(IsWordChar);
        }

        public Token ReadSignal()
        {
            SkipWhile(ch => !IsSignalChar(ch));
            return ReadWhile(IsSignalChar);
        }

        private string GetToEnd()
        {
            return GetWithoutMove(ReadToEnd);
        }

        private string GetWord()
        {
            return GetWithoutMove(ReadWord);
        }

        private string GetSignal()
        {
            return GetWithoutMove(ReadSignal);
        }

        private string GetWithoutMove(Func<Token> readerFunction)
        {
            m_PositionStack.Push(Column);
            Token result = readerFunction();
            Column = m_PositionStack.Peek();
            return result.Value;
        }

        private static bool IsWordChar(char ch)
        {
            return 
                (char.IsLetterOrDigit(ch) || 
                 char.IsPunctuation(ch)) 
                    && !IsSignalChar(ch);
        }

        private static bool IsSignalChar(char ch)
        {
            switch (ch)
            {
                case '-':
                    return true;
                case '<':
                    return true;
                case '>':
                    return true;
                default:
                    return false;
            }
        }

        public Token ReadWhile(Func<char, bool> condition)
        {
            return ReadWhile(() => condition(LineText[Column]));
        }

        private Token ReadWhile(Func<bool> condition)
        {
            StringBuilder buffer = new StringBuilder();
            while (!Eol && condition())
            {
                buffer.Append(CurrentChar);
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
            while (!Eol && condition(CurrentChar))
            {
                Column++;
            }
        }

        private string LineText
        {
            get
            {
                return Current ?? string.Empty;
            }
        }

        public bool Eol
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
            m_PositionStack.Clear();
            m_Lines.Reset();
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