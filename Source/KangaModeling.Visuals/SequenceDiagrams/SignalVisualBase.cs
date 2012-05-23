using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal abstract class SignalVisualBase : SDVisualBase
    {
        protected readonly ISignal m_Signal;
        protected readonly Row m_Row;
        protected Size m_TextSize;

        protected SignalVisualBase(IStyle style, ISignal signal, Row row)
            : base(style)
        {
            m_Signal = signal;
            m_Row = row;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            m_TextSize = graphicContext.MeasureText(m_Signal.Name, Style.Common.Font, Style.Signal.FontSize);
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
            float xFromCenterAbsoulute = Style.Activity.Width / 2 * pin.Level;

            return pin.Orientation == Orientation.Left
                       ? -xFromCenterAbsoulute
                       : xFromCenterAbsoulute;
        }
    }
}