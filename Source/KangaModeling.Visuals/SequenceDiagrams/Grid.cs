using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	internal class Grid : Visual
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

		private readonly int m_RowCount;
		private readonly int m_ColumnCount;
		private readonly Cell[,] m_Cells;

		public Grid(int rowCount, int columnCount)
		{
			m_RowCount = rowCount;
			m_ColumnCount = columnCount;
			m_Cells = new Cell[rowCount, columnCount];
		}

		protected override void LayoutCore(IGraphicContext graphicContext)
		{
			LayoutCellBodies(graphicContext);
			LayoutCellOuters(graphicContext);

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

			foreach (var row in Rows())
			{
				var rowDimension = rowDimensions[row];

				var x = 0f;

				foreach (var column in Columns())
				{
					var columnDimension = columnDimensions[column];

					var cell = GetCell(row, column);

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
			foreach (var row in Rows().Reverse())
			{
				foreach (var cell in CellsInRow(row).Reverse())
				{
					cell.LayoutOuters(graphicContext);
				}
			}
			NormalizeCellDimensions();
		}

		private void LayoutCellBodies(IGraphicContext graphicContext)
		{
			foreach (var row in Rows().Reverse())
			{
				foreach (var cell in CellsInRow(row).Reverse())
				{
					cell.LayoutBody(graphicContext);
				}
			}
			NormalizeCellDimensions();
		}

		public Cell GetCell(int row, int column)
		{
			return m_Cells[row, column];
		}

		internal void AddCell(Cell cell)
		{
			m_Cells[cell.Row, cell.Column] = cell;
			AddChild(cell);
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

		public int RowCount
		{
			get { return m_RowCount; }
		}

		public int ColumnCount
		{
			get { return m_ColumnCount; }
		}
	}
}