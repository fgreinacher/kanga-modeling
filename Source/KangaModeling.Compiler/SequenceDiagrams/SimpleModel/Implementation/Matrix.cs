using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Matrix : ISequenceDiagram
    {
        private readonly RootFragment m_Root;

        public Matrix()
        {
            Lifelines = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            Rows = new RowsCollection();
            CreateRow();
            m_Root = new RootFragment(this);
        }

        public RootFragment Root
        {
            get { return m_Root; }
        }

        public LifelineCollection Lifelines { get; private set; }

        public RowsCollection Rows { get; private set; }

        public Row LastRow
        {
            get { return Rows[Rows.Count - 1]; }
        }

        public Pin this[int lifelineIndex, int rowIndex]
        {
            get { return Rows[rowIndex][lifelineIndex]; }
        }

        #region ISequenceDiagram Members

        IFragment ISequenceDiagram.Root
        {
            get { return m_Root; }
        }

        IEnumerable<ILifeline> ISequenceDiagram.Lifelines
        {
            get { return Lifelines; }
        }

        public IEnumerable<IActivity> Activities
        {
            get
            {
                return
                    Rows
                        .SelectMany(row => row)
                        .Where(pin => pin.Activity != null && pin.Activity.Start.Equals(pin))
                        .Select(pin => pin.Activity);
            }
        }

        public IEnumerable<ISignal> Signals
        {
            get
            {
                return
                    Rows
                        .SelectMany(row => row)
                        .Where(pin => pin.Signal != null && pin.PinType == PinType.Out)
                        .Select(pin => pin.Signal);
            }
        }

        #endregion

        public Lifeline CreateLifeline(string id, string name)
        {
            var lifeline = new Lifeline(this, id, name, Lifelines.Count);
            Rows.Extend(lifeline);
            Lifelines.Add(lifeline);
            return lifeline;
        }

        public Row CreateRow()
        {
            var row = new Row(Rows.Count);
            row.Extend(Lifelines);
            Rows.Add(row);
            return row;
        }
    }

    internal class RootFragment : Fragment
    {
        private readonly Matrix m_Matrix;
        private string m_Title;

        public RootFragment(Matrix matrix)
            : base(null)
        {
            m_Matrix = matrix;
        }

        public override FragmentType FragmentType
        {
            get { return FragmentType.Root; }
        }

        public override ILifeline Left
        {
            get
            {
                return m_Matrix.Lifelines.Count == 0
                           ? null
                           : m_Matrix.Lifelines[0];
            }
        }

        public override ILifeline Right
        {
            get
            {
                return m_Matrix.Lifelines.Count == 0
                           ? null
                           : m_Matrix.Lifelines[m_Matrix.Lifelines.Count - 1];
            }
        }

        public override int Top
        {
            get { return 0; }
        }

        public override int Bottom
        {
            get { return m_Matrix.Rows.Count; }
        }

        public override string Title
        {
            get
            {
                return string.IsNullOrEmpty(m_Title)
                           ? string.Empty
                           : string.Format("sd: {0}", m_Title);
            }
        }

        public void SetTitle(string title)
        {
            m_Title = title;
        }
    }
}