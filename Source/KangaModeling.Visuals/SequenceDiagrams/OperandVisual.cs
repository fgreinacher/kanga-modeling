using System;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class OperandVisual : OperandVisualBase
    {
        private readonly bool m_IsFirst;
        private readonly Column m_LeftColumn;
        private readonly Column m_RightColumn;
        private readonly Row m_TopRow;
        private readonly string m_GuardExpressionText;

        public OperandVisual(IStyle style, IOperand operand, GridLayout gridLayout, bool isFirst, Column leftColumn, Column rightColumn)
            : base(style, operand, gridLayout) 
        {
            m_IsFirst = isFirst;
            m_LeftColumn = leftColumn;
            m_RightColumn = rightColumn;
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
                : graphicContext.MeasureText(m_GuardExpressionText, Style.Common.Font, Style.GuardExpression.FontSize);
            
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

            this.GridLayout.AllocateBetween(m_LeftColumn, m_RightColumn, this.Size.Width);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yLine = m_TopRow.TopGap.Bottom - BottomOffset + 4;
            float yText = yLine + 2;

            float xStart = Parent.Location.X;
            float xEnd = Parent.Location.X + Parent.Size.Width;

            if (!m_IsFirst)
            {
                graphicContext.DrawDashedLine(new Point(xStart, yLine), new Point(xEnd, yLine), Style.Fragment.OperandSeparatorWidth, Style.Fragment.OperandSeparatorColor, Style.Common.LineStyle);
            }
            
            Point textLocation = new Point(xStart + 5, yText);
            graphicContext.FillRectangle(textLocation, Size, Color.SemiTransparent);
            graphicContext.DrawText(
                textLocation, Size,
                m_GuardExpressionText, 
                Style.Common.Font, Style.GuardExpression.FontSize, Style.GuardExpression.TextColor,
                HorizontalAlignment.Left, VerticalAlignment.Middle);

            base.DrawCore(graphicContext);
        }
    }
}