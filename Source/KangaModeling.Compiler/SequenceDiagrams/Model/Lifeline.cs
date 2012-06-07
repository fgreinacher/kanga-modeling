using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    [DebuggerDisplay("Lifeline: ({index}) '{id}'")]
    internal class Lifeline : ILifeline
    {
        private readonly Matrix m_Matrix;
        private Row m_EndRow;
        private Row m_StartRow;

        public Lifeline(Matrix matrix, string id, string name, int index)
        {
            m_Matrix = matrix;
            Id = id;
            Index = index;
            Name = name;
            State = new LifelineState();
        }

        public LifelineState State { get; private set; }

        #region ILifeline Members

        public IEnumerable<IPin> Pins
        {
            get { return m_Matrix.Rows.Select(row => row[Index]); }
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public virtual int Index { get; private set; }

        #endregion

        public void SetEnd(Row endRow)
        {
            State.IsDisposed = endRow != null;
            m_EndRow = endRow;
        }

        public int EndRowIndex
        {
            get
            {
                return m_EndRow == null 
                    ? int.MaxValue 
                    : m_EndRow.Index;
            }
        }

        public void SetStart(Row startRow)
        {
            State.IsCreated = startRow != null;
            m_StartRow = startRow;
        }

        public int StartRowIndex
        {
            get
            {
                return m_StartRow == null
                    ? int.MaxValue
                    : m_StartRow.Index;
            }
        }
    }
}