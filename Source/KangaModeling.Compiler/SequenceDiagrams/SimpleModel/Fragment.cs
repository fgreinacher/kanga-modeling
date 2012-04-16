using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal abstract class Fragment : IFragment
    {
        private readonly IList<Fragment> m_Children;
        private readonly Fragment m_Parent;
        private readonly Stack<IActivity> m_Activities;
        private readonly Stack<ISignal> m_Signals;

        protected Fragment(Fragment parent)
        {
            m_Children = new List<Fragment>();
            m_Parent = parent;
            m_Activities = new Stack<IActivity>();
            m_Signals = new Stack<ISignal>();
        }

        public void Add(ISignal signal)
        {
            m_Signals.Append(signal);
        }

        public void Add(IActivity activity)
        {
            m_Activities.Append(activity);
        }

       #region IFragment Members

        public abstract FragmentType FragmentType { get; }

        public virtual string Title
        {
            get
            {
                switch (FragmentType)
                {
                    case FragmentType.Root:
                        return "sd";

                    case FragmentType.Opt:
                        return "opt";

                    case FragmentType.Alt:
                        return "alt";

                    case FragmentType.Loop:
                        return "loop";

                    default:
                        return string.Empty;
                }
            } 
        }

        public IFragment Parent
        {
            get { return m_Parent; }
        }

        public IEnumerable<IFragment> Children
        {
            get { return m_Children; }
        }

        public IEnumerable<IActivity> Activities
        {
            get { return m_Activities; }
        }

        public IEnumerable<ISignal> Signals
        {
            get
            {
                return m_Signals;
            }
        }

        #endregion
    }
}