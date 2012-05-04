using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams.Styles
{
    public interface ILifelineProperties
    {
        float Width { get; }

        Color Color { get; }

        Color NameFrameColor { get; } // Black

        Font NameFont { get; }

        float NameFontSize { get; }

        Color NameTextColor { get; }

        Color XCrossColor { get; }

        float XCrossWidth { get; }
    }

    public interface IGuardExpressionProperties
    {
        Font Font { get; }

        float FontSize { get; }

        Color TextColor { get; }
    }

    public interface IActivityProperties 
    {
        float Width { get; } // 10
        
        Color FrameColor { get; } // black

        Color BackColor { get; } // white
    }

    public interface IFragmentProperties
    {
        Font Font { get; }

        float FontSize { get; }

        Color FrameColor { get; }

        Color TextColor { get; }

        float TextFrameWidth { get; } // 1

        Color TextFrameColor { get; } // Color.Gray

        float OperandSeparatorWidth { get; }

        Color OperandSeparatorColor { get; }
    }
    public interface ISignalProperties
    {
        float FontSize { get; }

        Color TextColor { get; }

        Font Font { get; }

        float Width { get; }

        Color LineColor { get; }
    }

    public interface IStyle
    {
        bool DrawCellAreas { get; }

        LineStyle LineStyle { get; }
        
        ILifelineProperties Lifeline { get; }

        IGuardExpressionProperties GuardExpression { get; }

        IActivityProperties Activity { get; }

        IFragmentProperties Fragment { get; }

        ISignalProperties Signal { get; }
    }
}
