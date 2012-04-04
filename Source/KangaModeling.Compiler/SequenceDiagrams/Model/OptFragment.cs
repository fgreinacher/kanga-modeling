using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class OptFragment : CombinedFragment
    {
        public OptFragment(InteractionOperator op) 
            : base(InteractionOperator.Alternative)
        {
        }
    }
}
