using System;
using System.Diagnostics;

namespace KangaModeling.Compiler.SequenceDiagrams
{
	public interface IParticipant
	{
		string Name { get; }
	}

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
		/// <param name="name">The name of the participant. Must be non-empty.</param>
		public Participant(String name)
		{
			// TODO rules for good names?
			// TODO rules for case sensitiveness should be here!
			Name = name;
		}

		/// <summary>
		/// The name of this participant.
		/// </summary>
		public String Name { get; private set; }

	}

}

