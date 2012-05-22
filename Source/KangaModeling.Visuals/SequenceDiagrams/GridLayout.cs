using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Diagnostics;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class GridLayout : SDVisualBase
    {
        public GridLayout(IStyle style, int lifelineCount, int rowCount)
            : base(style)
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

        internal void AllocateBetween(Column from, Column to, float width)
        {
            IEnumerable<ColumnSection> allSectionsBetween = GetSectionsBetween(from, to, false);
            IEnumerable<ColumnSection> gapsBetween = GetSectionsBetween(from, to, true);
            float sumWidth = allSectionsBetween.Select(section => section.Width).Sum();
            int count = gapsBetween.Count();

            float additionalWidthNeeded = Math.Max(0, width - sumWidth);
            float additionalWidthPerSection = additionalWidthNeeded / count;
            foreach (var section in gapsBetween)
            {
                section.Allocate(additionalWidthPerSection);
            }
        }

        private IEnumerable<ColumnSection> GetSectionsBetween(Column from, Column to, bool gapsOnly)
        {
            int fromIndex = Columns.IndexOf(from);
            int toIndex = Columns.IndexOf(to);
            return GetSectionsBetween(fromIndex, toIndex, gapsOnly);
        }


        private IEnumerable<ColumnSection> GetSectionsBetween(int startLifelineId, int endLifelineId, bool gapsOnly)
        {
            yield return Columns[startLifelineId].RightGap;
            for (int i = startLifelineId + 1; i < endLifelineId; i++)
            {
                yield return Columns[i].LeftGap;
                if (!gapsOnly)
                {
                    yield return Columns[i].Body;
                }
                yield return Columns[i].RightGap;
            }
            yield return Columns[endLifelineId].LeftGap;
            yield return Columns[endLifelineId].Body;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            if (Columns.Count == 0)
            {
                return;
            }

            AdjustLocations();

            Column lastColumn = Columns[Columns.Count - 1];
            Row lastRow = FooterRow;

            Size = new Size(lastColumn.Right + Style.Common.GridPadding.Right, lastRow.Bottom + Style.Common.GridPadding.Bottom);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            if (Style.Debug.DrawCellAreas)
            {
                DrawCellAreas(graphicContext);
            }
        }

        private void AdjustLocations()
        {
            AdjustRowLocations();
            AdjustColumnLocations();
        }

        private void AdjustColumnLocations()
        {
            Column prevColumn = null;
            foreach (Column currentColumn in Columns)
            {
                float x = (prevColumn != null) ? prevColumn.Right : Style.Common.GridPadding.Left;
                currentColumn.AdjustLocation(x);
                prevColumn = currentColumn;
            }
        }

        private void AdjustRowLocations()
        {
            HeaderRow.AdjustLocation(Style.Common.GridPadding.Top);
            Row prevRow = HeaderRow;
            foreach (Row currentRow in Rows)
            {
                currentRow.AdjustLocation(prevRow.Bottom);
                prevRow = currentRow;
            }
            FooterRow.AdjustLocation(prevRow.Bottom);
        }

        private void DrawCellAreas(IGraphicContext graphicContext)
        {
            foreach (var row in Rows)
            {
                var rowTop = row.Top;
                var rowHeight = row.Bottom - rowTop;
                var rowBodyTop = row.Body.Top;
                var rowBodyHeight = row.Body.Bottom - row.Body.Top;

                foreach (var column in Columns)
                {
                    var columnLeft = column.Left;
                    var columnWidth = column.Right - columnLeft;
                    var columnBodyLeft = column.Body.Left;
                    var columnBodyWidth = column.Body.Right - columnBodyLeft;

                    graphicContext.FillRectangle(
                        new Point(columnLeft, rowTop),
                        new Size(columnWidth, rowHeight),
                        new Color(50, Color.Green));

                    graphicContext.DrawRectangle(
                        new Point(columnLeft, rowTop),
                        new Size(columnWidth, rowHeight),
                        new Color(100, Color.Green),
                        Style.Common.LineStyle);

                    graphicContext.FillRectangle(
                        new Point(columnBodyLeft, rowBodyTop),
                        new Size(columnBodyWidth, rowBodyHeight),
                        new Color(50, Color.Red));

                    graphicContext.DrawRectangle(
                        new Point(columnBodyLeft, rowBodyTop),
                        new Size(columnBodyWidth, rowBodyHeight),
                        new Color(100, Color.Red),
                        Style.Common.LineStyle);
                }
            }
        }
    }
}