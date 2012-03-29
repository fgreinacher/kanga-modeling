using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
	/// A Participant participates in a sequence diagram.
	/// It defines its own lifeline, can call other participants, and can receive calls.
	/// </summary>
	[DebuggerDisplay("{Id}")]
	public sealed class Participant : IParticipant
	{
	    /// <summary>
	    /// Initialize a new Participant instance and sets its fields.
	    /// </summary>
	    /// <param name="name">The name of the participant.</param>
        /// <param name="id">Unique id of participant. Must be non-empty. Used to identify participant in diagram.</param>
	    public Participant(string id, string name)
		{
	        if (name == null) throw new ArgumentNullException("name");
	        if (id == null) throw new ArgumentNullException("id");
	        if (id.Length==0) throw new ArgumentException("Id can not be empty.", "name");
			Name = name;
		    Id = id;
		}

	    /// <summary>
		/// The name of this participant.
		/// </summary>
		public String Name { get; private set; }

	    public string Id { get; private set; }
	}
}