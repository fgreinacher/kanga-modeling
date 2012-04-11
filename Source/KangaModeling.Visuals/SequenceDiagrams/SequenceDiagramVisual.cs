using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Linq;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class CellDescriptor
    {
        public CellDescriptor(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; private set; }

        public int Column { get; private set; }
    }

    internal abstract class Cell : Visual
    {
        private readonly Cell[,] m_Grid;
        private readonly CellDescriptor m_CellDescriptor;

        protected Cell(Cell[,] grid, CellDescriptor cellDescriptor)
        {
            m_Grid = grid;
            m_CellDescriptor = cellDescriptor;
        }

        public Cell[,] Grid
        {
            get { return m_Grid; }
        }

        public int Row
        {
            get { return m_CellDescriptor.Row; }
        }

        public int Column
        {
            get { return m_CellDescriptor.Column; }
        }

        public float BodyWidth { get; set; }
        public float BodyHeight { get; set; }

        public float LeftOuterWidth { get; set; }
        public float RightOuterWidth { get; set; }

        public float TopOuterHeight { get; set; }
        public float BottomOuterHeight { get; set; }

        public void LayoutBody(IGraphicContext graphicContext)
        {
            LayoutBodyCore(graphicContext);
        }

        public void LayoutOuters(IGraphicContext graphicContext)
        {
            LayoutOutersCore(graphicContext);
        }

        #region Overrides / Overrideables

        protected abstract void LayoutBodyCore(IGraphicContext graphicContext);

        protected abstract void LayoutOutersCore(IGraphicContext graphicContext);

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            graphicContext.DrawRectangle(new Point(LeftOuterWidth, TopOuterHeight), new Size(BodyWidth, BodyHeight));

            //string text = string.Format("R{0}, C{1}", Row, Column);

            //graphicContext.DrawText(text, HorizontalAlignment.Left, VerticalAlignment.Top, new Point(LeftOuterWidth, TopOuterHeight), new Size(1000, 1000));
        }

        #endregion
    }

    internal enum LifelineCellSignalDirection
    {
        None,
        In,
        Out,
    }

    internal enum LifelineCellSignalType
    {
        None,
        Call,
        Signal,
    }

    internal class LifelineCell : Cell
    {
        const float baseLifelineWidth = 4;
        const float MinimumBodyHeight = 30;

        public LifelineCell(Cell[,] grid, CellDescriptor cellDescriptor)
            : base(grid, cellDescriptor)
        {
        }

        public LifelineCellSignalType SignalType { get; set; }

        public LifelineCellSignalDirection SignalDirection { get; set; }

        public CellDescriptor SignalTarget { get; set; }

        public string SignalName { get; set; }

        public int EnterActivationLevel { get; set; }

        public int ExitActivationLevel { get; set; }

        public bool CanDrawSignal { get; set; }

        protected override void LayoutOutersCore(IGraphicContext graphicContext)
        {
            if (!CanDrawSignal || SignalType == LifelineCellSignalType.None)
            {
                return;
            }

            LifelineCell signalTargetCell = (LifelineCell)Grid[SignalTarget.Row, SignalTarget.Column];
            float availableSpaceForSignal = CalculateAvailableSpaceForSignal(signalTargetCell);

            Size nameSize = graphicContext.MeasureText(SignalName);

            float neededWidthDelta = nameSize.Width - availableSpaceForSignal;
            if (neededWidthDelta > 0)
            {
                signalTargetCell.LeftOuterWidth += neededWidthDelta;
            }

            TopOuterHeight = 5;
        }

        private Size m_SignalNameSize = Size.Empty;

        protected override void LayoutBodyCore(IGraphicContext graphicContext)
        {
            BodyWidth = 0;
            BodyHeight = 0;

            if (SignalType == LifelineCellSignalType.None)
            {
                return;
            }

            m_SignalNameSize = graphicContext.MeasureText(SignalName);
            BodyHeight = m_SignalNameSize.Height + 14;
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            DrawEnterLifeline(graphicContext);
            DrawExitLifeline(graphicContext);
            DrawSignal(graphicContext);

            //base.DrawCore(graphicContext);
        }

        private void DrawEnterLifeline(IGraphicContext graphicContext)
        {
            Point from = new Point(HorizontalLifelineCenter(), 0);
            Point to = new Point(HorizontalLifelineCenter(), VerticalLifelineCenter());

            graphicContext.DrawLine(from, to, baseLifelineWidth + baseLifelineWidth * EnterActivationLevel);
        }

        private void DrawExitLifeline(IGraphicContext graphicContext)
        {
            Point from = new Point(HorizontalLifelineCenter(), VerticalLifelineCenter());
            Point to = new Point(HorizontalLifelineCenter(), Height);

            graphicContext.DrawLine(from, to, baseLifelineWidth + baseLifelineWidth * ExitActivationLevel);
        }

        private void DrawSignal(IGraphicContext graphicContext)
        {
            if (!CanDrawSignal || SignalType == LifelineCellSignalType.None)
            {
                return;
            }

            LifelineCell signalTargetCell = (LifelineCell)Grid[SignalTarget.Row, SignalTarget.Column];

            Point from = new Point(HorizontalLifelineCenterRight(), VerticalLifelineCenter());
            Point to = new Point(
                (signalTargetCell.X - X) + signalTargetCell.HorizontalLifelineCenterLeft(),
                VerticalLifelineCenter());

            Size measuredTextSize = graphicContext.MeasureText(SignalName);

            Point textLocation = @from.Offset(0, -measuredTextSize.Height);

            Size textSize = new Size(to.X - from.X, measuredTextSize.Height);

            graphicContext.DrawText(SignalName, HorizontalAlignment.Center, VerticalAlignment.Bottom, textLocation, textSize);

            if (SignalDirection == LifelineCellSignalDirection.In)
            {
                Point tmp = from;
                from = to;
                to = tmp;
            }

            var lineOptions = LineOptions.ArrowEnd;
            if (SignalType == LifelineCellSignalType.Signal)
            {
                lineOptions |= LineOptions.Dashed;
            }
            graphicContext.DrawLine(@from, to, 2, lineOptions);
        }

        private float HorizontalLifelineCenterLeft()
        {
            return HorizontalLifelineCenter() -
                (baseLifelineWidth + baseLifelineWidth * Math.Max(EnterActivationLevel, ExitActivationLevel)) / 2;
        }

        private float HorizontalLifelineCenterRight()
        {
            return HorizontalLifelineCenter() +
                (baseLifelineWidth + baseLifelineWidth * Math.Max(EnterActivationLevel, ExitActivationLevel)) / 2;
        }

        private float HorizontalLifelineCenter()
        {
            return LeftOuterWidth + ((BodyWidth / 2));
        }

        private float VerticalLifelineCenter()
        {
            return TopOuterHeight + m_SignalNameSize.Height;
        }

        private float CalculateAvailableSpaceForSignal(Cell signalTargetCell)
        {
            var cellsBetweenThisCellAndTargetCell = Grid.Cast<Cell>().Where(
                cell => cell.Row == Row && cell.Column > Column && cell.Column < signalTargetCell.Column);

            var availableSpaceInCellsBetween =
                cellsBetweenThisCellAndTargetCell.Select(cell => cell.BodyWidth + cell.LeftOuterWidth + cell.RightOuterWidth).
                    Sum();
            var availableRightSpaceInThisCell = (BodyWidth / 2) + RightOuterWidth;
            var availableLeftSpaceInTargetCell = (signalTargetCell.BodyWidth / 2) + signalTargetCell.LeftOuterWidth;

            var availableSpaceForSignal =
                availableSpaceInCellsBetween +
                availableRightSpaceInThisCell +
                availableLeftSpaceInTargetCell;
            return availableSpaceForSignal;
        }
    }

    internal class LifelineNameCell : Cell
    {
        public LifelineNameCell(Cell[,] grid, CellDescriptor cellDescriptor)
            : base(grid, cellDescriptor)
        {
        }

        public string Name { get; set; }
        public bool IsTop { get; set; }
        public bool IsBottom { get; set; }
        public bool IsLeft { get; set; }
        public bool IsRight { get; set; }

        protected override void LayoutOutersCore(IGraphicContext graphicContext)
        {
            if (IsTop)
            {
                TopOuterHeight = 5;
                BottomOuterHeight = 0;
            }
            else if (IsBottom)
            {
                BottomOuterHeight = 5;
            }

            RightOuterWidth = 5;

            if (IsLeft)
            {
                LeftOuterWidth = 5;
            }
        }

        protected override void LayoutBodyCore(IGraphicContext graphicContext)
        {
            Size nameSize = graphicContext.MeasureText(Name);

            BodyWidth = nameSize.Width;
            BodyHeight = nameSize.Height;
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            Point location = new Point(
                LeftOuterWidth,
                TopOuterHeight);
            graphicContext.DrawRectangle(location, new Size(BodyWidth, BodyHeight));
            graphicContext.DrawText(Name, HorizontalAlignment.Center, VerticalAlignment.Middle, location, new Size(BodyWidth, BodyHeight));
        }
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

        private readonly Cell[,] m_CellGrid;

        #endregion

        #region Construction / Destruction / Initialisation

        public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
        {
            m_CellGrid = CreateCellGrid(sequenceDiagram);
        }

        #endregion

        #region Overrides / Overrideables

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            LayoutCellBodies(graphicContext);
            LayoutCellOuters(graphicContext);
        }

        #endregion

        #region Private Methods

        private Cell[,] CreateCellGrid(ISequenceDiagram sequenceDiagram)
        {
            var columnCount = CalculateColumnCount(sequenceDiagram);
            var rowCount = CalculateRowCount(sequenceDiagram);

            Cell[,] cellGrid = new Cell[rowCount, columnCount];

            foreach (var lifeline in sequenceDiagram.Lifelines)
            {
                var topLifelineNameCell = new LifelineNameCell(cellGrid, new CellDescriptor(0, lifeline.Index))
                {
                    IsTop = true,
                    IsLeft = lifeline.Index == 0,
                    IsRight = lifeline.Index == sequenceDiagram.Lifelines.Count(),
                    Name = lifeline.Name
                };
                AddCell(cellGrid, topLifelineNameCell);

                const int rowOffset = 1;

                foreach (var pin in lifeline.Pins)
                {
                    var lifelineCell = new LifelineCell(cellGrid, new CellDescriptor(pin.RowIndex + rowOffset, lifeline.Index));

                    if (pin.IsSignalStart() && pin.IsSignalEnd())
                    {
                        // Self signal, not implemented yet.
                    }
                    else if (pin.IsSignalStart())
                    {
                        lifelineCell.CanDrawSignal = (pin.Signal.End.LifelineIndex > pin.LifelineIndex);
                        lifelineCell.SignalName = pin.Signal.Name;
                        lifelineCell.SignalTarget = new CellDescriptor(pin.Signal.End.RowIndex + rowOffset,
                                                                       pin.Signal.End.LifelineIndex);
                        lifelineCell.SignalType = pin.Signal.SignalType == SignalType.Call
                                                      ? LifelineCellSignalType.Call
                                                      : LifelineCellSignalType.Signal;
                        lifelineCell.SignalDirection = LifelineCellSignalDirection.Out;
                    }
                    else if (pin.IsSignalEnd())
                    {
                        lifelineCell.CanDrawSignal = (pin.Signal.Start.LifelineIndex > pin.LifelineIndex);
                        lifelineCell.SignalName = pin.Signal.Name;
                        lifelineCell.SignalTarget = new CellDescriptor(pin.Signal.Start.RowIndex + rowOffset,
                                                                       pin.Signal.Start.LifelineIndex);
                        lifelineCell.SignalType = pin.Signal.SignalType == SignalType.Call
                                                      ? LifelineCellSignalType.Call
                                                      : LifelineCellSignalType.Signal;
                        lifelineCell.SignalDirection = LifelineCellSignalDirection.In;
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
                            ((LifelineCell)cellGrid[lifelineCell.Row - 1, lifelineCell.Column]).EnterActivationLevel = pin.Level + 1;
                            ((LifelineCell)cellGrid[lifelineCell.Row - 1, lifelineCell.Column]).ExitActivationLevel = pin.Level;
                        }
                    }
                    else
                    {
                        lifelineCell.EnterActivationLevel = lifelineCell.ExitActivationLevel = pin.Level;
                    }


                    AddCell(cellGrid, lifelineCell);
                }

                var bottomLifelineNameCell = new LifelineNameCell(cellGrid, new CellDescriptor(rowCount - 1, lifeline.Index))
                {
                    IsBottom = true,
                    IsLeft = lifeline.Index == 0,
                    IsRight = lifeline.Index == sequenceDiagram.Lifelines.Count(),
                    Name = lifeline.Name
                };
                AddCell(cellGrid, bottomLifelineNameCell);
            }

            return cellGrid;
        }

        private void AddCell(Cell[,] cellGrid, Cell cell)
        {
            cellGrid[cell.Row, cell.Column] = cell;
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

        private IEnumerable<int> Rows()
        {
            for (int row = 0; row <= m_CellGrid.GetUpperBound(0); row++)
            {
                yield return row;
            }
        }

        private IEnumerable<int> Columns()
        {
            for (int column = 0; column <= m_CellGrid.GetUpperBound(1); column++)
            {
                yield return column;
            }
        }

        private IEnumerable<Cell> CellsInRow(int row)
        {
            return m_CellGrid.Cast<Cell>().Where(c => c.Row == row);
        }

        private IEnumerable<Cell> CellsInColumn(int column)
        {
            return m_CellGrid.Cast<Cell>().Where(c => c.Column == column);
        }

        private void NormalizeCellDimensions()
        {
            var rowDimensions = CalculateRowDimensions();
            var columnDimensions = CalculateColumnDimensions();

            var y = 0f;

            foreach (var row in Rows())
            {
                var rowDimension = rowDimensions[row];

                var x = 0f;

                foreach (var column in Columns())
                {
                    var columnDimension = columnDimensions[column];

                    var cell = m_CellGrid[row, column];

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

            foreach (var row in Rows())
            {
                var rowDimension = new RowDimension();

                foreach (var cellInRow in CellsInRow(row))
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

            foreach (var column in Columns())
            {
                var columnDimension = new ColumnDimension();

                foreach (var cellInColumn in CellsInColumn(column))
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
            foreach (var row in Rows())
            {
                foreach (var cell in CellsInRow(row))
                {
                    cell.LayoutOuters(graphicContext);
                }
                NormalizeCellDimensions();
            }

        }

        private void LayoutCellBodies(IGraphicContext graphicContext)
        {
            foreach (var row in Rows())
            {
                foreach (var cell in CellsInRow(row))
                {
                    cell.LayoutBody(graphicContext);
                }
                NormalizeCellDimensions();
            }
        }

        #endregion
    }
}
