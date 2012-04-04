using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	internal class SignalVisual : Visual
	{
		private enum Direction
		{
			LeftToRight,
			RightToLeft,
		}

		private readonly ParticipantVisual m_LeftParticipantVisual;
		private readonly ParticipantVisual m_RightParticipantVisual;
		private readonly Direction m_Direction;

		private readonly SignalType m_Type;
		private readonly string m_Message;

		public SignalVisual(
			ParticipantVisual sourceParticipantVisual,
			ParticipantVisual targetParticipantVisual,
			SignalType type,
			string message)
		{
			m_Type = type;
			m_Message = message;

			if (sourceParticipantVisual.Index > targetParticipantVisual.Index)
			{
				m_LeftParticipantVisual = targetParticipantVisual;
				m_RightParticipantVisual = sourceParticipantVisual;
				m_Direction = Direction.RightToLeft;
			}
			else
			{
				m_RightParticipantVisual = targetParticipantVisual;
				m_LeftParticipantVisual = sourceParticipantVisual;
				m_Direction = Direction.LeftToRight;
			}
		}

		public ParticipantVisual LeftParticipantVisual
		{
			get { return m_LeftParticipantVisual; }
		}

		public ParticipantVisual RightParticipantVisual
		{
			get { return m_RightParticipantVisual; }
		}

		protected override void ArrangeCore(IGraphicContext graphicContext)
		{
			base.ArrangeCore(graphicContext);
		}

		protected override Size MeasureCore(IGraphicContext graphicContext)
		{
			return graphicContext.MeasureText(m_Message).Plus(10, 10);
		}

		protected override void DrawCore(IGraphicContext graphicContext)
		{
			Point from, to;

			switch (m_Direction)
			{
				case Direction.LeftToRight:
					from = new Point(0, Height - 2);
					to = from.Offset(Width, 0);
					break;
				case Direction.RightToLeft:
					from = new Point(Width, Height - 2);
					to = from.Offset(-Width, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var lineOptions = LineOptions.ArrowEnd;

			if (m_Type == SignalType.CallReturn)
			{
				lineOptions = lineOptions | LineOptions.Dashed;
			}

			graphicContext.DrawLine(from, to, 2, lineOptions);

			graphicContext.DrawText(m_Message, HorizontalAlignment.Center, VerticalAlignment.Middle,
				new Point(0, 0), Size);
		}
	}
}
