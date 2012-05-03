using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal abstract class OperandVisualBase : Visual
    {
        private readonly GridLayout m_GridLayout;

        protected GridLayout GridLayout
        {
            get { return m_GridLayout; }
        }

        private readonly IOperand m_Operand;
        public float BottomOffset { get; protected set; }


        protected OperandVisualBase(IOperand operand, GridLayout gridLayout)
        {
            m_Operand = operand;
            m_GridLayout = gridLayout;
        }

        protected void Initialize()
        {
            foreach (ICombinedFragment fragment in m_Operand.Children)
            {
                AddChild(new FragmentVisual(fragment, m_GridLayout));
            }
        }
    }
}