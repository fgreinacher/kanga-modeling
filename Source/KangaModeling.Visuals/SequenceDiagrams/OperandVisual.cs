using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisual : Visual
    {
        private readonly IOperand m_Operand;

        public OperandVisual(IOperand operand)
        {
            m_Operand = operand;
            Initialize();
        }

        private void Initialize()
        {
            foreach (var fragment in m_Operand.Children)
            {
                AddChild(new FragmentVisual(fragment, null));
            }
        }
    }
}