using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class TextVisual : Visual
    {
        private readonly Column m_Column;
        private readonly string m_Name;
        private readonly Row m_Row;

        public TextVisual(string name, Column column, Row row)
        {
            m_Name = name;
            m_Column = column;
            m_Row = row;
            Padding = new Padding(2);
        }

        protected Padding Padding { get; private set; }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);
            Size = graphicContext.MeasureText(m_Name) + Padding;
            m_Column.Allocate(Size.Width);
            m_Row.Allocate(Size.Height);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            Location = Parent.Location;
            graphicContext.DrawText(m_Name, HorizontalAlignment.Center, VerticalAlignment.Middle, Location, Size);
        }
    }
}