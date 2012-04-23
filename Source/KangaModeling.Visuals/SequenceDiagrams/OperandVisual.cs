using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisual : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IOperand m_Operand;
        private readonly Row m_TopRow;
        private Size m_TextSize;

        public OperandVisual(IOperand operand, Row topRow, GridLayout gridLayout)
        {
            m_Operand = operand;
            m_TopRow = topRow;
            m_GridLayout = gridLayout;
            Initialize();
        }

        private void Initialize()
        {
            foreach (ICombinedFragment fragment in m_Operand.Children)
            {
                AddChild(new FragmentVisual(fragment, m_GridLayout));
            }
        }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_TextSize = graphicContext.MeasureText(m_Operand.GuardExpression);
            Size = m_TextSize;
            m_TopRow.TopGap.Allocate(m_TextSize.Height);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yText = m_TopRow.Top;
            float yLine = yText + m_TextSize.Height;

            //float xStart = m_LeftColumn.Left;
            //float xEnd = m_RightColumn.Right;
            //graphicContext.DrawDashedLine(new Point(xStart, yLine), new Point(xEnd, yLine), 1);

            //graphicContext.DrawText(m_Operand.GuardExpression, HorizontalAlignment.Center, VerticalAlignment.Middle, new Point(xStart, yText), m_TextSize);

            base.DrawCore(graphicContext);
        }
    }
}