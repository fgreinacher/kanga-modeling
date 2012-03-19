using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.Model
{
	/// <summary>
	/// Main model class for sequence diagrams.
	/// 
	/// A sequence diagram has a global title, that identifies the main purpose of the diagram.
	/// 
	/// Note that these model classes are not designed for other use cases:
	///  * There is no eventing support right now. The model is just built, and just traversed to create the graphics.
	///  * There is no command infrastructure, because there is no UI and no undo.
	/// </summary>
	public sealed class SequenceDiagram
	{
		/// <summary>
		/// Initialize a new SequenceDiagram instance and set its fields.
		/// </summary>
		public SequenceDiagram ()
		{
			Participants = new List<Participant>();
		}

		/// <summary>
		/// The title of the sequence diagram.
		/// </summary>
		public String Title { get; set; }
		
		/// <summary>
		/// The participants that call each other in the sequence diagram.
		/// </summary>
		public List<Participant> Participants { get; private set; }
		
		/// <summary>
		/// The root content combined fragment. The idea is as follows: 
		/// 
		/// Sequence Diagrams can contain combined fragments, and these can be nested.
		/// Also, participants can call themselves more than one time. These need to be stored inside a container, too.
		/// So just use a "root" combined fragment that really just aggregates the calls on the "first level".
		/// </summary>
		public CombinedFragment Content { get; private set; }
		
	}
}

