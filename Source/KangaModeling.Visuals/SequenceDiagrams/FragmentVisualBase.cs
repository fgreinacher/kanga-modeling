using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FragmentVisualBase : Visual
    {
        public const float FramePadding = 5;
        private readonly IArea m_Area;
        private readonly ICombinedFragment m_Fragment;
        private readonly GridLayout m_GridLayout;
        private Size m_HeaderSize;
        private Padding m_InnerPadding;

        public FragmentVisualBase(ICombinedFragment fragment, GridLayout gridLayout)
        {
            m_Fragment = fragment;
            m_GridLayout = gridLayout;
            m_Area = m_Fragment.GetArea();
            Initialize();
        }

        protected Row TopRow { get; set; }
        protected Row BottomRow { get; set; }
        protected Column LeftColumn { get; set; }
        protected Column RightColumn { get; set; }

        protected IArea Area
        {
            get { return m_Area; }
        }

        private void Initialize()
        {
            foreach (IOperand operand in m_Fragment.Operands)
            {
                if (!operand.Signals.Any())
                {
                    continue;
                }

                int topRowIndex = operand.Signals.Select(signal => signal.RowIndex).Min();
                Row topRow = m_GridLayout.Rows[topRowIndex];
                AddChild(new OperandVisual(operand, topRow, m_GridLayout));
            }
        }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_HeaderSize = graphicContext.MeasureText(m_Fragment.Title);

            m_InnerPadding =
                new Padding(m_Area.LeftDepth(), m_Area.RightDepth(), m_Area.TopDepth(), m_Area.BottomDepth())*
                FramePadding;

            TopRow.TopGap.Allocate(m_HeaderSize.Height + 8 + m_InnerPadding.Top);
            BottomRow.BottomGap.Allocate(m_InnerPadding.Bottom);

            LeftColumn.Allocate(m_InnerPadding.Left);
            RightColumn.Allocate(m_InnerPadding.Right);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yStart = TopRow.TopGap.Bottom - m_HeaderSize.Height - 8;
            float yEnd = BottomRow.BottomGap.Bottom + m_InnerPadding.Bottom;

            float xStart = LeftColumn.Body.Left - m_InnerPadding.Left;
            float xEnd = RightColumn.Body.Right + m_InnerPadding.Right;

            DrawInternal(xEnd, xStart, yEnd, yStart, graphicContext);
            base.DrawCore(graphicContext);
        }

        protected void DrawInternal(float xEnd, float xStart, float yEnd, float yStart, IGraphicContext graphicContext)
        {
            Size = new Size(xEnd - xStart, yEnd - yStart);

            var textLocation = new Point(xStart + 4, yStart + 4);
            graphicContext.DrawText(m_Fragment.Title, HorizontalAlignment.Center, VerticalAlignment.Middle, textLocation,
                                    m_HeaderSize);
            graphicContext.DrawRectangle(new Point(xStart, yStart), Size);

            var textFramePoint1 = new Point(xStart, yStart + m_HeaderSize.Height + 5);
            var textFramePoint2 = new Point(xStart + m_HeaderSize.Width, yStart + m_HeaderSize.Height + 5);
            var textFramePoint3 = new Point(xStart + m_HeaderSize.Width + 16, yStart);

            graphicContext.DrawLine(textFramePoint1, textFramePoint2, 1);
            graphicContext.DrawLine(textFramePoint2, textFramePoint3, 1);
        }
    }
}