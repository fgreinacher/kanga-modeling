using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class InvisibleOperandVisual : OperandVisualBase
    {
        public InvisibleOperandVisual(IOperand operand, GridLayout gridLayout) 
            : base(operand, gridLayout)
        {
            Initialize();
        }
    }
}
