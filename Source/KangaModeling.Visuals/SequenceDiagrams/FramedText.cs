using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FramedText : SDVisualBase
    {
        private readonly Column m_Column;
        private readonly Row m_Row;
        private TextVisual m_TextVisual;

        public FramedText(IStyle style, string text, Column column, Row row)
            : base(style)
        {
            m_Column = column;
            m_Row = row;

            Initialize(text);
        }

        private void Initialize(string text)
        {
            m_TextVisual = new TextVisual(Style, text, m_Column, m_Row);
            AddChild(m_TextVisual);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            Size = m_TextVisual.Size;

            m_Column.Body.Allocate(Size.Width);
            m_Row.Body.Allocate(Size.Height);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float y = m_Row.Bottom - Size.Height;
            float x = m_Column.Body.Middle - Size.Width/2;

            Location = new Point(x, y);

            graphicContext.DrawRectangle(Location, Size, Style.Lifeline.NameFrameColor, Style.Common.LineStyle);
            base.DrawCore(graphicContext);
        }
    }
}