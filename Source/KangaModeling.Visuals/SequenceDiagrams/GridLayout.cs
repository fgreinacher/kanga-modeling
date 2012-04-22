using System.Collections.Generic;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class GridLayout
    {
        public GridLayout(int lifelineCount, int rowCount)
        {
            Rows = new List<Row>();
            Columns = new List<Column>();

            for (int i = 0; i < lifelineCount; i++)
            {
                Columns.Add(new Column());
            }


            HeaderRow = new Row();
            for (int i = 0; i < rowCount; i++)
            {
                Rows.Add(new Row());
            }
            FooterRow = new Row();
        }

        public IList<Row> Rows { get; private set; }
        public IList<Column> Columns { get; private set; }
        public Row HeaderRow { get; private set; }
        public Row FooterRow { get; private set; }

        public void AdjustLoactions()
        {
            AdjustRowLocations();
            AdjustColumnLocations();
        }

        private void AdjustColumnLocations()
        {
            Column prevColumn = null;
            foreach (Column currentColumn in Columns)
            {
                currentColumn.AdjustLocation(prevColumn);
                prevColumn = currentColumn;
            }
        }

        private void AdjustRowLocations()
        {
            HeaderRow.AdjustLocation(null);
            Row prevRow = HeaderRow;
            foreach (Row currentRow in Rows)
            {
                currentRow.AdjustLocation(prevRow);
                prevRow = currentRow;
            }
            FooterRow.AdjustLocation(prevRow);
        }
    }
}