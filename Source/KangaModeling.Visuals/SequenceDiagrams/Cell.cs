using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

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

        public float BodyWidth { get; set; }
        public float BodyHeight { get; set; }

        public float LeftOuterWidth { get; set; }
        public float RightOuterWidth { get; set; }

        public float TopOuterHeight { get; set; }
        public float BottomOuterHeight { get; set; }


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