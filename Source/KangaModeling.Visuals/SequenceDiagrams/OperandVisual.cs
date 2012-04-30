using System.Linq;
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

            Size = string.IsNullOrEmpty(m_GuardExpressionText) 
                ? Size.Empty 
                : graphicContext.MeasureText(m_GuardExpressionText);
            
            float childBottomOffset = 
                Children
                    .Where(visual => visual is FragmentVisual)
                    .Cast<FragmentVisual>()
                    .Where(fragment => fragment.TopRow == m_TopRow)
                    .Select(fragment => fragment.BottomOffset)
                    .DefaultIfEmpty()
                    .Max();
            BottomOffset = childBottomOffset + Size.Height + 8;

            m_TopRow.TopGap.Allocate(BottomOffset);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yLine = m_TopRow.TopGap.Bottom - BottomOffset + 4;
            float yText = yLine + 2;

            float xStart = Parent.Location.X;
            float xEnd = Parent.Location.X + Parent.Size.Width;

            if (!m_IsFirst)
            {
                graphicContext.DrawDashedLine(new Point(xStart, yLine), new Point(xEnd, yLine), 1);
            }
            graphicContext.DrawText(m_GuardExpressionText, HorizontalAlignment.Left, VerticalAlignment.Middle, new Point(xStart + 5, yText), Size);

            base.DrawCore(graphicContext);
        }
    }
}