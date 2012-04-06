using System;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal abstract class Signal : ISignal
    {
        private Pin m_End;
        private Pin m_Start;

        protected Signal(string name)
        {
            Name = name;
            m_End = null;
            m_Start = null;
        }

        #region ISignal Members

        public IPin End
        {
            get { return m_End; }
        }

        public string Name { get; set; }

        public IPin Start
        {
            get { return m_Start; }
        }

        public abstract SignalType SignalType { get; }

        public int RowIndex
        {
            get { return m_Start.RowIndex; }
        }

        #endregion

        public void Connect(Pin start, Pin end)
        {
            start.PinType = PinType.Out;
            m_Start = start;
            m_Start.SetSignal(this);
            end.PinType = PinType.In;
            m_End = end;
            m_End.SetSignal(this);
        }
    }
}