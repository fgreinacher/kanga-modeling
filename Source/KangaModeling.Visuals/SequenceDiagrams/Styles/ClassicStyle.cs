using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams.Styles
{
    public class ClassicStyle :
        IStyle,
        ICommonProperties,
        IDebugProperties,
        ILifelineProperties,
        IGuardExpressionProperties,
        IActivityProperties,
        IFragmentProperties,
        ISignalProperties
    {
        private const float c_BaseFontSize = 10;

        ICommonProperties IStyle.Common
        {
            get { return this; }
        }

        IDebugProperties IStyle.Debug
        {
            get { return this; }
        }

        ILifelineProperties IStyle.Lifeline
        {
            get { return this; }
        }

        IGuardExpressionProperties IStyle.GuardExpression
        {
            get { return this; }
        }

        IActivityProperties IStyle.Activity
        {
            get { return this; }
        }

        IFragmentProperties IStyle.Fragment
        {
            get { return this; }
        }

        ISignalProperties IStyle.Signal
        {
            get { return this; }
        }

        float ILifelineProperties.Width
        {
            get { return 2; }
        }

        Color ILifelineProperties.Color
        {
            get { return Color.Black; }
        }

        Color ILifelineProperties.NameFrameColor
        {
            get { return Color.Black; }
        }

        float ILifelineProperties.NameFontSize
        {
            get { return c_BaseFontSize; }
        }

        Color ILifelineProperties.NameTextColor
        {
            get { return Color.Black; }
        }

        Color ILifelineProperties.XCrossColor
        {
            get { return Color.Black; }
        }

        float ILifelineProperties.XCrossWidth
        {
            get { return 1; }
        }

        float IGuardExpressionProperties.FontSize
        {
            get { return c_BaseFontSize; }
        }

        Color IGuardExpressionProperties.TextColor
        {
            get { return Color.Black; }
        }

        float IActivityProperties.Width
        {
            get { return 10; }
        }

        Color IActivityProperties.FrameColor
        {
            get { return Color.Black; }
        }

        Color IActivityProperties.BackColor
        {
            get { return Color.White; }
        }

        float IFragmentProperties.FontSize
        {
            get { return c_BaseFontSize; }
        }

        Color IFragmentProperties.FrameColor
        {
            get { return Color.Black; }
        }

        Color IFragmentProperties.TextColor
        {
            get { return Color.Black; }
        }

        float IFragmentProperties.TextFrameWidth
        {
            get { return 1; }
        }

        Color IFragmentProperties.TextFrameColor
        {
            get { return Color.Gray; }
        }

        float IFragmentProperties.OperandSeparatorWidth
        {
            get { return 1; }
        }

        Color IFragmentProperties.OperandSeparatorColor
        {
            get { return Color.Gray; }
        }

        float ISignalProperties.FontSize
        {
            get { return c_BaseFontSize; }
        }

        Color ISignalProperties.TextColor
        {
            get { return Color.Black; }
        }
        
        float ISignalProperties.Width
        {
            get { return 2; }
        }

        Color ISignalProperties.LineColor
        {
            get { return Color.Black; }
        }
        Font ICommonProperties.Font
        {
            get { return Font.SansSerif; }
        }

        LineStyle ICommonProperties.LineStyle
        {
            get { return LineStyle.Clean; }
        }

        bool IDebugProperties.DrawCellAreas
        {
            get { return false; }
        }
    }
}
