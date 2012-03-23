using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    public sealed class ParticipantVisual : Visual
    {
        #region Fields
        
        private readonly IParticipant m_Participant;
        private readonly ParticipantNameVisual m_TopNameVisual;
        private readonly ParticipantNameVisual m_BottomNameVisual;
        private readonly ParticipantLifelineVisual m_LifelineVisual;
        
        #endregion

        #region Construction / Destruction / Initialisation

        public ParticipantVisual(IParticipant participant)
        {
            if (participant == null)
                throw new ArgumentNullException("participant");

            m_Participant = participant;

            m_TopNameVisual = new ParticipantNameVisual(m_Participant.Name);
            Children.Add(m_TopNameVisual);

            m_LifelineVisual = new ParticipantLifelineVisual();
            Children.Add(m_LifelineVisual);

            m_BottomNameVisual = new ParticipantNameVisual(m_Participant.Name);
            Children.Add(m_BottomNameVisual);
        }

        #endregion

        #region Overrides / Overrideables

        protected override void ArrangeCore(IGraphicContext graphicContext)
        {
            m_TopNameVisual.Location = new Point(0, 0);

            m_LifelineVisual.Location = new Point(
                m_TopNameVisual.X + (m_TopNameVisual.Width / 2),
                m_TopNameVisual.Y + m_TopNameVisual.Height);

            m_BottomNameVisual.Location = new Point(
                0,
                m_LifelineVisual.Y + m_LifelineVisual.Height);
        }

        #endregion
    }
}
