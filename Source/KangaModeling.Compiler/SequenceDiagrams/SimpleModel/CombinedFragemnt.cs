using System;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class CombinedFragemnt :  Fragment
    {
        private readonly FragmentType m_FragmentType;

        public CombinedFragemnt(Fragment parent, FragmentType fragmentType) 
            : base(parent)
        {
            m_FragmentType = fragmentType;
        }

        public override FragmentType FragmentType
        {
            get { return m_FragmentType; }
        }
    }
}
