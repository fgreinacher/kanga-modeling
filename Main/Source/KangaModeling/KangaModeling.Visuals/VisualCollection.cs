using System;
using System.Collections.Generic;

namespace KangaModeling.Visuals
{
    public sealed class VisualCollection : IEnumerable<Visual>
    {
        #region Fields
        
        private readonly Visual m_OwningVisual;
        private readonly List<Visual> m_Visuals = new List<Visual>();

        #endregion

        #region Construction / Destruction / Initialisation
        
        public VisualCollection(Visual owningVisual)
        {
            if (owningVisual == null) throw new ArgumentNullException("owningVisual");

            m_OwningVisual = owningVisual;
        }

        #endregion

        #region Public Methods

        public void Add(Visual visual)
        {
            m_Visuals.Add(visual);
        }

        public void Remove(Visual visual)
        {
            m_Visuals.Remove(visual);
        }

        #endregion

        #region IEnumerable<Visual> Members

        public IEnumerator<Visual> GetEnumerator()
        {
            return m_Visuals.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
