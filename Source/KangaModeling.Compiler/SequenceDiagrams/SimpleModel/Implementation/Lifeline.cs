﻿namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Lifeline : ILifeline
    {
        private readonly Matrix m_Matrix;

        public Lifeline(Matrix matrix, string id, string name, int index)
        {
            m_Matrix = matrix;
            Id = id;
            Index = index;
            Name = name;
            State = new LifelineState();
        }

        public LifelineState State { get; private set; }

        public Pin this[int rowIndex]
        {
            get { return m_Matrix[Index, rowIndex]; }
        }

        #region ILifeline Members

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Index { get; private set; }

        #endregion
    }
}