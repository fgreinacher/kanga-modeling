using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using System.Linq;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class Grid
    {
        private readonly int m_RowCount;
        private readonly int m_ColumnCount;
        private readonly Cell[,] m_Cells;

        public Grid(int rowCount, int columnCount)
        {
            m_RowCount = rowCount;
            m_ColumnCount = columnCount;
            m_Cells = new Cell[rowCount, columnCount];
        }

        public Cell GetCell(int row, int column)
        {
            return m_Cells[row, column];
        }

        internal void AddCell(Cell cell)
        {
            m_Cells[cell.Row, cell.Column] = cell;
        }

        public IEnumerable<Cell> Cells()
        {
            return m_Cells.Cast<Cell>();
        }

        public IEnumerable<int> Rows()
        {
            for (int row = 0; row < m_RowCount; row++)
            {
                yield return row;
            }
        }

        public IEnumerable<int> Columns()
        {
            for (int column = 0; column < m_ColumnCount; column++)
            {
                yield return column;
            }
        }

        public IEnumerable<Cell> CellsInRow(int row)
        {
            return Cells().Where(cell => cell.Row == row);
        }

        public IEnumerable<Cell> CellsInColumn(int column)
        {
            return Cells().Where(cell => cell.Column == column);
        }


        public int RowCount { get; set; }

        public int ColumnCount { get; set; }
    }

    public sealed class SequenceDiagramVisual : Visual
    {
        private sealed class RowDimension
        {
            public float BodyHeight { get; set; }

            public float TopOuterHeight { get; set; }

            public float BottomOuterHeight { get; set; }

            public float Height()
            {
                return BodyHeight + TopOuterHeight + BottomOuterHeight;
            }
        }

        private sealed class ColumnDimension
        {
            public float BodyWidth { get; set; }

            public float LeftOuterWidth { get; set; }

            public float RightOuterWidth { get; set; }

            public float Width()
            {
                return BodyWidth + LeftOuterWidth + RightOuterWidth;
            }
        }

        #region Fields

        private readonly Grid m_Grid;
        private readonly TitleVisual m_Title;

        #endregion

        #region Construction / Destruction / Initialisation

        public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
        {
            m_Grid = CreateGrid(sequenceDiagram);
            m_Title = new TitleVisual(sequenceDiagram.Root.Title);
            AddChild(m_Title);
        }

        #endregion

        #region Overrides / Overrideables

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            base.DrawCore(graphicContext);


        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            LayoutCellBodies(graphicContext);
            LayoutCellOuters(graphicContext);
        }

        #endregion

        #region Private Methods

        private Grid CreateGrid(ISequenceDiagram sequenceDiagram)
        {
            var rowCount = CalculateRowCount(sequenceDiagram);
            var columnCount = CalculateColumnCount(sequenceDiagram);

            var grid = new Grid(rowCount, columnCount);

            foreach (var lifeline in sequenceDiagram.Lifelines)
            {
                var topLifelineNameCell = new LifelineNameCell(grid, 0, lifeline.Index)
                {
                    IsTop = true,
                    IsLeft = lifeline.Index == 0,
                    IsRight = lifeline.Index == sequenceDiagram.Lifelines.Count(),
                    Name = lifeline.Name
                };
                AddCellToGridAndChildren(grid, topLifelineNameCell);

                const int rowOffset = 1;

                foreach (var pin in lifeline.Pins)
                {
                    var lifelineCell = new LifelineCell(grid, pin.RowIndex + rowOffset, lifeline.Index);

                    if (pin.IsSignalStart() && pin.IsSignalEnd())
                    {
                        // Self signal, not implemented yet.
                    }
                    else if (pin.IsSignalStart())
                    {
                        lifelineCell.CanDrawSignal = (pin.Signal.End.LifelineIndex > pin.LifelineIndex);
                        lifelineCell.SignalName = pin.Signal.Name;
                        lifelineCell.SignalTargetRow = pin.Signal.End.RowIndex + rowOffset;
                        lifelineCell.SignalTargetColumn = pin.Signal.End.LifelineIndex;

                        lifelineCell.SignalType = pin.Signal.SignalType == Compiler.SequenceDiagrams.SignalType.Call
                                                      ? SignalType.Call
                                                      : SignalType.Signal;
                        lifelineCell.SignalDirection = SignalDirection.Out;
                    }
                    else if (pin.IsSignalEnd())
                    {
                        lifelineCell.CanDrawSignal = (pin.Signal.Start.LifelineIndex > pin.LifelineIndex);
                        lifelineCell.SignalName = pin.Signal.Name;
                        lifelineCell.SignalTargetRow = pin.Signal.Start.RowIndex + rowOffset;
                        lifelineCell.SignalTargetColumn = pin.Signal.Start.LifelineIndex;
                        lifelineCell.SignalType = pin.Signal.SignalType == Compiler.SequenceDiagrams.SignalType.Call
                                                      ? SignalType.Call
                                                      : SignalType.Signal;
                        lifelineCell.SignalDirection = SignalDirection.In;
                    }

                    if (pin.Activity != null)
                    {
                        if (pin.Activity.Start == pin)
                        {
                            lifelineCell.EnterActivationLevel = pin.Level;
                            lifelineCell.ExitActivationLevel = pin.Level + 1;
                        }
                        else if (pin.Activity.End == pin)
                        {
                            LifelineCell previousLifelineCell = (LifelineCell)grid.GetCell(lifelineCell.Row - 1, lifelineCell.Column);

                            previousLifelineCell.EnterActivationLevel = pin.Level + 1;
                            previousLifelineCell.ExitActivationLevel = pin.Level;
                        }
                    }
                    else
                    {
                        lifelineCell.EnterActivationLevel = lifelineCell.ExitActivationLevel = pin.Level;
                    }


                    AddCellToGridAndChildren(grid, lifelineCell);
                }

                var bottomLifelineNameCell = new LifelineNameCell(grid, rowCount - 1, lifeline.Index)
                {
                    IsBottom = true,
                    IsLeft = lifeline.Index == 0,
                    IsRight = lifeline.Index == sequenceDiagram.Lifelines.Count(),
                    Name = lifeline.Name
                };

                AddCellToGridAndChildren(grid, bottomLifelineNameCell);
            }

            return grid;
        }

        private void AddCellToGridAndChildren(Grid grid, Cell cell)
        {
            grid.AddCell(cell);
            AddChild(cell);
        }

        private static int CalculateRowCount(ISequenceDiagram sequenceDiagram)
        {
            int rows = 2;

            if (sequenceDiagram.Lifelines.Any())
            {
                rows += sequenceDiagram.Lifelines.First().Pins.Count();
            }

            return rows;
        }

        private static int CalculateColumnCount(ISequenceDiagram sequenceDiagram)
        {
            int columns = sequenceDiagram.Lifelines.Count();
            return columns;
        }

        private void NormalizeCellDimensions()
        {
            var rowDimensions = CalculateRowDimensions();
            var columnDimensions = CalculateColumnDimensions();

            var y = 0f;

            foreach (var row in m_Grid.Rows())
            {
                var rowDimension = rowDimensions[row];

                var x = 0f;

                foreach (var column in m_Grid.Columns())
                {
                    var columnDimension = columnDimensions[column];

                    var cell = m_Grid.GetCell(row, column);

                    cell.X = x;
                    cell.Y = y;
                    cell.Width = columnDimension.Width();
                    cell.Height = rowDimension.Height();

                    cell.BodyWidth = columnDimension.BodyWidth;
                    cell.BodyHeight = rowDimension.BodyHeight;
                    cell.TopOuterHeight = rowDimension.TopOuterHeight;
                    cell.BottomOuterHeight = rowDimension.BottomOuterHeight;
                    cell.LeftOuterWidth = columnDimension.LeftOuterWidth;
                    cell.RightOuterWidth = columnDimension.RightOuterWidth;

                    x += cell.Width;
                }

                y += rowDimension.Height();
            }

            Width = columnDimensions.Select(rd => rd.Width()).Sum();
            Height = rowDimensions.Select(rd => rd.Height()).Sum();
        }

        private List<RowDimension> CalculateRowDimensions()
        {
            var rowDimensions = new List<RowDimension>();

            foreach (var row in m_Grid.Rows())
            {
                var rowDimension = new RowDimension();

                foreach (var cellInRow in m_Grid.CellsInRow(row))
                {
                    rowDimension.BodyHeight = Math.Max(cellInRow.BodyHeight, rowDimension.BodyHeight);
                    rowDimension.TopOuterHeight = Math.Max(cellInRow.TopOuterHeight, rowDimension.TopOuterHeight);
                    rowDimension.BottomOuterHeight = Math.Max(cellInRow.BottomOuterHeight, rowDimension.BottomOuterHeight);
                }

                rowDimensions.Add(rowDimension);
            }
            return rowDimensions;
        }

        private List<ColumnDimension> CalculateColumnDimensions()
        {
            var columnDimensions = new List<ColumnDimension>();

            foreach (var column in m_Grid.Columns())
            {
                var columnDimension = new ColumnDimension();

                foreach (var cellInColumn in m_Grid.CellsInColumn(column))
                {
                    columnDimension.BodyWidth = Math.Max(cellInColumn.BodyWidth, columnDimension.BodyWidth);
                    columnDimension.LeftOuterWidth = Math.Max(cellInColumn.LeftOuterWidth, columnDimension.LeftOuterWidth);
                    columnDimension.RightOuterWidth = Math.Max(cellInColumn.RightOuterWidth, columnDimension.RightOuterWidth);
                }

                columnDimensions.Add(columnDimension);
            }
            return columnDimensions;
        }

        private void LayoutCellOuters(IGraphicContext graphicContext)
        {
            foreach (var row in m_Grid.Rows())
            {
                foreach (var cell in m_Grid.CellsInRow(row))
                {
                    cell.LayoutOuters(graphicContext);
                }
                NormalizeCellDimensions();
            }

        }

        private void LayoutCellBodies(IGraphicContext graphicContext)
        {
            foreach (var row in m_Grid.Rows())
            {
                foreach (var cell in m_Grid.CellsInRow(row))
                {
                    cell.LayoutBody(graphicContext);
                }
                NormalizeCellDimensions();
            }
        }

        #endregion
    }
}
