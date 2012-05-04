using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams.Styles
{
    public interface ICommonProperties
    {
        Font Font { get; }
        LineStyle LineStyle { get; }
    }

    public interface IDebugProperties
    {
        bool DrawCellAreas { get; }
    }

    public interface ILifelineProperties
    {
        float Width { get; }

        Color Color { get; }

        Color NameFrameColor { get; }
        
        float NameFontSize { get; }

        Color NameTextColor { get; }

        Color XCrossColor { get; }

        float XCrossWidth { get; }
    }

    public interface IGuardExpressionProperties
    {
        float FontSize { get; }

        Color TextColor { get; }
    }

    public interface IActivityProperties
    {
        float Width { get; }

        Color FrameColor { get; }

        Color BackColor { get; }
    }

    public interface IFragmentProperties
    {
        float FontSize { get; }

        Color FrameColor { get; }

        Color TextColor { get; }

        float TextFrameWidth { get; }

        Color TextFrameColor { get; }

        float OperandSeparatorWidth { get; }

        Color OperandSeparatorColor { get; }
    }
    public interface ISignalProperties
    {
        float FontSize { get; }

        Color TextColor { get; }
        
        float Width { get; }

        Color LineColor { get; }
    }

    public interface IStyle
    {
        ICommonProperties Common { get; }

        IDebugProperties Debug { get; }

        ILifelineProperties Lifeline { get; }

        IGuardExpressionProperties GuardExpression { get; }

        IActivityProperties Activity { get; }

        IFragmentProperties Fragment { get; }

        ISignalProperties Signal { get; }
    }
}
