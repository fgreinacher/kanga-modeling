using System;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal abstract class Pin : IPin
    {
        private readonly Lifeline m_Lifeline;
        private Activity m_Activity;
        private PinType m_PinType;
        private Signal m_Signal;

        protected Pin()
        {
            m_PinType = PinType.None;
        }

        protected Pin(Row row, Lifeline lifeline, PinType pinType)
        {
            Row = row;
            m_Lifeline = lifeline;
            m_PinType = pinType;
        }

        public virtual Row Row { get; private set; }

        public Activity Activity
        {
            get { return m_Activity; }
        }

        #region IPin Members

        public int Level
        {
            get; private set;
        }

        public virtual ILifeline Lifeline
        {
            get { return m_Lifeline; }
        }

        public virtual PinType PinType
        {
            get { return m_PinType; }
            set { m_PinType = value; }
        }

        public ISignal Signal
        {
            get { return m_Signal; }
        }

        IActivity IPin.Activity
        {
            get { return m_Activity; }
        }

        public abstract Orientation Orientation { get; }

        public int LifelineIndex
        {
            get { return Lifeline.Index; }
        }

        public virtual int RowIndex
        {
            get { return Row.Index; }
        }

        #endregion

        public virtual void SetSignal(Signal signal)
        {
            m_Signal = signal;
        }

        public virtual void SetActivity(Activity activity)
        {
            m_Activity = activity;
        }

        public Activity GetActivity()
        {
            return m_Activity;
        }

        public void UpdateLevel()
        {
            this.Level = m_Lifeline.State.GetLevel(this.Orientation);
        }
    }
}