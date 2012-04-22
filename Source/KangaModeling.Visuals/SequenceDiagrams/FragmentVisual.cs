using System;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FragmentVisual : Visual
    {
        private readonly ICombinedFragment m_Root;

        public FragmentVisual(ICombinedFragment root, GridLayout gridLayout)
        {
            m_Root = root;
            Initialize();
        }

        private void Initialize()
        {
            foreach (var operand in m_Root.Operands)
            {
                AddChild(new OperandVisual(operand));
            }
        }
    }
}