using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	public sealed class ParticipantVisual : Visual
	{
		#region Fields

		private readonly ILifeline m_Participant;
		private readonly int m_Index;

		private Size m_NameSize;

		#endregion

		#region Construction / Destruction / Initialisation

		public ParticipantVisual(ILifeline participant, int index)
		{
			if (participant == null)
				throw new ArgumentNullException("participant");

			m_Participant = participant;
			m_Index = index;
		}

		#endregion

		#region Properties

		public int Index
		{
			get { return m_Index; }
		}

		public Size NameSize
		{
			get { return m_NameSize; }
		}

		public float HalfWidth
		{
			get { return Width / 2; }
		}

		#endregion

		#region Overrides / Overrideables

		protected override Size MeasureCore(IGraphicContext graphicContext)
		{
			m_NameSize = graphicContext.MeasureText(m_Participant.Name).Plus(10, 10);

			return m_NameSize.Plus(5, 0);
		}

		protected override void DrawCore(IGraphicContext graphicContext)
		{
			graphicContext.DrawLine(new Point(Width / 2, 0), new Point(Width / 2, Height), 2);

			graphicContext.DrawRectangle(new Point(0, 0), m_NameSize);
			graphicContext.DrawRectangle(new Point(0, Height - m_NameSize.Height), m_NameSize);

			graphicContext.DrawText(m_Participant.Name, HorizontalAlignment.Center, VerticalAlignment.Middle, new Point(0, 0), m_NameSize);
			graphicContext.DrawText(m_Participant.Name, HorizontalAlignment.Center, VerticalAlignment.Middle, new Point(0, Height - m_NameSize.Height), m_NameSize);
		}
		#endregion
	}
}
