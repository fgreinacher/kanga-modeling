using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class LifelineState
    {
        private readonly int[] m_LevelsByOrientation;
        public Stack<OpenPin> OpenPins { get; private set; }


        public LifelineState()
        {
            OpenPins = new Stack<OpenPin>();
            m_LevelsByOrientation = new int[3];
        }

        public int GetLevel(Orientation orientation)
        {
            return m_LevelsByOrientation[(int)orientation] + m_LevelsByOrientation[(int)Orientation.None];
        }
       
        public void IncLevel(Orientation orientation)
        {
            if (m_LevelsByOrientation[(int)Orientation.None]==0)
            {
                m_LevelsByOrientation[(int) Orientation.None]++;
                return;
            }
            m_LevelsByOrientation[(int) orientation]++;
        }

        public void DecLevel(Orientation orientation)
        {
            int level = m_LevelsByOrientation[(int) orientation];
            if (level==0)
            {
                return;
            }

            m_LevelsByOrientation[(int)orientation]--;
        }

        public bool IsDisposed { get; set; }
        public bool IsCreated { get; set; }
    }
}