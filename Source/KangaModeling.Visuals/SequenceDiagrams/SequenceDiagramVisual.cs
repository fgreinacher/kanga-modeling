using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using System.Linq;
using System;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	public sealed class SequenceDiagramVisual : Visual
	{
		#region Fields

		private readonly Grid m_Grid;

		#endregion

		#region Construction / Destruction / Initialisation

		public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
		{
			m_Grid = CreateGrid(sequenceDiagram);
			AddChild(m_Grid);
		}

		#endregion

		#region Overrides / Overrideables

		protected override void LayoutCore(IGraphicContext graphicContext)
		{
			m_Grid.Layout(graphicContext);
			m_Grid.Location = new Point(0, 0);

			Size = new Size(m_Grid.Width, m_Grid.Height);
		}

		#endregion

		#region Private Methods

		private Grid CreateGrid(ISequenceDiagram sequenceDiagram)
		{
			var grid = new Grid(sequenceDiagram.RowCount + 2, sequenceDiagram.LifelineCount);

			AddCellsToGrid(grid, sequenceDiagram);

			InitializeFragmentProperties(grid, sequenceDiagram.Root);

			return grid;
		}

		const int rowOffset = 1;

		private void InitializeFragmentProperties(Grid grid, IFragment fragment)
		{
			if(grid.ColumnCount == 0) return;

			var startLifelineCell = grid.GetCell(0, 0);
			var endLifelineCell = grid.GetCell(grid.RowCount - 1, grid.ColumnCount - 1);
			endLifelineCell.IsFragmentEnd  = true;
			startLifelineCell.IsFragmentStart = true;
			startLifelineCell.FragmentType = fragment.FragmentType;
			startLifelineCell.FragmentTitle = fragment.Title;
			startLifelineCell.FragmentEndCell = endLifelineCell;

			foreach (IFragment childFragment in fragment.Children)
			{
				InitializeFragmentProperties(grid, fragment);
			}
		}

		private void AddCellsToGrid(Grid grid, ISequenceDiagram sequenceDiagram)
		{
			foreach (var lifeline in sequenceDiagram.Lifelines)
			{
				AddTopLifelineNameCell(grid, sequenceDiagram, lifeline);

				int previousExitActivationLevel = 0;

				foreach (var pin in lifeline.Pins)
				{
					var lifelineCell = new LifelineCell(grid, pin.RowIndex + rowOffset, lifeline.Index);
					grid.AddCell(lifelineCell);

					InitializeSignalProperties(lifelineCell, pin, rowOffset);
					InitializeActivityProperties(lifelineCell, pin, ref previousExitActivationLevel);
				}

				AddBottomLifelineNameCell(grid, sequenceDiagram, lifeline);
			}
		}

		private void InitializeSignalProperties(LifelineCell lifelineCell, IPin pin, int rowOffset)
		{
			if (pin.IsSignalStart() && pin.IsSignalEnd())
			{
				// Self signal, not implemented yet.
			}
			else if (pin.IsSignalStart())
			{
				lifelineCell.CanDrawSignal = pin.Orientation == Orientation.Right;
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
				lifelineCell.CanDrawSignal = pin.Orientation == Orientation.Right;
				lifelineCell.SignalName = pin.Signal.Name;
				lifelineCell.SignalTargetRow = pin.Signal.Start.RowIndex + rowOffset;
				lifelineCell.SignalTargetColumn = pin.Signal.Start.LifelineIndex;
				lifelineCell.SignalType = pin.Signal.SignalType == Compiler.SequenceDiagrams.SignalType.Call
											  ? SignalType.Call
											  : SignalType.Signal;
				lifelineCell.SignalDirection = SignalDirection.In;
			}
		}

		private void InitializeActivityProperties(LifelineCell lifelineCell, IPin pin, ref int previousExitActivationLevel)
		{
			if (pin.IsActivityStart())
			{
				lifelineCell.EnterActivationLevel = pin.Activity.Level;
				lifelineCell.ExitActivationLevel = pin.Activity.Level + 1;
			}
			else if (pin.IsActivityEnd())
			{
				lifelineCell.EnterActivationLevel = pin.Activity.Level + 1;
				lifelineCell.ExitActivationLevel = pin.Activity.Level;
			}
			else
			{
				lifelineCell.EnterActivationLevel = previousExitActivationLevel;
				lifelineCell.ExitActivationLevel = previousExitActivationLevel;
			}

			previousExitActivationLevel = lifelineCell.ExitActivationLevel;
		}

		private void AddTopLifelineNameCell(Grid grid, ISequenceDiagram sequenceDiagram, ILifeline lifeline)
		{
			var topLifelineNameCell = new LifelineNameCell(grid, 0, lifeline.Index)
			{
				IsTop = true,
				IsLeft = lifeline.Index == 0,
				IsRight = lifeline.Index == sequenceDiagram.LifelineCount - 1,
				Name = lifeline.Name
			};
			grid.AddCell(topLifelineNameCell);
		}

		private void AddBottomLifelineNameCell(Grid grid, ISequenceDiagram sequenceDiagram, ILifeline lifeline)
		{
			var bottomLifelineNameCell = new LifelineNameCell(grid, sequenceDiagram.RowCount + 1, lifeline.Index)
			{
				IsBottom = true,
				IsLeft = lifeline.Index == 0,
				IsRight = lifeline.Index == sequenceDiagram.LifelineCount - 1,
				Name = lifeline.Name
			};
			grid.AddCell(bottomLifelineNameCell);
		}

		#endregion
	}
}
