using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Linq;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	public sealed class SequenceDiagramVisual : Visual
	{
		#region Fields

		private readonly ISequenceDiagram m_SequenceDiagram;
		private readonly TitleVisual m_TitleVisual;
		private readonly IDictionary<IParticipant, ParticipantVisual> m_ParticipantVisuals =
			new Dictionary<IParticipant, ParticipantVisual>();

		private readonly IList<SignalVisual> m_SignalVisuals = new List<SignalVisual>();

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

			int participantIndex = 0;
			foreach (var participant in m_SequenceDiagram.Participants)
			{
				var participantVisual = new ParticipantVisual(participant);
				participantVisual.Index = participantIndex++;
				m_ParticipantVisuals.Add(participant, participantVisual);
				Children.Add(participantVisual);
			}

			foreach (var signal in m_SequenceDiagram.Content.SelectMany(c => c).OfType<SignalElement>())
			{
				var sourceParticipantVisual = m_ParticipantVisuals[signal.SourceParticipant];
				var targetParticipantVisual = m_ParticipantVisuals[signal.TargetParticipant];

				var signalVisual = new SignalVisual(sourceParticipantVisual, targetParticipantVisual, signal.Name);
				m_SignalVisuals.Add(signalVisual);
				Children.Add(signalVisual);
			}

			AutoSize = true;
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

			foreach (var participantVisual in m_ParticipantVisuals.Values)
			{
				participantVisual.Location = new Point(x, y);
				x += participantVisual.Size.Width;
			}

			y += 30;

			foreach (var signalVisual in m_SignalVisuals)
			{
				x = signalVisual.LeftParticipantVisual.X + signalVisual.LeftParticipantVisual.Width / 2;

				EnsureParticipantIsAtLeastAt(signalVisual.RightParticipantVisual, x + signalVisual.MeasuredSize.Width - (signalVisual.RightParticipantVisual.Width / 2));
			}

			foreach (var signalVisual in m_SignalVisuals)
			{
				x = signalVisual.LeftParticipantVisual.X + signalVisual.LeftParticipantVisual.Width / 2;

				signalVisual.Location = new Point(x, y);
				signalVisual.Size = new Size(
					signalVisual.RightParticipantVisual.X - signalVisual.LeftParticipantVisual.X,
					signalVisual.MeasuredSize.Height);

				y += signalVisual.Height;
			}
		}

		private void EnsureParticipantIsAtLeastAt(ParticipantVisual participantVisual, float x)
		{
			float delta = x - participantVisual.X;

			if (delta > 0)
			{
				foreach (ParticipantVisual otherParticipantVisual in m_ParticipantVisuals.Values
					.Where(pv => pv.Index >= participantVisual.Index)
					.OrderBy(pv => pv.Index))
				{
					otherParticipantVisual.Location = new Point(otherParticipantVisual.X + delta, otherParticipantVisual.Y);
				}
			}
		}

		#endregion
	}
}
