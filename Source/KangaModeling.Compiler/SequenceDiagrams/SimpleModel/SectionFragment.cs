namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class SectionFragment : Fragment
    {
        private readonly string m_Title;
        private readonly int m_Top;
        private int m_Bottom;

        public SectionFragment(Fragment parent, string title, int stratRowIndex) 
            : base(parent)
        {
            m_Title = title;
            m_Top = stratRowIndex;
        }

        public void SetBottom(int endRowIndex)
        {
            m_Bottom = endRowIndex;
        }

        public override FragmentType FragmentType
        {
            get { return FragmentType.Leaf; }
        }

        public override ILifeline Left
        {
            get { return Parent.Left; }
        }

        public override ILifeline Right
        {
            get { return Parent.Right; }
        }

        public override int Top
        {
            get { return m_Top; }
        }

        public override int Bottom
        {
            get { return m_Bottom; }
        }

        public override string Title
        {
            get { return m_Title; }
        }
    }
}
