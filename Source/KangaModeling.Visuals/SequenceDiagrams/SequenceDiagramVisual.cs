using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Linq;
using System;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	public sealed class SequenceDiagramVisual : Visual
	{
		#region Constants

		private const float c_OuterMargin = 10;
		private const float c_InnerMargin = 10;

		#endregion

		#region Fields

		private readonly TitleVisual m_TitleVisual;
		private readonly IDictionary<IParticipant, ParticipantVisual> m_ParticipantVisuals = new Dictionary<IParticipant, ParticipantVisual>();
		private readonly IList<SignalVisual> m_SignalVisuals = new List<SignalVisual>();

		#endregion

		#region Construction / Destruction / Initialisation

		public SequenceDiagramVisual(ISequenceDiagram sequenceDiagram)
		{
			if (sequenceDiagram.Title != null)
			{
				m_TitleVisual = new TitleVisual(sequenceDiagram.Title);
				AddChild(m_TitleVisual);
			}

			int participantIndex = 0;
			foreach (var participant in sequenceDiagram.Participants)
			{
				var participantVisual = new ParticipantVisual(participant, participantIndex++);
				m_ParticipantVisuals.Add(participant, participantVisual);
				AddChild(participantVisual);
			}

			foreach (var signal in sequenceDiagram.Content.SelectMany(c => c).OfType<SignalElement>())
			{
				var signalVisual = new SignalVisual(
					m_ParticipantVisuals[signal.SourceParticipant],
					m_ParticipantVisuals[signal.TargetParticipant],
					signal.SignalType,
					signal.Name);
				m_SignalVisuals.Add(signalVisual);
				AddChild(signalVisual);
			}

			AutoSize = true;
		}

		#endregion

		#region Overrides / Overrideables

		protected override void ArrangeCore(IGraphicContext graphicContext)
		{
			float x = c_OuterMargin, y = c_OuterMargin;

			if (m_TitleVisual != null)
			{
				m_TitleVisual.Location = new Point(x, y);
				y += m_TitleVisual.Height;
			}

			float maximumParticipantNameHeight = 0;

			foreach (var participantVisual in m_ParticipantVisuals.Values)
			{
				participantVisual.Location = new Point(x, y);
				participantVisual.Width = participantVisual.NameSize.Width + c_InnerMargin;

				x += participantVisual.Size.Width;
				
				maximumParticipantNameHeight = Math.Max(
					participantVisual.NameSize.Height, 
					maximumParticipantNameHeight);
			}

			y += maximumParticipantNameHeight;
			y += c_InnerMargin;

			foreach (var signalVisual in m_SignalVisuals)
			{
				x = signalVisual.LeftParticipantVisual.CenterX;

				EnsureParticipantIsAtLeastAt(
					signalVisual.RightParticipantVisual,
					x + signalVisual.MeasuredSize.Width - (signalVisual.RightParticipantVisual.HalfWidth));
			}

			y += c_InnerMargin;

			foreach (var signalVisual in m_SignalVisuals)
			{
				signalVisual.Location = new Point(
					signalVisual.LeftParticipantVisual.CenterX,
					y);

				signalVisual.Size = new Size(
					signalVisual.RightParticipantVisual.CenterX - signalVisual.LeftParticipantVisual.CenterX,
					signalVisual.MeasuredSize.Height);

				y += signalVisual.Height;
				y += c_InnerMargin;
			}

			foreach (var participantVisual in m_ParticipantVisuals.Values)
			{
				participantVisual.Height = participantVisual.NameSize.Height + y - participantVisual.Y;
			}
		}

		protected override Size MeasureCore(IGraphicContext graphicContext)
		{
			return base.MeasureCore(graphicContext).Plus(c_OuterMargin, c_OuterMargin);
		}

		#endregion

		#region Private Methods

		private void EnsureParticipantIsAtLeastAt(ParticipantVisual participantVisual, float x)
		{
			float delta = x - participantVisual.X;

			if (delta > 0)
			{
				var rightNeighbors = m_ParticipantVisuals.Values
					.Where(pv => pv.Index >= participantVisual.Index)
					.OrderBy(pv => pv.Index);

				foreach (var rightNeighbor in rightNeighbors)
				{
					rightNeighbor.X += delta;
				}
			}
		}

		#endregion
	}
}
