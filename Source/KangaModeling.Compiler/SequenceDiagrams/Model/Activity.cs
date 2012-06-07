namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class Activity : IActivity
    {
        private Pin m_End;
        private Pin m_Start;

        public Activity(int level)
        {
            Level = level;
        }

        #region IActivity Members

        public IPin Start
        {
            get { return m_Start; }
        }

        public int StartRowIndex
        {
            get { return m_Start.RowIndex; }
        }

        public IPin End
        {
            get { return m_End; }
        }

        public int EndRowIndex
        {
            get { return m_End.RowIndex; }
        }

        public ILifeline Lifeline
        {
            get { return m_Start.Lifeline; }
        }

        public Orientation Orientation
        {
            get
            {
                return
                    Level == 0 || Start == null
                        ? Orientation.None
                        : Start.Orientation;
            }
        }

        public int Level { get; private set; }

        #endregion

        public void Connect(Pin startPin, Pin endPin)
        {
            startPin.SetActivity(this);
            m_Start = startPin;
            ReconnectEnd(endPin);
        }

        public void ReconnectEnd(Pin endPin)
        {
            endPin.SetActivity(this);
            m_End = endPin;
            m_Start.UpdateLevel();
            m_End.UpdateLevel();
        }
    }
}