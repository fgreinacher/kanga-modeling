using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisual : Visual
    {
        private readonly GridLayout m_GridLayout;
        private readonly IOperand m_Operand;
        private readonly int m_OperandIndex;
        private readonly Row m_TopRow;

        public OperandVisual(IOperand operand, int operandIndex, Row topRow, GridLayout gridLayout)
        {
            m_Operand = operand;
            m_OperandIndex = operandIndex;
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

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            if (string.IsNullOrEmpty(m_Operand.GuardExpression))
            {
                Size = Size.Empty;
            }
            else
            {
                Size = graphicContext.MeasureText(m_Operand.GuardExpression);
                m_TopRow.TopGap.Allocate(Height);
            }
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yLine = m_TopRow.TopGap.Bottom - Height;
            float yText = yLine + 5;

            float xStart = Parent.Location.X;
            float xEnd = Parent.Location.X + Parent.Size.Width;

            if (m_OperandIndex > 0)
            {
                graphicContext.DrawDashedLine(new Point(xStart, yLine), new Point(xEnd, yLine), 1);
            }

            graphicContext.DrawText(m_Operand.GuardExpression, HorizontalAlignment.Left, VerticalAlignment.Top, new Point(xStart + 5, yText), Size);

            base.DrawCore(graphicContext);
        }
    }
}