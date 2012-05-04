using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal abstract class FragmentVisualBase : Visual
    {
        public const float FramePadding = 10;
        private readonly IArea m_Area;
        private readonly ICombinedFragment m_Fragment;
        private readonly GridLayout m_GridLayout;
        public float BottomOffset { get; protected set; }

        public GridLayout GridLayout
        {
            get { return m_GridLayout; }
        }

        private readonly Padding m_PaddingDepth;

        private Size m_TextSize;
        private Padding m_InnerPadding;


        protected FragmentVisualBase(IStyle style, ICombinedFragment fragment, GridLayout gridLayout)
            : base(style)
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
        }

        public Row TopRow { get; set; }
        protected Row BottomRow { get; set; }
        protected Column LeftColumn { get; set; }
        protected Column RightColumn { get; set; }

        protected IArea Area
        {
            get { return m_Area; }
        }

        protected void Initialize()
        {
            bool isFirst = true;
            foreach (IOperand operand in m_Fragment.Operands)
            {
                AddChild(CreateOperandVisual(operand, isFirst, LeftColumn, RightColumn));
                isFirst = false;
            }
        }

        protected abstract Visual CreateOperandVisual(IOperand operand, bool isFirst, Column leftColumn, Column rightColumn);

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_TextSize = graphicContext.MeasureText(m_Fragment.Title, Style.Common.Font, Style.Fragment.FontSize);

            m_InnerPadding = m_PaddingDepth * FramePadding;

            Width = m_TextSize.Width + FramePadding;

            var firstChild = Children.FirstOrDefault() as OperandVisualBase;
            var firstChildBottomOffset = firstChild != null ? firstChild.BottomOffset : 0;

            BottomOffset = firstChildBottomOffset + m_TextSize.Height;

            TopRow.TopGap.Allocate(BottomOffset + 4);
            BottomRow.BottomGap.Allocate(m_InnerPadding.Bottom);

            LeftColumn.LeftGap.Allocate(m_InnerPadding.Left);
            RightColumn.RightGap.Allocate(m_InnerPadding.Right);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float yStart = TopRow.TopGap.Bottom - BottomOffset - 4 ;
            float yEnd = BottomRow.BottomGap.Top + m_InnerPadding.Bottom;

            float xStart = LeftColumn.Body.Left - m_InnerPadding.Left;
            float xEnd = RightColumn.Body.Right + m_InnerPadding.Right;

            DrawInternal(xEnd, xStart, yEnd, yStart, graphicContext);
            base.DrawCore(graphicContext);
        }

        protected void DrawInternal(float xEnd, float xStart, float yEnd, float yStart, IGraphicContext graphicContext)
        {
            Location = new Point(xStart, yStart);
            Size = new Size(xEnd - xStart, yEnd - yStart);

            if (!string.IsNullOrEmpty(m_Fragment.Title))
            {
                DrawTextFrame(xStart, yStart, graphicContext);
                DrawText(graphicContext);
            }

            DrawOuterFrame(graphicContext);
        }

        private void DrawOuterFrame(IGraphicContext graphicContext)
        {
            graphicContext.DrawRectangle(Location, Size, Style.Fragment.FrameColor, Style.Common.LineStyle);
        }

        private void DrawText(IGraphicContext graphicContext)
        {
            Size textArea = m_TextSize + new Padding(FramePadding / 2);
            graphicContext.DrawText(Location, textArea, m_Fragment.Title, Style.Common.Font, Style.Fragment.FontSize, Style.Fragment.TextColor, HorizontalAlignment.Center, VerticalAlignment.Middle);
        }

        private void DrawTextFrame(float xStart, float yStart, IGraphicContext graphicContext)
        {
            var textFramePoint1 = new Point(xStart, yStart + m_TextSize.Height + FramePadding / 2);
            var textFramePoint2 = new Point(xStart + m_TextSize.Width, yStart + m_TextSize.Height + FramePadding / 2);
            var textFramePoint3 = new Point(xStart + m_TextSize.Width + FramePadding, yStart);

            graphicContext.FillPolygon( new[] {Location, textFramePoint1, textFramePoint2, textFramePoint3, Location}, Color.SemiTransparent);

            graphicContext.DrawLine(textFramePoint1, textFramePoint2, Style.Fragment.TextFrameWidth, Style.Fragment.TextFrameColor, Style.Common.LineStyle);
            graphicContext.DrawLine(textFramePoint2, textFramePoint3, Style.Fragment.TextFrameWidth, Style.Fragment.TextFrameColor, Style.Common.LineStyle);
        }
    }
}