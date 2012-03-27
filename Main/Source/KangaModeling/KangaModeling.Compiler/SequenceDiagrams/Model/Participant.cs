using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
	/// A Participant participates in a sequence diagram.
	/// It defines its own lifeline, can call other participants, and can receive calls.
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public sealed class Participant : IParticipant
	{
	    /// <summary>
	    /// Initialize a new Participant instance and sets its fields.
	    /// </summary>
	    /// <param name="name">The name of the participant. Must be non-empty. Used to identify participant in diagram.</param>
	    /// <param name="description">Description to display in diagram.</param>
	    public Participant(String name, string description)
		{
	        if (name == null) throw new ArgumentNullException("name");
	        if (description == null) throw new ArgumentNullException("description");
	        if (name.Length==0) throw new ArgumentException("Name can not be empty.", "name");
	        // TODO rules for good names?
			// TODO rules for case sensitiveness should be here!
			Name = name;
		    Description = description;
		}

	    /// <summary>
		/// The name of this participant.
		/// </summary>
		public String Name { get; private set; }

	    public string Description { get; private set; }
	}
}