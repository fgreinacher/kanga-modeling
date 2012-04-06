using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal abstract class Fragment : IFragment
    {
        private readonly IList<Fragment> m_Children;
        private readonly Fragment m_Parent;

        protected Fragment(Fragment parent)
        {
            m_Children = new List<Fragment>();
            m_Parent = parent;
        }

        public Fragment Parnet
        {
            get { return m_Parent; }
        }

        public IEnumerable<Fragment> Children
        {
            get { return m_Children; }
        }

        protected IFragment Parent
        {
            get
            {
                return m_Parent;
            }
        }

        #region IFragment Members

        public abstract FragmentType FragmentType { get; }
        public abstract ILifeline Left { get; }
        public abstract ILifeline Right { get; }
        public abstract int Top { get; }
        public abstract int Bottom { get; }

        public abstract string Title { get; }

        IFragment IFragment.Parent
        {
            get { return m_Parent; }
        }

        IEnumerable<IFragment> IFragment.Children
        {
            get { return m_Children; }
        }

        #endregion
    }
}