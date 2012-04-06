using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
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

        public void Extend(Lifeline lifeLine)
        {
            Add(new RegularPin(this, lifeLine));
        }

        public void Extend(IEnumerable<Lifeline> lifeLine)
        {
            foreach (Lifeline lifeline in lifeLine)
            {
                Extend(lifeline);
            }
        }
    }
}