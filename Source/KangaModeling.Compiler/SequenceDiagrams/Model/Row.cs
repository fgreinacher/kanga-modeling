using System.Collections.Generic;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    [DebuggerDisplay("Row: ({index})")]
    internal class Row : List<RegularPin>
    {
        public Row(int index)
        {
            Index = index;
        }

        public virtual int Index { get; private set; }

        public Pin this[Lifeline lifeline]
        {
            get { return this[lifeline.Index]; }
        }

        public virtual void Extend(Lifeline lifeLine)
        {
            Add(new RegularPin(this, lifeLine));
        }

        public void Extend(IEnumerable<Lifeline> lifeLines)
        {
            foreach (Lifeline lifeline in lifeLines)
            {
                Extend(lifeline);
            }
        }
    }
}