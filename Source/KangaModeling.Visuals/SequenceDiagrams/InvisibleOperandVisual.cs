using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class InvisibleOperandVisual : OperandVisualBase
    {
        public InvisibleOperandVisual(IStyle style, IOperand operand, GridLayout gridLayout) 
            : base(style, operand, gridLayout)
        {
            Initialize();
        }
    }
}
