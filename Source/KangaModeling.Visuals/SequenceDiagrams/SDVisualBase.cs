using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public class SDVisualBase : Visuals.Visual
    {
        private readonly IStyle m_Style;

        public SDVisualBase(IStyle style)
        {
            m_Style = style;
        }

        protected IStyle Style
        {
            get { return m_Style; }
        }
    }
}
