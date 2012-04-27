using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisualBase : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IOperand m_Operand;

        protected OperandVisualBase(IOperand operand, GridLayout gridLayout)
        {
            m_Operand = operand;
            m_GridLayout = gridLayout;
            Initialize();
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