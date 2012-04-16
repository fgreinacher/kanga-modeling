namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Operand : Fragment
    {
        private readonly string m_GuardExpression;

        public Operand(Fragment parent, string guardExpression) 
            : base(parent)
        {
            m_GuardExpression = guardExpression;
        }

        public override FragmentType FragmentType
        {
            get { return FragmentType.Leaf; }
        }

        public override string Title
        {
            get { return m_GuardExpression; }
        }
    }
}
