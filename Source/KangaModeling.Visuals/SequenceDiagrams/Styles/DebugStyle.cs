using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams.Styles
{
    public class DebugStyle : IStyle, IDebugProperties
    {
        private readonly IStyle m_BaseStyle;

        public DebugStyle(IStyle baseStyle)
        {
            m_BaseStyle = baseStyle;
        }

        ICommonProperties IStyle.Common
        {
            get { return m_BaseStyle.Common; }
        }

        IDebugProperties IStyle.Debug
        {
            get { return this; }
        }

        ILifelineProperties IStyle.Lifeline
        {
            get { return m_BaseStyle.Lifeline; }
        }

        IGuardExpressionProperties IStyle.GuardExpression
        {
            get { return m_BaseStyle.GuardExpression; }
        }

        IActivityProperties IStyle.Activity
        {
            get { return m_BaseStyle.Activity; }
        }

        IFragmentProperties IStyle.Fragment
        {
            get { return m_BaseStyle.Fragment; }
        }

        ISignalProperties IStyle.Signal
        {
            get { return m_BaseStyle.Signal; }
        }

        bool IDebugProperties.DrawCellAreas
        {
            get { return true; }
        }
    }
}
