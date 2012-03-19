using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.Model
{
	/// <summary>
	/// Main model class for sequence diagrams.
	/// 
	/// A sequence diagram has a global title, that identifies the main purpose of the diagram.
	/// </summary>
	public sealed class SequenceDiagram
	{
		
		public SequenceDiagram ()
		{
			Participants = new List<Participant>();
		}
		
		public String Title { get; set; }
		
		public List<Participant> Participants { get; private set; }
		
		public CombinedFragment Content { get; private set; }
		
	}
}

