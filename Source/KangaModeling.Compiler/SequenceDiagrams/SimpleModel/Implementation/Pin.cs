namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal abstract class Pin : IPin
    {
        private readonly Lifeline m_Lifeline;
        private Activity m_Activity;
        private int m_Level;
        private Signal m_Signal;

        protected Pin()
        {
            PinType = PinType.None;
        }

        protected Pin(Row row, Lifeline lifeline, PinType pinType)
        {
            Row = row;
            m_Lifeline = lifeline;
            PinType = pinType;
        }

        public Row Row { get; private set; }

        #region IPin Members

        public int Level
        {
            get { return m_Level; }
        }

        public ILifeline Lifeline
        {
            get { return m_Lifeline; }
        }

        public PinType PinType { get; set; }

        public ISignal Signal
        {
            get { return m_Signal; }
        }

        public IActivity Activity
        {
            get { return m_Activity; }
        }


        public abstract Orientation Orientation { get; }

        public int LifelineIndex
        {
            get { return Lifeline.Index; }
        }

        public int RowIndex
        {
            get { return Row.Index; }
        }

        #endregion

        public void SetSignal(Signal signal)
        {
            m_Signal = signal;
        }

        public void SetActivity(Activity activity)
        {
            m_Activity = activity;
        }

        public Activity GetActivity()
        {
            return m_Activity;
        }

        public void SetLevel(int level)
        {
            m_Level = level;
        }
    }
}