using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	internal abstract class Cell : Visual
	{
		#region Fields

		private readonly Grid m_Grid;
		private readonly int m_Row;
		private readonly int m_Column;

		#endregion

		#region Construction / Destruction / Initialisation

		protected Cell(Grid grid, int row, int column)
		{
			m_Grid = grid;
			m_Row = row;
			m_Column = column;
		}

		#endregion

		#region Properties

		public Grid Grid
		{
			get { return m_Grid; }
		}

		public int Row
		{
			get { return m_Row; }
		}

		public int Column
		{
			get { return m_Column; }
		}

		public float BodyWidth
		{
			get
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				return columnDimension.BodyWidth;
			}
			set
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				columnDimension.BodyWidth = Math.Max(value, columnDimension.BodyWidth);
			}
		}

		public float BodyHeight
		{
			get
			{
				var rowDimension = m_Grid.RowDimension(Row);
				return rowDimension.BodyHeight;
			}
			set
			{
				var rowDimension = m_Grid.RowDimension(Row);
				rowDimension.BodyHeight = Math.Max(value, rowDimension.BodyHeight);
			}
		}

		public float LeftOuterWidth
		{
			get
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				return columnDimension.LeftOuterWidth;
			}
			set
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				columnDimension.LeftOuterWidth = Math.Max(value, columnDimension.LeftOuterWidth);
			}
		}
		public float RightOuterWidth
		{
			get
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				return columnDimension.RightOuterWidth;
			}
			set
			{
				var columnDimension = m_Grid.ColumnDimension(Column);
				columnDimension.RightOuterWidth = Math.Max(value, columnDimension.RightOuterWidth);
			}
		}

		public float TopOuterHeight
		{
			get
			{
				var rowDimension = m_Grid.RowDimension(Row);
				return rowDimension.TopOuterHeight;
			}
			set
			{
				var rowDimension = m_Grid.RowDimension(Row);
				rowDimension.TopOuterHeight = Math.Max(value, rowDimension.TopOuterHeight);
			}
		}
		public float BottomOuterHeight
		{
			get
			{
				var rowDimension = m_Grid.RowDimension(Row);
				return rowDimension.BottomOuterHeight;
			}
			set
			{
				var rowDimension = m_Grid.RowDimension(Row);
				rowDimension.BottomOuterHeight = Math.Max(value, rowDimension.BottomOuterHeight);
			}
		}

		public bool IsFragmentStart { get; set; }
		public Cell FragmentEndCell { get; set; }

		#endregion

		#region Public Methods

		public void LayoutBody(IGraphicContext graphicContext)
		{
			LayoutBodyCore(graphicContext);
		}

		public void LayoutOuters(IGraphicContext graphicContext)
		{
			LayoutOutersCore(graphicContext);
		}

		#endregion

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

		public bool IsFragmentEnd { get; set; }

		public Compiler.SequenceDiagrams.FragmentType FragmentType { get; set; }

		public string FragmentTitle { get; set; }
	}
}