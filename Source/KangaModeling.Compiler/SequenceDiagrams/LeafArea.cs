using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class LeafArea : IArea, IEquatable<LeafArea>
    {
        private readonly int m_Bottom;
        private readonly int m_Left;
        private readonly int m_Right;

        private readonly int m_Top;

        public LeafArea(int left, int right, int top, int bottom)
        {
            m_Left = left;
            m_Right = right;
            m_Top = top;
            m_Bottom = bottom;
        }

        #region IArea Members

        public int Left
        {
            get { return m_Left; }
        }

        public int Right
        {
            get { return m_Right; }
        }

        public int Top
        {
            get { return m_Top; }
        }

        public int Bottom
        {
            get { return m_Bottom; }
        }

        public IEnumerable<IArea> Children
        {
            get { return Enumerable.Empty<LeafArea>(); }
        }

        public bool HasFrame
        {
            get { return false; }
        }

        #endregion

        #region IEquatable<LeafArea> Members

        public bool Equals(LeafArea other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.m_Left == m_Left && other.m_Right == m_Right && other.m_Top == m_Top &&
                   other.m_Bottom == m_Bottom;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LeafArea)) return false;
            return Equals((LeafArea) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = m_Left;
                result = (result*397) ^ m_Right;
                result = (result*397) ^ m_Top;
                result = (result*397) ^ m_Bottom;
                return result;
            }
        }

        public static bool operator ==(LeafArea left, LeafArea right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LeafArea left, LeafArea right)
        {
            return !Equals(left, right);
        }
    }
}