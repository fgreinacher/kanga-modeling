using System;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Matrix
    {
        public Matrix()
        {
            Lifelines = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            Rows = new RowsCollection();
            CreateRow();
        }

        public string Title { get; set; }

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
            Rows.Add(row);
            return row;
        }
    }
}