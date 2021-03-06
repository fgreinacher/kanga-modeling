﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class Scanner : IEnumerator<string>
    {
        private const char EscapeCharacter = '"';
        private readonly IEnumerator<string> m_Lines;
        private readonly Stack<int> m_PositionStack;


        public Scanner(string text) :
            this(text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None))
        {
        }

        public Scanner(IEnumerable<string> linesOfText)
        {
            m_PositionStack = new Stack<int>();
            m_Lines = linesOfText.GetEnumerator();
        }

        public int Line { get; private set; }
        public int Column { get; private set; }

        private char CurrentChar
        {
            get { return LineText[Column]; }
        }

        private string LineText
        {
            get { return Current ?? string.Empty; }
        }

        public bool Eol
        {
            get { return Current == null || Column == Current.Length; }
        }

        #region IEnumerator<string> Members

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

        #endregion

        public string GetKeyWord()
        {
            string signal = GetSignal();
            return MightBeValidSignal(signal) ? signal : GetWord();
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
            int index = Current.IndexOf(text, Column, StringComparison.InvariantCulture);
            while (index > 0 && char.IsWhiteSpace(Current[index - 1]))
            {
                index--;
            }
            return ReadWhile(() => index != Column);
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

        private Token ReadWhile(Func<char, bool> condition)
        {
            return ReadWhile(() => condition(LineText[Column]));
        }

        private Token ReadWhile(Func<bool> condition)
        {
            bool isInEscapeMode = false;

            var buffer = new StringBuilder();
            while (!Eol && (isInEscapeMode || condition()))
            {
                if (CurrentChar == EscapeCharacter)
                {
                    isInEscapeMode = !isInEscapeMode;
                }
                else
                {
                    buffer.Append(CurrentChar);
                }
                Column++;
            }
            return new Token(Line, Column, buffer.ToString());
        }

        public void SkipWhile(Func<char, bool> condition)
        {
            while (!Eol && condition(CurrentChar))
            {
                Column++;
            }
        }

        private static bool MightBeValidSignal(string potentialSignal)
        {
            return potentialSignal != null &&
                   potentialSignal.Length > 1;
        }
    }
}