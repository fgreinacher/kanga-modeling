using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal abstract class OperandVisualBase : SDVisualBase
    {
        private readonly GridLayout m_GridLayout;

        protected GridLayout GridLayout
        {
            get { return m_GridLayout; }
        }

        private readonly IOperand m_Operand;
        public float BottomOffset { get; protected set; }


        protected OperandVisualBase(IStyle style, IOperand operand, GridLayout gridLayout)
            : base(style)
        {
            m_Operand = operand;
            m_GridLayout = gridLayout;
        }

        protected void Initialize()
        {
            foreach (ICombinedFragment fragment in m_Operand.Children)
            {
                AddChild(new FragmentVisual(Style, fragment, m_GridLayout));
            }
        }
    }
}