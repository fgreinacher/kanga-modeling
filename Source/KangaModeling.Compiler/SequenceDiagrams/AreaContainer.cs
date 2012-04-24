using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class AreaContainer : IArea
    {
        private readonly IEnumerable<IArea> m_Children;
        private readonly bool m_HasFrame;

        public AreaContainer(IEnumerable<IArea> children, bool hasFrame)
        {
            m_Children = children;
            m_HasFrame = hasFrame;
        }

        #region IArea Members

        public int Left
        {
			get { return m_Children.Any() ? m_Children.Select(a => a.Left).Min() : 0; }
        }

        public int Right
        {
			get { return m_Children.Any() ? m_Children.Select(a => a.Right).Max() : 0; }
        }

        public int Top
        {
            get { return m_Children.Any() ? m_Children.Select(a => a.Top).Min() : 0; }
        }

        public int Bottom
        {
			get { return m_Children.Any() ? m_Children.Select(a => a.Bottom).Max() : 0; }
        }

        public IEnumerable<IArea> Children
        {
            get { return m_Children; }
        }

        public bool HasFrame
        {
            get { return m_HasFrame; }
        }

        #endregion
    }
}