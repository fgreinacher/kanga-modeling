namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class RootFragment : Fragment
    {
        private readonly Matrix m_Matrix;
        private string m_Title;

        public RootFragment(Matrix matrix)
            : base(null)
        {
            m_Matrix = matrix;
        }

        public override FragmentType FragmentType
        {
            get { return FragmentType.Root; }
        }

        public override ILifeline Left
        {
            get
            {
                return m_Matrix.Lifelines.Count == 0
                           ? null
                           : m_Matrix.Lifelines[0];
            }
        }

        public override ILifeline Right
        {
            get
            {
                return m_Matrix.Lifelines.Count == 0
                           ? null
                           : m_Matrix.Lifelines[m_Matrix.Lifelines.Count - 1];
            }
        }

        public override int Top
        {
            get { return 0; }
        }

        public override int Bottom
        {
            get { return m_Matrix.Rows.Count; }
        }

        public override string Title
        {
            get
            {
                return m_Title ?? string.Empty;
            }
        }

        public void SetTitle(string title)
        {
            m_Title = title;
        }
    }
}