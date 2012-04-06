using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public class ActivityVisual : Visual
    {
        public const float ActivityWidth = 20;

        private readonly IActivity m_Activity;

        public ActivityVisual(IActivity activity)
        {
            m_Activity = activity;
        }

        protected override Size MeasureCore(Graphics.IGraphicContext graphicContext)
        {
            var lengthInRows = m_Activity.End.RowIndex - m_Activity.Start.RowIndex;
            return new Size(ActivityWidth, lengthInRows * 20);
        }

        protected override void DrawCore(Graphics.IGraphicContext graphicContext)
        {
            graphicContext.DrawRectangle(this.Location, this.Size);
            base.DrawCore(graphicContext);
        }
    }
}
