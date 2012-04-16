namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class RootFragment : Fragment
    {
        private string m_Title;

        public RootFragment()
            : base(null)
        {
        }

        public override FragmentType FragmentType
        {
            get { return FragmentType.Root; }
        }

        public override string Title
        {
            get
            {
                return
                    string.IsNullOrEmpty(m_Title)
                        ? string.Empty
                        : string.Format("{0} {1}", base.Title, m_Title);
            }
        }

        public void SetTitle(string title)
        {
            m_Title = title;
        }
    }
}