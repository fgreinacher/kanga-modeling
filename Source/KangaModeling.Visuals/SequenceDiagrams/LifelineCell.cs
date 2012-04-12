using System;
using System.Linq;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class LifelineCell : Cell
    {
        #region Constants / Enums

        const float c_BaseLifelineWidth = 2;

		const float c_LineWidth = 2;

		const float c_ArrowCapWidth = 7;

		const float c_ArrowCapHeight = 4;

        #endregion

        #region Construction / Destruction / Initialisation

        public LifelineCell(Grid grid, int row, int column)
            : base(grid, row, column)
        {
        }

        #endregion

        #region Properties

        public bool CanDrawSignal { get; set; }

        public SignalType SignalType { get; set; }

        public SignalDirection SignalDirection { get; set; }

        public int SignalTargetRow { get; set; }

        public int SignalTargetColumn { get; set; }

        public string SignalName { get; set; }

        public int EnterActivationLevel { get; set; }

        public int ExitActivationLevel { get; set; }

        #endregion

        #region Overrides / Overrideables

        protected override void LayoutOutersCore(IGraphicContext graphicContext)
        {
            if (!CanDrawSignal || SignalType == SignalType.None)
            {
                return;
            }

            LifelineCell signalTargetCell = (LifelineCell)Grid.GetCell(SignalTargetRow, SignalTargetColumn);
            float availableSpaceForSignal = CalculateAvailableSpaceForSignal(signalTargetCell);

            Size nameSize = graphicContext.MeasureText(SignalName).Plus(20, 0);

            float neededWidthDelta = nameSize.Width - availableSpaceForSignal;
            if (neededWidthDelta > 0)
            {
				RightOuterWidth += neededWidthDelta;
            }

            TopOuterHeight = string.IsNullOrEmpty(SignalName) ? 15 : 5;
        }
        
        protected override void LayoutBodyCore(IGraphicContext graphicContext)
        {
            BodyWidth = 0;
            BodyHeight = 0;

            if (SignalType == SignalType.None)
            {
                return;
            }

            BodyHeight = graphicContext.MeasureText(SignalName).Height + 2 * 7;
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            DrawEnterLifeline(graphicContext);
            DrawExitLifeline(graphicContext);
            DrawSignal(graphicContext);
        }

        #endregion

        #region Private Methods

        private void DrawEnterLifeline(IGraphicContext graphicContext)
        {
            Point from = new Point(CalculateHorizontalLifelineCenter(), 0);
            Point to = new Point(CalculateHorizontalLifelineCenter(), CalculateVerticalLifelineCenter(graphicContext));

            graphicContext.DrawLine(from, to, c_BaseLifelineWidth + c_BaseLifelineWidth * EnterActivationLevel);
        }

        private void DrawExitLifeline(IGraphicContext graphicContext)
        {
            Point from = new Point(CalculateHorizontalLifelineCenter(), CalculateVerticalLifelineCenter(graphicContext));
            Point to = new Point(CalculateHorizontalLifelineCenter(), Height);

            graphicContext.DrawLine(from, to, c_BaseLifelineWidth + c_BaseLifelineWidth * ExitActivationLevel);
        }

        private void DrawSignal(IGraphicContext graphicContext)
        {
            if (!CanDrawSignal || SignalType == SignalType.None)
            {
                return;
            }

            LifelineCell signalTargetCell = (LifelineCell)Grid.GetCell(SignalTargetRow, SignalTargetColumn);

            Point from = new Point(CalculateHorizontalLifelineCenterRight(), CalculateVerticalLifelineCenter(graphicContext));
            Point to = new Point(
                (signalTargetCell.X - X) + signalTargetCell.CalculateHorizontalLifelineCenterLeft(),
                CalculateVerticalLifelineCenter(graphicContext));

            Size measuredTextSize = graphicContext.MeasureText(SignalName);

            Point textLocation = @from.Offset(0, -measuredTextSize.Height);

            Size textSize = new Size(to.X - from.X, measuredTextSize.Height);

            graphicContext.DrawText(SignalName, HorizontalAlignment.Center, VerticalAlignment.Bottom, textLocation, textSize);

            if (SignalDirection == SignalDirection.In)
            {
                Point tmp = from;
                from = to;
                to = tmp;
            }

            if (SignalType == SignalType.Signal)
			{
				graphicContext.DrawDashedArrow(@from, to, c_LineWidth, c_ArrowCapWidth, c_ArrowCapHeight);
            }
			else if(SignalType == SignalType.Call)
			{
				graphicContext.DrawArrow(@from, to, c_LineWidth, c_ArrowCapWidth, c_ArrowCapHeight);
			}
        }

        private float CalculateHorizontalLifelineCenterLeft()
        {
            return CalculateHorizontalLifelineCenter() -
                   (c_BaseLifelineWidth + c_BaseLifelineWidth * Math.Max(EnterActivationLevel, ExitActivationLevel)) / 2;
        }

        private float CalculateHorizontalLifelineCenterRight()
        {
            return CalculateHorizontalLifelineCenter() +
                   (c_BaseLifelineWidth + c_BaseLifelineWidth * Math.Max(EnterActivationLevel, ExitActivationLevel)) / 2;
        }

        private float CalculateHorizontalLifelineCenter()
        {
            return LeftOuterWidth + ((BodyWidth / 2));
        }

        private float CalculateVerticalLifelineCenter(IGraphicContext graphicContext)
        {
            if (!string.IsNullOrEmpty(SignalName))
            {
                return TopOuterHeight + graphicContext.MeasureText(SignalName).Height;
            }

            return TopOuterHeight;
        }

        private float CalculateAvailableSpaceForSignal(Cell signalTargetCell)
        {
            var cellsBetweenThisCellAndTargetCell = Grid
                .CellsInRow(Row)
                .Where(cell => cell.Column > Column && cell.Column < signalTargetCell.Column);
				
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

        #endregion
	}
}