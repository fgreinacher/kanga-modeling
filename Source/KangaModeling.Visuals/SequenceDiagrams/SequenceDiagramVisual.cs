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
        private readonly TitleVisual m_Title;

        #endregion

        #region Construction / Destruction / Initialisation

        public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
        {
            m_Grid = CreateGrid(sequenceDiagram);
            AddChild(m_Grid);

            m_Title = new TitleVisual(sequenceDiagram.Root.Title);
            AddChild(m_Title);
        }

        #endregion

        #region Overrides / Overrideables

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            m_Title.Layout(graphicContext);
            m_Grid.Layout(graphicContext);
            
            m_Title.Location = new Point(0, 0);
            m_Grid.Location = new Point(0, m_Title.Height);

            Size = new Size(
                Math.Max(m_Title.Width, m_Grid.Width),
                m_Title.Height + m_Grid.Height);
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
                grid.AddCell(topLifelineNameCell);

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

                    grid.AddCell(lifelineCell);
                }

                var bottomLifelineNameCell = new LifelineNameCell(grid, rowCount - 1, lifeline.Index)
                {
                    IsBottom = true,
                    IsLeft = lifeline.Index == 0,
                    IsRight = lifeline.Index == sequenceDiagram.Lifelines.Count(),
                    Name = lifeline.Name
                };
                grid.AddCell(bottomLifelineNameCell);
            }

            return grid;
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

        #endregion
    }
}
