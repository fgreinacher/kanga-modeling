using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class LifelineState
    {
        public LifelineState()
        {
            OpenPins = new Stack<OpenPin>();
        }

        public Stack<OpenPin> OpenPins { get; private set; }
        public int LeftLevel { get; set; }
        public int RightLevel { get; set; }
    }
}