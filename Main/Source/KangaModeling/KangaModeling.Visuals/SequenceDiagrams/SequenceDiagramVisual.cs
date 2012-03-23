using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public sealed class SequenceDiagramVisual : Visual
    {
        #region Fields
        
        private readonly ISequenceDiagram m_SequenceDiagram;
        private readonly TitleVisual m_TitleVisual;
        private readonly IList<ParticipantVisual> m_ParticipantVisuals = new List<ParticipantVisual>();
        
        #endregion

        #region Construction / Destruction / Initialisation

        public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
        {
            m_SequenceDiagram = sequenceDiagram;

            if (m_SequenceDiagram.Title != null)
            {
                m_TitleVisual = new TitleVisual(m_SequenceDiagram.Title);
                Children.Add(m_TitleVisual);
            }

            foreach (var participant in m_SequenceDiagram.Participants)
            {
                var participantVisual = new ParticipantVisual(participant);
                m_ParticipantVisuals.Add(participantVisual);
                Children.Add(participantVisual);
            }

            Padding = 10;
        }

        #endregion

        #region Overrides / Overrideables

        protected override void ArrangeCore(IGraphicContext graphicContext)
        {
            float x = 0, y = 0;

            if (m_TitleVisual != null)
            {
                m_TitleVisual.Location = new Point(0, 0);
                y += m_TitleVisual.Height;
            }

            foreach (var participantVisual in m_ParticipantVisuals)
            {
                participantVisual.Location = new Point(x, y);

                x += participantVisual.Size.Width + 10;
            }
        }

        #endregion
    }
}
