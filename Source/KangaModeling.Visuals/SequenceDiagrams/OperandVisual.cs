using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisual : OperandVisualBase
    {
        private readonly bool m_IsFirst;
        private readonly Row m_TopRow;
        private readonly string m_GuardExpressionText;

        public OperandVisual(IOperand operand, GridLayout gridLayout, bool isFirst) 
            : base(operand, gridLayout) 
        {
            m_IsFirst = isFirst;
            IArea area = operand.GetArea();
            m_TopRow = gridLayout.Rows[area.Top];
            Initialize();
            m_GuardExpressionText = string.Format("[{0}]", operand.GuardExpression);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            if (string.IsNullOrEmpty(m_GuardExpressionText))
            {
                Size = Size.Empty;
            }
            else
            {
                Size = graphicContext.MeasureText(m_GuardExpressionText);
                m_TopRow.TopGap.Allocate(Height + 4);
            }
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yLine = m_TopRow.TopGap.Bottom - Height;
            float yText = yLine + 4;

            float xStart = Parent.Location.X;
            float xEnd = Parent.Location.X + Parent.Size.Width;

            if (!m_IsFirst)
            {
                graphicContext.DrawDashedLine(new Point(xStart, yLine), new Point(xEnd, yLine), 1);
            }
            graphicContext.DrawText(m_GuardExpressionText, HorizontalAlignment.Left, VerticalAlignment.Top, new Point(xStart + 5, yText), Size);

            base.DrawCore(graphicContext);
        }
    }
}