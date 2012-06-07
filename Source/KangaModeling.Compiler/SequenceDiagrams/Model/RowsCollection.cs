using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class RowsCollection : List<Row>
    {
        public void Extend(Lifeline lifeline)
        {
            foreach (Row row in this)
            {
                row.Extend(lifeline);
            }
        }
    }
}