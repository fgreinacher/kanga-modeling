using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal abstract class SignalVisualBase : Visual
    {
        public const float TextPadding = 6;
        public const int ArrowCapHeight = 8;

        protected readonly ISignal m_Signal;
        protected readonly Row m_Row;
        protected Size m_TextSize;

        protected SignalVisualBase(ISignal signal, Row row)
        {
            m_Signal = signal;
            m_Row = row;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_TextSize = graphicContext.MeasureText(m_Signal.Name);
        }

        protected sealed override void DrawCore(IGraphicContext graphicContext)
        {
            DrawArrow(graphicContext);
            DrawText(graphicContext);
        }

        protected abstract void DrawArrow(IGraphicContext graphicContext);

        protected abstract void DrawText(IGraphicContext graphicContext);

        protected float GetXFromCenterRelative(IPin pin)
        {
            float xFromCenterAbsoulute = ActivityVisual.ActivityWidth / 2 * pin.Level;

            return pin.Orientation == Orientation.Left
                       ? -xFromCenterAbsoulute
                       : xFromCenterAbsoulute;
        }
    }    
}