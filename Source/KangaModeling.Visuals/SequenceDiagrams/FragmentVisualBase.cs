using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class FragmentVisualBase : Visual
    {
        public const float FramePadding = 10;
        private readonly IArea m_Area;
        private readonly ICombinedFragment m_Fragment;
        private readonly GridLayout m_GridLayout;
        private readonly Padding m_PaddingDepth;

        private Size m_TextSize;
        private Padding m_InnerPadding;


        public FragmentVisualBase(ICombinedFragment fragment, GridLayout gridLayout)
        {
            m_Fragment = fragment;
            m_GridLayout = gridLayout;
            m_Area = m_Fragment.GetArea();

            int topDepth = m_Area.TopDepth();
            int leftDepth = m_Area.LeftDepth();

            int bottomDepth = m_Area.BottomDepth();
            int rightDepth = m_Area.RightDepth();

            m_PaddingDepth =
                new Padding(
                    leftDepth,
                    rightDepth,
                    topDepth,
                    bottomDepth);

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
            int operandIndex = 0;
            foreach (IOperand operand in m_Fragment.Operands)
            {
                Row topRow = m_GridLayout.Rows[m_Area.Top];
                AddChild(new OperandVisual(operand, operandIndex++, topRow, m_GridLayout));
            }
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_TextSize = graphicContext.MeasureText(m_Fragment.Title);

            m_InnerPadding =
                new Padding(m_PaddingDepth.Left * FramePadding, m_PaddingDepth.Right * FramePadding, (m_TextSize.Height + FramePadding) * m_PaddingDepth.Top, m_PaddingDepth.Bottom * FramePadding);

            var firstChild = Children.FirstOrDefault();
            var firstChildHeight = firstChild != null ? firstChild.Height : 0;

            TopRow.TopGap.Allocate(m_InnerPadding.Top + firstChildHeight);
            BottomRow.BottomGap.Allocate(m_InnerPadding.Bottom);

            LeftColumn.LeftGap.Allocate(m_InnerPadding.Left);
            RightColumn.RightGap.Allocate(m_InnerPadding.Right);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yStart = TopRow.TopGap.Top;
            float yEnd = BottomRow.BottomGap.Top + m_InnerPadding.Bottom;

            float xStart = LeftColumn.Body.Left - m_InnerPadding.Left;
            float xEnd = RightColumn.Body.Right + m_InnerPadding.Right;

            DrawInternal(xEnd, xStart, yEnd, yStart, graphicContext);
            base.DrawCore(graphicContext);
        }

        protected void DrawInternal(float xEnd, float xStart, float yEnd, float yStart, IGraphicContext graphicContext)
        {
            Size = new Size(xEnd - xStart, yEnd - yStart);

            Location = new Point(xStart, yStart);
            const float hPadding = FramePadding / 2;
            graphicContext.DrawText(m_Fragment.Title, HorizontalAlignment.Center, VerticalAlignment.Middle, Location, m_TextSize + new Padding(hPadding));
            graphicContext.DrawRectangle(Location, Size, Color.Black);

            var textFramePoint1 = new Point(xStart, yStart + m_TextSize.Height + hPadding);
            var textFramePoint2 = new Point(xStart + m_TextSize.Width, yStart + m_TextSize.Height + hPadding);
            var textFramePoint3 = new Point(xStart + m_TextSize.Width + FramePadding, yStart);

            graphicContext.DrawLine(textFramePoint1, textFramePoint2, 1);
            graphicContext.DrawLine(textFramePoint2, textFramePoint3, 1);
        }
    }
}