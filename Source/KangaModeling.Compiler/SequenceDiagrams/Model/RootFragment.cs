namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class RootFragment : CombinedFragment
    {
        private string m_Title;

        public RootFragment()
            : base(null, OperatorType.Root, new Token(0, 0, string.Empty))
        {
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