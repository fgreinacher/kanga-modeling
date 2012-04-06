using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Fragment
    {
        private readonly IList<Fragment> m_ChildFragments;
        private readonly Fragment m_Parent;

        public Fragment(Fragment parent)
        {
            m_ChildFragments = new List<Fragment>();
            m_Parent = parent;
        }

        public string Title { get; private set; }

        public Fragment Parnet
        {
            get { return m_Parent; }
        }

        public IEnumerable<Fragment> ChildFragments
        {
            get { return m_ChildFragments; }
        }
    }
}
